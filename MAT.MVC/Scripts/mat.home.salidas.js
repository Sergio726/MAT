/**
 * Panel "Próximas salidas" del inicio (Views/Home/_ProximasSalidas.cshtml).
 * Expone window.MatProximasSalidas.init({ contenedor, url, urlReserva }).
 */
(function (window, $) {
    'use strict';

    var VISIBLES = 3;
    var MESES = ['ene', 'feb', 'mar', 'abr', 'may', 'jun', 'jul', 'ago', 'sep', 'oct', 'nov', 'dic'];
    // Solo los estados que avisan algo llevan etiqueta: "Disponible" en todos los viajes no informaba nada.
    var ESTADOS = {
        completo: { texto: 'Completo', clase: 'hs-chip--completo' },
        ultimos: { texto: 'Últimos lugares', clase: 'hs-chip--ultimos' },
        sinPasajes: { texto: 'Sin pasajes cargados', clase: 'hs-chip--sin' }
    };

    function plural(n, singular, varios) { return n + ' ' + (n === 1 ? singular : varios); }

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
        var estado = ESTADOS[v.estado];
        var hora = v.horaSalida ? ' · ' + esc(v.horaSalida) + ' h' : '';
        // El paquete solo agrega ruido salvo cuando hay dos viajes con el mismo nombre (por ejemplo, con noche y sin noche)
        var mostrarPaquete = v.paquete && repetidos[String(v.descripcion || '').toUpperCase()] > 1;

        // Una sola métrica: la barra es la ocupación (vendidos + pre-reservas sobre el total) y el texto dice lo mismo.
        var total = v.total || 0;
        var pctVendidos = total ? v.vendidos * 100 / total : 0;
        var pctReservas = total ? v.preReservas * 100 / total : 0;
        var pctOcupado = Math.round(pctVendidos + pctReservas);
        var principal = total ? '<strong>' + v.vendidos + '</strong> de ' + total + ' vendidos' : 'Sin pasajes cargados';
        var apoyo = total ? [plural(v.libres, 'libre', 'libres')].concat(v.preReservas ? [plural(v.preReservas, 'pre-reserva', 'pre-reservas')] : []).join(' · ') : '';
        var lectura = total ? v.vendidos + ' de ' + total + ' vendidos, ' + apoyo.replace(/ · /g, ', ') + '. ' + pctOcupado + '% ocupado' : '';

        return '<li class="hs-fila' + (v.diasFaltan <= 0 ? ' is-hoy' : '') + '">' +
            '<div class="hs-fecha" aria-hidden="true"><span class="hs-dia">' + dia + '</span><span class="hs-mes">' + mes + '</span></div>' +
            '<div class="hs-info">' +
                '<a class="hs-viaje" href="' + esc(urlReserva) + '?viajeid=' + encodeURIComponent(v.viajeId) + '">' + esc(v.descripcion) + '</a>' +
                (mostrarPaquete ? '<div class="hs-paquete" title="' + esc(v.paquete) + '">' + esc(v.paquete) + '</div>' : '') +
                '<div class="hs-cuando">' + cuando(v.diasFaltan) + hora + '</div>' +
            '</div>' +
            '<div class="hs-ocupacion">' +
                '<div class="hs-cifras">' + principal +
                    (estado && v.estado !== 'sinPasajes' ? ' <span class="hs-chip ' + estado.clase + '">' + estado.texto + '</span>' : '') + '</div>' +
                (total ? '<div class="hs-barra" role="progressbar" aria-valuemin="0" aria-valuemax="100" aria-valuenow="' + pctOcupado + '"' +
                    ' aria-valuetext="' + esc(lectura) + '" aria-label="Ocupación de ' + esc(v.descripcion) + '">' +
                    '<span class="hs-barra-vendidos" style="width:' + pctVendidos.toFixed(1) + '%"></span>' +
                    '<span class="hs-barra-reservas" style="width:' + pctReservas.toFixed(1) + '%"></span></div>' +
                    '<div class="hs-detalle">' + esc(apoyo) + '</div>' : '') +
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
