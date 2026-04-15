/**
 * Modal "Listado de Facturas" — Reporte de Ventas.
 * Requiere: jQuery, Bootstrap 5, mat.reportes-excel-export.js
 */
(function (window, $) {
    'use strict';

    window.MatReportes = window.MatReportes || {};

    var _getLastRows, _getQueryLabel, _getQueryParams, _urlExcel, _showErr;
    var _filterState = { texto: '', estado: '', moneda: '', desde: '', hasta: '' };
    var _sortCol = 'facturaFecha';
    var _sortAsc = false; /* más reciente primero por defecto */
    var _expandedIds = {};

    var moneyFmt = new Intl.NumberFormat('es-AR', { minimumFractionDigits: 0, maximumFractionDigits: 2 });

    /* ── Helpers ─────────────────────────────────────────────────── */

    function _debounce(fn, ms) {
        var t;
        return function () {
            var ctx = this, args = arguments;
            clearTimeout(t);
            t = setTimeout(function () { fn.apply(ctx, args); }, ms);
        };
    }

    function _parseDate(s) {
        if (!s) return null;
        /* YYYY-MM-DD (input[type=date]): parsear como local para evitar offset UTC */
        var iso = /^(\d{4})-(\d{2})-(\d{2})/.exec(s);
        if (iso) return new Date(parseInt(iso[1], 10), parseInt(iso[2], 10) - 1, parseInt(iso[3], 10));
        var m = /\/Date\((\d+)/.exec(s);
        if (m) return new Date(parseInt(m[1], 10));
        var parts = s.split('/');
        if (parts.length === 3) {
            var day = parseInt(parts[0], 10), month = parseInt(parts[1], 10) - 1, year = parseInt(parts[2], 10);
            /* jQuery UI dateFormat dd/mm/yy → año 2 dígitos; Date(y<100) usa 1900+y en JS */
            if (!isNaN(year) && year >= 0 && year < 100) year += 2000;
            return new Date(year, month, day);
        }
        return new Date(s);
    }

    function _formatDate(s) {
        var d = _parseDate(s);
        if (!d || isNaN(d.getTime())) return s || '';
        return (d.getDate() < 10 ? '0' : '') + d.getDate() + '/' +
               ((d.getMonth() + 1) < 10 ? '0' : '') + (d.getMonth() + 1) + '/' + d.getFullYear();
    }

    function _esc(s) {
        if (!s) return '';
        return String(s)
            .replace(/&/g, '&amp;')
            .replace(/</g, '&lt;')
            .replace(/>/g, '&gt;')
            .replace(/"/g, '&quot;');
    }

    function _parseMoneyCell(v) {
        if (v == null || v === '') return 0;
        if (typeof v === 'number') return isNaN(v) ? 0 : v;
        var s = String(v).trim().replace(/\s/g, '');
        var n = parseFloat(s.replace(/\./g, '').replace(',', '.'));
        return isNaN(n) ? 0 : n;
    }

    function _renderMonedaBadge(tipo) {
        if (tipo === '1') return '<span class="badge rounded-pill bg-success-subtle text-success">$ARS</span>';
        if (tipo === '3') return '<span class="badge rounded-pill bg-info-subtle text-info">U$S</span>';
        return '<span class="badge rounded-pill bg-secondary-subtle text-secondary">' + (tipo || '-') + '</span>';
    }

    function _renderEstadoBadge(estado) {
        if (!estado) return '<span class="badge bg-secondary-subtle text-secondary">-</span>';
        var lower = estado.toLowerCase();
        var cls = 'secondary';
        if (lower.indexOf('pag') !== -1) cls = 'success';
        else if (lower.indexOf('pend') !== -1) cls = 'warning';
        else if (lower.indexOf('cancel') !== -1) cls = 'danger';
        return '<span class="badge bg-' + cls + '-subtle text-' + cls + '">' + _esc(estado) + '</span>';
    }

    /* ── Filtros y orden ─────────────────────────────────────────── */

    function _applyFilters(rows) {
        var q = (_filterState.texto || '').toLowerCase().trim();
        var estado = _filterState.estado;
        var moneda = _filterState.moneda;
        var desde = _filterState.desde ? _parseDate(_filterState.desde) : null;
        var hasta = _filterState.hasta ? _parseDate(_filterState.hasta) : null;
        if (hasta) { hasta = new Date(hasta.getTime()); hasta.setHours(23, 59, 59, 999); }

        return rows.filter(function (r) {
            if (q) {
                var haystack = [
                    r.facturaId || '', r.clienteFullName || '',
                    r.vendedorFullName || '', r.viajeDescripcion || '', r.facturaEstado || ''
                ].join(' ').toLowerCase();
                if (haystack.indexOf(q) === -1) return false;
            }
            if (estado && r.facturaEstado !== estado) return false;
            if (moneda && r.monedaTipo !== moneda) return false;
            if (desde || hasta) {
                var fd = _parseDate(r.facturaFecha);
                if (fd) {
                    if (desde && fd < desde) return false;
                    if (hasta && fd > hasta) return false;
                }
            }
            return true;
        });
    }

    function _sortRows(rows) {
        var col = _sortCol, asc = _sortAsc;
        return rows.slice().sort(function (a, b) {
            var va = a[col], vb = b[col];
            if (col === 'facturaFecha') {
                var da = _parseDate(va), db = _parseDate(vb);
                va = da ? da.getTime() : 0;
                vb = db ? db.getTime() : 0;
            } else if (col === 'totalFactura' || col === 'montoPagado' || col === 'saldo') {
                va = _parseMoneyCell(va);
                vb = _parseMoneyCell(vb);
            } else {
                va = String(va || '').toLowerCase();
                vb = String(vb || '').toLowerCase();
            }
            if (va < vb) return asc ? -1 : 1;
            if (va > vb) return asc ? 1 : -1;
            return 0;
        });
    }

    /* ── Renderizado ─────────────────────────────────────────────── */

    function _renderTotalesModal(filtered) {
        var facturado = { '1': 0, '3': 0, _otra: 0 };
        var cobrado   = { '1': 0, '3': 0, _otra: 0 };
        var saldo     = { '1': 0, '3': 0, _otra: 0 };
        filtered.forEach(function (r) {
            var m = String(r.monedaTipo != null ? r.monedaTipo : '').trim();
            if (m !== '1' && m !== '3') m = '_otra';
            facturado[m] = (facturado[m] || 0) + _parseMoneyCell(r.totalFactura);
            cobrado[m]   = (cobrado[m]   || 0) + _parseMoneyCell(r.montoPagado);
            saldo[m]     = (saldo[m]     || 0) + _parseMoneyCell(r.saldo);
        });

        function fmtObj(obj) {
            var parts = [];
            [['1', '$ARS', 'success'], ['3', 'U$S', 'info']].forEach(function (x) {
                parts.push(
                    '<span class="badge rounded-pill bg-' + x[2] + '-subtle text-' + x[2] + ' me-1">' + x[1] + '</span>' +
                    '<strong>$ ' + moneyFmt.format(obj[x[0]] || 0) + '</strong>'
                );
            });
            if (obj._otra) {
                parts.push('<span class="badge rounded-pill bg-secondary-subtle text-secondary me-1">Otra</span>' +
                    '<strong>$ ' + moneyFmt.format(obj._otra) + '</strong>');
            }
            return parts.join(' <span class="text-secondary mx-1">|</span> ');
        }

        $('#modalTotFact').html(fmtObj(facturado));
        $('#modalTotCob').html(fmtObj(cobrado));
        var haySaldo = (saldo['1'] || 0) > 0 || (saldo['3'] || 0) > 0 || (saldo._otra || 0) > 0;
        $('#modalTotSaldo').html('<span class="' + (haySaldo ? 'text-danger' : '') + '">' + fmtObj(saldo) + '</span>');
    }

    var COLS = [
        { key: 'facturaFecha',     label: 'Fecha' },
        { key: 'clienteFullName',  label: 'Cliente' },
        { key: 'vendedorFullName', label: 'Vendedor' },
        { key: 'facturaEstado',    label: 'Estado' },
        { key: 'monedaTipo',       label: 'Mon.' },
        { key: 'totalFactura',     label: 'Total' },
        { key: 'montoPagado',      label: 'Pagado' },
        { key: 'saldo',            label: 'Saldo' }
    ];

    function _renderTable(filtered) {
        var html = '<table class="table table-sm table-bordered table-hover align-middle mb-0" style="min-width:700px">';
        html += '<thead class="table-light"><tr>';
        COLS.forEach(function (c) {
            var arrow = (_sortCol === c.key)
                ? (' <i class="bi bi-arrow-' + (_sortAsc ? 'up' : 'down') + '"></i>')
                : '';
            html += '<th class="js-sort-col small fw-semibold" data-col="' + c.key +
                    '" style="cursor:pointer;white-space:nowrap">' + c.label + arrow + '</th>';
        });
        html += '<th class="small fw-semibold">Acciones</th></tr></thead><tbody>';

        if (filtered.length === 0) {
            html += '<tr><td colspan="' + (COLS.length + 1) + '" class="text-center text-secondary py-4">' +
                    '<i class="bi bi-search me-1"></i>Sin resultados — intentá limpiar los filtros.</td></tr>';
        } else {
            filtered.forEach(function (r) {
                var saldoVal = _parseMoneyCell(r.saldo);
                var saldoCls = saldoVal > 0 ? ' text-danger fw-semibold' : '';
                var fid = r.facturaId || '';
                var isExp = !!_expandedIds[fid];

                html += '<tr data-factura-id="' + fid + '">';
                html += '<td class="small text-nowrap">' + _formatDate(r.facturaFecha) + '</td>';
                html += '<td class="small">' + (_esc(r.clienteFullName) || '-') + '</td>';
                html += '<td class="small">' + (_esc(r.vendedorFullName) || '-') + '</td>';
                html += '<td class="small">' + _renderEstadoBadge(r.facturaEstado) + '</td>';
                html += '<td class="small">' + _renderMonedaBadge(r.monedaTipo) + '</td>';
                html += '<td class="small text-end text-nowrap">$ ' + moneyFmt.format(_parseMoneyCell(r.totalFactura)) + '</td>';
                html += '<td class="small text-end text-nowrap">$ ' + moneyFmt.format(_parseMoneyCell(r.montoPagado)) + '</td>';
                html += '<td class="small text-end text-nowrap' + saldoCls + '">$ ' + moneyFmt.format(saldoVal) + '</td>';
                html += '<td class="text-nowrap">';
                html += '<button class="btn btn-outline-secondary btn-sm py-0 px-1 me-1 js-copiar-factura" ' +
                        'title="Copiar ID de factura" aria-label="Copiar ID" data-factura-id="' + fid + '">' +
                        '<i class="bi bi-clipboard"></i></button>';
                html += '<button class="btn btn-outline-secondary btn-sm py-0 px-1 js-expand-fila" ' +
                        'title="' + (isExp ? 'Contraer' : 'Expandir') + ' detalle" ' +
                        'aria-label="' + (isExp ? 'Contraer' : 'Expandir') + ' detalle">' +
                        '<i class="bi bi-chevron-' + (isExp ? 'up' : 'down') + '"></i></button>';
                html += '</td></tr>';

                if (isExp) {
                    html += '<tr class="table-light"><td colspan="' + (COLS.length + 1) + '">';
                    html += '<div class="small px-2 py-1"><div class="row g-2">';
                    html += '<div class="col-auto"><span class="text-secondary">Viaje:</span> <strong>' + (_esc(r.viajeDescripcion) || '-') + '</strong></div>';
                    html += '<div class="col-auto"><span class="text-secondary">Fecha salida:</span> <strong>' + _formatDate(r.fechaSalida) + '</strong></div>';
                    html += '<div class="col-auto"><span class="text-secondary">Butacas:</span> <strong>' + (r.cantidadButacas != null ? _esc(String(r.cantidadButacas)) : '-') + '</strong></div>';
                    html += '<div class="col-12"><span class="text-secondary">Factura ID:</span> <code class="small">' + _esc(fid || '-') + '</code></div>';
                    html += '</div></div></td></tr>';
                }
            });
        }
        html += '</tbody></table>';
        $('#modalFacturasTablaWrap').html(html);
    }

    function _render() {
        var all = _getLastRows();
        var filtered = _sortRows(_applyFilters(all));
        $('#modalFacturasContador').text('Mostrando ' + filtered.length + ' de ' + all.length + ' facturas');
        _renderTotalesModal(filtered);
        _renderTable(filtered);
    }

    function _populateEstadoDropdown(rows) {
        var estados = {};
        rows.forEach(function (r) { if (r.facturaEstado) estados[r.facturaEstado] = true; });
        var $sel = $('#modalFacturasEstado');
        var current = $sel.val();
        $sel.find('option:not([value=""])').remove();
        Object.keys(estados).sort().forEach(function (e) {
            $sel.append($('<option>').val(e).text(e));
        });
        if (current) $sel.val(current);
    }

    function _limpiarFiltros() {
        _filterState = { texto: '', estado: '', moneda: '', desde: '', hasta: '' };
        $('#modalFacturasTexto').val('');
        $('#modalFacturasEstado').val('');
        $('#modalFacturasMoneda').val('');
        $('#modalFacturasDesde, #modalFacturasHasta').each(function () {
            var $el = $(this);
            if ($el.hasClass('hasDatepicker') && $.fn.datepicker) {
                try {
                    $el.datepicker('setDate', null);
                } catch (ex) {
                    $el.val('');
                }
            } else {
                $el.val('');
            }
        });
    }

    /* ── Init ────────────────────────────────────────────────────── */

    function init(opts) {
        _getLastRows    = opts.getLastRows;
        _getQueryLabel  = opts.getQueryLabel;
        _getQueryParams = opts.getQueryParams;
        _urlExcel       = opts.urlExcel;
        _showErr        = opts.showErr;

        /* Card clic → abrir modal */
        $('#cardFacturado').on('click', function () {
            if (!_getLastRows().length) return;
            bootstrap.Modal.getOrCreateInstance(document.getElementById('modalListadoFacturas')).show();
        }).on('keydown', function (e) {
            if ((e.key === 'Enter' || e.key === ' ') && _getLastRows().length) {
                e.preventDefault();
                bootstrap.Modal.getOrCreateInstance(document.getElementById('modalListadoFacturas')).show();
            }
        });

        /* Botón Excel de la card */
        $('#btnExcelCardFacturado').on('click', function (e) {
            e.stopPropagation();
            window.MatReportes.downloadReporteExcel(_urlExcel, _getQueryParams, _showErr, '#btnExcelCardFacturado');
        });

        /* Al abrir el modal */
        $('#modalListadoFacturas').on('show.bs.modal', function () {
            _expandedIds = {};
            _limpiarFiltros();
            /* Resetear ícono expandir/contraer todo */
            $('#btnExpandAll').find('i').removeClass('bi-arrows-collapse').addClass('bi-arrows-expand');
            _populateEstadoDropdown(_getLastRows());
            var label = _getQueryLabel ? _getQueryLabel() : '';
            $('#modalFacturasSubtitulo').text(label ? 'Consulta: ' + label : '');
            _sortCol = 'facturaFecha';
            _sortAsc = false;
            _render();
        });

        /* Filtros */
        $('#modalFacturasTexto').on('input', _debounce(function () {
            _filterState.texto = $(this).val();
            _render();
        }, 250));

        $('#modalFacturasEstado').on('change', function () {
            _filterState.estado = $(this).val();
            _render();
        });

        $('#modalFacturasMoneda').on('change', function () {
            _filterState.moneda = $(this).val();
            _render();
        });

        $('#modalFacturasDesde').on('change', function () {
            _filterState.desde = $(this).val();
            _render();
        });

        $('#modalFacturasHasta').on('change', function () {
            _filterState.hasta = $(this).val();
            _render();
        });

        $('#modalFacturasLimpiar').on('click', function () {
            _limpiarFiltros();
            _render();
        });

        /* Expandir / contraer todo */
        $('#btnExpandAll').on('click', function () {
            var all = _getLastRows();
            var allExp = all.length > 0 && all.every(function (r) { return !!_expandedIds[r.facturaId]; });
            all.forEach(function (r) { _expandedIds[r.facturaId] = !allExp; });
            _renderTable(_sortRows(_applyFilters(all)));
            $(this).find('i').toggleClass('bi-arrows-expand bi-arrows-collapse');
        });

        /* Ordenar por columna (delegado) */
        $('#modalFacturasTablaWrap').on('click', '.js-sort-col', function () {
            var col = $(this).data('col');
            _sortAsc = (_sortCol === col) ? !_sortAsc : false;
            _sortCol = col;
            _render();
        });

        /* Expandir / contraer fila (delegado) */
        $('#modalFacturasTablaWrap').on('click', '.js-expand-fila', function (e) {
            e.stopPropagation();
            var fid = $(this).closest('tr').data('factura-id');
            _expandedIds[fid] = !_expandedIds[fid];
            _renderTable(_sortRows(_applyFilters(_getLastRows())));
        });

        /* Copiar facturaId (delegado) */
        $('#modalFacturasTablaWrap').on('click', '.js-copiar-factura', function (e) {
            e.stopPropagation();
            var fid = $(this).data('factura-id');
            var $btn = $(this);
            function feedback() {
                $btn.html('<i class="bi bi-check text-success"></i>');
                setTimeout(function () { $btn.html('<i class="bi bi-clipboard"></i>'); }, 1500);
            }
            if (navigator.clipboard && navigator.clipboard.writeText) {
                navigator.clipboard.writeText(String(fid)).then(feedback, feedback);
            } else {
                var $tmp = $('<textarea style="position:fixed;opacity:0">').val(fid).appendTo('body').select();
                try { document.execCommand('copy'); } catch (ex) { /* silent */ }
                $tmp.remove();
                feedback();
            }
        });

        /* Tooltip BS5 en la card */
        var cardEl = document.getElementById('cardFacturado');
        if (cardEl && window.bootstrap && bootstrap.Tooltip) {
            new bootstrap.Tooltip(cardEl, { trigger: 'hover focus', placement: 'top' });
        }
    }

    window.MatReportes.FacturadoModal = { init: init };

})(window, jQuery);
