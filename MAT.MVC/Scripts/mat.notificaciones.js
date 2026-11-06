/**
 * Campanita de avisos (Views/Shared/_Notificaciones.cshtml).
 * - El número muestra los avisos sin leer; se actualiza al entrar a cada página y cada 5 minutos.
 * - Al abrir el panel se ven los avisos y los nuevos se marcan como leídos (siguen resaltados hasta cerrarlo).
 * - "Leído" se guarda por usuario en la base: no reaparece en otra PC.
 * Expone window.MatNotificaciones.init({ urlLista, urlMarcar }).
 */
(function (window, $) {
    'use strict';

    var REFRESCO_MS = 5 * 60 * 1000;
    var ICONOS = {
        ViajeProximo: { icono: 'bi-bus-front', clase: 'ntf-ico--viaje' },
        UltimosLugares: { icono: 'bi-exclamation-triangle', clase: 'ntf-ico--ultimos' },
        ViajeCompleto: { icono: 'bi-check2-circle', clase: 'ntf-ico--completo' },
        Cumpleanios: { icono: 'bi-cake2', clase: 'ntf-ico--cumple' }
    };

    function esc(s) {
        return String(s == null ? '' : s).replace(/[&<>"']/g, function (c) {
            return { '&': '&amp;', '<': '&lt;', '>': '&gt;', '"': '&quot;', "'": '&#039;' }[c];
        });
    }

    /** "2026-11-06T15:30:46" (hora argentina) → "hace 2 h" */
    function hace(iso) {
        var m = /^(\d{4})-(\d{2})-(\d{2})T(\d{2}):(\d{2})/.exec(iso || '');
        if (!m) return '';
        var minutos = Math.round((Date.now() - new Date(+m[1], +m[2] - 1, +m[3], +m[4], +m[5]).getTime()) / 60000);
        if (minutos < 1) return 'recién';
        if (minutos < 60) return 'hace ' + minutos + ' min';
        var horas = Math.round(minutos / 60);
        if (horas < 24) return 'hace ' + horas + ' h';
        var dias = Math.round(horas / 24);
        return dias === 1 ? 'ayer' : 'hace ' + dias + ' días';
    }

    function init(opciones) {
        var $panel = $('#ntfPanel');
        if (!$panel.length) return;
        // El panel es position: fixed; dentro de la barra superior podría quedar recortado o mal ubicado
        $panel.appendTo(document.body);
        var $lista = $panel.find('[data-rol="lista"]'), $resumen = $panel.find('[data-rol="resumen"]');
        var $marcarTodas = $panel.find('[data-rol="marcar-todas"]');
        var $numero = $('#ntf [data-rol="numero"]'), $numeroLector = $('#ntf [data-rol="numero-lector"]');
        var avisos = [], abierto = false;

        function token() { return $('#ntfToken input[name="__RequestVerificationToken"]').val(); }

        function pintarNumero(n) {
            $numero.toggleClass('d-none', !n).text(n > 9 ? '9+' : n);
            $numeroLector.text(n ? n + (n === 1 ? ' aviso nuevo' : ' avisos nuevos') : 'Sin avisos nuevos');
            $('#ntf .ntf-campana').toggleClass('tiene-nuevos', n > 0);
        }

        function pintarLista() {
            var nuevos = avisos.filter(function (a) { return !a.leida || a.nuevoEnEstaVista; }).length;
            $resumen.text(avisos.length ? (nuevos ? nuevos + (nuevos === 1 ? ' nuevo' : ' nuevos') : 'Todo leído') : '');
            $marcarTodas.prop('disabled', !avisos.some(function (a) { return !a.leida; }));
            if (!avisos.length) {
                $lista.html('<li class="ntf-vacio"><i class="bi bi-bell-slash" aria-hidden="true"></i><p>No hay avisos por ahora.</p></li>');
                return;
            }
            $lista.html(avisos.map(function (a) {
                var t = ICONOS[a.tipo] || { icono: 'bi-info-circle', clase: '' };
                var nuevo = !a.leida || a.nuevoEnEstaVista;
                return '<li class="ntf-item' + (nuevo ? ' is-nuevo' : '') + '">' +
                    '<a href="' + esc(a.url || '#') + '" data-id="' + a.id + '">' +
                    '<span class="ntf-ico ' + t.clase + '" aria-hidden="true"><i class="bi ' + t.icono + '"></i></span>' +
                    '<span class="ntf-texto"><span class="ntf-titulo">' + esc(a.titulo) + '</span>' +
                    (a.detalle ? '<span class="ntf-detalle">' + esc(a.detalle) + '</span>' : '') +
                    '<span class="ntf-cuando">' + (nuevo ? '<span class="ntf-punto">Nuevo</span> · ' : '') + hace(a.fecha) + '</span></span>' +
                    '</a></li>';
            }).join(''));
        }

        function cargar() {
            return $.ajax({ url: opciones.urlLista, dataType: 'json', cache: false, global: false }).done(function (r) {
                if (!r.success) return;
                // Lo que ya se vio en este panel abierto queda resaltado hasta cerrarlo
                var vistos = {};
                avisos.forEach(function (a) { if (a.nuevoEnEstaVista) vistos[a.id] = true; });
                avisos = (r.data || []).map(function (a) { a.nuevoEnEstaVista = !!vistos[a.id]; return a; });
                pintarNumero(r.noLeidas || 0);
                if (abierto) pintarLista();
            });
        }

        function marcar(ids) {
            return $.ajax({
                type: 'POST', url: opciones.urlMarcar, dataType: 'json', global: false,
                data: { ids: ids ? ids.join(',') : '', __RequestVerificationToken: token() }
            });
        }

        /** Al abrir: se muestran y los no leídos pasan a leídos (siguen resaltados mientras el panel esté abierto). */
        $panel.on('show.bs.offcanvas', function () {
            abierto = true;
            pintarLista();
            cargar().done(function () {
                var ids = avisos.filter(function (a) { return !a.leida; }).map(function (a) { return a.id; });
                if (!ids.length) return;
                marcar(ids).done(function (r) {
                    if (!r || !r.success) return;
                    avisos.forEach(function (a) { if (!a.leida) { a.leida = true; a.nuevoEnEstaVista = true; } });
                    pintarNumero(0);
                    pintarLista();
                });
            });
        });
        $panel.on('hidden.bs.offcanvas', function () {
            abierto = false;
            avisos.forEach(function (a) { a.nuevoEnEstaVista = false; });
        });

        $marcarTodas.on('click', function () {
            marcar(null).done(function (r) {
                if (!r || !r.success) return;
                avisos.forEach(function (a) { a.leida = true; a.nuevoEnEstaVista = false; });
                pintarNumero(0);
                pintarLista();
            });
        });

        cargar();
        setInterval(function () { if (!document.hidden) cargar(); }, REFRESCO_MS);
    }

    window.MatNotificaciones = { init: init };
})(window, jQuery);
