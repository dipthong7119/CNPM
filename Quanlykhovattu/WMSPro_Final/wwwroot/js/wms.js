// ── THEME TOGGLE ──
function _saveTheme(value) {
    try { localStorage.setItem('wms-theme', value); } catch (e) { /* Brave Shields có thể chặn localStorage trên localhost */ }
}
function _loadTheme() {
    try { return localStorage.getItem('wms-theme'); } catch (e) { return null; }
}

function toggleTheme() {
    var html = document.documentElement;
    var isDark = html.getAttribute('data-theme') === 'dark';
    if (isDark) {
        html.removeAttribute('data-theme');
        _saveTheme('light');
    } else {
        html.setAttribute('data-theme', 'dark');
        _saveTheme('dark');
    }
    updateThemeIcon();
}

// Áp dụng theme đã lưu khi trang tải xong (backup phòng trường hợp script head chưa chạy)
(function () {
    var saved = _loadTheme();
    if (saved === 'dark') document.documentElement.setAttribute('data-theme', 'dark');
})();

// ── TOAST ──
function showToast(msg, type = 'success') {
    const icons = { success: '✅', error: '❌', warning: '⚠️', info: 'ℹ️' };
    let container = document.getElementById('toastContainer');
    if (!container) {
        container = document.createElement('div');
        container.id = 'toastContainer';
        container.className = 'toast-container';
        document.body.appendChild(container);
    }
    const toast = document.createElement('div');
    toast.className = `toast ${type}`;
    toast.innerHTML = `<span>${icons[type] || '✅'}</span><span>${msg}</span>`;
    container.appendChild(toast);
    setTimeout(() => {
        toast.style.transition = 'opacity .3s, transform .3s';
        toast.style.opacity = '0';
        toast.style.transform = 'translateX(100%)';
        setTimeout(() => toast.remove(), 300);
    }, 3500);
}

// ── FILTER TABLE ──
function filterTable(input) {
    const q = input.value.toLowerCase();
    const table = input.closest('.card')?.querySelector('table') || document.getElementById('mainTable');
    if (!table) return;
    table.querySelectorAll('tbody tr').forEach(row => {
        row.style.display = row.textContent.toLowerCase().includes(q) ? '' : 'none';
    });
}

// ── CẬP NHẬT ICON THEME ──
function updateThemeIcon() {
    var icon = document.getElementById('themeIcon');
    if (!icon) return;
    var isDark = document.documentElement.getAttribute('data-theme') === 'dark';
    icon.textContent = isDark ? '☀️' : '🌙';
}

// ── GLOBAL SEARCH ──
document.addEventListener('DOMContentLoaded', () => {
    // Cập nhật icon theme khi trang load xong
    updateThemeIcon();

    const gs = document.getElementById('globalSearch');
    if (gs) {
        gs.addEventListener('keydown', e => {
            if (e.key === 'Enter' && e.target.value.trim()) {
                window.location.href = `/VatTu?q=${encodeURIComponent(e.target.value)}`;
            }
        });
    }
    // Auto-show success toast
    const alertEl = document.querySelector('.alert-success');
    if (alertEl) {
        setTimeout(() => showToast(alertEl.textContent.replace('✅','').trim(), 'success'), 300);
    }
    const errorEl = document.querySelector('.alert-error');
    if (errorEl) {
        setTimeout(() => showToast(errorEl.textContent.replace('❌','').trim(), 'error'), 300);
    }
});
