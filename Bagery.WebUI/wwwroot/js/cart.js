// Bagery sepet işlemleri. _WebUILayout'ta yüklenir, her sayfada "BageryCart" olarak kullanılır.
window.BageryCart = (function () {
    'use strict';

    // Bu başlık sayesinde ExceptionFilter hataları HTML sayfası yerine JSON olarak döner (Adım 4.1)
    const HEADERS = { 'X-Requested-With': 'XMLHttpRequest' };

    // null/boş değerleri göndermiyoruz; ör. variantId "" giderse Guid'e çevrilemez
    function clean(data) {
        const result = {};
        Object.keys(data || {}).forEach(key => {
            const value = data[key];
            if (value !== null && value !== undefined && value !== '') result[key] = value;
        });
        return result;
    }

    // Tüm istekler buradan geçer: data yoksa GET, varsa POST
    async function request(url, data) {
        const options = { method: data === undefined ? 'GET' : 'POST', headers: HEADERS };
        if (data !== undefined) options.body = new URLSearchParams(clean(data));

        let response;
        try {
            response = await fetch(url, options);
        } catch {
            return { success: false, message: 'Sunucuya ulaşılamadı. İnternet bağlantınızı kontrol edin.' };
        }

        // Giriş gerektiren bir istekte oturum yoksa ASP.NET 401 döner (Adım 8'de lazım olacak)
        if (response.status === 401) {
            return { success: false, message: 'Bu işlem için giriş yapmalısınız.' };
        }

        let json = null;
        try { json = await response.json(); } catch { /* JSON değilse (ör. 500 hata sayfası) */ }
        if (!json) return { success: false, message: 'Beklenmeyen bir hata oluştu. Lütfen tekrar deneyin.' };

        if (json.cart) updateBadge(json.cart.totalQuantity); // her cevapta header'daki sayı güncellenir
        return json;
    }

    function updateBadge(count) {
        document.querySelectorAll('.cart-count').forEach(el => {
            el.textContent = count > 0 ? count : ''; // 0 ise boş bırak, CSS gizler
        });
    }

    function formatPrice(value) {
        return Number(value || 0).toLocaleString('tr-TR', { minimumFractionDigits: 2, maximumFractionDigits: 2 }) + ' ₺';
    }

    // Ürün adını HTML'e basarken zararlı kod çalışmasın diye özel karakterleri etkisizleştirir
    function escapeHtml(text) {
        return String(text ?? '').replace(/[&<>"']/g, c =>
            ({ '&': '&amp;', '<': '&lt;', '>': '&gt;', '"': '&quot;', "'": '&#39;' }[c]));
    }

    // Alttan çıkan bildirim. link verilirse mesajın yanında gösterilir (ör. "Sepete git")
    let toastTimer;
    function toast(message, success, link) {
        let box = document.getElementById('bageryToast');
        if (!box) {
            box = document.createElement('div');
            box.id = 'bageryToast';
            box.className = 'bagery-toast';
            box.setAttribute('role', 'status'); // ekran okuyucular mesajı seslendirsin
            document.body.appendChild(box);
        }
        box.classList.toggle('is-error', success === false);
        box.innerHTML = escapeHtml(message) + (link ? ` <a href="${link.href}">${escapeHtml(link.text)}</a>` : '');
        box.classList.add('is-visible');

        clearTimeout(toastTimer);
        toastTimer = setTimeout(() => box.classList.remove('is-visible'), link ? 4000 : 2500);
    }

    // Dışarıya açılan fonksiyonlar - adresler Adım 4'teki CartController action'ları
    return {
        get: () => request('/Cart/GetCart'),
        add: (productId, quantity, variantId) => request('/Cart/Add', { productId, quantity, variantId }),
        decrease: (productId, variantId) => request('/Cart/Decrease', { productId, variantId }),
        remove: (productId, variantId) => request('/Cart/Remove', { productId, variantId }),
        applyCoupon: (couponCode) => request('/Cart/ApplyCoupon', { couponCode }),
        removeCoupon: () => request('/Cart/RemoveCoupon', {}),
        post: request, // Adım 8'de ödeme sayfası kullanacak
        formatPrice,
        escapeHtml,
        toast
    };
})();

// Her sayfa açıldığında header'daki sepet sayısını doldur
document.addEventListener('DOMContentLoaded', () => { window.BageryCart.get(); });