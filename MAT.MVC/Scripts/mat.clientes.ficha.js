/**
 * Ficha del cliente (Views/PersonaCliente/Details.cshtml): pestañas con estado en la URL (#viajes, #pagos…),
 * Pagos / Cuenta corriente / Facturas cargados al abrir su pestaña, y registro de contactos
 * (Seguimiento/RegistrarContacto). Expone window.MatClienteFicha.init(urls).
 */
(function (window, $) {
    'use strict';

    var PANELES = ['datos', 'viajes', 'contactos', 'pagos', 'cuenta', 'facturas'];
    var DIAS_AVISO_CONTACTO = 7;

    function esc(s) {
        return String(s == null ? '' : s).replace(/[&<>"']/g, function (c) {
            return { '&': '&amp;', '<': '&lt;', '>': '&gt;', '"': '&quot;', "'": '&#039;' }[c];
        });
    }

    function fecha(iso) {
        var m = /^(\d{4})-(\d{2})-(\d{2})/.exec(iso || '');
        return m ? m[3] + '/' + m[2] + '/' + m[1] : '—';
    }

    function diasDesde(iso) {
        var m = /^(\d{4})-(\d{2})-(\d{2})/.exec(iso || '');
        if (!m) return null;
        var d = new Date(+m[1], +m[2] - 1, +m[3]), hoy = new Date();
        hoy.setHours(0, 0, 0, 0);
        return Math.round((hoy - d) / 86400000);
    }

    function tabla(encabezados, filas) {
        return '<div class="table-responsive"><table class="cf-tabla"><thead><tr>' +
            encabezados.map(function (h) { return '<th scope="col">' + h + '</th>'; }).join('') + '</tr></thead><tbody>' +
            filas.map(function (f) { return '<tr>' + f.map(function (c) { return '<td>' + c + '</td>'; }).join('') + '</tr>'; }).join('') +
            '</tbody></table></div>';
    }

    function vacio(icono, texto) {
        return '<div class="cf-vacio-panel"><i class="bi ' + icono + '" aria-hidden="true"></i><p>' + texto + '</p></div>';
    }

    function init(urls) {
        $(function () { iniciar(urls); });
    }

    function iniciar(urls) {
        var datos = JSON.parse(document.getElementById('cfDatos').textContent);
        var cargados = {};

        // ---------------- Pestañas
        function mostrar(panel, enfocar) {
            if (PANELES.indexOf(panel) < 0) panel = 'datos';
            $('.cf-tab').each(function () {
                var activo = $(this).data('panel') === panel;
                $(this).toggleClass('is-activo', activo).attr({ 'aria-selected': activo, tabindex: activo ? 0 : -1 });
            });
            $('.cf-panel').each(function () { this.hidden = this.id !== 'panel-' + panel; });
            cargar(panel);
            if (enfocar) $('#tab-' + panel).trigger('focus');
        }

        function cargar(panel) {
            if (cargados[panel]) return;
            var $p = $('#panel-' + panel);
            if (panel === 'viajes') { pintarViajes(datos.viajes); cargados[panel] = true; return; }
            if (panel === 'contactos') { pintarContactos(datos.contactos); cargados[panel] = true; return; }
            var url = $p.data('url');
            if (!url) { cargados[panel] = true; return; }
            cargados[panel] = true;
            $p.attr('aria-busy', 'true').html('<div class="cf-cargando"><div class="spinner-border text-primary" role="status"><span class="visually-hidden">Cargando…</span></div></div>');
            $.ajax({ url: url, dataType: 'html', cache: false }).done(function (html) {
                $p.html(html);
                // Los parciales viejos no muestran nada cuando la lista está vacía
                if ($p.find('table').length && !$p.find('tbody tr').length) {
                    var textos = { pagos: 'Este cliente todavía no tiene pagos registrados.', facturas: 'Este cliente no es titular de ninguna factura.' };
                    $p.html(vacio('bi-inbox', textos[panel] || 'Sin movimientos.'));
                }
            }).fail(function () {
                cargados[panel] = false;
                $p.html('<div class="alert alert-danger">No se pudo cargar esta sección. <button type="button" class="btn btn-link p-0 align-baseline cf-reintentar" data-panel="' + panel + '">Reintentar</button></div>');
            }).always(function () { $p.attr('aria-busy', 'false'); });
        }

        function pintarViajes(viajes) {
            var $p = $('#panel-viajes');
            if (!datos.seguimiento) { $p.html(vacio('bi-exclamation-circle', 'No se pudo calcular el historial de viajes.')); return; }
            if (!viajes.length) { $p.html(vacio('bi-airplane', 'Este cliente todavía no viajó ni pagó viajes para otros.')); return; }
            $p.html('<p class="cf-tenue cf-nota">Todos los viajes con pasaje del cliente o pagados por él, incluidas reservas y viajes por venir. ' +
                '"Viajes realizados" (arriba) cuenta solo los ya hechos y confirmados.</p>' +
                tabla(['Salida', 'Regreso', 'Viaje', 'Participación', 'Estado'], viajes.map(function (v) {
                var rol = esc(v.rol || '') + (v.pasajes > 1 ? ' <span class="cf-tenue">(' + v.pasajes + ' pasajes)</span>' : '');
                return [fecha(v.fechaSalida), fecha(v.fechaRegreso), esc(v.viaje || ''), rol, esc(v.estadoPasaje || '—')];
            })));
        }

        function pintarContactos(contactos) {
            var $p = $('#panel-contactos');
            var boton = '<p class="cf-panel-acciones"><button type="button" class="btn-modern btn-modern-secondary btn-sm cf-abrir-registro"><i class="bi bi-journal-plus" aria-hidden="true"></i> <span>Registrar contacto</span></button></p>';
            if (!contactos.length) { $p.html(boton + vacio('bi-chat-dots', 'Todavía no hay contactos registrados.')); return; }
            $p.html(boton + tabla(['Fecha', 'Resultado', 'Quién', 'Observación'], contactos.map(function (x) {
                return [fecha(x.fecha), esc(x.resultadoNombre || ''), esc(x.quien || ''), esc(x.observacion || '')];
            })));
        }

        $('.cf-tab').on('click', function () {
            var panel = $(this).data('panel');
            history.replaceState(null, '', panel === 'datos' ? location.pathname + location.search : '#' + panel);
            mostrar(panel);
        });
        // Teclado: flechas entre pestañas (patrón ARIA de tabs)
        $('.cf-tabs').on('keydown', '.cf-tab', function (e) {
            var i = PANELES.indexOf($(this).data('panel'));
            if (e.key === 'ArrowRight') i = (i + 1) % PANELES.length;
            else if (e.key === 'ArrowLeft') i = (i - 1 + PANELES.length) % PANELES.length;
            else if (e.key === 'Home') i = 0;
            else if (e.key === 'End') i = PANELES.length - 1;
            else return;
            e.preventDefault();
            $('#tab-' + PANELES[i]).trigger('click').trigger('focus');
        });
        $(document).on('click', '.cf-reintentar', function () { mostrar($(this).data('panel')); });
        window.addEventListener('hashchange', function () { mostrar(location.hash.replace('#', '')); });

        // ---------------- Gasto en pesos y dólares
        $('.cf-gasto').each(function () {
            var ars = parseFloat($(this).data('ars')) || 0, usd = parseFloat($(this).data('usd')) || 0, partes = [];
            if (ars) partes.push(ars.toLocaleString('es-AR', { style: 'currency', currency: 'ARS', maximumFractionDigits: 0 }));
            if (usd) partes.push('US$ ' + usd.toLocaleString('es-AR', { maximumFractionDigits: 0 }));
            $(this).html(partes.length ? partes.map(esc).join('<br>') : '—');
        });

        // ---------------- Copiar ID
        $('#cfCopiarId').on('click', function () {
            var $b = $(this), id = String($b.data('id'));
            var listo = function () { $b.find('span').text('ID copiado'); setTimeout(function () { $b.find('span').text('Copiar ID del cliente'); }, 2000); };
            if (navigator.clipboard) navigator.clipboard.writeText(id).then(listo, function () { window.prompt('ID del cliente', id); });
            else window.prompt('ID del cliente', id);
        });

        // ---------------- Registrar contacto
        var ultimo = datos.contactos && datos.contactos.length ? datos.contactos[0] : null;
        var modal = bootstrap.Modal.getOrCreateInstance(document.getElementById('cfModalContacto'));

        function abrirRegistro() {
            $('#cfContactoResultado').val('');
            $('#cfContactoObs').val('');
            $('#cfContactoError').addClass('d-none');
            var dias = ultimo ? diasDesde(ultimo.fecha) : null, aviso = '';
            if (dias !== null && dias <= DIAS_AVISO_CONTACTO) {
                aviso = 'Contactado ' + (dias === 0 ? 'hoy' : dias === 1 ? 'ayer' : 'hace ' + dias + ' días') +
                    ' por ' + (ultimo.quien || 'otro usuario') + ' (' + (ultimo.resultadoNombre || '') + ').';
            }
            $('#cfContactoAviso').toggleClass('d-none', !aviso).text(aviso);
            modal.show();
        }

        $('#cfRegistrar').on('click', abrirRegistro);
        $(document).on('click', '.cf-abrir-registro', abrirRegistro);
        // WhatsApp se abre en otra pestaña; al volver queda el formulario para registrar el contacto.
        $('#cfWhatsApp').on('click', function () { setTimeout(abrirRegistro, 300); });
        document.getElementById('cfModalContacto').addEventListener('shown.bs.modal', function () { $('#cfContactoResultado').trigger('focus'); });

        $('#cfFormContacto').on('submit', function (e) {
            e.preventDefault();
            var $btn = $('#cfContactoGuardar'), resultado = $('#cfContactoResultado').val();
            if (!resultado) {
                $('#cfContactoError').text('Elegí el resultado del contacto.').removeClass('d-none');
                $('#cfContactoResultado').trigger('focus');
                return;
            }
            $btn.prop('disabled', true).find('span').text('Guardando…');
            $.ajax({
                type: 'POST', url: urls.urlRegistrar, dataType: 'json',
                data: {
                    clienteId: datos.clienteId,
                    resultado: resultado,
                    observacion: $('#cfContactoObs').val(),
                    __RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val()
                }
            }).done(function (r) {
                if (!r.success) { $('#cfContactoError').text(r.message).removeClass('d-none'); return; }
                modal.hide();
                $('#cfAviso').removeClass('d-none alert-danger').addClass('alert-success').text(r.message);
                refrescarContactos();
            }).fail(function () {
                $('#cfContactoError').text('No se pudo registrar el contacto. Probá de nuevo.').removeClass('d-none');
            }).always(function () { $btn.prop('disabled', false).find('span').text('Guardar'); });
        });

        function refrescarContactos() {
            $.ajax({ url: urls.urlFicha, data: { id: datos.clienteId }, dataType: 'json', cache: false }).done(function (r) {
                if (!r.success) return;
                var c = r.data.cliente;
                datos.contactos = r.data.contactos;
                ultimo = datos.contactos.length ? datos.contactos[0] : null;
                $('#cfCuentaContactos').text(datos.contactos.length);
                if (c && c.ultimoContactoFecha) {
                    $('#cfUltimoContacto').text(fecha(c.ultimoContactoFecha));
                    $('#cfUltimoContactoSub').text((c.ultimoContactoResultadoNombre || '') + (c.ultimoContactoPor ? ' · ' + c.ultimoContactoPor : ''));
                }
                cargados.contactos = false;
                if (!$('#panel-contactos')[0].hidden) cargar('contactos');
            });
        }

        mostrar(location.hash.replace('#', ''));
    }

    window.MatClienteFicha = { init: init };
})(window, jQuery);
