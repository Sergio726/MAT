/* DistribucionCoche — búsqueda, toggle mapa/lista, acciones butaca, vínculos tutor↔menor */
(function (window, $) {
    "use strict";

    var searchMatches = [];
    var searchIndex = -1;
    var searchTimer = null;
    var $activeSeatMenu = null;

    function normalizeSearchText(text) {
        return (text || "").toLowerCase().normalize("NFD").replace(/[\u0300-\u036f]/g, "");
    }

    function mostrarErrorDistribucion(msg) {
        $(".distribucion-error-banner").remove();
        var $banner = $(
            '<div class="distribucion-error-banner" style="background:#ffebee;color:#c62828;padding:10px 15px;border-left:4px solid #c62828;margin:8px 0;border-radius:4px;font-size:13px;display:flex;align-items:center;gap:8px;">' +
            '<i class="bi bi-exclamation-triangle-fill" style="font-size:16px;flex-shrink:0;"></i>' +
            '<span style="flex:1;"></span>' +
            '<button type="button" style="background:none;border:none;color:#c62828;font-size:18px;cursor:pointer;padding:0 4px;line-height:1;" title="Cerrar">&times;</button>' +
            "</div>"
        );
        $banner.find("span").first().text(msg);
        $banner.find("button").on("click", function () {
            $banner.fadeOut(200, function () { $banner.remove(); });
        });
        $(".bus-distribution-page").prepend($banner);
        setTimeout(function () {
            $banner.fadeOut(300, function () { $banner.remove(); });
        }, 15000);
    }

    function parseTutoresList(tutores) {
        if (Array.isArray(tutores)) {
            return tutores;
        }
        if (typeof tutores === "string" && tutores.length > 0) {
            try {
                return JSON.parse(tutores);
            } catch (e) {
                return [];
            }
        }
        return [];
    }

    function markTutorsFallback(list) {
        $(".bus-seat").each(function () {
            var $seat = $(this);
            if ($seat.attr("data-es-tutor") === "1") {
                return;
            }
            var id = $seat.attr("id");
            if (id && list.indexOf(id) >= 0) {
                $seat.addClass("Tutor");
            }
        });
    }

    function hideTutoresLoading() {
        $("#tutores-loading, #legend-tutores-loading").hide();
    }

    function getActiveView() {
        return $("#dc-btn-lista").hasClass("active") ? "lista" : "mapa";
    }

    function setActiveView(view) {
        var isLista = view === "lista";
        $("#dc-btn-mapa").toggleClass("active", !isLista);
        $("#dc-btn-lista").toggleClass("active", isLista);
        if (isLista) {
            $("#dc-list-view").removeAttr("hidden");
            $("#dc-map-view").attr("hidden", "hidden");
        } else {
            $("#dc-list-view").attr("hidden", "hidden");
            $("#dc-map-view").removeAttr("hidden");
            redrawVinculos();
        }
        try {
            sessionStorage.setItem("dc-view-mode", view);
        } catch (e) { /* ignore */ }
    }

    function applyBusSearch(query) {
        var q = normalizeSearchText(query);
        $(".bus-seat").removeClass("bus-seat-match bus-seat-dimmed bus-seat-current");
        $(".dc-list-row").removeClass("dc-list-row-match dc-list-row-dimmed dc-list-row-current");
        searchMatches = [];
        searchIndex = -1;
        $("#buscar-resultado").text("");

        if (!q) {
            return;
        }

        $(".bus-seat").each(function () {
            var key = normalizeSearchText($(this).attr("data-search") || "");
            if (key.indexOf(q) >= 0) {
                searchMatches.push(this);
                $(this).addClass("bus-seat-match");
            } else {
                $(this).addClass("bus-seat-dimmed");
            }
        });

        $(".dc-list-row").each(function () {
            var key = normalizeSearchText($(this).attr("data-search") || "");
            if (key.indexOf(q) >= 0) {
                searchMatches.push(this);
                $(this).addClass("dc-list-row-match");
            } else {
                $(this).addClass("dc-list-row-dimmed");
            }
        });

        if (searchMatches.length > 0) {
            searchIndex = 0;
            focusSearchMatch(0);
            $("#buscar-resultado").text(
                searchMatches.length + (searchMatches.length === 1 ? " coincidencia" : " coincidencias")
            );
        } else {
            $("#buscar-resultado").text("Sin coincidencias");
        }
    }

    function focusSearchMatch(idx) {
        $(".bus-seat").removeClass("bus-seat-current");
        $(".dc-list-row").removeClass("dc-list-row-current");
        if (searchMatches.length === 0) {
            return;
        }
        var el = searchMatches[idx];
        if ($(el).hasClass("bus-seat")) {
            $(el).addClass("bus-seat-current");
        } else {
            $(el).addClass("dc-list-row-current");
        }
        if (el.scrollIntoView) {
            el.scrollIntoView({ behavior: "smooth", block: "center", inline: "nearest" });
        }
    }

    function initBusSeatTooltips() {
        if (typeof bootstrap === "undefined" || !bootstrap.Tooltip) {
            return;
        }
        [].slice.call(document.querySelectorAll(".bus-seat[data-bs-toggle='tooltip']")).forEach(function (el) {
            new bootstrap.Tooltip(el);
        });
    }

    function closeSeatActionsMenu() {
        if ($activeSeatMenu) {
            $activeSeatMenu.remove();
            $activeSeatMenu = null;
        }
        $(document).off("click.distribSeatMenu keydown.distribSeatMenu");
    }

    function openDetalleFacturaModal(facturaId) {
        if (!facturaId) {
            mostrarErrorDistribucion("Esta butaca no tiene factura asociada.");
            return;
        }
        var $body = $("#modalDetalleFacturaDistribucionBody");
        $body.html('<div class="text-center text-muted py-4"><span class="spinner-border spinner-border-sm" role="status"></span> Cargando…</div>');
        var modalEl = document.getElementById("modalDetalleFacturaDistribucion");
        var modal = bootstrap.Modal.getOrCreateInstance(modalEl);
        modal.show();
        $.get("/PersonaCliente/DetalleFacturaFast", { facturaid: facturaId })
            .done(function (html) { $body.html(html); })
            .fail(function () {
                $.get("/PersonaCliente/DetalleFactura", { facturaid: facturaId })
                    .done(function (html) { $body.html(html); })
                    .fail(function () {
                        $body.html('<div class="alert alert-danger mb-0">No se pudo cargar el detalle de la factura.</div>');
                    });
            });
    }

    function abrirCambioButacaDesdeDistribucion(pasajeroId, pasajeId) {
        if (window.opener && !window.opener.closed && typeof window.opener.abrirCambioButaca === "function") {
            window.opener.abrirCambioButaca(pasajeroId, pasajeId);
            window.opener.focus();
            return;
        }
        var url = "/PersonaCliente/ElegirNuevaButaca?shell=1&anteriorid=" +
            encodeURIComponent(pasajeroId) + "&pasajeid=" + encodeURIComponent(pasajeId);
        window.open(url, "CambioButaca", "width=960,height=720,resizable=yes,scrollbars=yes");
    }

    function showSeatActionsMenu($source, evt) {
        closeSeatActionsMenu();
        var pasajeId = ($source.attr("data-pasaje-id") || "").trim();
        var pasajeroId = ($source.attr("data-pasajero-id") || $source.attr("id") || "").trim();
        var facturaId = ($source.attr("data-factura-id") || "").trim();
        if (!pasajeId || !pasajeroId) {
            return;
        }

        var nombre = ($source.find(".spnB").text() || $source.attr("data-bs-title") || "").trim();
        var codigo = ($source.attr("data-codigo") || "").trim();
        var titulo = nombre || ("Butaca " + codigo);

        var $menu = $('<div class="bus-seat-actions-menu" role="menu"></div>');
        $menu.append('<div class="bus-seat-actions-menu__title"></div>');
        $menu.find(".bus-seat-actions-menu__title").text(titulo);

        if (facturaId) {
            $menu.append(
                '<button type="button" class="bus-seat-actions-menu__btn" data-action="factura">' +
                '<i class="bi bi-receipt"></i> Ver detalle de factura</button>'
            );
        }
        $menu.append(
            '<button type="button" class="bus-seat-actions-menu__btn" data-action="cambio">' +
            '<i class="bi bi-arrow-left-right"></i> Cambiar butaca</button>'
        );
        $menu.append(
            '<button type="button" class="bus-seat-actions-menu__btn bus-seat-actions-menu__btn--muted" data-action="cerrar">Cerrar</button>'
        );

        $("body").append($menu);
        $activeSeatMenu = $menu;

        var left = (evt && evt.pageX ? evt.pageX : $source.offset().left) + 8;
        var top = (evt && evt.pageY ? evt.pageY : $source.offset().top) + 8;
        $menu.css({ left: left + "px", top: top + "px" });

        $menu.on("click", ".bus-seat-actions-menu__btn", function (e) {
            e.stopPropagation();
            var action = $(this).data("action");
            closeSeatActionsMenu();
            if (action === "factura") {
                openDetalleFacturaModal(facturaId);
            } else if (action === "cambio") {
                abrirCambioButacaDesdeDistribucion(pasajeroId, pasajeId);
            }
        });

        setTimeout(function () {
            $(document).on("click.distribSeatMenu", function () { closeSeatActionsMenu(); });
            $(document).on("keydown.distribSeatMenu", function (e) {
                if (e.key === "Escape") { closeSeatActionsMenu(); }
            });
        }, 0);
    }

    function seatElementFromListRow($row) {
        var pasajeroId = ($row.attr("data-pasajero-id") || "").trim();
        var butacaNro = ($row.attr("data-butaca-nro") || "").trim();
        var $seat = pasajeroId ? $("#" + pasajeroId) : $();
        if (!$seat.length && butacaNro) {
            $seat = $('.bus-seat[data-butaca-nro="' + butacaNro + '"]').first();
        }
        if ($seat.length) {
            return $seat;
        }
        return $row;
    }

    function redrawVinculos() {
        var $svg = $("#dc-vinculos-svg");
        if (!$svg.length) {
            return;
        }
        $svg.empty();
        if (!$("#dc-toggle-vinculos").is(":checked") || getActiveView() !== "mapa") {
            return;
        }

        var $wrap = $("#cole1");
        if (!$wrap.length) {
            return;
        }

        var wrapRect = $wrap[0].getBoundingClientRect();

        $(".bus-seat.Menor[data-tutor-butaca-nro]").each(function () {
            var tutorNro = ($(this).attr("data-tutor-butaca-nro") || "").trim();
            if (!tutorNro) {
                return;
            }
            var $tutorSeat = $('.bus-seat[data-butaca-nro="' + tutorNro + '"]').first();
            if (!$tutorSeat.length) {
                return;
            }
            var r1 = this.getBoundingClientRect();
            var r2 = $tutorSeat[0].getBoundingClientRect();
            var x1 = r1.left + r1.width / 2 - wrapRect.left;
            var y1 = r1.top + r1.height / 2 - wrapRect.top;
            var x2 = r2.left + r2.width / 2 - wrapRect.left;
            var y2 = r2.top + r2.height / 2 - wrapRect.top;
            var ns = "http://www.w3.org/2000/svg";
            var line = document.createElementNS(ns, "line");
            line.setAttribute("x1", x1);
            line.setAttribute("y1", y1);
            line.setAttribute("x2", x2);
            line.setAttribute("y2", y2);
            $svg[0].appendChild(line);
        });
    }

    function initDistribucionCoche(options) {
        options = options || {};
        var viajeId = options.viajeId || "";

        initBusSeatTooltips();

        var savedView = null;
        try {
            savedView = sessionStorage.getItem("dc-view-mode");
        } catch (e) { /* ignore */ }

        if (savedView === "lista" || (window.matchMedia && window.matchMedia("(max-width: 768px)").matches && savedView !== "mapa")) {
            setActiveView("lista");
        } else {
            setActiveView("mapa");
        }

        $("#dc-btn-mapa").on("click", function () { setActiveView("mapa"); });
        $("#dc-btn-lista").on("click", function () { setActiveView("lista"); });

        $("#dc-toggle-vinculos").on("change", redrawVinculos);
        $(window).on("resize", function () {
            clearTimeout(window._dcVinculosTimer);
            window._dcVinculosTimer = setTimeout(redrawVinculos, 150);
        });
        setTimeout(redrawVinculos, 300);

        $(document).on("click", ".bus-seat--occupied", function (e) {
            e.preventDefault();
            e.stopPropagation();
            showSeatActionsMenu($(this), e);
        });

        $(document).on("click", ".dc-list-action-btn", function (e) {
            e.preventDefault();
            e.stopPropagation();
            var $row = $(this).closest(".dc-list-row");
            showSeatActionsMenu(seatElementFromListRow($row), e);
        });

        $("#buscar-pasajero").on("input", function () {
            var val = $(this).val();
            clearTimeout(searchTimer);
            searchTimer = setTimeout(function () {
                applyBusSearch(val);
            }, 200);
        });

        $("#buscar-pasajero").on("keydown", function (e) {
            if (e.key === "Enter" || e.key === "F3") {
                e.preventDefault();
                if (searchMatches.length === 0) {
                    return;
                }
                searchIndex = (searchIndex + 1) % searchMatches.length;
                focusSearchMatch(searchIndex);
            }
        });

        if (!viajeId) {
            hideTutoresLoading();
            return;
        }

        var hasServerTutors = $(".bus-seat[data-es-tutor='1']").length > 0;
        if (hasServerTutors) {
            hideTutoresLoading();
            return;
        }

        $.ajax({
            url: "/Reserva/GetListTutoresByViajeID",
            data: { sViajeId: viajeId },
            dataType: "json",
            success: function (data) {
                if (data.error) {
                    mostrarErrorDistribucion(data.error);
                    return;
                }
                markTutorsFallback(parseTutoresList(data.Tutores));
            },
            error: function () {
                mostrarErrorDistribucion("No se pudieron cargar los tutores de menores. El mapa se muestra sin esa información.");
            },
            complete: function () {
                hideTutoresLoading();
            }
        });
    }

    window.MatDistribucionCoche = {
        init: initDistribucionCoche,
        mostrarError: mostrarErrorDistribucion
    };
})(window, jQuery);
