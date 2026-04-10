(function (global, $) {
    "use strict";
    if (!$ || !$.ui || !$.ui.autocomplete) return;

    global.MatReportes = global.MatReportes || {};

    function fmtSalidaDdMmYyyy(fechaSalida) {
        if (!fechaSalida) return "";
        var s = String(fechaSalida).split("T")[0];
        var p = s.split("-");
        if (p.length === 3) return p[2] + "/" + p[1] + "/" + p[0];
        return s;
    }

    function buildViajeLabel(x) {
        var desc = (x && x.descripcion) ? x.descripcion : "";
        var fs = fmtSalidaDdMmYyyy(x && x.fechaSalida);
        if (desc && fs) return desc + " — " + fs;
        return desc || fs || "";
    }

    function mapRowsToItems(rows) {
        return $.map(rows || [], function (x) {
            var id = x.id || "";
            return {
                label: buildViajeLabel(x),
                value: id
            };
        });
    }

    /**
     * Autocompletar de viaje por nombre + GUID oculto (reportes Admin).
     * @param {{ urlBuscarViajes: string, inputSearch: string|JQuery, inputHidden: string|JQuery, buttonLista?: string|JQuery }} opts
     */
    global.MatReportes.initViajeAutocomplete = function (opts) {
        var $search = typeof opts.inputSearch === "string" ? $(opts.inputSearch) : opts.inputSearch;
        var $hidden = typeof opts.inputHidden === "string" ? $(opts.inputHidden) : opts.inputHidden;
        var url = opts.urlBuscarViajes;
        var $btn = opts.buttonLista
            ? (typeof opts.buttonLista === "string" ? $(opts.buttonLista) : opts.buttonLista)
            : $();
        if (!$search.length || !$hidden.length || !url) return;

        var minLen = 2;

        $search.autocomplete({
            minLength: minLen,
            delay: 250,
            source: function (request, response) {
                var showRecent = $search.data("matViajeRecent");
                if (showRecent) {
                    $search.removeData("matViajeRecent");
                    $.getJSON(url, { recent: true })
                        .done(function (res) {
                            if (!res || !res.ok) {
                                response([]);
                                return;
                            }
                            response(mapRowsToItems(res.data));
                        })
                        .fail(function () { response([]); });
                    return;
                }
                $.getJSON(url, { q: request.term })
                    .done(function (res) {
                        if (!res || !res.ok) {
                            response([]);
                            return;
                        }
                        response(mapRowsToItems(res.data));
                    })
                    .fail(function () { response([]); });
            },
            select: function (event, ui) {
                $search.val(ui.item.label);
                $hidden.val(ui.item.value || "");
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

        if ($btn.length) {
            $btn.on("click", function () {
                var inst = $search.autocomplete("instance");
                if (!inst) return;
                $search.data("matViajeRecent", true);
                var prev = inst.options.minLength;
                inst.options.minLength = 0;
                $search.autocomplete("search", "");
                setTimeout(function () {
                    inst.options.minLength = prev;
                }, 0);
            });
        }
    };
})(window, window.jQuery);
