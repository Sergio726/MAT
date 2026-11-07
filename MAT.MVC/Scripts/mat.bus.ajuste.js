/* Reservas: el dibujo del colectivo se achica para entrar entero en su panel (2026-11-07).
   Con el menú lateral expandido, a 1366px el panel tiene ~690px y el dibujo mide ~750px (los asientos van en
   posición absoluta, con el frente del coche a la izquierda): quedaba una barra de scroll y asientos fuera de vista.
   Desde 768px se aplica CSS zoom al plano de cada piso según el lugar real: se achica si no entra (hasta 65 %;
   debajo de eso queda el scroll de siempre) y se agranda si sobra (hasta 115 %). Reemplaza a las escalas fijas por
   ancho de ventana de modern-dashboard.css, que no consideraban el menú lateral. En el celular siguen esas escalas.
   Se recalcula al cambiar de piso, al redimensionar y al expandir o contraer el menú. */
(function ($) {
    "use strict";

    var MINIMO = 0.65;   // tablets: la escala fija anterior llegaba a 0,7 entre 768 y 1023px
    var MAXIMO = 1.15;   // en pantallas anchas se agranda, como hacía la escala fija anterior (1,15 desde 1600px)

    function ajustar() {
        // En el celular manda la escala fija de modern-dashboard.css
        if (!window.matchMedia("(min-width: 768px)").matches) { $("#bus-tabs .ui-tabs-panel > ul").css("zoom", ""); return; }
        var $panelVisible = $("#bus-tabs .ui-tabs-panel:visible").first();
        if (!$panelVisible.length) return;
        var $planos = $("#bus-tabs .ui-tabs-panel > ul");
        if (!$planos.length) return;

        // Medidas visuales (getBoundingClientRect): hasta dónde llega el último asiento o marcador contra el borde
        // derecho del panel. Con zoom las medidas de layout (offsetLeft) no son fiables; por eso se mide lo que se
        // ve y, si todavía no entra, se corrige el factor (hasta dos veces).
        var panel = $panelVisible[0];
        var plano = $panelVisible.children("ul")[0];
        if (!plano) return;

        function exceso() {
            var limite = panel.getBoundingClientRect().right - 8;   // aire al final
            var derecha = 0;
            $(plano).children("li").each(function () {
                var r = this.getBoundingClientRect();
                if (r.width && r.right > derecha) derecha = r.right;
            });
            var inicio = plano.getBoundingClientRect().left;
            return { ocupa: derecha - inicio, cabe: limite - inicio };
        }

        $planos.css("zoom", "");
        var m = exceso();
        if (m.ocupa <= 0 || m.cabe <= 0) return;
        // Primer factor: lo que entra, entre el mínimo y el máximo
        var factor = Math.min(MAXIMO, Math.max(MINIMO, Math.floor((m.cabe / m.ocupa) * 1000) / 1000));
        if (factor === 1) return;
        $planos.css("zoom", factor);
        // Verificación con lo que se ve: si todavía no entra, se corrige (hasta dos veces)
        for (var intento = 0; intento < 2 && factor > MINIMO; intento++) {
            m = exceso();
            if (m.ocupa <= m.cabe) break;
            factor = Math.max(MINIMO, Math.floor(factor * (m.cabe / m.ocupa) * 1000) / 1000);
            $planos.css("zoom", factor);
        }
    }

    var pendiente = null;
    function programar() {
        if (pendiente) cancelAnimationFrame(pendiente);
        pendiente = requestAnimationFrame(function () { pendiente = null; ajustar(); });
    }

    $(function () {
        var contenedor = document.getElementById("bus-tabs");
        if (!contenedor) return;
        programar();
        $(contenedor).on("tabsactivate", programar);
        $(window).on("resize", programar);
        // El ancho del panel también cambia al expandir o contraer el menú lateral (sin evento de ventana).
        // Solo cuenta el ANCHO: el alto cambia con el propio zoom y, si se recalculara por eso, quedaría en un
        // bucle (zoom → cambia el alto → recalcula → saca el zoom → cambia el alto…).
        var observado = document.getElementById("container") || contenedor.parentElement || contenedor;
        var anchoAnterior = observado.clientWidth;
        if (window.ResizeObserver) {
            new ResizeObserver(function () {
                var ancho = observado.clientWidth;
                if (ancho === anchoAnterior) return;
                anchoAnterior = ancho;
                programar();
            }).observe(observado);
        }
    });
})(jQuery);
