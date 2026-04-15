/**
 * Top 3 Destinos + Modal "Lista completa" — Reporte de Ventas.
 * Requiere: jQuery, Bootstrap 5
 * Nota: el ranking de destinos se ordena por cantidad de ventas (filas), no por monto.
 * El "Monto total" suma ambas monedas sin distinguir — limitación conocida, documentada aquí.
 */
(function (window, $) {
    'use strict';

    window.MatReportes = window.MatReportes || {};

    var _getLastRows;
    var _currentAgg = [];
    var _sortCol = 'ventas';
    var _sortAsc = false;
    var _searchTerm = '';
    var _debounceTimer;

    var moneyFmt = new Intl.NumberFormat('es-AR', { minimumFractionDigits: 0, maximumFractionDigits: 2 });

    function _pad2(n) { return n < 10 ? '0' + n : String(n); }

    function _parseMoneyCell(v) {
        if (v == null || v === '') return 0;
        if (typeof v === 'number') return isNaN(v) ? 0 : v;
        var s = String(v).trim().replace(/\s/g, '');
        var n = parseFloat(s.replace(/\./g, '').replace(',', '.'));
        return isNaN(n) ? 0 : n;
    }

    function _esc(s) {
        if (!s) return '';
        return String(s)
            .replace(/&/g, '&amp;').replace(/</g, '&lt;')
            .replace(/>/g, '&gt;').replace(/"/g, '&quot;');
    }

    /* ── Agregación ──────────────────────────────────────────────── */

    function aggregateByViaje(rows) {
        var map = {};
        rows.forEach(function (r) {
            var id = r.viajeId != null ? String(r.viajeId) : '__sin_id__';
            if (!map[id]) {
                map[id] = {
                    viajeId: r.viajeId,
                    descripcion: r.viajeDescripcion || '(sin descripción)',
                    ventas: 0,
                    total: 0
                };
            }
            var e = map[id];
            e.ventas++;
            e.total += _parseMoneyCell(r.totalFactura);
        });
        /* Ordenar por cantidad de ventas desc */
        return Object.keys(map).map(function (k) { return map[k]; })
            .sort(function (a, b) { return b.ventas - a.ventas; });
    }

    /* ── Cards Top 3 ─────────────────────────────────────────────── */

    var GEO_ICONS = [
        { icon: 'bi-geo-alt-fill', cls: 'text-warning' },
        { icon: 'bi-geo-alt-fill', cls: 'text-secondary' },
        { icon: 'bi-geo-alt-fill', cls: 'text-danger' }
    ];

    function _renderCards(agg) {
        var top3 = agg.slice(0, 3);
        var html = '';
        top3.forEach(function (v, i) {
            var ico = GEO_ICONS[i] || GEO_ICONS[2];
            var promedio = v.ventas > 0 ? v.total / v.ventas : 0;

            html += '<div class="col"><div class="card shadow-sm border-0 h-100 card-top-ranking"><div class="card-body">';
            html += '<div class="d-flex align-items-center gap-2 mb-2">';
            html += '<i class="bi ' + ico.icon + ' fs-4 ' + ico.cls + '"></i>';
            html += '<span class="fw-bold">#' + (i + 1) + '</span>';
            html += '</div>';
            html += '<p class="fw-semibold mb-1 text-truncate" title="' + _esc(v.descripcion) + '">' + _esc(v.descripcion) + '</p>';
            html += '<p class="small text-secondary mb-2">' + v.ventas + ' venta' + (v.ventas !== 1 ? 's' : '') + '</p>';
            html += '<div class="d-flex align-items-center gap-2 py-1 border-bottom border-light">';
            html += '<span class="small text-secondary">Total</span>';
            html += '<span class="fw-semibold ms-auto">$ ' + moneyFmt.format(v.total) + '</span>';
            html += '</div>';
            html += '<p class="small text-muted mt-2 mb-0">Promedio: $ ' + moneyFmt.format(promedio) + '</p>';
            html += '</div></div></div>';
        });
        $('#topDestinosCards').html(html);
    }

    /* ── Modal ───────────────────────────────────────────────────── */

    var MODAL_COLS = [
        { key: 'descripcion', label: 'Destino',       numeric: false },
        { key: 'ventas',      label: 'Total ventas',  numeric: true },
        { key: 'total',       label: 'Monto total',   numeric: true },
        { key: 'promedio',    label: 'Promedio',       numeric: true }
    ];

    function _applySearch(agg) {
        var q = _searchTerm.toLowerCase().trim();
        if (!q) return agg;
        return agg.filter(function (v) {
            return v.descripcion.toLowerCase().indexOf(q) !== -1;
        });
    }

    function _sortAgg(agg) {
        var col = _sortCol, asc = _sortAsc;
        return agg.slice().sort(function (a, b) {
            var va = col === 'descripcion' ? String(a.descripcion || '').toLowerCase()
                   : col === 'promedio' ? (a.ventas > 0 ? a.total / a.ventas : 0)
                   : (a[col] || 0);
            var vb = col === 'descripcion' ? String(b.descripcion || '').toLowerCase()
                   : col === 'promedio' ? (b.ventas > 0 ? b.total / b.ventas : 0)
                   : (b[col] || 0);
            if (va < vb) return asc ? -1 : 1;
            if (va > vb) return asc ? 1 : -1;
            return 0;
        });
    }

    function _renderModalTable(filtered) {
        $('#modalDestinosContador').text('Mostrando ' + filtered.length + ' de ' + _currentAgg.length + ' destinos');

        var html = '<table class="table table-sm table-bordered table-hover align-middle mb-0" style="min-width:500px">';
        html += '<thead class="table-light sticky-top"><tr>';
        html += '<th class="small fw-semibold">#</th>';
        MODAL_COLS.forEach(function (c) {
            var arrow = (_sortCol === c.key) ? (' <i class="bi bi-arrow-' + (_sortAsc ? 'up' : 'down') + '"></i>') : '';
            html += '<th class="js-dest-sort small fw-semibold" data-col="' + c.key +
                    '" style="cursor:pointer;white-space:nowrap">' + c.label + arrow + '</th>';
        });
        html += '</tr></thead><tbody>';

        if (filtered.length === 0) {
            html += '<tr><td colspan="' + (MODAL_COLS.length + 1) + '" class="text-center text-secondary py-4">' +
                    '<i class="bi bi-geo-alt me-1"></i>Sin resultados.</td></tr>';
        } else {
            filtered.forEach(function (v, i) {
                var promedio = v.ventas > 0 ? v.total / v.ventas : 0;
                html += '<tr>';
                html += '<td class="small text-secondary">' + (i + 1) + '</td>';
                html += '<td class="small">' + _esc(v.descripcion) + '</td>';
                html += '<td class="small text-end">' + v.ventas + '</td>';
                html += '<td class="small text-end">$ ' + moneyFmt.format(v.total) + '</td>';
                html += '<td class="small text-end">$ ' + moneyFmt.format(promedio) + '</td>';
                html += '</tr>';
            });
        }
        html += '</tbody></table>';
        $('#modalDestinosTablaWrap').html(html);
    }

    function _renderModal() {
        _renderModalTable(_sortAgg(_applySearch(_currentAgg)));
    }

    function _exportCsv() {
        var headers = ['Destino', 'Total ventas', 'Monto total', 'Promedio'];
        var csvRows = [headers.join(',')];
        _currentAgg.forEach(function (v) {
            var promedio = v.ventas > 0 ? v.total / v.ventas : 0;
            csvRows.push([
                '"' + v.descripcion.replace(/"/g, '""') + '"',
                v.ventas,
                v.total.toFixed(2),
                promedio.toFixed(2)
            ].join(','));
        });
        var blob = new Blob(['\uFEFF' + csvRows.join('\r\n')], { type: 'text/csv;charset=utf-8;' });
        var d = new Date();
        var name = 'destinos-' + d.getFullYear() + '-' + _pad2(d.getMonth() + 1) + '-' + _pad2(d.getDate()) + '.csv';
        var url = URL.createObjectURL(blob);
        var a = document.createElement('a');
        a.href = url; a.download = name;
        document.body.appendChild(a); a.click(); a.remove();
        URL.revokeObjectURL(url);
    }

    /* ── Init ────────────────────────────────────────────────────── */

    function init(opts) {
        _getLastRows = opts.getLastRows;

        $('#btnVerTodosDestinos').on('click', function () {
            _currentAgg = aggregateByViaje(_getLastRows());
            _searchTerm = '';
            _sortCol = 'ventas';
            _sortAsc = false;
            $('#modalDestinosBuscar').val('');
            _renderModal();
            bootstrap.Modal.getOrCreateInstance(document.getElementById('modalListadoDestinos')).show();
        });

        $('#modalDestinosBuscar').on('input', function () {
            var val = $(this).val();
            clearTimeout(_debounceTimer);
            _debounceTimer = setTimeout(function () {
                _searchTerm = val;
                _renderModal();
            }, 250);
        });

        $('#modalDestinosTablaWrap').on('click', '.js-dest-sort', function () {
            var col = $(this).data('col');
            _sortAsc = (_sortCol === col) ? !_sortAsc : false;
            _sortCol = col;
            _renderModal();
        });

        $('#btnCsvDestinos').on('click', _exportCsv);
    }

    function render(rows) {
        var agg = aggregateByViaje(rows);
        if (agg.length === 0) { $('#topDestinos').addClass('d-none'); return; }
        _renderCards(agg);
        $('#topDestinos').removeClass('d-none');
    }

    function hide() {
        $('#topDestinos').addClass('d-none');
    }

    window.MatReportes.Destinos = { init: init, render: render, hide: hide };

})(window, jQuery);
