/**
 * Utilidades compartidas — tablas de reportes Admin.
 * Expone: window.MatReportes.formatFechaES, parseMoneyCell, moneyFmt,
 *         renderMonedaBadge, renderMontoCurrency, estadoBadgeHtml,
 *         emptyTableHtml, dataTableEmptyLanguage, parseHttpErrorBody
 */
(function (window) {
    'use strict';

    window.MatReportes = window.MatReportes || {};

    function escapeHtml(str) {
        if (str == null) return '';
        return String(str)
            .replace(/&/g, '&amp;')
            .replace(/</g, '&lt;')
            .replace(/>/g, '&gt;')
            .replace(/"/g, '&quot;');
    }

    window.MatReportes.escapeHtml = escapeHtml;

    window.MatReportes.moneyFmt = new Intl.NumberFormat('es-AR', {
        minimumFractionDigits: 0,
        maximumFractionDigits: 2
    });

    window.MatReportes.parseMoneyCell = function (v) {
        if (v == null || v === '') return 0;
        if (typeof v === 'number') return isNaN(v) ? 0 : v;
        var s = String(v).trim().replace(/\s/g, '');
        var n = parseFloat(s.replace(/\./g, '').replace(',', '.'));
        return isNaN(n) ? 0 : n;
    };

    window.MatReportes.currencySymbol = function (monedaTipo) {
        var m = String(monedaTipo != null ? monedaTipo : '').trim();
        if (m === '3') return 'U$D ';
        if (m === '1') return '$ ';
        return '$ ';
    };

    window.MatReportes.renderMonedaBadge = function (tipo) {
        var t = String(tipo != null ? tipo : '').trim();
        if (t === '1') {
            return '<span class="badge rounded-pill bg-success-subtle text-success">$ARS</span>';
        }
        if (t === '3') {
            return '<span class="badge rounded-pill bg-info-subtle text-info">U$D</span>';
        }
        return '<span class="badge rounded-pill bg-secondary-subtle text-secondary">' +
            escapeHtml(t || '-') + '</span>';
    };

    window.MatReportes.renderMontoCurrency = function (monto, monedaTipo, type) {
        var n = window.MatReportes.parseMoneyCell(monto);
        if (type && type !== 'display') return n;
        var sym = window.MatReportes.currencySymbol(monedaTipo);
        return '<span class="text-nowrap">' + sym + window.MatReportes.moneyFmt.format(n) + '</span>';
    };

    window.MatReportes.estadoBadgeHtml = function (val) {
        if (val == null || val === '') {
            return '<span class="badge bg-secondary-subtle text-secondary">-</span>';
        }
        var text = String(val);
        var lower = text.toLowerCase();
        var cls = 'secondary';
        if (lower.indexOf('anulado') !== -1) {
            cls = 'danger';
        } else if (lower.indexOf('pre-reserva') !== -1) {
            cls = 'warning';
        } else if (lower.indexOf('pagado') !== -1) {
            cls = 'success';
        }
        return '<span class="badge bg-' + cls + '-subtle text-' + cls + '">' + escapeHtml(text) + '</span>';
    };

    window.MatReportes.emptyTableHtml = function (title, hint) {
        title = title || 'No hay resultados';
        hint = hint || '';
        return '<div class="admin-empty-state py-4">' +
            '<i class="bi bi-inbox admin-empty-icon text-secondary d-block mb-2"></i>' +
            '<p class="admin-empty-title mb-1">' + escapeHtml(title) + '</p>' +
            (hint ? '<p class="admin-empty-hint text-secondary mb-0">' + escapeHtml(hint) + '</p>' : '') +
            '</div>';
    };

    window.MatReportes.dataTableEmptyLanguage = function (title, hint) {
        return { emptyTable: window.MatReportes.emptyTableHtml(title, hint) };
    };

    /**
     * Formatea un valor de fecha a "dd/mm/aaaa" (solo fecha, sin hora).
     */
    window.MatReportes.formatFechaES = function (val) {
        if (val == null || val === '') return '';

        if (typeof val === 'string' && /^\d{2}\/\d{2}\/\d{4}$/.test(val)) return val;

        var d;

        if (typeof val === 'string') {
            var dotnet = /\/Date\((-?\d+)/.exec(val);
            if (dotnet) {
                d = new Date(parseInt(dotnet[1], 10));
            } else if (/^\d{4}-\d{2}-\d{2}$/.test(val)) {
                var p = val.split('-');
                d = new Date(parseInt(p[0], 10), parseInt(p[1], 10) - 1, parseInt(p[2], 10));
            } else {
                d = new Date(val);
            }
        } else if (typeof val === 'number') {
            d = new Date(val);
        } else if (val instanceof Date) {
            d = val;
        }

        if (!d || isNaN(d.getTime())) return typeof val === 'string' ? val : '';

        var day   = d.getDate();
        var month = d.getMonth() + 1;
        var year  = d.getFullYear();
        return (day   < 10 ? '0' : '') + day   + '/' +
               (month < 10 ? '0' : '') + month + '/' + year;
    };

    if (!window.MatReportes.parseHttpErrorBody) {
        window.MatReportes.parseHttpErrorBody = function (xhr) {
            if (!xhr) return 'Error de red';
            var txt = xhr.responseText;
            if (txt) {
                try {
                    var o = JSON.parse(txt);
                    if (o && o.message) return o.message;
                } catch (_) { /* JSON inválido */ }
            }
            if (xhr.statusText && xhr.statusText !== 'error') return xhr.statusText;
            return 'Error de conexión';
        };
    }

})(window);
