/**
 * Utilidades compartidas — tablas de reportes Admin.
 * Expone: window.MatReportes.formatFechaES
 */
(function (window) {
    'use strict';

    window.MatReportes = window.MatReportes || {};

    /**
     * Formatea un valor de fecha a "dd/mm/aaaa" (solo fecha, sin hora).
     * Acepta:
     *   - ISO con hora  "2024-03-15T00:00:00"  → parseado como hora local por el navegador
     *   - ISO solo fecha "2024-03-15"           → parseado como local (evita offset UTC)
     *   - .NET legacy    "/Date(ms)/"            → usa getDate() local
     *   - número (ms)                            → new Date(ms)
     *   - Date                                   → directo
     *   - "dd/mm/aaaa"                           → devuelve tal cual
     * Devuelve "" si el valor no es parseable.
     */
    window.MatReportes.formatFechaES = function (val) {
        if (val == null || val === '') return '';

        /* Ya está en formato esperado */
        if (typeof val === 'string' && /^\d{2}\/\d{2}\/\d{4}$/.test(val)) return val;

        var d;

        if (typeof val === 'string') {
            /* /Date(ms)/ — serialización legacy de .NET */
            var dotnet = /\/Date\((-?\d+)/.exec(val);
            if (dotnet) {
                d = new Date(parseInt(dotnet[1], 10));
            /* YYYY-MM-DD (solo fecha) — parsear como local para evitar desfase UTC */
            } else if (/^\d{4}-\d{2}-\d{2}$/.test(val)) {
                var p = val.split('-');
                d = new Date(parseInt(p[0], 10), parseInt(p[1], 10) - 1, parseInt(p[2], 10));
            /* ISO con parte horaria — el navegador lo interpreta como hora local */
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

})(window);
