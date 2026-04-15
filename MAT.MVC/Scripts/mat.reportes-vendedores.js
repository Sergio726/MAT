/**
 * Top 3 Vendedores + Modal "Lista completa" — Reporte de Ventas.
 * Requiere: jQuery, Bootstrap 5
 */
(function (window, $) {
    'use strict';

    window.MatReportes = window.MatReportes || {};

    var _getLastRows;
    var _currentAgg = [];
    var _sortCol = 'total';
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

    function aggregateByVendedor(rows) {
        var map = {};
        rows.forEach(function (r) {
            var id = r.vendedorId != null ? String(r.vendedorId) : '__sin_id__';
            if (!map[id]) {
                map[id] = {
                    vendedorId: r.vendedorId,
                    nombre: r.vendedorFullName || '(sin nombre)',
                    ventas: 0, total: 0,
                    arsTotal: 0, usdTotal: 0,
                    arsSaldo: 0, usdSaldo: 0
                };
            }
            var e = map[id];
            var tf = _parseMoneyCell(r.totalFactura);
            var sd = _parseMoneyCell(r.saldo);
            var m  = String(r.monedaTipo != null ? r.monedaTipo : '').trim();
            e.ventas++;
            e.total += tf;
            if (m === '1') { e.arsTotal += tf; e.arsSaldo += sd; }
            else if (m === '3') { e.usdTotal += tf; e.usdSaldo += sd; }
        });
        return Object.keys(map).map(function (k) { return map[k]; })
            .sort(function (a, b) { return b.total - a.total; });
    }

    /* ── Cards Top 3 ─────────────────────────────────────────────── */

    /* Unicode: el CSS de Bootstrap Icons del proyecto no define bi-medal (el 2º quedaba sin ícono). */
    var RANK_MARKS = ['🥇', '🥈', '🥉'];

    function _renderCards(agg) {
        var top3 = agg.slice(0, 3);
        var html = '';
        top3.forEach(function (v, i) {
            var haySaldo = v.arsSaldo > 0 || v.usdSaldo > 0;
            var mark = RANK_MARKS[i] || RANK_MARKS[2];
            var cardCls = 'card shadow-sm border-0 h-100 card-top-ranking' +
                (i === 0 ? ' card-top-ranking-first' : '');

            html += '<div class="col"><div class="' + cardCls + '"><div class="card-body">';
            html += '<div class="d-flex align-items-center gap-2 mb-2">';
            html += '<span class="mat-rank-emoji fs-3 lh-1" role="img" aria-label="Puesto ' + (i + 1) + '">' +
                mark + '</span>';
            html += '<span class="fw-bold">#' + (i + 1) + '</span>';
            html += '</div>';
            html += '<p class="fw-semibold mb-1 text-truncate" title="' + _esc(v.nombre) + '">' + _esc(v.nombre) + '</p>';
            html += '<p class="small text-secondary mb-2">' + v.ventas + ' venta' + (v.ventas !== 1 ? 's' : '') + '</p>';

            if (v.arsTotal > 0) {
                html += '<div class="d-flex align-items-center gap-2 py-1 border-bottom border-light">';
                html += '<span class="badge rounded-pill bg-success-subtle text-success">$ARS</span>';
                html += '<span class="fw-semibold ms-auto">$ ' + moneyFmt.format(v.arsTotal) + '</span></div>';
            }
            if (v.usdTotal > 0) {
                html += '<div class="d-flex align-items-center gap-2 py-1 border-bottom border-light">';
                html += '<span class="badge rounded-pill bg-info-subtle text-info">U$S</span>';
                html += '<span class="fw-semibold ms-auto">$ ' + moneyFmt.format(v.usdTotal) + '</span></div>';
            }

            html += '<div class="mt-2">';
            if (haySaldo) {
                if (v.arsSaldo > 0) {
                    html += '<div class="small text-danger"><span class="badge rounded-pill bg-success-subtle text-success me-1">$ARS</span>';
                    html += 'Saldo: $ ' + moneyFmt.format(v.arsSaldo) + '</div>';
                }
                if (v.usdSaldo > 0) {
                    html += '<div class="small text-danger"><span class="badge rounded-pill bg-info-subtle text-info me-1">U$S</span>';
                    html += 'Saldo: $ ' + moneyFmt.format(v.usdSaldo) + '</div>';
                }
            } else {
                html += '<span class="small text-muted">Sin saldo pendiente</span>';
            }
            html += '</div>';
            html += '</div></div></div>';
        });
        $('#topVendedoresCards').html(html);
    }

    /* ── Modal ───────────────────────────────────────────────────── */

    var MODAL_COLS = [
        { key: 'nombre',   label: 'Vendedor',     numeric: false },
        { key: 'ventas',   label: 'Total ventas', numeric: true },
        { key: 'arsTotal', label: 'Total $ARS',   numeric: true },
        { key: 'usdTotal', label: 'Total U$S',    numeric: true },
        { key: 'arsSaldo', label: 'Saldo $ARS',   numeric: true },
        { key: 'usdSaldo', label: 'Saldo U$S',    numeric: true },
        { key: 'promedio', label: 'Promedio',      numeric: true }
    ];

    function _applySearch(agg) {
        var q = _searchTerm.toLowerCase().trim();
        if (!q) return agg;
        return agg.filter(function (v) {
            return v.nombre.toLowerCase().indexOf(q) !== -1;
        });
    }

    function _sortAgg(agg) {
        var col = _sortCol, asc = _sortAsc;
        return agg.slice().sort(function (a, b) {
            var va = col === 'nombre' ? String(a.nombre || '').toLowerCase()
                   : col === 'promedio' ? (a.ventas > 0 ? a.total / a.ventas : 0)
                   : (a[col] || 0);
            var vb = col === 'nombre' ? String(b.nombre || '').toLowerCase()
                   : col === 'promedio' ? (b.ventas > 0 ? b.total / b.ventas : 0)
                   : (b[col] || 0);
            if (va < vb) return asc ? -1 : 1;
            if (va > vb) return asc ? 1 : -1;
            return 0;
        });
    }

    function _renderModalTable(filtered) {
        var all = _currentAgg;
        $('#modalVendedoresContador').text('Mostrando ' + filtered.length + ' de ' + all.length + ' vendedores');

        var html = '<table class="table table-sm table-bordered table-hover align-middle mb-0" style="min-width:650px">';
        html += '<thead class="table-light sticky-top"><tr>';
        html += '<th class="small fw-semibold">#</th>';
        MODAL_COLS.forEach(function (c) {
            var arrow = (_sortCol === c.key) ? (' <i class="bi bi-arrow-' + (_sortAsc ? 'up' : 'down') + '"></i>') : '';
            html += '<th class="js-vend-sort small fw-semibold" data-col="' + c.key +
                    '" style="cursor:pointer;white-space:nowrap">' + c.label + arrow + '</th>';
        });
        html += '</tr></thead><tbody>';

        if (filtered.length === 0) {
            html += '<tr><td colspan="' + (MODAL_COLS.length + 1) + '" class="text-center text-secondary py-4">' +
                    '<i class="bi bi-people me-1"></i>Sin resultados.</td></tr>';
        } else {
            filtered.forEach(function (v, i) {
                var promedio = v.ventas > 0 ? v.total / v.ventas : 0;
                html += '<tr>';
                html += '<td class="small text-secondary">' + (i + 1) + '</td>';
                html += '<td class="small">' + _esc(v.nombre) + '</td>';
                html += '<td class="small text-end">' + v.ventas + '</td>';
                html += '<td class="small text-end">$ ' + moneyFmt.format(v.arsTotal) + '</td>';
                html += '<td class="small text-end">$ ' + moneyFmt.format(v.usdTotal) + '</td>';
                html += '<td class="small text-end ' + (v.arsSaldo > 0 ? 'text-danger' : 'text-muted') + '">' +
                        (v.arsSaldo > 0 ? '$ ' + moneyFmt.format(v.arsSaldo) : 'Sin saldo') + '</td>';
                html += '<td class="small text-end ' + (v.usdSaldo > 0 ? 'text-danger' : 'text-muted') + '">' +
                        (v.usdSaldo > 0 ? '$ ' + moneyFmt.format(v.usdSaldo) : 'Sin saldo') + '</td>';
                html += '<td class="small text-end">$ ' + moneyFmt.format(promedio) + '</td>';
                html += '</tr>';
            });
        }
        html += '</tbody></table>';
        $('#modalVendedoresTablaWrap').html(html);
    }

    function _renderModal() {
        _renderModalTable(_sortAgg(_applySearch(_currentAgg)));
    }

    function _exportCsv() {
        var headers = ['Vendedor', 'Total ventas', 'Total ARS', 'Total USD', 'Saldo ARS', 'Saldo USD', 'Promedio'];
        var csvRows = [headers.join(',')];
        _currentAgg.forEach(function (v) {
            var promedio = v.ventas > 0 ? v.total / v.ventas : 0;
            csvRows.push([
                '"' + v.nombre.replace(/"/g, '""') + '"',
                v.ventas,
                v.arsTotal.toFixed(2), v.usdTotal.toFixed(2),
                v.arsSaldo.toFixed(2), v.usdSaldo.toFixed(2),
                promedio.toFixed(2)
            ].join(','));
        });
        var blob = new Blob(['\uFEFF' + csvRows.join('\r\n')], { type: 'text/csv;charset=utf-8;' });
        var d = new Date();
        var name = 'vendedores-' + d.getFullYear() + '-' + _pad2(d.getMonth() + 1) + '-' + _pad2(d.getDate()) + '.csv';
        var url = URL.createObjectURL(blob);
        var a = document.createElement('a');
        a.href = url; a.download = name;
        document.body.appendChild(a); a.click(); a.remove();
        URL.revokeObjectURL(url);
    }

    /* ── Init ────────────────────────────────────────────────────── */

    function init(opts) {
        _getLastRows = opts.getLastRows;

        $('#btnVerTodosVendedores').on('click', function () {
            _currentAgg = aggregateByVendedor(_getLastRows());
            _searchTerm = '';
            _sortCol = 'total';
            _sortAsc = false;
            $('#modalVendedoresBuscar').val('');
            _renderModal();
            bootstrap.Modal.getOrCreateInstance(document.getElementById('modalListadoVendedores')).show();
        });

        $('#modalVendedoresBuscar').on('input', function () {
            var val = $(this).val();
            clearTimeout(_debounceTimer);
            _debounceTimer = setTimeout(function () {
                _searchTerm = val;
                _renderModal();
            }, 250);
        });

        $('#modalVendedoresTablaWrap').on('click', '.js-vend-sort', function () {
            var col = $(this).data('col');
            _sortAsc = (_sortCol === col) ? !_sortAsc : false;
            _sortCol = col;
            _renderModal();
        });

        $('#btnCsvVendedores').on('click', _exportCsv);
    }

    function render(rows) {
        var agg = aggregateByVendedor(rows);
        if (agg.length === 0) { $('#topVendedores').addClass('d-none'); return; }
        _renderCards(agg);
        $('#topVendedores').removeClass('d-none');
    }

    function hide() {
        $('#topVendedores').addClass('d-none');
    }

    window.MatReportes.Vendedores = { init: init, render: render, hide: hide };

})(window, jQuery);
