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

// ── GLOBAL SEARCH ──
document.addEventListener('DOMContentLoaded', () => {
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
