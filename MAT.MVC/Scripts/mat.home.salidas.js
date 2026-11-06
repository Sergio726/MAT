/**
 * Panel "Próximas salidas" del inicio (Views/Home/_ProximasSalidas.cshtml).
 * Expone window.MatProximasSalidas.init({ contenedor, url, urlReserva }).
 */
(function (window, $) {
    'use strict';

    var VISIBLES = 8;
    var MESES = ['ene', 'feb', 'mar', 'abr', 'may', 'jun', 'jul', 'ago', 'sep', 'oct', 'nov', 'dic'];
    var ESTADOS = {
        completo: { texto: 'Completo', clase: 'hs-chip--completo' },
        ultimos: { texto: 'Últimos lugares', clase: 'hs-chip--ultimos' },
        disponible: { texto: 'Disponible', clase: 'hs-chip--disponible' },
        sinPasajes: { texto: 'Sin pasajes cargados', clase: 'hs-chip--sin' }
    };

    function esc(s) {
        return String(s == null ? '' : s).replace(/[&<>"']/g, function (c) {
            return { '&': '&amp;', '<': '&lt;', '>': '&gt;', '"': '&quot;', "'": '&#039;' }[c];
        });
    }

    function cuando(dias) {
        if (dias <= 0) return 'Sale hoy';
        if (dias === 1) return 'Sale mañana';
        return 'En ' + dias + ' días';
    }

    function fila(v, urlReserva, repetidos) {
        var m = /^(\d{4})-(\d{2})-(\d{2})/.exec(v.fechaSalida || '');
        var dia = m ? +m[3] : '', mes = m ? MESES[+m[2] - 1] : '';
        var estado = ESTADOS[v.estado] || ESTADOS.disponible;
        var ocupados = Math.max(0, v.total - v.libres);
        var pct = v.total > 0 ? Math.round(ocupados * 100 / v.total) : 0;
        var hora = v.horaSalida ? ' · ' + esc(v.horaSalida) + ' h' : '';
        // El paquete solo agrega ruido salvo cuando hay dos viajes con el mismo nombre (por ejemplo, con noche y sin noche)
        var mostrarPaquete = v.paquete && repetidos[String(v.descripcion || '').toUpperCase()] > 1;
        var detalle = v.vendidos + ' vendidos' + (v.preReservas ? ' · ' + v.preReservas + ' pre-reserva' + (v.preReservas === 1 ? '' : 's') : '');

        return '<li class="hs-fila' + (v.diasFaltan <= 0 ? ' is-hoy' : '') + '">' +
            '<div class="hs-fecha" aria-hidden="true"><span class="hs-dia">' + dia + '</span><span class="hs-mes">' + mes + '</span></div>' +
            '<div class="hs-info">' +
                '<a class="hs-viaje" href="' + esc(urlReserva) + '?viajeid=' + encodeURIComponent(v.viajeId) + '">' + esc(v.descripcion) + '</a>' +
                (mostrarPaquete ? '<div class="hs-paquete" title="' + esc(v.paquete) + '">' + esc(v.paquete) + '</div>' : '') +
                '<div class="hs-cuando">' + cuando(v.diasFaltan) + hora + '</div>' +
            '</div>' +
            '<div class="hs-ocupacion">' +
                '<div class="hs-cifras"><strong>' + v.libres + '</strong> libre' + (v.libres === 1 ? '' : 's') + ' de ' + v.total +
                    ' <span class="hs-chip ' + estado.clase + '">' + estado.texto + '</span></div>' +
                '<div class="hs-barra" role="img" aria-label="' + pct + '% ocupado: ' + esc(detalle) + '"><span style="width:' + pct + '%"></span></div>' +
                '<div class="hs-detalle">' + esc(detalle) + '</div>' +
            '</div>' +
        '</li>';
    }

    function init(opciones) {
        var $c = $(opciones.contenedor);
        if (!$c.length) return;
        var $lista = $c.find('[data-rol="lista"]'), $resumen = $c.find('[data-rol="resumen"]'), $mas = $c.find('[data-rol="mas"]');

        $.ajax({ url: opciones.url, dataType: 'json', cache: false }).done(function (r) {
            if (!r.success) { $resumen.text(r.message || 'No se pudieron cargar las salidas.'); return; }
            var viajes = r.data || [];
            if (!viajes.length) {
                $resumen.text('No hay salidas en los próximos 40 días.');
                return;
            }
            var hoy = viajes.filter(function (v) { return v.diasFaltan <= 0; }).length;
            var completos = viajes.filter(function (v) { return v.estado === 'completo'; }).length;
            var partes = [viajes.length + ' viaje' + (viajes.length === 1 ? '' : 's') + ' en los próximos 40 días'];
            if (hoy) partes.push(hoy + ' sale' + (hoy === 1 ? '' : 'n') + ' hoy');
            if (completos) partes.push(completos + ' completo' + (completos === 1 ? '' : 's'));
            $resumen.text(partes.join(' · '));

            var repetidos = {};
            viajes.forEach(function (v) { var k = String(v.descripcion || '').toUpperCase(); repetidos[k] = (repetidos[k] || 0) + 1; });
            $lista.html(viajes.map(function (v, i) {
                return fila(v, opciones.urlReserva, repetidos).replace('<li class="hs-fila', '<li class="hs-fila' + (i >= VISIBLES ? ' d-none hs-extra' : ''));
            }).join(''));

            var extras = viajes.length - VISIBLES;
            if (extras > 0) {
                $mas.removeClass('d-none').text('Ver ' + extras + ' más').on('click', function () {
                    var abierto = $mas.attr('aria-expanded') === 'true';
                    $lista.find('.hs-extra').toggleClass('d-none', abierto);
                    $mas.attr('aria-expanded', abierto ? 'false' : 'true').text(abierto ? 'Ver ' + extras + ' más' : 'Ver menos');
                });
            }
        }).fail(function () {
            $resumen.text('No se pudieron cargar las salidas.');
        }).always(function () {
            $c.attr('aria-busy', 'false');
        });
    }

    window.MatProximasSalidas = { init: init };
})(window, jQuery);
