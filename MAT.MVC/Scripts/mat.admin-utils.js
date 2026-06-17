/**
 * Utilidades compartidas del panel Admin.
 * Expone: window.MatAdmin.toast, window.MatAdmin.confirm, window.MatAdmin.initConfirmModal,
 *         window.MatAdmin.showLoading, window.MatAdmin.hideLoading
 */
(function (window) {
    'use strict';

    window.MatAdmin = window.MatAdmin || {};

    var confirmModal = null;
    var confirmResolve = null;

    var toastIcons = {
        success: 'bi-check-circle-fill',
        danger: 'bi-exclamation-circle-fill',
        warning: 'bi-exclamation-triangle-fill',
        info: 'bi-info-circle-fill'
    };

    var toastBg = {
        success: 'text-bg-success',
        danger: 'text-bg-danger',
        warning: 'text-bg-warning',
        info: 'text-bg-primary'
    };

    function escapeHtml(str) {
        if (str == null) return '';
        return String(str)
            .replace(/&/g, '&amp;')
            .replace(/</g, '&lt;')
            .replace(/>/g, '&gt;')
            .replace(/"/g, '&quot;');
    }

    function getToastContainer() {
        var el = document.getElementById('adminToastContainer');
        if (!el) {
            el = document.createElement('div');
            el.id = 'adminToastContainer';
            el.className = 'toast-container position-fixed bottom-0 end-0 p-3';
            el.setAttribute('aria-live', 'polite');
            el.style.zIndex = '1100';
            document.body.appendChild(el);
        }
        return el;
    }

    window.MatAdmin.toast = function (message, type) {
        type = type || 'info';
        if (!window.bootstrap || !window.bootstrap.Toast) return;

        var container = getToastContainer();
        var id = 'admin-toast-' + Date.now();
        var icon = toastIcons[type] || toastIcons.info;
        var bg = toastBg[type] || toastBg.info;

        container.insertAdjacentHTML('beforeend',
            '<div id="' + id + '" class="toast align-items-center border-0 ' + bg + '" role="alert" aria-live="assertive" aria-atomic="true">' +
            '<div class="d-flex">' +
            '<div class="toast-body"><i class="bi ' + icon + ' me-2"></i>' + escapeHtml(message) + '</div>' +
            '<button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast" aria-label="Cerrar"></button>' +
            '</div></div>');

        var toastEl = document.getElementById(id);
        var toast = new bootstrap.Toast(toastEl, { autohide: true, delay: 4000 });
        toastEl.addEventListener('hidden.bs.toast', function () {
            if (toastEl.parentNode) toastEl.parentNode.removeChild(toastEl);
        });
        toast.show();
    };

    window.MatAdmin.initConfirmModal = function () {
        var el = document.getElementById('adminConfirmModal');
        if (!el || !window.bootstrap || !window.bootstrap.Modal) return;

        confirmModal = new bootstrap.Modal(el);

        var acceptBtn = document.getElementById('adminConfirmModalAccept');
        if (acceptBtn && !acceptBtn.getAttribute('data-mat-admin-bound')) {
            acceptBtn.setAttribute('data-mat-admin-bound', '1');
            acceptBtn.addEventListener('click', function () {
                if (confirmResolve) {
                    var resolve = confirmResolve;
                    confirmResolve = null;
                    resolve(true);
                }
                if (confirmModal) confirmModal.hide();
            });
        }

        if (!el.getAttribute('data-mat-admin-bound')) {
            el.setAttribute('data-mat-admin-bound', '1');
            el.addEventListener('hidden.bs.modal', function () {
                if (confirmResolve) {
                    var resolve = confirmResolve;
                    confirmResolve = null;
                    resolve(false);
                }
            });
        }
    };

    function resolveEl(target) {
        if (!target) return null;
        if (target.jquery) return target[0] ? target : null;
        if (typeof target === 'string') return document.querySelector(target);
        return target.nodeType === 1 ? target : null;
    }

    window.MatAdmin.showLoading = function (target, options) {
        options = options || {};
        var el = resolveEl(target);
        if (!el || el.getAttribute('data-mat-loading') === '1') return;

        var mode = options.mode || 'button';
        el.setAttribute('data-mat-loading', '1');
        el.setAttribute('data-mat-loading-mode', mode);

        if (mode === 'overlay') {
            var $container = window.jQuery ? window.jQuery(el) : null;
            if ($container && $container.length) {
                $container.css('position', 'relative');
                var text = options.text || 'Cargando...';
                $container.append(
                    '<div class="mat-admin-loading-overlay position-absolute top-0 start-0 w-100 h-100 d-flex align-items-center justify-content-center" ' +
                    'style="background:rgba(255,255,255,0.85);z-index:10;">' +
                    '<div class="modern-loading-container border-0 bg-transparent">' +
                    '<div class="modern-loading-spinner"></div>' +
                    '<div class="modern-loading-text">' + escapeHtml(text) + '</div>' +
                    '</div></div>'
                );
            }
            return;
        }

        if (el.tagName === 'BUTTON' || el.tagName === 'INPUT') {
            el.setAttribute('data-mat-loading-html', el.innerHTML);
            el.disabled = true;
            var label = options.loadingText || 'Consultando...';
            el.innerHTML = '<span class="spinner-border spinner-border-sm me-1" role="status" aria-hidden="true"></span>' +
                escapeHtml(label);
        }
    };

    window.MatAdmin.hideLoading = function (target) {
        var el = resolveEl(target);
        if (!el || el.getAttribute('data-mat-loading') !== '1') return;

        var mode = el.getAttribute('data-mat-loading-mode') || 'button';

        if (mode === 'overlay' && window.jQuery) {
            window.jQuery(el).find('.mat-admin-loading-overlay').remove();
        } else {
            var saved = el.getAttribute('data-mat-loading-html');
            if (saved != null) el.innerHTML = saved;
            el.disabled = false;
        }

        el.removeAttribute('data-mat-loading');
        el.removeAttribute('data-mat-loading-mode');
        el.removeAttribute('data-mat-loading-html');
    };

    window.MatAdmin.confirm = function (options) {
        options = options || {};
        return new Promise(function (resolve) {
            var el = document.getElementById('adminConfirmModal');
            if (!el || !window.bootstrap || !window.bootstrap.Modal) {
                resolve(window.confirm(options.message || '¿Confirmar esta acción?'));
                return;
            }

            if (!confirmModal) window.MatAdmin.initConfirmModal();

            var titleEl = document.getElementById('adminConfirmModalLabel');
            var msgEl = document.getElementById('adminConfirmModalMessage');
            var acceptBtn = document.getElementById('adminConfirmModalAccept');

            if (titleEl) titleEl.textContent = options.title || 'Confirmación';
            if (msgEl) msgEl.textContent = options.message || '¿Confirmar esta acción?';
            if (acceptBtn) {
                acceptBtn.textContent = options.confirmLabel || 'Confirmar';
                acceptBtn.className = 'btn ' + (options.confirmClass || 'btn-danger');
            }

            confirmResolve = resolve;
            confirmModal.show();
        });
    };

    function onReady(fn) {
        if (document.readyState === 'loading') {
            document.addEventListener('DOMContentLoaded', fn);
        } else {
            fn();
        }
    }

    onReady(function () {
        window.MatAdmin.initConfirmModal();
    });
})(window);
