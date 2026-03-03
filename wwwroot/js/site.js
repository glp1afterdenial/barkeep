document.addEventListener('DOMContentLoaded', function () {
    // Auto-dismiss toast notifications after 4 seconds
    document.querySelectorAll('.toast.show').forEach(function (toastEl) {
        setTimeout(function () {
            toastEl.style.transition = 'opacity 0.5s ease, transform 0.5s ease';
            toastEl.style.opacity = '0';
            toastEl.style.transform = 'translateX(100%)';
            setTimeout(function () { toastEl.remove(); }, 500);
        }, 4000);
    });

    // Add-to-cart button feedback on submit
    document.querySelectorAll('.btn-add-to-cart').forEach(function (btn) {
        btn.closest('form').addEventListener('submit', function () {
            btn.disabled = true;
            btn.innerHTML = '<i class="bi bi-check-lg me-1"></i>Adding\u2026';
            btn.classList.add('btn-adding');
        });
    });
});
