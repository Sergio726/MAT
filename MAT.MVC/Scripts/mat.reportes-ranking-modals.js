/**
 * mat.reportes-ranking-modals.js
 * Renderiza tablas dentro de los 5 modales del dashboard Ranking de compras.
 * Cada modal: búsqueda con debounce, ordenación por columna, contador.
 * Expone: window.MatReportes.RankingModals.init(cfg)
 */
(function (window) {
    'use strict';

    window.MatReportes = window.MatReportes || {};

    var _getLastRows;
    var _debounceTimers = {};

    function _D() { return window.MatReportes.RankingDashboard || {}; }
    function esc(s) { var fn = _D().escHtml; return fn ? fn(s) : String(s || ''); }
    function fmtFecha(v) { return window.MatReportes.formatFechaES ? window.MatReportes.formatFechaES(v) : String(v || ''); }

    /* ── Generic sort helper ─────────────────────────────────── */

    function sortRows(arr, col, asc) {
        arr.sort(function (a, b) {
            var va = a[col], vb = b[col];
            if (va == null) va = '';
            if (vb == null) vb = '';
            if (typeof va === 'number' && typeof vb === 'number') return asc ? va - vb : vb - va;
            va = String(va).toLowerCase(); vb = String(vb).toLowerCase();
            if (va < vb) return asc ? -1 : 1;
            if (va > vb) return asc ? 1 : -1;
            return 0;
        });
    }

    function arrowHtml(col, sortCol, sortAsc) {
        if (col !== sortCol) return '<span class="sort-arrow text-muted">\u25B2</span>';
        return '<span class="sort-arrow">' + (sortAsc ? '\u25B2' : '\u25BC') + '</span>';
    }

    /* ── Registros modal ─────────────────────────────────────── */

    var regState = { sortCol: 'fecha', sortAsc: false, search: '' };

    function renderRegistros() {
        var rows = (_getLastRows() || []).slice();
        var q = regState.search.toLowerCase();
        if (q) {
            rows = rows.filter(function (r) {
                return (r.clienteFullName || '').toLowerCase().indexOf(q) >= 0 ||
                       (r.viajeDescripcion || '').toLowerCase().indexOf(q) >= 0;
            });
        }
        sortRows(rows, regState.sortCol, regState.sortAsc);

        var $m = $('#modalRegistros');
        var cols = [
            { key: 'fecha', label: 'Fecha' },
            { key: 'clienteFullName', label: 'Cliente' },
            { key: 'viajeDescripcion', label: 'Viaje' }
        ];
        var thead = '<tr>';
        for (var c = 0; c < cols.length; c++) {
            thead += '<th data-col="' + cols[c].key + '">' + cols[c].label + ' ' + arrowHtml(cols[c].key, regState.sortCol, regState.sortAsc) + '</th>';
        }
        thead += '</tr>';

        var tbody = '';
        for (var i = 0; i < rows.length; i++) {
            var r = rows[i];
            var fs = fmtFecha(r.viajeFechaSalida);
            tbody += '<tr>';
            tbody += '<td>' + fmtFecha(r.fecha) + '</td>';
            tbody += '<td>' + esc(r.clienteFullName) + '</td>';
            tbody += '<td><span class="text-truncate d-inline-block" style="max-width:220px" title="' + esc(r.viajeDescripcion) + '">' + esc(r.viajeDescripcion) + '</span>';
            if (fs) tbody += '<br><span class="text-muted small"><i class="bi bi-calendar3 me-1"></i>' + fs + '</span>';
            tbody += '</td>';
            tbody += '</tr>';
        }
        if (!rows.length) tbody = '<tr><td colspan="3" class="text-center text-muted py-3"><i class="bi bi-inbox"></i> Sin resultados</td></tr>';

        $m.find('thead').html(thead);
        $m.find('tbody').html(tbody);
        $m.find('.ranking-modal-count').text(rows.length + ' de ' + (_getLastRows() || []).length);
    }

    /* ── Clientes modal ──────────────────────────────────────── */

    var cliState = { sortCol: 'facturas', sortAsc: false, search: '' };

    function aggregateClientes(rows) {
        var map = {};
        for (var i = 0; i < rows.length; i++) {
            var r = rows[i];
            var id = r.clienteId;
            if (!id) continue;
            if (!map[id]) map[id] = { cliente: r.clienteFullName || '', facturas: {}, pasajes: 0, viajes: {} };
            var g = map[id];
            if (r.facturaId) g.facturas[r.facturaId] = 1;
            g.pasajes += parseInt(r.cantidadPasajesXFactura, 10) || 0;
            if (r.viajeId) g.viajes[r.viajeId] = 1;
        }
        var arr = [];
        for (var k in map) {
            if (!map.hasOwnProperty(k)) continue;
            arr.push({
                cliente: map[k].cliente,
                facturas: Object.keys(map[k].facturas).length,
                pasajes: map[k].pasajes,
                viajes: Object.keys(map[k].viajes).length
            });
        }
        return arr;
    }

    function renderClientes() {
        var all = aggregateClientes(_getLastRows() || []);
        var q = cliState.search.toLowerCase();
        var rows = q ? all.filter(function (r) { return r.cliente.toLowerCase().indexOf(q) >= 0; }) : all;
        sortRows(rows, cliState.sortCol, cliState.sortAsc);

        var $m = $('#modalClientes');
        var cols = [
            { key: 'cliente', label: 'Cliente' },
            { key: 'facturas', label: 'Facturas' },
            { key: 'pasajes', label: 'Pasajes' },
            { key: 'viajes', label: 'Viajes' }
        ];
        var thead = '<tr>';
        for (var c = 0; c < cols.length; c++) {
            var cls = c > 0 ? ' class="text-center"' : '';
            thead += '<th data-col="' + cols[c].key + '"' + cls + '>' + cols[c].label + ' ' + arrowHtml(cols[c].key, cliState.sortCol, cliState.sortAsc) + '</th>';
        }
        thead += '</tr>';

        var tbody = '';
        for (var i = 0; i < rows.length; i++) {
            var r = rows[i];
            tbody += '<tr>';
            tbody += '<td>' + esc(r.cliente) + '</td>';
            tbody += '<td class="text-center fw-semibold">' + r.facturas + '</td>';
            tbody += '<td class="text-center fw-semibold">' + r.pasajes + '</td>';
            tbody += '<td class="text-center fw-semibold">' + r.viajes + '</td>';
            tbody += '</tr>';
        }
        if (!rows.length) tbody = '<tr><td colspan="4" class="text-center text-muted py-3"><i class="bi bi-inbox"></i> Sin resultados</td></tr>';

        $m.find('thead').html(thead);
        $m.find('tbody').html(tbody);
        $m.find('.ranking-modal-count').text(rows.length + ' de ' + all.length);
    }

    /* ── Pasajeros modal ─────────────────────────────────────── */

    var pasState = { sortCol: 'pasajeros', sortAsc: false, search: '' };

    function aggregatePasajeros(rows) {
        var map = {};
        for (var i = 0; i < rows.length; i++) {
            var r = rows[i];
            var desc = r.viajeDescripcion || '(sin viaje)';
            if (!map[desc]) map[desc] = { viaje: desc, pasajeros: 0, fechas: {} };
            var g = map[desc];
            g.pasajeros += parseInt(r.cantidadPasajesXFactura, 10) || 0;
            var fs = fmtFecha(r.viajeFechaSalida);
            if (fs) g.fechas[fs] = 1;
        }
        var arr = [];
        for (var k in map) {
            if (!map.hasOwnProperty(k)) continue;
            var item = map[k];
            item.fechasList = Object.keys(item.fechas).join(', ');
            arr.push(item);
        }
        return arr;
    }

    function renderPasajeros() {
        var all = aggregatePasajeros(_getLastRows() || []);
        var q = pasState.search.toLowerCase();
        var rows = q ? all.filter(function (r) { return r.viaje.toLowerCase().indexOf(q) >= 0; }) : all;
        sortRows(rows, pasState.sortCol, pasState.sortAsc);

        var $m = $('#modalPasajeros');
        var cols = [
            { key: 'viaje', label: 'Viaje' },
            { key: 'fechasList', label: 'Fechas de salida' },
            { key: 'pasajeros', label: 'Pasajeros' }
        ];
        var thead = '<tr>';
        for (var c = 0; c < cols.length; c++) {
            var cls = c === 2 ? ' class="text-center"' : '';
            thead += '<th data-col="' + cols[c].key + '"' + cls + '>' + cols[c].label + ' ' + arrowHtml(cols[c].key, pasState.sortCol, pasState.sortAsc) + '</th>';
        }
        thead += '</tr>';

        var tbody = '';
        for (var i = 0; i < rows.length; i++) {
            var r = rows[i];
            tbody += '<tr>';
            tbody += '<td><span class="text-truncate d-inline-block" style="max-width:220px" title="' + esc(r.viaje) + '">' + esc(r.viaje) + '</span></td>';
            tbody += '<td class="small text-muted">' + esc(r.fechasList) + '</td>';
            tbody += '<td class="text-center fw-semibold">' + r.pasajeros + '</td>';
            tbody += '</tr>';
        }
        if (!rows.length) tbody = '<tr><td colspan="3" class="text-center text-muted py-3"><i class="bi bi-inbox"></i> Sin resultados</td></tr>';

        $m.find('thead').html(thead);
        $m.find('tbody').html(tbody);
        $m.find('.ranking-modal-count').text(rows.length + ' de ' + all.length);
    }

    /* ── Facturas modal ──────────────────────────────────────── */

    var facState = { sortCol: 'fecha', sortAsc: false, search: '' };

    function aggregateFacturas(rows) {
        var map = {};
        for (var i = 0; i < rows.length; i++) {
            var r = rows[i];
            var fid = r.facturaId;
            if (!fid) continue;
            if (!map[fid]) {
                map[fid] = {
                    facturaId: fid,
                    fecha: r.fecha,
                    cliente: r.clienteFullName || '',
                    viajes: [],
                    viajeNames: {},
                    pasajes: 0
                };
            }
            var g = map[fid];
            g.pasajes += parseInt(r.cantidadPasajesXFactura, 10) || 0;
            var vn = r.viajeDescripcion || '';
            if (vn && !g.viajeNames[vn]) {
                g.viajeNames[vn] = 1;
                g.viajes.push(vn);
            }
        }
        var arr = [];
        for (var k in map) {
            if (!map.hasOwnProperty(k)) continue;
            var item = map[k];
            item.viajeLabel = item.viajes[0] || '-';
            if (item.viajes.length > 1) item.viajeLabel += ' +' + (item.viajes.length - 1);
            arr.push(item);
        }
        return arr;
    }

    function renderFacturas() {
        var all = aggregateFacturas(_getLastRows() || []);
        var q = facState.search.toLowerCase();
        var rows = q ? all.filter(function (r) {
            return r.cliente.toLowerCase().indexOf(q) >= 0 ||
                   r.viajes.join(' ').toLowerCase().indexOf(q) >= 0;
        }) : all;
        sortRows(rows, facState.sortCol, facState.sortAsc);

        var $m = $('#modalFacturas');
        var cols = [
            { key: 'fecha', label: 'Fecha' },
            { key: 'cliente', label: 'Cliente' },
            { key: 'viajeLabel', label: 'Viaje' },
            { key: 'pasajes', label: 'Pasajes' }
        ];
        var thead = '<tr>';
        for (var c = 0; c < cols.length; c++) {
            var cls = c === 3 ? ' class="text-center"' : '';
            thead += '<th data-col="' + cols[c].key + '"' + cls + '>' + cols[c].label + ' ' + arrowHtml(cols[c].key, facState.sortCol, facState.sortAsc) + '</th>';
        }
        thead += '</tr>';

        var tbody = '';
        for (var i = 0; i < rows.length; i++) {
            var r = rows[i];
            tbody += '<tr>';
            tbody += '<td>' + fmtFecha(r.fecha) + '</td>';
            tbody += '<td>' + esc(r.cliente) + '</td>';
            tbody += '<td title="' + esc(r.viajes.join(', ')) + '">' + esc(r.viajeLabel) + '</td>';
            tbody += '<td class="text-center fw-semibold">' + r.pasajes + '</td>';
            tbody += '</tr>';
        }
        if (!rows.length) tbody = '<tr><td colspan="4" class="text-center text-muted py-3"><i class="bi bi-inbox"></i> Sin resultados</td></tr>';

        $m.find('thead').html(thead);
        $m.find('tbody').html(tbody);
        $m.find('.ranking-modal-count').text(rows.length + ' de ' + all.length);
    }

    /* ── Top viajes modal ────────────────────────────────────── */

    var tvState = { sortCol: 'ventas', sortAsc: false, search: '' };

    function renderTopViajes() {
        var aggFn = _D().aggregateViajes || function () { return []; };
        var all = aggFn(_getLastRows() || []);

        var rankMap = {};
        for (var ri = 0; ri < all.length; ri++) { rankMap[all[ri].viaje] = ri; }

        var q = tvState.search.toLowerCase();
        var rows = q ? all.filter(function (r) { return r.viaje.toLowerCase().indexOf(q) >= 0; }) : all;
        sortRows(rows, tvState.sortCol, tvState.sortAsc);

        var $m = $('#modalTopViajes');
        var cols = [
            { key: '_pos', label: '#' },
            { key: 'viaje', label: 'Viaje' },
            { key: 'ventas', label: 'Ventas' },
            { key: 'pasajeros', label: 'Pasajeros' },
            { key: 'cantClientes', label: 'Clientes' }
        ];
        var thead = '<tr>';
        for (var c = 0; c < cols.length; c++) {
            var cls = (c === 0 || c >= 2) ? ' class="text-center"' : '';
            thead += '<th data-col="' + cols[c].key + '"' + cls + '>' + cols[c].label + ' ' + arrowHtml(cols[c].key, tvState.sortCol, tvState.sortAsc) + '</th>';
        }
        thead += '</tr>';

        var marks = ['\uD83E\uDD47', '\uD83E\uDD48', '\uD83E\uDD49'];
        var tbody = '';
        for (var i = 0; i < rows.length; i++) {
            var r = rows[i];
            var originalPos = rankMap.hasOwnProperty(r.viaje) ? rankMap[r.viaje] : i;
            var mark = marks[originalPos] || '';
            tbody += '<tr>';
            tbody += '<td class="text-center">' + (mark ? '<span class="fs-5">' + mark + '</span>' : (originalPos + 1)) + '</td>';
            tbody += '<td><span class="text-truncate d-inline-block" style="max-width:240px" title="' + esc(r.viaje) + '">' + esc(r.viaje) + '</span></td>';
            tbody += '<td class="text-center fw-semibold">' + r.ventas + '</td>';
            tbody += '<td class="text-center fw-semibold">' + r.pasajeros + '</td>';
            tbody += '<td class="text-center fw-semibold">' + r.cantClientes + '</td>';
            tbody += '</tr>';
        }
        if (!rows.length) tbody = '<tr><td colspan="5" class="text-center text-muted py-3"><i class="bi bi-inbox"></i> Sin resultados</td></tr>';

        $m.find('thead').html(thead);
        $m.find('tbody').html(tbody);
        $m.find('.ranking-modal-count').text(rows.length + ' de ' + all.length);
    }

    /* ── Tabla de render por modal ────────────────────────────── */

    var MODAL_CONFIG = {
        modalRegistros:  { render: renderRegistros,  state: regState },
        modalClientes:   { render: renderClientes,   state: cliState },
        modalPasajeros:  { render: renderPasajeros,   state: pasState },
        modalFacturas:   { render: renderFacturas,    state: facState },
        modalTopViajes:  { render: renderTopViajes,   state: tvState }
    };

    /* ── Event wiring ────────────────────────────────────────── */

    function init(cfg) {
        _getLastRows = cfg.getLastRows;

        /* Card click → open modal (render happens on show.bs.modal) */
        $('.ranking-card-kpi[data-modal]').on('click', function () {
            var id = $(this).attr('data-modal');
            if (!id || $(this).hasClass('card-sin-datos')) return;
            bootstrap.Modal.getOrCreateInstance(document.getElementById(id)).show();
        }).on('keydown', function (e) {
            if (e.key === 'Enter' || e.key === ' ') {
                e.preventDefault();
                $(this).trigger('click');
            }
        });

        /* Reset search on open */
        $.each(MODAL_CONFIG, function (id) {
            $('#' + id).on('show.bs.modal', function () {
                var conf = MODAL_CONFIG[id];
                $(this).find('.ranking-modal-search').val('');
                conf.state.search = '';
                conf.render();
            });
        });

        /* Search with debounce */
        $.each(MODAL_CONFIG, function (id) {
            $('#' + id).on('input', '.ranking-modal-search', function () {
                var val = $(this).val();
                var conf = MODAL_CONFIG[id];
                clearTimeout(_debounceTimers[id]);
                _debounceTimers[id] = setTimeout(function () {
                    conf.state.search = val;
                    conf.render();
                }, 250);
            });
        });

        /* Column sort */
        $.each(MODAL_CONFIG, function (id) {
            $('#' + id).on('click', 'th[data-col]', function () {
                var col = $(this).attr('data-col');
                if (col === '_pos') return;
                var conf = MODAL_CONFIG[id];
                if (conf.state.sortCol === col) conf.state.sortAsc = !conf.state.sortAsc;
                else { conf.state.sortCol = col; conf.state.sortAsc = true; }
                conf.render();
            });
        });
    }

    window.MatReportes.RankingModals = { init: init };

})(window);
