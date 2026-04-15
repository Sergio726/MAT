/**
 * mat.reportes-ranking-dashboard.js
 * Agrega datos de lastRows para las 5 tarjetas KPI del dashboard de ranking.
 * Expone: window.MatReportes.RankingDashboard.init(cfg) / .render(rows) / .downloadPdf()
 */
(function (window) {
    'use strict';

    window.MatReportes = window.MatReportes || {};

    var _getLastRows;

    function escHtml(s) {
        return String(s || '').replace(/&/g, '&amp;').replace(/</g, '&lt;').replace(/>/g, '&gt;').replace(/"/g, '&quot;');
    }

    function fmtFecha(val) {
        return window.MatReportes.formatFechaES ? window.MatReportes.formatFechaES(val) : String(val || '');
    }

    /* ── Agregaciones ────────────────────────────────────────── */

    function countDistinct(rows, key) {
        var set = {};
        for (var i = 0; i < rows.length; i++) {
            var v = rows[i][key];
            if (v) set[v] = 1;
        }
        return Object.keys(set).length;
    }

    function sumField(rows, key) {
        var total = 0;
        for (var i = 0; i < rows.length; i++) {
            var v = rows[i][key];
            if (v != null) total += parseInt(v, 10) || 0;
        }
        return total;
    }

    function aggregateViajes(rows) {
        var map = {};
        for (var i = 0; i < rows.length; i++) {
            var r = rows[i];
            var desc = r.viajeDescripcion || '(sin viaje)';
            if (!map[desc]) {
                map[desc] = { viaje: desc, ventas: 0, pasajeros: 0, clientes: {}, fechas: {} };
            }
            var g = map[desc];
            g.ventas++;
            g.pasajeros += parseInt(r.cantidadPasajesXFactura, 10) || 0;
            if (r.clienteId) g.clientes[r.clienteId] = 1;
            var fs = fmtFecha(r.viajeFechaSalida);
            if (fs) g.fechas[fs] = 1;
        }
        var arr = [];
        for (var k in map) {
            if (!map.hasOwnProperty(k)) continue;
            var item = map[k];
            item.cantClientes = Object.keys(item.clientes).length;
            item.fechasList = Object.keys(item.fechas).join(', ');
            arr.push(item);
        }
        arr.sort(function (a, b) { return b.ventas - a.ventas; });
        return arr;
    }

    /* ── Render dashboard ────────────────────────────────────── */

    function render(rows) {
        $('#kpiRegistros').text(String(rows.length));
        $('#kpiClientes').text(String(countDistinct(rows, 'clienteId')));
        $('#kpiPasajeros').text(String(sumField(rows, 'cantidadPasajesXFactura')));
        $('#kpiFacturas').text(String(countDistinct(rows, 'facturaId')));

        var viajes = aggregateViajes(rows);
        var top3 = viajes.slice(0, 3);
        var html = '';
        for (var i = 0; i < top3.length; i++) {
            var marks = ['\uD83E\uDD47', '\uD83E\uDD48', '\uD83E\uDD49'];
            html += '<div class="d-flex align-items-center gap-2 mb-1">';
            html += '<span class="fs-5 lh-1">' + (marks[i] || '') + '</span>';
            html += '<span class="fw-semibold text-truncate small" style="max-width:160px" title="' + escHtml(top3[i].viaje) + '">';
            html += escHtml(top3[i].viaje) + '</span>';
            html += '<span class="badge bg-secondary-subtle text-secondary ms-auto">' + top3[i].ventas + '</span>';
            html += '</div>';
        }
        if (top3.length === 0) html = '<span class="text-muted small">\u2014</span>';
        $('#kpiTopViajes').html(html);
    }

    /* ── PDF download (window.print fallback) ────────────────── */

    function downloadPdf() {
        if (typeof html2pdf !== 'undefined') {
            var el = document.getElementById('dashboardCards');
            var opt = {
                margin: [10, 10, 10, 10],
                filename: 'ranking-compras-' + new Date().toISOString().slice(0, 10) + '.pdf',
                image: { type: 'jpeg', quality: 0.95 },
                html2canvas: { scale: 2, useCORS: true },
                jsPDF: { unit: 'mm', format: 'a4', orientation: 'landscape' }
            };
            html2pdf().set(opt).from(el).save();
        } else {
            window.print();
        }
    }

    /* ── API pública ─────────────────────────────────────────── */

    function init(cfg) {
        _getLastRows = cfg.getLastRows;
    }

    window.MatReportes.RankingDashboard = {
        init: init,
        render: render,
        downloadPdf: downloadPdf,
        aggregateViajes: aggregateViajes,
        countDistinct: countDistinct,
        sumField: sumField,
        escHtml: escHtml
    };

})(window);
