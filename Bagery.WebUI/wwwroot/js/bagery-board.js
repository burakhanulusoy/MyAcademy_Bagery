// Bagery sipariş panosu (Waiter paneli)
// Canlı: SignalR ile anında haber alır. Yedek: 30 sn'de bir kendini yeniler.
(function () {
    'use strict';

    const root = document.getElementById('kbBoard');
    if (!root) return;

    const urls = {
        data: root.dataset.dataUrl,
        advance: root.dataset.advanceUrl,
        undo: root.dataset.undoUrl
    };
    const isAdmin = root.dataset.isAdmin === 'true';
    const token = root.querySelector('input[name="__RequestVerificationToken"]')?.value;

    const REFRESH_MS = 30000;          // SignalR yedeği: yarım dakikada bir tazele
    const UNDO_MS = 5 * 60 * 1000;     // geri alma süresi (sunucuyla aynı)
    const FRESH_MS = 60000;            // yeni sipariş vurgusu ne kadar sürsün

    let board = null;                  // son gelen veri
    let offset = 0;                    // sunucu saati - bu bilgisayarın saati
    let freshOrders = new Set();       // yeni gelen siparişler (vurgulanır)
    let busy = false;

    // ---------- yardımcılar ----------
    const esc = s => String(s ?? '').replace(/[&<>"']/g, c => ({ '&': '&amp;', '<': '&lt;', '>': '&gt;', '"': '&quot;', "'": '&#39;' }[c]));
    const now = () => Date.now() + offset;
    const minutesSince = iso => Math.max(0, Math.floor((now() - new Date(iso).getTime()) / 60000));
    const clock = iso => new Date(iso).toLocaleTimeString('tr-TR', { hour: '2-digit', minute: '2-digit' });
    const money = v => Number(v).toLocaleString('tr-TR', { minimumFractionDigits: 2, maximumFractionDigits: 2 }) + ' ₺';
    const timerLevel = min => min >= 40 ? 'is-late' : min >= 25 ? 'is-warn' : '';

    function toast(message) {
        const el = document.getElementById('kbToast');
        el.textContent = message;
        el.hidden = false;
        clearTimeout(toast.timer);
        toast.timer = setTimeout(() => el.hidden = true, 4000);
    }

    // ---------- kart çizimi ----------
    function canUndo(lastStepIso) {
        return !!lastStepIso && (isAdmin || now() - new Date(lastStepIso).getTime() < UNDO_MS);
    }

    function activeCard(c, status) {
        const total = minutesSince(c.paidAt);
        const lastStep = status === 'OnTheWay' ? c.dispatchedAt : null;
        const items = c.items.map(i =>
            `<li><b>${i.quantity}×</b> ${esc(i.name)}${i.variant ? ` <span>(${esc(i.variant)})</span>` : ''}</li>`).join('');

        return `
            <article class="kb-card ${freshOrders.has(c.orderNo) ? 'is-new' : ''}" data-order="${esc(c.orderNo)}">
                <header class="kb-card-head">
                    <span class="kb-no">#${esc(c.orderNo.slice(0, 8).toUpperCase())}</span>
                    <span class="kb-timer ${timerLevel(total)}" data-since="${c.paidAt}" title="Ödemeden bu yana geçen süre">
                        <i class='bx bx-time-five'></i> <b>${total}</b> dk
                    </span>
                </header>
                <div class="kb-customer">
                    <strong>${esc(c.fullName)}</strong>
                    <a href="tel:${esc(c.phoneNumber)}"><i class='bx bx-phone'></i> ${esc(c.phoneNumber)}</a>
                </div>
                <p class="kb-address"><i class='bx bx-map'></i> ${esc(c.address)}</p>
                ${c.note ? `<p class="kb-note"><i class='bx bx-note'></i> ${esc(c.note)}</p>` : ''}
                <ul class="kb-items">${items}</ul>
                ${status === 'OnTheWay' ? `<p class="kb-meta"><i class='bx bx-cycling'></i> ${clock(c.dispatchedAt)} yola çıktı · ${esc(c.dispatchedBy)}</p>` : ''}
                <footer class="kb-card-foot">
                    <span class="kb-price">${money(c.paidPrice)}</span>
                    ${canUndo(lastStep) ? `<button type="button" class="kb-undo" data-action="undo" data-from="${status}">Geri al</button>` : ''}
                    <button type="button" class="kb-go" data-action="advance" data-from="${status}">
                        ${status === 'Waiting'
                ? "<i class='bx bx-run'></i> Yola çıktı"
                : "<i class='bx bx-check-double'></i> Teslim edildi"}
                    </button>
                </footer>
            </article>`;
    }

    function doneCard(c) {
        const duration = Math.round((new Date(c.deliveredAt) - new Date(c.paidAt)) / 60000);
        return `
            <article class="kb-card is-done" data-order="${esc(c.orderNo)}">
                <header class="kb-card-head">
                    <span class="kb-no">#${esc(c.orderNo.slice(0, 8).toUpperCase())}</span>
                    <span class="kb-duration ${duration > 40 ? 'is-late' : 'is-ok'}" title="Ödemeden teslime toplam süre">${duration} dk</span>
                </header>
                <div class="kb-customer"><strong>${esc(c.fullName)}</strong></div>
                <p class="kb-meta"><i class='bx bx-check-double'></i> ${clock(c.deliveredAt)} teslim · ${esc(c.deliveredBy)}</p>
                ${canUndo(c.deliveredAt) ? `<button type="button" class="kb-undo" data-action="undo" data-from="Delivered">Geri al</button>` : ''}
            </article>`;
    }

    function renderColumn(key, cards, draw, emptyText, emptyIcon) {
        root.querySelector(`[data-count="${key}"]`).textContent = cards.length;
        root.querySelector(`[data-col="${key}"]`).innerHTML = cards.length
            ? cards.map(draw).join('')
            : `<p class="kb-empty"><i class='bx ${emptyIcon}'></i>${emptyText}</p>`;
    }

    function render() {
        renderColumn('waiting', board.waiting, c => activeCard(c, 'Waiting'), 'Bekleyen sipariş yok', 'bx-coffee');
        renderColumn('onTheWay', board.onTheWay, c => activeCard(c, 'OnTheWay'), 'Yolda sipariş yok', 'bx-cycling');
        renderColumn('deliveredToday', board.deliveredToday, doneCard, 'Bugün henüz teslimat yok', 'bx-check-double');
    }

    // Sayaçları kartları yeniden çizmeden günceller (her 10 sn)
    function tickTimers() {
        root.querySelectorAll('.kb-timer[data-since]').forEach(el => {
            const min = minutesSince(el.dataset.since);
            el.querySelector('b').textContent = min;
            el.classList.remove('is-warn', 'is-late');
            const level = timerLevel(min);
            if (level) el.classList.add(level);
        });
    }

    // ---------- sesli uyarı ----------
    let audio = null;
    let soundOn = localStorage.getItem('kbSound') === 'on';

    // Tek "ding" (ses açma denemesi için)
    function ding() {
        beep([880, 1320], 0.3);
    }

    // Yeni sipariş alarmı: üç kez, daha yüksek ve daha uzun
    function alarm() {
        if (!soundOn) return;
        beep([988, 1319, 988], 0.55, 3);
    }

    // frekanslar: sırayla çalınacak notalar, volume: ses seviyesi, repeat: kaç kez tekrar
    function beep(frequencies, volume, repeat = 1) {
        if (!soundOn) return;
        try {
            audio ??= new AudioContext();
            audio.resume();
            const step = 0.16;
            const groupLength = frequencies.length * step + 0.25;

            for (let r = 0; r < repeat; r++) {
                frequencies.forEach((freq, i) => {
                    const oscillator = audio.createOscillator();
                    const gain = audio.createGain();
                    const t = audio.currentTime + 0.02 + r * groupLength + i * step;

                    oscillator.type = 'square';           // daha keskin, ortamda duyulur
                    oscillator.frequency.value = freq;
                    gain.gain.setValueAtTime(0.0001, t);
                    gain.gain.exponentialRampToValueAtTime(volume, t + 0.02);
                    gain.gain.exponentialRampToValueAtTime(0.0001, t + step);

                    oscillator.connect(gain).connect(audio.destination);
                    oscillator.start(t);
                    oscillator.stop(t + step + 0.05);
                });
            }
        } catch { /* ses desteklenmiyorsa sessizce geç */ }
    }

    // Sekme başlığı yanıp sönsün (garson başka sekmedeyse fark etsin)
    const originalTitle = document.title;
    let titleTimer = null;
    function flashTitle(count) {
        clearInterval(titleTimer);
        let on = true;
        titleTimer = setInterval(() => {
            document.title = on ? `🔔 ${count} YENİ SİPARİŞ!` : originalTitle;
            on = !on;
        }, 900);

        const stop = () => {
            clearInterval(titleTimer);
            document.title = originalTitle;
            window.removeEventListener('focus', stop);
        };
        window.addEventListener('focus', stop);      // ekrana dönünce sussun
        setTimeout(stop, FRESH_MS);
    }

    // ---------- sunucu ile konuşma ----------
    async function load() {
        try {
            const res = await fetch(urls.data, { headers: { 'X-Requested-With': 'XMLHttpRequest' }, cache: 'no-store' });
            if (!res.ok) throw new Error();
            board = await res.json();                 // oturum kapandıysa HTML gelir, burada hata verir
            offset = new Date(board.serverTimeUtc).getTime() - Date.now();
            render();
            document.getElementById('kbOffline').hidden = true;
        } catch {
            document.getElementById('kbOffline').hidden = false;
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

    // Butonlar: tek dinleyici (kartlar her yenilemede yeniden çiziliyor)
    root.addEventListener('click', async e => {
        const button = e.target.closest('button[data-action]');
        if (!button || busy) return;

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
    const liveEl = document.getElementById('kbLive');

    function setLive(state) {
        if (!liveEl) return;
        const texts = { on: 'Canlı', off: 'Bağlanıyor', dead: 'Çevrimdışı' };
        liveEl.className = 'kb-live is-' + state;
        liveEl.querySelector('span:last-child').textContent = texts[state];
    }

    async function connectRealtime() {
        if (!window.signalR) { setLive('dead'); return; }   // kütüphane yüklenmediyse yedek yenilemeyle devam

        const connection = new signalR.HubConnectionBuilder()
            .withUrl('/hubs/orders')
            .withAutomaticReconnect([0, 2000, 5000, 10000, 20000]) // kopunca kendi kendine dener
            .build();

        // Yeni sipariş: vurgula, alarm çal, başlığı yanıp söndür, panoyu tazele
        connection.on('orderReceived', data => {
            freshOrders.add(data.orderNo);
            setTimeout(() => freshOrders.delete(data.orderNo), FRESH_MS);

            alarm();
            flashTitle(freshOrders.size);
            toast(`Yeni sipariş: ${data.customerName}`);
            load();
        });

        // Durum değişti (başka bir garson ya da admin işaretledi): panoyu tazele
        connection.on('deliveryChanged', () => load());

        connection.onreconnecting(() => setLive('off'));
        connection.onreconnected(() => { setLive('on'); load(); });   // kopukken kaçanları yakala
        connection.onclose(() => { setLive('dead'); setTimeout(connectRealtime, 5000); });

        try {
            await connection.start();
            setLive('on');
        } catch {
            setLive('dead');
            setTimeout(connectRealtime, 5000);   // sunucu kapalıysa yeniden dene
        }
    }

    // ---------- üst çubuk: saat, ses, tam ekran ----------
    const clockEl = document.getElementById('kbClock');
    if (clockEl) {
        const tick = () => clockEl.textContent = new Date(now()).toLocaleTimeString('tr-TR', { hour: '2-digit', minute: '2-digit', second: '2-digit' });
        tick();
        setInterval(tick, 1000);
    }

    const soundBtn = document.getElementById('kbSound');
    function paintSound() {
        if (!soundBtn) return;
        soundBtn.classList.toggle('is-on', soundOn);
        soundBtn.setAttribute('aria-pressed', soundOn);
        soundBtn.innerHTML = soundOn
            ? "<i class='bx bx-volume-full'></i> <span>Ses açık</span>"
            : "<i class='bx bx-volume-mute'></i> <span>Ses kapalı</span>";
    }
    soundBtn?.addEventListener('click', () => {
        soundOn = !soundOn;
        localStorage.setItem('kbSound', soundOn ? 'on' : 'off');
        if (soundOn) ding();   // deneme sesi
        paintSound();
    });
    paintSound();
    // Tarayıcılar ses için bir kullanıcı tıklaması ister: sayfadaki ilk tıklamada sesi hazırla
    document.addEventListener('click', () => audio?.resume(), { once: true });

    document.getElementById('kbFullscreen')?.addEventListener('click', () => {
        if (document.fullscreenElement) document.exitFullscreen();
        else document.documentElement.requestFullscreen?.();
    });

    // Ekran kararmasın (destekleyen tarayıcılarda)
    async function keepAwake() { try { await navigator.wakeLock?.request('screen'); } catch { } }
    keepAwake();
    document.addEventListener('visibilitychange', () => { if (document.visibilityState === 'visible') keepAwake(); });

    // ---------- başlat ----------
    load();
    connectRealtime();
    setInterval(load, REFRESH_MS);   // SignalR koparsa diye yedek
    setInterval(tickTimers, 10000);
})();