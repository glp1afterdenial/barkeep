document.addEventListener('DOMContentLoaded', function () {
    // Auto-dismiss toast notifications after 4 seconds
    document.querySelectorAll('.toast.show').forEach(function (toastEl) {
        setTimeout(function () {
            toastEl.style.transition = 'opacity 0.4s ease, transform 0.4s ease';
            toastEl.style.opacity = '0';
            toastEl.style.transform = 'translateX(100%)';
            setTimeout(function () { toastEl.remove(); }, 400);
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

    // Scroll-triggered fade-in animations via IntersectionObserver
    var prefersReducedMotion = window.matchMedia('(prefers-reduced-motion: reduce)').matches;

    if (!prefersReducedMotion) {
        var observer = new IntersectionObserver(function (entries) {
            entries.forEach(function (entry) {
                if (entry.isIntersecting) {
                    entry.target.classList.add('is-visible');
                    observer.unobserve(entry.target);
                }
            });
        }, { threshold: 0.1, rootMargin: '0px 0px -40px 0px' });

        document.querySelectorAll('.scroll-reveal').forEach(function (el) {
            observer.observe(el);
        });
    } else {
        // If reduced motion, show everything immediately
        document.querySelectorAll('.scroll-reveal').forEach(function (el) {
            el.classList.add('is-visible');
        });
    }
});
