(function (global, $) {
    "use strict";
    if (!$ || !$.ui || !$.ui.autocomplete) return;

    global.MatReportes = global.MatReportes || {};

    /**
     * Autocompletar de viaje por nombre + GUID oculto (reportes Admin).
     * @param {{ urlBuscarViajes: string, inputSearch: string|JQuery, inputHidden: string|JQuery }} opts
     */
    global.MatReportes.initViajeAutocomplete = function (opts) {
        var $search = typeof opts.inputSearch === "string" ? $(opts.inputSearch) : opts.inputSearch;
        var $hidden = typeof opts.inputHidden === "string" ? $(opts.inputHidden) : opts.inputHidden;
        var url = opts.urlBuscarViajes;
        if (!$search.length || !$hidden.length || !url) return;

        $search.autocomplete({
            minLength: 2,
            delay: 250,
            source: function (request, response) {
                $.getJSON(url, { q: request.term })
                    .done(function (res) {
                        if (!res || !res.ok) {
                            response([]);
                            return;
                        }
                        var rows = res.data || [];
                        response($.map(rows, function (x) {
                            return {
                                label: x.descripcion || "",
                                value: x.id || ""
                            };
                        }));
                    })
                    .fail(function () { response([]); });
            },
            select: function (event, ui) {
                $search.val(ui.item.label);
                $hidden.val(ui.item.value);
                $search.data("matViajeLabel", ui.item.label);
                return false;
            },
            focus: function (event, ui) {
                event.preventDefault();
                $search.val(ui.item.label);
            }
        });

        $search.on("input", function () {
            if ($search.val() !== ($search.data("matViajeLabel") || "")) {
                $hidden.val("");
                $search.removeData("matViajeLabel");
            }
        });
    };
})(window, window.jQuery);
