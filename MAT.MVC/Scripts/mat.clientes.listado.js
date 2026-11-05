/**
 * Listado de clientes (Views/PersonaCliente/Index.cshtml): búsqueda paginada en el servidor
 * (PersonaCliente/Buscar → usp_MAT_PersonaCliente_Buscar). Expone window.MatClientesListado.init(urls).
 */
(function (window, $) {
    'use strict';

    var ESPERA_MS = 300;

    function esc(s) {
        return String(s == null ? '' : s).replace(/[&<>"']/g, function (c) {
            return { '&': '&amp;', '<': '&lt;', '>': '&gt;', '"': '&quot;', "'": '&#039;' }[c];
        });
    }

    function sinAcentos(s) {
        return String(s || '').normalize('NFD').replace(/[̀-ͯ]/g, '').toLowerCase();
    }

    /** Resalta palabras sin importar acentos ni mayúsculas; devuelve HTML seguro. */
    function resaltar(texto, palabras) {
        texto = String(texto || '');
        if (!palabras.length || !texto) return esc(texto);
        var base = sinAcentos(texto), marcas = new Array(texto.length).fill(false);
        palabras.forEach(function (p) {
            var w = sinAcentos(p);
            if (!w) return;
            // Palabra numérica: se busca entre los dígitos ("3875327984" marca "3875 32-7984")
            var digitos = w.replace(/[.\-()+\/]/g, '');
            if (/^\d+$/.test(digitos)) {
                var pos = [], soloDigitos = '';
                for (var d = 0; d < texto.length; d++) if (/\d/.test(texto[d])) { pos.push(d); soloDigitos += texto[d]; }
                var e = soloDigitos.indexOf(digitos);
                while (e >= 0) {
                    for (var m = pos[e]; m <= pos[e + digitos.length - 1]; m++) marcas[m] = true;
                    e = soloDigitos.indexOf(digitos, e + digitos.length);
                }
                return;
            }
            var i = base.indexOf(w);
            while (i >= 0) {
                for (var k = i; k < i + w.length; k++) marcas[k] = true;
                i = base.indexOf(w, i + w.length);
            }
        });
        var html = '', abierto = false;
        for (var j = 0; j < texto.length; j++) {
            if (marcas[j] && !abierto) { html += '<mark>'; abierto = true; }
            if (!marcas[j] && abierto) { html += '</mark>'; abierto = false; }
            html += esc(texto[j]);
        }
        return html + (abierto ? '</mark>' : '');
    }

    function iniciales(apellido, nombre) {
        var a = $.trim(apellido || '').charAt(0), n = $.trim(nombre || '').charAt(0);
        return esc((a + n).toUpperCase() || '?');
    }

    function fecha(iso) {
        var m = /^(\d{4})-(\d{2})-(\d{2})/.exec(iso || '');
        return m ? m[3] + '/' + m[2] + '/' + m[1] : '—';
    }

    function init(urls) {
        $(function () { iniciar(urls); });
    }

    function iniciar(urls) {
        var estado = { q: '', tipo: '', categoria: '', orden: 'apellido', pagina: 1, tamanio: 25, inactivos: false };
        var pedido = null, temporizador = null;
        var $texto = $('#clTexto'), $filas = $('#clFilas');

        // ---------------- Estado en la URL (compartir, recargar, "atrás")
        function leerUrl() {
            var p = new URLSearchParams(window.location.search);
            estado.q = p.get('q') || '';
            estado.tipo = p.get('tipo') || '';
            estado.categoria = p.get('categoria') || '';
            estado.orden = ['apellido', 'alta', 'documento'].indexOf(p.get('orden')) >= 0 ? p.get('orden') : 'apellido';
            estado.pagina = Math.max(1, parseInt(p.get('pagina'), 10) || 1);
            estado.tamanio = [25, 50, 100].indexOf(parseInt(p.get('tamanio'), 10)) >= 0 ? parseInt(p.get('tamanio'), 10) : 25;
            $texto.val(estado.q);
            $('#clTipo').val(estado.tipo);
            $('#clCategoria').val(estado.categoria);
            $('#clTamanio').val(String(estado.tamanio));
            estado.inactivos = p.get('inactivos') === '1';
            $('#clInactivos').prop('checked', estado.inactivos);
        }

        function escribirUrl(reemplazar) {
            var p = new URLSearchParams();
            if (estado.q) p.set('q', estado.q);
            if (estado.tipo) p.set('tipo', estado.tipo);
            if (estado.categoria) p.set('categoria', estado.categoria);
            if (estado.orden !== 'apellido') p.set('orden', estado.orden);
            if (estado.pagina > 1) p.set('pagina', estado.pagina);
            if (estado.tamanio !== 25) p.set('tamanio', estado.tamanio);
            if (estado.inactivos) p.set('inactivos', '1');
            var url = window.location.pathname + (p.toString() ? '?' + p.toString() : '');
            if (url === window.location.pathname + window.location.search) return;
            window.history[reemplazar ? 'replaceState' : 'pushState'](null, '', url);
        }

        // ---------------- Carga
        function esqueleto() {
            var fila = '<tr class="cl-esqueleto" aria-hidden="true">' + new Array(7).join('<td><span></span></td>') + '</tr>';
            $filas.html(new Array(Math.min(estado.tamanio, 8) + 1).join(fila));
            $('#clVacio').addClass('d-none');
        }

        function cargar() {
            if (pedido) pedido.abort();
            $('#clError').addClass('d-none');
            $('#clContador').text('Buscando…');
            $('.cl-tabla-wrap').attr('aria-busy', 'true');
            esqueleto();
            pedido = $.ajax({
                url: urls.urlBuscar, dataType: 'json', cache: false,
                data: { q: estado.q, tipo: estado.tipo, categoria: estado.categoria, orden: estado.orden, pagina: estado.pagina, tamanio: estado.tamanio, inactivos: estado.inactivos }
            }).done(function (r) {
                if (!r.success) { mostrarError(r.message); return; }
                var paginas = Math.max(1, Math.ceil(r.total / r.tamanio));
                if (estado.pagina > paginas && r.total > 0) { estado.pagina = paginas; escribirUrl(true); cargar(); return; }
                pintar(r);
            }).fail(function (xhr, status) {
                if (status !== 'abort') mostrarError('No se pudo cargar el listado. Probá de nuevo.');
            }).always(function () {
                $('.cl-tabla-wrap').attr('aria-busy', 'false');
            });
        }

        function mostrarError(msg) {
            $filas.empty();
            $('#clContador').text('');
            $('#clError').text(msg || 'No se pudo cargar el listado.').removeClass('d-none');
        }

        // ---------------- Pintado
        function pintar(r) {
            var palabras = estado.q ? estado.q.split(/[\s,]+/).filter(Boolean) : [];
            var desde = r.total ? (r.pagina - 1) * r.tamanio + 1 : 0, hasta = Math.min(r.pagina * r.tamanio, r.total);
            $('#clContador').html(r.total
                ? 'Mostrando <strong>' + desde + '–' + hasta + '</strong> de <strong>' + r.total.toLocaleString('es-AR') + '</strong> cliente' + (r.total === 1 ? '' : 's')
                : 'Sin resultados');
            $('#clVacio').toggleClass('d-none', r.total > 0);
            $filas.html(r.data.map(function (c) { return fila(c, palabras); }).join(''));
            paginacion(r.total, r.pagina, r.tamanio);
            marcarOrden();
        }

        function fila(c, palabras) {
            var id = esc(c.personaId), nombre = [c.apellido, c.nombre].filter(Boolean).join(', ') || 'Sin nombre';
            var telefono = c.celular || c.telefono;
            var ficha = urls.urlFicha + '/' + id;
            var html = '<tr class="cl-fila' + (c.activo === false ? ' is-baja' : '') + '" data-ficha="' + ficha + '" tabindex="-1">';
            html += '<td><div class="cl-cliente"><span class="cl-avatar" aria-hidden="true">' + iniciales(c.apellido, c.nombre) + '</span><div>' +
                '<a class="cl-nombre" href="' + ficha + '">' + resaltar(nombre, palabras) + '</a>' +
                '<div class="cl-sub">' + esc(c.localidadNombre || '') + (c.tipoNombre ? (c.localidadNombre ? ' · ' : '') + esc(c.tipoNombre) : '') + '</div>' +
                '</div></div></td>';
            html += '<td class="cl-num">' + resaltar(c.nroDocumento || '—', palabras) + '</td>';
            html += '<td>' + (telefono ? '<div class="cl-num">' + resaltar(telefono, palabras) + '</div>' : '<span class="cl-sub">Sin teléfono</span>') +
                (c.email ? '<div class="cl-sub cl-email">' + resaltar(c.email, palabras) + '</div>' : '') + '</td>';
            html += '<td>' + (c.categoria ? '<span class="cl-badge cl-badge--' + esc(c.categoria) + '">' + esc(c.categoriaNombre || c.categoria) + '</span>' : '') +
                (c.esMenor ? ' <span class="cl-badge cl-badge--menor">Menor</span>' : '') +
                (c.activo === false ? ' <span class="cl-badge cl-badge--baja">Dado de baja</span>' : '') + '</td>';
            html += '<td class="cl-num">' + fecha(c.fechaAlta) + '</td>';
            html += '<td class="text-end"><div class="cl-acciones">' +
                '<a class="btn-modern btn-modern-secondary btn-sm" href="' + ficha + '"><i class="bi bi-person-vcard" aria-hidden="true"></i><span>Ficha</span></a>' +
                (c.tieneWhatsApp
                    ? '<a class="btn btn-sm cl-wa" href="' + esc(c.whatsAppUrl) + '" target="_blank" rel="noopener" aria-label="WhatsApp a ' + esc(nombre) + '" title="WhatsApp"><i class="bi bi-whatsapp" aria-hidden="true"></i></a>'
                    : '<span class="btn btn-sm cl-wa disabled" aria-label="Sin teléfono válido para WhatsApp" title="Sin teléfono válido"><i class="bi bi-whatsapp" aria-hidden="true"></i></span>') +
                '<div class="dropdown"><button type="button" class="btn btn-sm cl-mas" data-bs-toggle="dropdown" aria-expanded="false" aria-label="Más acciones para ' + esc(nombre) + '"' +
                ' data-bs-popper-config=\'{"strategy":"fixed"}\'><i class="bi bi-three-dots-vertical" aria-hidden="true"></i></button>' +
                '<ul class="dropdown-menu dropdown-menu-end">' +
                '<li><a class="dropdown-item" href="' + urls.urlEditar + '/' + id + '"><i class="bi bi-pencil" aria-hidden="true"></i> Editar</a></li>' +
                // Pestañas de la ficha (F3)
                '<li><a class="dropdown-item" href="' + ficha + '#viajes"><i class="bi bi-airplane" aria-hidden="true"></i> Viajes</a></li>' +
                '<li><a class="dropdown-item" href="' + ficha + '#pagos"><i class="bi bi-clock-history" aria-hidden="true"></i> Pagos</a></li>' +
                '<li><a class="dropdown-item" href="' + ficha + '#cuenta"><i class="bi bi-credit-card" aria-hidden="true"></i> Cuenta corriente</a></li>' +
                (c.esTitularFacturas ? '<li><a class="dropdown-item" href="' + ficha + '#facturas"><i class="bi bi-receipt" aria-hidden="true"></i> Facturas</a></li>' : '') +
                '</ul></div></div></td>';
            return html + '</tr>';
        }

        function paginacion(total, pagina, tamanio) {
            var paginas = Math.ceil(total / tamanio), $nav = $('#clPaginacion');
            if (paginas <= 1) { $nav.empty(); return; }
            var botones = [], visibles = [1, pagina - 1, pagina, pagina + 1, paginas].filter(function (n, i, a) { return n >= 1 && n <= paginas && a.indexOf(n) === i; }).sort(function (a, b) { return a - b; });
            botones.push(boton(pagina - 1, '<i class="bi bi-chevron-left" aria-hidden="true"></i><span class="visually-hidden">Anterior</span>', pagina === 1));
            visibles.forEach(function (n, i) {
                if (i > 0 && n - visibles[i - 1] > 1) botones.push('<span class="cl-pag-salto" aria-hidden="true">…</span>');
                botones.push(boton(n, String(n), false, n === pagina));
            });
            botones.push(boton(pagina + 1, '<span class="visually-hidden">Siguiente</span><i class="bi bi-chevron-right" aria-hidden="true"></i>', pagina === paginas));
            $nav.html(botones.join(''));
        }

        function boton(n, contenido, deshabilitado, actual) {
            return '<button type="button" class="cl-pag' + (actual ? ' is-actual' : '') + '" data-pagina="' + n + '"' +
                (deshabilitado ? ' disabled' : '') + (actual ? ' aria-current="page"' : '') +
                (/^\d+$/.test(contenido) ? ' aria-label="Página ' + n + '"' : '') + '>' + contenido + '</button>';
        }

        function marcarOrden() {
            $('.cl-orden').each(function () {
                var activo = $(this).data('orden') === estado.orden;
                var desc = estado.orden === 'alta';
                $(this).closest('th').attr('aria-sort', activo ? (desc ? 'descending' : 'ascending') : 'none');
                $(this).find('i').attr('class', 'bi ' + (activo ? (desc ? 'bi-sort-down' : 'bi-sort-alpha-down') : ''));
            });
        }

        // ---------------- Eventos
        function cambiar(cambios, inmediato) {
            $.extend(estado, cambios);
            $('#clLimpiar').toggleClass('d-none', !estado.q);
            clearTimeout(temporizador);
            var ejecutar = function () { escribirUrl(false); cargar(); };
            if (inmediato) ejecutar(); else temporizador = setTimeout(ejecutar, ESPERA_MS);
        }

        $texto.on('input', function () { cambiar({ q: $.trim(this.value), pagina: 1 }); });
        $texto.on('keydown', function (e) { if (e.key === 'Enter') { e.preventDefault(); cambiar({ q: $.trim(this.value), pagina: 1 }, true); } });
        $('#clLimpiar').on('click', function () { $texto.val('').trigger('focus'); cambiar({ q: '', pagina: 1 }, true); });
        $('#clTipo').on('change', function () { cambiar({ tipo: this.value, pagina: 1 }, true); });
        $('#clCategoria').on('change', function () { cambiar({ categoria: this.value, pagina: 1 }, true); });
        $('#clTamanio').on('change', function () { cambiar({ tamanio: parseInt(this.value, 10), pagina: 1 }, true); });
        $('#clInactivos').on('change', function () { cambiar({ inactivos: this.checked, pagina: 1 }, true); });
        $('.cl-orden').on('click', function () { cambiar({ orden: $(this).data('orden'), pagina: 1 }, true); });
        $('#clPaginacion').on('click', '.cl-pag:not([disabled])', function () {
            cambiar({ pagina: parseInt($(this).data('pagina'), 10) }, true);
            $('.clientes-listado')[0].scrollIntoView({ block: 'start' });
        });

        // Fila clickeable (sin pisar enlaces, botones ni el menú)
        $filas.on('click', '.cl-fila', function (e) {
            if ($(e.target).closest('a, button, .dropdown-menu').length) return;
            window.location.href = $(this).data('ficha');
        });

        window.addEventListener('popstate', function () { leerUrl(); cargar(); });

        leerUrl();
        $('#clLimpiar').toggleClass('d-none', !estado.q);
        cargar();
    }

    window.MatClientesListado = { init: init };
})(window, jQuery);
