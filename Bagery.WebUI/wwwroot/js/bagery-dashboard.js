// Bagery dashboard grafik yardımcısı (ApexCharts admin layout'unda zaten yüklü)
window.BageryDash = (function () {
    'use strict';

    const charts = []; // tema değişince yeniden boyamak için çizilen grafikleri tutar

    // CSS değişkenini oku (.bd içindeki --ink, --c1 ... koyu temada otomatik değişir)
    function css(name) {
        const root = document.querySelector('.bd') || document.documentElement;
        return getComputedStyle(root).getPropertyValue(name).trim();
    }

    const isDark = () => document.documentElement.classList.contains('dark-theme');

    function palette() {
        const p = {
            ink: css('--ink'), muted: css('--muted'), line: css('--line'), card: css('--card'),
            c1: css('--c1'), c2: css('--c2'), c3: css('--c3'), c4: css('--c4'), c5: css('--c5'), c6: css('--c6')
        };
        p.list = [p.c1, p.c4, p.c3, p.c2, p.c5, p.c6, p.ink]; // çok dilimli grafikler için sıra
        return p;
    }

    // Her grafikte ortak olan, temaya bağlı ayarlar
    function base() {
        const p = palette();
        return {
            chart: { foreColor: p.muted, fontFamily: 'inherit', background: 'transparent', toolbar: { show: false }, zoom: { enabled: false } },
            grid: { borderColor: p.line, strokeDashArray: 4 },
            dataLabels: { enabled: false },
            legend: { labels: { colors: p.muted }, markers: { radius: 4 } },
            tooltip: { theme: isDark() ? 'dark' : 'light' },
            theme: { mode: isDark() ? 'dark' : 'light' },
            noData: { text: 'Bu dönemde veri yok', style: { color: p.muted } }
        };
    }

    // İç içe nesneleri birleştirir (grafiğe özel ayarlar ortak ayarların üstüne yazılır)
    function merge(a, b) {
        const out = { ...a };
        Object.keys(b || {}).forEach(key => {
            const bv = b[key], av = a ? a[key] : undefined;
            const bothObjects = bv && av && typeof bv === 'object' && typeof av === 'object' && !Array.isArray(bv) && !Array.isArray(av);
            out[key] = bothObjects ? merge(av, bv) : bv;
        });
        return out;
    }

    // optionsFn: palet alıp grafik ayarını döndüren fonksiyon. Tema değişince tekrar çağrılır -> renkler güncellenir.
    function chart(selector, optionsFn) {
        const el = document.querySelector(selector);
        if (!el || !window.ApexCharts) return null;

        const instance = new ApexCharts(el, merge(base(), optionsFn(palette())));
        instance.render();
        charts.push({ instance, optionsFn });
        return instance;
    }

    // Admin temasının "Dark / Light" düğmesi <html> sınıfını değiştirir -> bütün grafikleri yeni renklerle yeniden boya
    new MutationObserver(() => {
        charts.forEach(({ instance, optionsFn }) => instance.updateOptions(merge(base(), optionsFn(palette())), true, false));
    }).observe(document.documentElement, { attributes: true, attributeFilter: ['class'] });

    const money = v => Number(v || 0).toLocaleString('tr-TR', { minimumFractionDigits: 2, maximumFractionDigits: 2 }) + ' ₺';
    const moneyShort = v => new Intl.NumberFormat('tr-TR', { notation: 'compact', maximumFractionDigits: 1 }).format(v || 0) + ' ₺';
    const count = v => Math.round(v || 0).toLocaleString('tr-TR');
    const hasData = values => values.some(v => v > 0); // hepsi 0 ise donut boş görünmesin, "veri yok" yazsın

    return { chart, money, moneyShort, count, hasData };
})();