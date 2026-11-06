/* Menú lateral angosto ("rail" de 64 px: solo íconos con tooltip). Bloque D del plan del panel principal.
   La preferencia queda en localStorage; un script al comienzo del <body> (_Layout) aplica la clase antes de
   pintar, para que no parpadee. Solo en escritorio: en el celular el menú sigue siendo el panel deslizable. */
(function ($) {
    "use strict";

    var CLAVE = "mat.menu.rail";
    var tooltips = [];

    function esEscritorio() {
        return window.matchMedia("(min-width: 769px)").matches;
    }

    function guardar(rail) {
        try { localStorage.setItem(CLAVE, rail ? "1" : "0"); } catch (e) { }
    }

    function quitarTooltips() {
        tooltips.forEach(function (t) { t.dispose(); });
        tooltips = [];
    }

    function crearTooltips() {
        if (typeof bootstrap === "undefined" || !esEscritorio()) return;
        $("#menu .menu-0 > li > a").each(function () {
            var texto = $.trim($(this).children("span").first().text());
            if (!texto) return;
            tooltips.push(new bootstrap.Tooltip(this, { title: texto, placement: "right", trigger: "hover focus", container: "body" }));
        });
    }

    function aplicar(rail) {
        $("body").toggleClass("menu-rail", rail);
        var texto = rail ? "Expandir menú" : "Contraer menú";
        $("#menuRailToggle").attr({ "aria-expanded": rail ? "false" : "true", "aria-label": texto, title: texto });
        quitarTooltips();
        if (rail) crearTooltips();
    }

    $(function () {
        aplicar($("body").hasClass("menu-rail"));

        $("#menuRailToggle").on("click", function () {
            var rail = !$("body").hasClass("menu-rail");
            guardar(rail);
            aplicar(rail);
        });

        // En el rail los submenús no entran: tocar un grupo expande el menú y abre ese grupo.
        // Corta la propagación para que el data-api de Bootstrap (escucha en document) no lo alterne.
        $("#menu").on("click", ".menu-0 > li.hasSubmenu > a", function (e) {
            if (!$("body").hasClass("menu-rail") || !esEscritorio()) return;
            e.preventDefault();
            e.stopPropagation();
            guardar(false);
            aplicar(false);
            var ul = $(this).siblings("ul.collapse")[0];
            if (ul && typeof bootstrap !== "undefined") bootstrap.Collapse.getOrCreateInstance(ul, { toggle: false }).show();
        });
    });
})(jQuery);
