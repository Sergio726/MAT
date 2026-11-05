/**
 * Indicadores de carga para todo el sitio (módulo Clientes F5). Lo carga _Layout.
 * - Barra fina arriba si un pedido AJAX o un cambio de página tarda más de 300 ms (antes no había ninguna señal).
 * - MatUI.cargando(boton, true|false): deshabilita el botón y muestra un spinner mientras dura una acción.
 */
(function (window, $) {
    'use strict';

    var ESPERA_MS = 300;
    var pendientes = 0, temporizador = null, $barra = null;

    function barra() {
        if (!$barra) {
            $barra = $('<div class="mat-carga" role="progressbar" aria-label="Cargando" aria-hidden="true"><span></span></div>').appendTo(document.body);
        }
        return $barra;
    }

    function mostrar() {
        clearTimeout(temporizador);
        temporizador = setTimeout(function () { barra().addClass('is-visible').attr('aria-hidden', 'false'); }, ESPERA_MS);
    }

    function ocultar() {
        clearTimeout(temporizador);
        if ($barra) $barra.removeClass('is-visible').attr('aria-hidden', 'true');
    }

    $(document).ajaxSend(function (e, xhr, opciones) {
        if (opciones && opciones.global === false) return;
        pendientes++;
        mostrar();
    }).ajaxComplete(function (e, xhr, opciones) {
        if (opciones && opciones.global === false) return;
        pendientes = Math.max(0, pendientes - 1);
        if (!pendientes) ocultar();
    });

    // Cambio de página (enlaces y formularios): si el servidor tarda, se ve la barra.
    window.addEventListener('beforeunload', mostrar);
    // Volver con "atrás" desde la caché del navegador: sin barra colgada.
    window.addEventListener('pageshow', ocultar);

    function cargando(boton, activo) {
        var $b = $(boton);
        if (!$b.length) return;
        if (activo) {
            if ($b.data('matCargando')) return;
            $b.data('matCargando', true).prop('disabled', true).attr('aria-busy', 'true')
                .prepend('<span class="spinner-border spinner-border-sm me-1 mat-cargando-spinner" aria-hidden="true"></span>');
        } else {
            $b.removeData('matCargando').prop('disabled', false).removeAttr('aria-busy').find('.mat-cargando-spinner').remove();
        }
    }

    window.MatUI = { cargando: cargando, mostrarCarga: mostrar, ocultarCarga: ocultar };
})(window, jQuery);
