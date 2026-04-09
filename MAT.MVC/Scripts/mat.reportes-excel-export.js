/**
 * Utilidades compartidas — reportes Admin (JSON + descarga Excel).
 * Requiere: jQuery, fetch, URL.
 */
(function (window, $) {
    'use strict';

    window.MatReportes = window.MatReportes || {};

    /** Interpreta cuerpo JSON { ok, message } o texto plano (Excel 400/500). */
    window.MatReportes.parseHttpErrorBody = function (xhr) {
        if (!xhr) return 'Error de red';
        var txt = xhr.responseText;
        if (txt) {
            try {
                var o = JSON.parse(txt);
                if (o && o.message) return o.message;
            } catch (e) { /* no es JSON */ }
            if (txt.trim()) return txt.trim();
        }
        return xhr.statusText || 'Error de red';
    };

    /**
     * GET Excel vía fetch + blob; errores HTTP muestran texto de respuesta en showErr.
     * @param {string} excelUrl URL sin querystring
     * @param {function():Object} getQueryParams objeto plano para $.param
     * @param {function(string)} showErr
     */
    window.MatReportes.downloadReporteExcel = function (excelUrl, getQueryParams, showErr) {
        showErr('');
        var q = $.param(getQueryParams());
        var url = excelUrl + (q ? '?' + q : '');
        var $btn = $('#btnExcel').prop('disabled', true);
        var p = fetch(url, { credentials: 'same-origin' })
            .then(function (res) {
                if (!res.ok) {
                    return res.text().then(function (t) {
                        var msg = (t && t.trim()) ? t.trim() : ('Error ' + res.status);
                        throw new Error(msg);
                    });
                }
                var cd = res.headers.get('Content-Disposition');
                return res.blob().then(function (blob) {
                    return { blob: blob, cd: cd };
                });
            })
            .then(function (o) {
                var name = 'reporte.xlsx';
                if (o.cd) {
                    var star = /filename\*=UTF-8''([^;]+)/i.exec(o.cd);
                    var quoted = /filename="([^"]+)"/i.exec(o.cd);
                    var plain = /filename=([^;\s]+)/i.exec(o.cd);
                    if (star) name = decodeURIComponent(star[1].trim());
                    else if (quoted) name = quoted[1];
                    else if (plain) name = plain[1].replace(/^"+|"+$/g, '');
                }
                var a = document.createElement('a');
                a.href = URL.createObjectURL(o.blob);
                a.download = name;
                document.body.appendChild(a);
                a.click();
                a.remove();
                URL.revokeObjectURL(a.href);
            })
            .catch(function (err) {
                showErr(err.message || 'No se pudo exportar.');
            });
        if (p.finally) p.finally(function () { $btn.prop('disabled', false); });
        else p.then(function () { $btn.prop('disabled', false); }, function () { $btn.prop('disabled', false); });
    };
})(window, jQuery);
