// Admin > Sipariş takibi: pano verisini çeker, göstergeleri ve kartları çizer.
(function () {
    'use strict';

    const root = document.getElementById('adBoard');
    if (!root) return;

    const urls = { data: root.dataset.dataUrl, advance: root.dataset.advanceUrl, undo: root.dataset.undoUrl };
    const token = root.querySelector('input[name="__RequestVerificationToken"]')?.value;
    const REFRESH_MS = 15000;

    let board = null;
    let offset = 0;   // sunucu saati - bu bilgisayarın saati
    let busy = false;

    // ---------- yardımcılar ----------
    const esc = s => String(s ?? '').replace(/[&<>"']/g, c => ({ '&': '&amp;', '<': '&lt;', '>': '&gt;', '"': '&quot;', "'": '&#39;' }[c]));
    const now = () => Date.now() + offset;
    const minutesSince = iso => Math.max(0, Math.floor((now() - new Date(iso).getTime()) / 60000));
    const minutesBetween = (a, b) => Math.round((new Date(b) - new Date(a)) / 60000);
    const clock = iso => new Date(iso).toLocaleTimeString('tr-TR', { hour: '2-digit', minute: '2-digit' });
    const money = v => Number(v).toLocaleString('tr-TR', { minimumFractionDigits: 2, maximumFractionDigits: 2 }) + ' ₺';
    const timerLevel = min => min >= 40 ? 'is-late' : min >= 25 ? 'is-warn' : '';
    const shortNo = no => esc(no.slice(0, 8).toUpperCase());
    const detailUrl = no => '/Admin/Order/Detail?orderNo=' + encodeURIComponent(no);

    function toast(message) {
        const el = document.getElementById('adToast');
        el.textContent = message;
        el.hidden = false;
        clearTimeout(toast.timer);
        toast.timer = setTimeout(() => el.hidden = true, 4000);
    }

    // ---------- kartlar ----------
    function activeCard(c, status) {
        const total = minutesSince(c.paidAt);
        const items = c.items.map(i => `${i.quantity}× ${esc(i.name)}${i.variant ? ` (${esc(i.variant)})` : ''}`).join(', ');

        return `
            <article class="ad-card" data-order="${esc(c.orderNo)}">
                <header class="ad-card-head">
                    <a class="ad-no" href="${detailUrl(c.orderNo)}" title="Sipariş detayı">#${shortNo(c.orderNo)}</a>
                    <span class="ad-timer ${timerLevel(total)}" data-since="${c.paidAt}"><i class='bx bx-time-five'></i> <b>${total}</b> dk</span>
                </header>
                <strong class="ad-name">${esc(c.fullName)}</strong>
                <p class="ad-line"><i class='bx bx-phone'></i> ${esc(c.phoneNumber)}</p>
                <p class="ad-line"><i class='bx bx-map'></i> ${esc(c.address)}</p>
                ${c.note ? `<p class="ad-note"><i class='bx bx-note'></i> ${esc(c.note)}</p>` : ''}
                <p class="ad-items">${items}</p>
                ${status === 'OnTheWay' ? `<p class="ad-line"><i class='bx bx-cycling'></i> ${clock(c.dispatchedAt)} yola çıktı · ${esc(c.dispatchedBy)}</p>` : ''}
                <footer class="ad-card-foot">
                    <span class="ad-price">${money(c.paidPrice)}</span>
                    ${status === 'OnTheWay' ? `<button type="button" class="ad-undo" data-action="undo" data-from="OnTheWay" title="Yola çıktı adımını geri al"><i class='bx bx-undo'></i></button>` : ''}
                    <button type="button" class="ad-go" data-action="advance" data-from="${status}">${status === 'Waiting' ? 'Yola çıktı' : 'Teslim edildi'}</button>
                </footer>
            </article>`;
    }

    function doneCard(c) {
        const duration = minutesBetween(c.paidAt, c.deliveredAt);
        return `
            <article class="ad-card" data-order="${esc(c.orderNo)}">
                <header class="ad-card-head">
                    <a class="ad-no" href="${detailUrl(c.orderNo)}" title="Sipariş detayı">#${shortNo(c.orderNo)}</a>
                    <span class="ad-duration ${duration > 40 ? 'is-late' : 'is-ok'}" title="Ödemeden teslime">${duration} dk</span>
                </header>
                <strong class="ad-name">${esc(c.fullName)}</strong>
                <p class="ad-line"><i class='bx bx-cycling'></i> ${clock(c.dispatchedAt)} · ${esc(c.dispatchedBy)}</p>
                <footer class="ad-card-foot">
                    <span class="ad-line" style="margin:0"><i class='bx bx-check-double'></i> ${clock(c.deliveredAt)} · ${esc(c.deliveredBy)}</span>
                    <span style="margin-left:auto"></span>
                    <button type="button" class="ad-undo" data-action="undo" data-from="Delivered" title="Teslim edildi adımını geri al"><i class='bx bx-undo'></i></button>
                </footer>
            </article>`;
    }

    function renderColumn(key, cards, draw, emptyText) {
        root.querySelector(`[data-count="${key}"]`).textContent = cards.length;
        root.querySelector(`[data-col="${key}"]`).innerHTML = cards.length
            ? cards.map(draw).join('')
            : `<p class="ad-empty">${emptyText}</p>`;
    }

    function renderStats() {
        const durations = board.deliveredToday.map(c => minutesBetween(c.paidAt, c.deliveredAt));
        const average = durations.length ? Math.round(durations.reduce((a, b) => a + b, 0) / durations.length) : null;
        const late = [...board.waiting, ...board.onTheWay].filter(c => minutesSince(c.paidAt) >= 40).length;

        document.getElementById('adStatWaiting').textContent = board.waiting.length;
        document.getElementById('adStatRoad').textContent = board.onTheWay.length;
        document.getElementById('adStatDone').textContent = board.deliveredToday.length;
        document.getElementById('adStatAvg').textContent = average === null ? '-' : average + ' dk';
        document.getElementById('adStatLate').textContent = late;
    }

    function render() {
        renderColumn('waiting', board.waiting, c => activeCard(c, 'Waiting'), 'Bekleyen sipariş yok');
        renderColumn('onTheWay', board.onTheWay, c => activeCard(c, 'OnTheWay'), 'Yolda sipariş yok');
        renderColumn('deliveredToday', board.deliveredToday, doneCard, 'Bugün henüz teslimat yok');
        renderStats();
        document.getElementById('adUpdated').textContent = new Date(now()).toLocaleTimeString('tr-TR');
    }

    // Sayaçları yeniden çizmeden güncelle
    function tickTimers() {
        root.querySelectorAll('.ad-timer[data-since]').forEach(el => {
            const min = minutesSince(el.dataset.since);
            el.querySelector('b').textContent = min;
            el.classList.remove('is-warn', 'is-late');
            const level = timerLevel(min);
            if (level) el.classList.add(level);
        });
        if (board) renderStats();
    }

    // ---------- sunucu ----------
    async function load() {
        try {
            const res = await fetch(urls.data, { headers: { 'X-Requested-With': 'XMLHttpRequest' }, cache: 'no-store' });
            if (!res.ok) throw new Error();
            board = await res.json();
            offset = new Date(board.serverTimeUtc).getTime() - Date.now();
            render();
            document.getElementById('adOffline').hidden = true;
        } catch {
            document.getElementById('adOffline').hidden = false;
        }
    }

    async function post(url, data) {
        try {
            const res = await fetch(url, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded',
                    'X-Requested-With': 'XMLHttpRequest',
                    'RequestVerificationToken': token
                },
                body: new URLSearchParams(data)
            });
            const json = await res.json().catch(() => null);
            return json ?? { success: res.ok, message: 'Beklenmeyen bir hata oluştu.' };
        } catch {
            return { success: false, message: 'Bağlantı hatası. Tekrar deneyin.' };
        }
    }

    root.addEventListener('click', async e => {
        const button = e.target.closest('button[data-action]');
        if (!button || busy) return;

        // Admin geri alması süresiz ve kayda geçiyor: önce onay
        if (button.dataset.action === 'undo' && !confirm('Son adım geri alınsın mı? Bu işlem sipariş geçmişine kaydedilir.'))
            return;

        busy = true;
        button.disabled = true;
        const orderNo = button.closest('[data-order]').dataset.order;
        const url = button.dataset.action === 'undo' ? urls.undo : urls.advance;

        const result = await post(url, { OrderNo: orderNo, From: button.dataset.from });
        if (!result.success) toast(result.message);

        busy = false;
        await load();
    });

    // ---------- CANLI BAĞLANTI (SignalR) ----------
    function connectRealtime() {
        if (!window.signalR) return;   // kütüphane yoksa yedek yenileme yeterli

        const connection = new signalR.HubConnectionBuilder()
            .withUrl('/hubs/orders')
            .withAutomaticReconnect([0, 2000, 5000, 10000, 20000])
            .build();

        connection.on('orderReceived', function (data) {
            toast('Yeni sipariş: ' + data.customerName);   // admin ekranında ses yok, sadece bilgi
            load();
        });

        connection.on('deliveryChanged', () => load());     // garson işaretledi, pano tazelensin

        connection.onreconnected(() => load());             // kopukken kaçanları yakala
        connection.onclose(() => setTimeout(connectRealtime, 5000));

        connection.start().catch(() => setTimeout(connectRealtime, 5000));
    }

    // ---------- başlat ----------
    load();
    connectRealtime();
    setInterval(load, REFRESH_MS);   // SignalR koparsa diye yedek
    setInterval(tickTimers, 10000);
})();