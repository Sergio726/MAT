/**
 * Seguimiento de clientes (Views/Seguimiento/Index.cshtml) y recuadro "Cumpleaños de hoy" del inicio.
 * Expone: window.MatSeguimiento.initPagina(urls), window.MatSeguimiento.initWidget(opciones).
 */
(function (window, $) {
    'use strict';

    var MR = window.MatReportes || {};
    var RESULTADO_SALUDO = 1;
    var DIAS_AVISO_CONTACTO = 7;

    function escHtml(s) {
        if (MR.escapeHtml) return MR.escapeHtml(s);
        return String(s == null ? '' : s).replace(/&/g, '&amp;').replace(/</g, '&lt;').replace(/>/g, '&gt;').replace(/"/g, '&quot;');
    }

    function parseIso(val) {
        if (!val) return null;
        var m = String(val).match(/^(\d{4})-(\d{2})-(\d{2})(?:T(\d{2}):(\d{2}))?/);
        return m ? new Date(+m[1], +m[2] - 1, +m[3], +(m[4] || 0), +(m[5] || 0)) : null;
    }

    function pad(n) { return n < 10 ? '0' + n : '' + n; }

    function fmtFecha(val) {
        var d = parseIso(val);
        return d ? pad(d.getDate()) + '/' + pad(d.getMonth() + 1) + '/' + d.getFullYear() : '—';
    }

    function diasDesde(val) {
        var d = parseIso(val);
        if (!d) return null;
        var hoy = new Date(); hoy.setHours(0, 0, 0, 0);
        var dia = new Date(d.getFullYear(), d.getMonth(), d.getDate());
        return Math.round((hoy - dia) / 86400000);
    }

    function textoHace(dias) {
        if (dias === null || dias <= 0) return 'hoy';
        if (dias === 1) return 'ayer';
        return 'hace ' + dias + ' días';
    }

    function nombreCompleto(r) {
        return [r.apellido, r.nombre].filter(Boolean).join(', ');
    }

    function badgeCategoria(r) {
        return '<span class="sg-badge sg-badge--' + escHtml(r.categoria) + '">' + escHtml(r.categoriaNombre || r.categoria) + '</span>';
    }

    function renderCliente(r) {
        var html = '<strong>' + escHtml(nombreCompleto(r)) + '</strong>';
        var sub = [];
        if (r.nroDocumento) sub.push('DNI ' + escHtml(r.nroDocumento));
        if (r.celular || r.telefono) sub.push(escHtml(r.celular || r.telefono));
        if (sub.length) html += '<div class="sg-sub">' + sub.join(' · ') + '</div>';
        if (r.esMenor) html += ' <span class="sg-badge sg-badge--menor">Menor de edad</span>';
        if (r.fechaNacimientoDudosa) html += '<div class="sg-warn"><i class="bi bi-exclamation-circle"></i> Verificar fecha de nacimiento con el cliente</div>';
        if (r.telefonoCompartido > 0) {
            html += '<div class="sg-sub"><i class="bi bi-people"></i> Teléfono compartido con ' + r.telefonoCompartido +
                (r.telefonoCompartido === 1 ? ' cliente más' : ' clientes más') + '</div>';
        }
        return html;
    }

    function renderViajes(r) {
        var html = '<strong>' + (r.viajesTotales || 0) + '</strong>';
        if (r.viajes12Meses) html += '<div class="sg-sub">' + r.viajes12Meses + ' en 12 meses</div>';
        var pagados = (r.viajesTotales || 0) - (r.viajesComoPasajero || 0);
        if (pagados > 0) html += '<div class="sg-sub">' + pagados + ' pagado' + (pagados === 1 ? '' : 's') + ' para otros</div>';
        return html;
    }

    function renderContacto(r) {
        if (!r.ultimoContactoFecha) return '<span class="sg-sub">Sin contactos</span>';
        var dias = diasDesde(r.ultimoContactoFecha);
        var cls = dias !== null && dias <= DIAS_AVISO_CONTACTO ? 'sg-warn' : 'sg-sub';
        return '<div class="' + cls + '">' + escHtml(r.ultimoContactoResultadoNombre || '') + ' · ' + textoHace(dias) + '</div>' +
            '<div class="sg-sub">' + escHtml(r.ultimoContactoPor || '') + '</div>';
    }

    function renderSaludo(r) {
        if (r.ultimoSaludoFecha) {
            return '<span class="sg-ok"><i class="bi bi-check2-circle"></i> Saludado ' + textoHace(diasDesde(r.ultimoSaludoFecha)) +
                (r.ultimoSaludoPor ? ' por ' + escHtml(r.ultimoSaludoPor) : '') + '</span>';
        }
        return '<span class="sg-sub">Pendiente</span>';
    }

    function renderAcciones(r) {
        var id = escHtml(r.clienteId);
        var wa = r.tieneWhatsApp
            ? '<a href="' + escHtml(r.whatsAppUrl) + '" target="_blank" rel="noopener" class="btn btn-sm sg-btn-wa" data-accion="wa" data-id="' + id + '" title="Abrir WhatsApp" aria-label="Abrir WhatsApp"><i class="bi bi-whatsapp" aria-hidden="true"></i></a>'
            : '<button type="button" class="btn btn-sm btn-outline-secondary" disabled title="Sin teléfono válido" aria-label="Sin teléfono válido para WhatsApp"><i class="bi bi-whatsapp" aria-hidden="true"></i></button>';
        return '<div class="sg-actions">' + wa +
            '<button type="button" class="btn btn-sm btn-outline-secondary" data-accion="ficha" data-id="' + id + '" title="Ver ficha" aria-label="Ver ficha"><i class="bi bi-person-vcard" aria-hidden="true"></i></button>' +
            '<button type="button" class="btn btn-sm btn-outline-secondary" data-accion="registrar" data-id="' + id + '" title="Registrar contacto" aria-label="Registrar contacto"><i class="bi bi-journal-plus" aria-hidden="true"></i></button>' +
            '</div>';
    }

    function dtLanguage() {
        return { url: '/Scripts/dataTable/i18n/Spanish.json' };
    }

    function getJson(url, data) {
        return $.ajax({ url: url, data: data, dataType: 'json', cache: false });
    }

    function mostrarAlerta($el, msg, tipo) {
        $el.removeClass('d-none alert-danger alert-success').addClass(tipo === 'ok' ? 'alert-success' : 'alert-danger').text(msg);
        if (tipo === 'ok') setTimeout(function () { $el.addClass('d-none'); }, 4000);
    }

    // ---------------------------------------------------------------- Página
    function initPagina(urls) {
        var clientes = {};          // clienteId -> fila (para modales)
        var tablas = {};
        var diasCumple = 0;
        var categoriaActual = null;
        var textoActual = null;
        var contactoCliente = null;
        var contextoActual = 'hoy';

        function guardarFilas(rows) {
            (rows || []).forEach(function (r) { clientes[r.clienteId] = r; });
        }

        function pintarTabla(clave, selector, rows, columns, order) {
            var $tbl = $(selector);
            if (tablas[clave]) { tablas[clave].destroy(); $tbl.find('tbody').empty(); }
            tablas[clave] = $tbl.DataTable({
                data: rows, columns: columns, order: order || [], pageLength: 25, autoWidth: false, language: dtLanguage(),
                deferRender: true // la categoría Dormido trae más de 6.000 clientes
            });
        }

        var columnasLista = [
            { data: null, render: function (d, t, r) { return t === 'display' ? renderCliente(r) : nombreCompleto(r) + ' ' + (r.nroDocumento || ''); } },
            { data: 'categoriaNombre', render: function (d, t, r) { return t === 'display' ? badgeCategoria(r) : d; } },
            { data: 'viajesTotales', render: function (d, t, r) { return t === 'display' ? renderViajes(r) : d; } },
            { data: 'ultimoViaje', render: function (d, t) { return t === 'display' ? fmtFecha(d) : (d || ''); } },
            { data: 'proximoViaje', render: function (d, t) { return t === 'display' ? (d ? '<span class="sg-ok">' + fmtFecha(d) + '</span>' : '—') : (d || ''); } },
            { data: 'ultimoContactoFecha', render: function (d, t, r) { return t === 'display' ? renderContacto(r) : (d || ''); } },
            { data: null, orderable: false, render: function (d, t, r) { return renderAcciones(r); } }
        ];

        // Para hoy
        function cargarCumpleanios() {
            var $alert = $('#sg-hoy-alert').addClass('d-none');
            getJson(urls.urlCumpleanios, { dias: diasCumple }).done(function (res) {
                if (!res.success) { mostrarAlerta($alert, res.message); return; }
                guardarFilas(res.data);
                $('#sg-hoy-empty').toggleClass('d-none', res.data.length > 0);
                $('#sg-hoy-wrap').toggleClass('d-none', res.data.length === 0);
                pintarTabla('hoy', '#sg-tbl-hoy', res.data, [
                    { data: 'proximoCumple', render: function (d, t, r) {
                        if (t !== 'display') return d || '';
                        return fmtFecha(d) + '<div class="sg-sub">' + (r.diasFaltan === 0 ? 'Hoy' : 'En ' + r.diasFaltan + ' días') + '</div>';
                    } },
                    { data: null, render: function (d, t, r) { return t === 'display' ? renderCliente(r) : nombreCompleto(r); } },
                    { data: 'edadQueCumple', render: function (d) { return d == null ? '—' : d; } },
                    { data: 'categoriaNombre', render: function (d, t, r) { return t === 'display' ? badgeCategoria(r) : d; } },
                    { data: 'viajesTotales', render: function (d, t, r) { return t === 'display' ? renderViajes(r) : d; } },
                    { data: 'ultimoSaludoFecha', render: function (d, t, r) { return t === 'display' ? renderSaludo(r) : (d ? 1 : 0); } },
                    { data: null, orderable: false, render: function (d, t, r) { return renderAcciones(r); } }
                ], [[0, 'asc']]);
            }).fail(function () { mostrarAlerta($alert, 'No se pudieron cargar los cumpleaños.'); });
        }

        // Por categoría
        function cargarResumen() {
            getJson(urls.urlResumen).done(function (res) {
                if (!res.success) return;
                res.data.forEach(function (c) {
                    $('[data-count-for="' + c.codigo + '"]').text((c.cantidad || 0).toLocaleString('es-AR'));
                });
            });
        }

        function cargarCategoria() {
            if (!categoriaActual) return;
            var $alert = $('#sg-cat-alert').addClass('d-none');
            $('#sg-cat-wrap').addClass('d-none');
            $('#sg-cat-empty').removeClass('d-none').find('div').text('Cargando…');
            getJson(urls.urlClientes, { categoria: categoriaActual }).done(function (res) {
                if (!res.success) { mostrarAlerta($alert, res.message); return; }
                guardarFilas(res.data);
                $('#sg-cat-empty').toggleClass('d-none', res.data.length > 0).find('div').text('No hay clientes en esta categoría.');
                $('#sg-cat-wrap').toggleClass('d-none', res.data.length === 0);
                pintarTabla('cat', '#sg-tbl-cat', res.data, columnasLista, [[2, 'desc']]);
            }).fail(function () {
                $('#sg-cat-empty').find('div').text('');
                mostrarAlerta($alert, 'No se pudo cargar la categoría.');
            });
        }

        // Buscar
        function buscar() {
            if (!textoActual) return;
            var $alert = $('#sg-buscar-alert').addClass('d-none');
            getJson(urls.urlClientes, { texto: textoActual }).done(function (res) {
                if (!res.success) { mostrarAlerta($alert, res.message); return; }
                guardarFilas(res.data);
                $('#sg-buscar-empty').toggleClass('d-none', res.data.length > 0).find('div').text('No se encontraron clientes.');
                $('#sg-buscar-wrap').toggleClass('d-none', res.data.length === 0);
                pintarTabla('buscar', '#sg-tbl-buscar', res.data, columnasLista, [[2, 'desc']]);
            }).fail(function () { mostrarAlerta($alert, 'No se pudo buscar.'); });
        }

        function recargarActual() {
            if (contextoActual === 'hoy') cargarCumpleanios();
            else if (contextoActual === 'cat') { cargarCategoria(); cargarResumen(); }
            else buscar();
        }

        // Ficha
        function abrirFicha(id) {
            var $body = $('#sg-ficha-body').html('<div class="sg-empty"><div class="spinner-border text-primary"></div></div>');
            $('#sg-ficha-wa').addClass('d-none');
            $('#sg-modal-ficha').data('id', id);
            bootstrap.Modal.getOrCreateInstance(document.getElementById('sg-modal-ficha')).show();
            getJson(urls.urlFicha, { id: id }).done(function (res) {
                if (!res.success) { $body.html('<div class="alert alert-danger">' + escHtml(res.message) + '</div>'); return; }
                var f = res.data, c = f.cliente;
                guardarFilas([c]);
                $('#sg-ficha-titulo').text(nombreCompleto(c));
                if (c.tieneWhatsApp) $('#sg-ficha-wa').attr('href', c.whatsAppUrl).attr('data-id', c.clienteId).removeClass('d-none');

                var anios = c.primerViaje ? Math.max(0, new Date().getFullYear() - parseIso(c.primerViaje).getFullYear()) : null;
                var gasto = [];
                if (c.gastoArs) gasto.push(MR.renderMontoCurrency ? MR.renderMontoCurrency(c.gastoArs, 1, 'display') : '$ ' + c.gastoArs);
                if (c.gastoUsd) gasto.push(MR.renderMontoCurrency ? MR.renderMontoCurrency(c.gastoUsd, 3, 'display') : 'US$ ' + c.gastoUsd);

                var html = '<div>' + badgeCategoria(c) + ' <span class="sg-sub">' + escHtml(c.accionSugerida || '') + '</span></div>' +
                    '<div class="mt-2">' + renderCliente(c) + (c.email ? '<div class="sg-sub">' + escHtml(c.email) + '</div>' : '') + '</div>' +
                    '<div class="sg-metrics">' +
                    metric('Nacimiento', fmtFecha(c.fechaNacimiento)) +
                    metric('Viajes', (c.viajesTotales || 0) + (c.viajes12Meses ? ' (' + c.viajes12Meses + ' en 12 meses)' : '')) +
                    metric('Cliente desde', anios === null ? '—' : (anios === 0 ? 'Este año' : anios + (anios === 1 ? ' año' : ' años'))) +
                    metric('Último viaje', fmtFecha(c.ultimoViaje)) +
                    metric('Próximo viaje', c.proximoViaje ? fmtFecha(c.proximoViaje) : 'Sin reservas') +
                    metric('Gastado', gasto.length ? gasto.join('<br>') : '—') +
                    '</div>';

                html += '<h6 class="mt-3">Viajes</h6>';
                html += f.viajes.length ? tablaSimple(['Salida', 'Viaje', 'Participación', 'Estado'], f.viajes.map(function (v) {
                    var rol = escHtml(v.rol || '') + (v.pasajes > 1 ? ' <span class="sg-sub">(' + v.pasajes + ' pasajes)</span>' : '');
                    return [fmtFecha(v.fechaSalida), escHtml(v.viaje || ''), rol, escHtml(v.estadoPasaje || '—')];
                })) : '<div class="sg-sub">Sin viajes registrados.</div>';

                html += '<h6 class="mt-3">Contactos</h6>';
                html += f.contactos.length ? tablaSimple(['Fecha', 'Resultado', 'Quién', 'Observación'], f.contactos.map(function (x) {
                    return [fmtFecha(x.fecha), escHtml(x.resultadoNombre || ''), escHtml(x.quien || ''), escHtml(x.observacion || '')];
                })) : '<div class="sg-sub">Todavía no hay contactos registrados.</div>';

                $body.html(html);
            }).fail(function () { $body.html('<div class="alert alert-danger">No se pudo cargar la ficha.</div>'); });
        }

        function metric(label, value) {
            return '<div class="sg-metric"><div class="sg-metric-label">' + label + '</div><div class="sg-metric-value">' + value + '</div></div>';
        }

        function tablaSimple(headers, rows) {
            return '<div class="table-responsive"><table class="table table-sm table-bordered mb-0"><thead><tr>' +
                headers.map(function (h) { return '<th>' + h + '</th>'; }).join('') + '</tr></thead><tbody>' +
                rows.map(function (r) { return '<tr>' + r.map(function (c) { return '<td>' + c + '</td>'; }).join('') + '</tr>'; }).join('') +
                '</tbody></table></div>';
        }

        // Registrar contacto
        function abrirRegistro(id, resultadoSugerido) {
            var r = clientes[id];
            if (!r) return;
            contactoCliente = r;
            $('#sg-contacto-cliente').text(nombreCompleto(r));
            $('#sg-contacto-resultado').val(resultadoSugerido ? String(resultadoSugerido) : '');
            $('#sg-contacto-obs').val('');
            $('#sg-contacto-error').addClass('d-none');

            var aviso = '';
            var dias = diasDesde(r.ultimoContactoFecha);
            if (dias !== null && dias <= DIAS_AVISO_CONTACTO) {
                aviso = 'Contactado ' + textoHace(dias) + ' por ' + (r.ultimoContactoPor || 'otro usuario') +
                    ' (' + (r.ultimoContactoResultadoNombre || '') + ').';
            }
            $('#sg-contacto-aviso').toggleClass('d-none', !aviso).text(aviso);
            bootstrap.Modal.getOrCreateInstance(document.getElementById('sg-modal-contacto')).show();
        }

        $('#sg-contacto-guardar').on('click', function () {
            var $btn = $(this);
            var resultado = $('#sg-contacto-resultado').val();
            if (!resultado) { mostrarAlerta($('#sg-contacto-error'), 'Elegí el resultado del contacto.'); return; }
            $btn.prop('disabled', true);
            $.ajax({
                type: 'POST',
                url: urls.urlRegistrar,
                dataType: 'json',
                data: {
                    clienteId: contactoCliente.clienteId,
                    resultado: resultado,
                    observacion: $('#sg-contacto-obs').val(),
                    __RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val()
                }
            }).done(function (res) {
                if (!res.success) { mostrarAlerta($('#sg-contacto-error'), res.message); return; }
                bootstrap.Modal.getOrCreateInstance(document.getElementById('sg-modal-contacto')).hide();
                var $alert = contextoActual === 'hoy' ? $('#sg-hoy-alert') : contextoActual === 'cat' ? $('#sg-cat-alert') : $('#sg-buscar-alert');
                mostrarAlerta($alert, res.message, 'ok');
                recargarActual();
            }).fail(function () {
                mostrarAlerta($('#sg-contacto-error'), 'No se pudo registrar el contacto.');
            }).always(function () { $btn.prop('disabled', false); });
        });

        // Eventos
        $(document).on('click', '[data-accion="ficha"]', function () { abrirFicha($(this).data('id')); });
        $(document).on('click', '[data-accion="registrar"]', function () { abrirRegistro($(this).data('id')); });
        $(document).on('click', '[data-accion="wa"], #sg-ficha-wa', function () {
            var id = $(this).attr('data-id');
            var sugerido = contextoActual === 'hoy' ? RESULTADO_SALUDO : null;
            if (this.id === 'sg-ficha-wa') bootstrap.Modal.getOrCreateInstance(document.getElementById('sg-modal-ficha')).hide();
            // WhatsApp se abre en otra pestaña; al volver queda el formulario para registrar el contacto.
            setTimeout(function () { abrirRegistro(id, sugerido); }, 300);
        });
        $('#sg-ficha-registrar').on('click', function () {
            var id = $('#sg-modal-ficha').data('id');
            bootstrap.Modal.getOrCreateInstance(document.getElementById('sg-modal-ficha')).hide();
            abrirRegistro(id, contextoActual === 'hoy' ? RESULTADO_SALUDO : null);
        });

        $('.sg-chip[data-dias]').on('click', function () {
            $('.sg-chip[data-dias]').removeClass('active');
            $(this).addClass('active');
            diasCumple = +$(this).data('dias');
            cargarCumpleanios();
        });

        $('.sg-cat').on('click', function () {
            $('.sg-cat').removeClass('active');
            $(this).addClass('active');
            categoriaActual = $(this).data('cat');
            $('#sg-cat-title').text($(this).find('.sg-cat-head span').first().text());
            cargarCategoria();
        });

        $('#sg-form-buscar').on('submit', function (e) {
            e.preventDefault();
            textoActual = $.trim($('#sg-buscar-texto').val());
            if (textoActual.length < 2) {
                mostrarAlerta($('#sg-buscar-alert'), 'Escribí al menos 2 letras del apellido, nombre o DNI.');
                return;
            }
            buscar();
        });

        $('button[data-bs-toggle="pill"]').on('shown.bs.tab', function (e) {
            var target = $(e.target).data('bs-target');
            contextoActual = target === '#sg-tab-hoy' ? 'hoy' : target === '#sg-tab-cat' ? 'cat' : 'buscar';
            if (contextoActual === 'cat') cargarResumen();
        });

        cargarCumpleanios();
        if (urls.fichaInicial) abrirFicha(urls.fichaInicial);
    }

    // ---------------------------------------------------------------- Recuadro del inicio
    function initWidget(opciones) {
        var $box = $(opciones.contenedor);
        if (!$box.length) return;
        getJson(opciones.url, { dias: 0 }).done(function (res) {
            if (!res.success) { $box.find('[data-rol="lista"]').html('<div class="sg-sub">No se pudieron cargar los cumpleaños.</div>'); return; }
            var rows = res.data || [];
            var pendientes = rows.filter(function (r) { return !r.ultimoSaludoFecha; }).length;
            $box.find('[data-rol="contador"]').text(rows.length ? rows.length + ' cumpleaños · ' + pendientes + ' sin saludar' : 'Nadie cumple años hoy');
            var max = opciones.maximo || 6;
            var html = rows.slice(0, max).map(function (r) {
                var estado = r.ultimoSaludoFecha ? '<span class="sg-ok" title="Ya saludado"><i class="bi bi-check2-circle" aria-hidden="true"></i> saludado</span>'
                    : !r.tieneWhatsApp ? '<span class="sg-sub" title="Sin teléfono válido">sin WhatsApp</span>'
                    : '<span class="sg-sub">pendiente</span>';
                return '<li><a href="' + escHtml(opciones.urlSeguimiento + '?ficha=' + encodeURIComponent(r.clienteId)) + '">' + escHtml(nombreCompleto(r)) + '</a>' +
                    (r.edadQueCumple ? ' <span class="sg-sub">(' + r.edadQueCumple + ')</span>' : '') +
                    (r.esMenor ? ' <span class="sg-sub">· menor de edad</span>' : '') + ' ' + estado + '</li>';
            }).join('');
            if (rows.length > max) html += '<li class="sg-sub">y ' + (rows.length - max) + ' más…</li>';
            $box.find('[data-rol="lista"]').html(html ? '<ul class="sg-widget-list">' + html + '</ul>' : '');
        }).fail(function () {
            $box.find('[data-rol="lista"]').html('<div class="sg-sub">No se pudieron cargar los cumpleaños.</div>');
        });
    }

    window.MatSeguimiento = { initPagina: initPagina, initWidget: initWidget };
})(window, jQuery);
