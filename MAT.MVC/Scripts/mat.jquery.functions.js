function SelectDate() {

}

// Popin Pagos (Factura): implementación limpia usando ShowFormDialog (un solo dialog top-level).
// Evita conflictos de IDs duplicados / dialogs embebidos, y el botón X funciona de forma nativa.
window.openPagosFacturaDialog = function (FacturaID) {
    var id = "PopinPagosFactura";
    var title = "Pagos";
    var url = "/PersonaCliente/partialHistorialdePagosByFactura?FacturaID=" + encodeURIComponent(FacturaID || "");
    ShowFormDialog(url, id, title, "wide");
};

// Función para mostrar mensaje de éxito de manera moderna
function ShowSuccessMessage(message, title, callback) {
    title = title || 'Operación exitosa';
    message = message || 'La operación se realizó correctamente.';
    
    // Remover diálogo si ya existe
    if ($("#modern-success-dialog").length > 0) {
        $("#modern-success-dialog").dialog("destroy").remove();
    }

    // Normalizar mensajes comunes (mejor UX)
    var msg = (message || "").toString();
    // Caso típico: eliminación de pasajero en factura
    if (/pasajero\s+eliminado/i.test(msg) && /factura/i.test(msg)) {
        msg = "Pasajero eliminado.\nLa factura fue actualizada (butaca/habitación liberadas si correspondía).";
    }
    msg = msg.replace(/\r\n/g, "\n");
    var msgHtml = msg
        .split("\n")
        .map(function (line) { return $("<div/>").text(line).html(); })
        .join("<br/>");
    
    var dialogContent = '<div id="modern-success-dialog" style="display: none;">' +
        '<div style="display: flex; align-items: flex-start; gap: 1.25rem;">' +
        '<i class="bi bi-check-circle-fill" style="font-size: 2.5rem; color: #10b981; flex-shrink: 0; margin-top: 0.125rem;"></i>' +
        '<div style="flex: 1;">' +
        '<div style="font-weight: 700; font-size: 1.05rem; color: #0f172a; margin-bottom: 0.25rem;">Listo</div>' +
        '<div style="line-height: 1.55; font-size: 0.95rem; color: #1f2937;">' + msgHtml + '</div>' +
        '</div>' +
        '</div>' +
        '</div>';
    
    $("body").append(dialogContent);
    
    $("#modern-success-dialog").dialog({
        autoOpen: true,
        modal: true,
        width: 500,
        minWidth: 400,
        maxWidth: 600,
        // jQuery UI Dialog no soporta HTML en title: dejar texto plano para evitar que se vea "<i ...>"
        title: title,
        resizable: false,
        draggable: false,
        dialogClass: 'modern-success-dialog',
        buttons: {
            "Aceptar": function() {
                $(this).dialog("close");
                if (typeof callback === 'function') {
                    callback();
                }
            }
        },
        close: function() {
            $(this).dialog("destroy").remove();
            if (typeof callback === 'function') {
                callback();
            }
        },
        open: function() {
            // Asegurar que el diálogo esté centrado
            var $dialog = $(this).parent();
            $dialog.css({
                position: 'fixed',
                top: '50%',
                left: '50%',
                transform: 'translate(-50%, -50%)',
                zIndex: 10000
            });
            
            // Asegurar que los botones sean visibles
            setTimeout(function() {
                var $buttonPane = $dialog.find('.ui-dialog-buttonpane');
                if ($buttonPane.length > 0) {
                    $buttonPane.css({
                        display: 'flex !important',
                        visibility: 'visible !important',
                        opacity: '1 !important'
                    });
                    
                    // Asegurar que el botón tenga los estilos correctos
                    var $button = $buttonPane.find('.ui-button, button');
                    $button.css({
                        display: 'inline-block !important',
                        visibility: 'visible !important',
                        opacity: '1 !important'
                    });
                }
            }, 100);
        },
        create: function() {
            // Asegurar que el overlay sea visible
            $('.ui-widget-overlay').css({
                background: 'rgba(0, 0, 0, 0.5)',
                opacity: '1',
                zIndex: '9999'
            });
        }
    });
}

// Configuración centralizada de modales
var ModalConfig = {
    getWidth: function(size) {
        // Usar anchos en píxeles para mejor centrado con sidebar fijo
        var sizes = { 'min': 600, 'medium': 800, 'default': 1000, 'wide': 1100, 'max': '95%', '30%': 400, 'fullscreen': '100%' };
        return sizes[size] || 1000;
    },
    setupFullscreen: function (dialogid, enableByDefault) {
        var $content = $("#" + dialogid);
        if (!$content.length) return;

        $content.data("matFullscreenEnabled", !!enableByDefault);

        function ensureMaximizeButton() {
            var $widget = $content.dialog("widget");
            if (!$widget || !$widget.length) return;

            var $titlebar = $widget.find(".ui-dialog-titlebar");
            if (!$titlebar.length) return;

            if ($titlebar.find(".mat-dialog-titlebar-maximize").length) return;

            var $btn = $("<button/>", {
                type: "button",
                class: "mat-dialog-titlebar-maximize",
                "aria-label": "Maximizar / restaurar",
                title: "Maximizar / restaurar"
            }).append($("<i/>", { class: "bi bi-arrows-fullscreen" }));

            // Insertar antes del botón cerrar si existe
            var $close = $titlebar.find(".ui-dialog-titlebar-close");
            if ($close.length) $btn.insertBefore($close);
            else $titlebar.append($btn);

            $btn.on("click", function () {
                var enabled = !!$content.data("matFullscreenEnabled");
                ModalConfig.setFullscreen(dialogid, !enabled);
            });
        }

        function syncFullscreenWidget() {
            if (!$content.length) return;
            if (!$content.data("matFullscreenEnabled")) return;

            var $widget = $content.dialog("widget");
            if (!$widget || !$widget.length) return;

            var margin = 12;
            var w = Math.max(320, Math.floor($(window).width() - margin * 2));
            var h = Math.max(240, Math.floor($(window).height() - margin * 2));
            w = Math.min(1400, w);

            // Forzar dimensiones y re-centrar (jQuery UI puede quedar con top/left viejos)
            $content.dialog("option", "width", w);
            $content.dialog("option", "height", h);
            $content.dialog("option", "position", { my: "center", at: "center", of: window });

            // Asegurar fixed (sin forzar top/left manualmente)
            $widget.css({ position: "fixed" });
        }

        function syncFullscreenLayout() {
            if (!$content.length) return;
            if (!$content.data("matFullscreenEnabled")) return;

            var $widget = $content.dialog("widget");
            if (!$widget || !$widget.length) return;

            // Calcular alto disponible para el content (evita que se corte dentro del fullscreen)
            var titleH = $widget.find(".ui-dialog-titlebar").outerHeight() || 0;
            var buttonPaneH = $widget.find(".ui-dialog-buttonpane").outerHeight() || 0;
            var widgetH = $widget.innerHeight() || 0;
            var padding = 0;
            var contentH = Math.max(120, widgetH - titleH - buttonPaneH - padding);

            $content.css({
                height: contentH + "px",
                overflow: "auto"
            });
        }

        // Guardar para reuso (y cleanup on close)
        $content.data("matEnsureMaximizeButton", ensureMaximizeButton);
        $content.data("matSyncFullscreenWidget", syncFullscreenWidget);
        $content.data("matSyncFullscreenLayout", syncFullscreenLayout);

        // Sync en resize mientras esté abierto
        $(window).off("resize.matDialogFullscreen-" + dialogid).on("resize.matDialogFullscreen-" + dialogid, function () {
            // Esperar a que el browser aplique layout antes de recalcular
            var raf = window.requestAnimationFrame || function (cb) { return setTimeout(cb, 0); };
            raf(function () {
                syncFullscreenWidget();
                syncFullscreenLayout();
            });
        });
    },
    setFullscreen: function (dialogid, enabled) {
        var $content = $("#" + dialogid);
        if (!$content.length) return;

        var $widget = $content.dialog("widget");
        if (!$widget || !$widget.length) return;

        $content.data("matFullscreenEnabled", !!enabled);

        if (enabled) {
            $widget.addClass("mat-dialog--fullscreen");

            // Forzar tamaño real fullscreen (no depender solo de CSS)
            // Importante: primero ajustar widget, luego recalcular el alto del content.
            var raf = window.requestAnimationFrame || function (cb) { return setTimeout(cb, 0); };
            raf(function () {
                var syncWidget = $content.data("matSyncFullscreenWidget");
                if (typeof syncWidget === "function") syncWidget();

                var syncLayout = $content.data("matSyncFullscreenLayout");
                if (typeof syncLayout === "function") syncLayout();
            });
        } else {
            $widget.removeClass("mat-dialog--fullscreen");
            var baseWidth = $content.data("matBaseWidth");
            if (baseWidth) $content.dialog("option", "width", baseWidth);
            $content.dialog("option", "height", "auto");
            $content.dialog("option", "position", { my: "center", at: "center", of: window });
            $content.css({ height: "", overflow: "" });
            $widget.css({ width: "", height: "", top: "", left: "", right: "", bottom: "", transform: "" });
        }

        // Icono toggle
        var $icon = $widget.find(".mat-dialog-titlebar-maximize i");
        if ($icon.length) {
            $icon.removeClass("bi-arrows-fullscreen bi-fullscreen-exit").addClass(enabled ? "bi-fullscreen-exit" : "bi-arrows-fullscreen");
        }

        // En fullscreen lo sincronizamos en RAF arriba (para evitar height incorrecto).
        if (!enabled) {
            var sync = $content.data("matSyncFullscreenLayout");
            if (typeof sync === "function") sync();
        }
    },
    setupOverlayClose: function(dialogid) {
        // Cerrar modal al hacer clic en el overlay
        $(document).off('click.modalOverlay').on('click.modalOverlay', '.ui-widget-overlay', function() {
            if ($("#" + dialogid).length && $("#" + dialogid).dialog("isOpen")) {
                $("#" + dialogid).dialog("close");
            }
        });
    }
};

function ShowFormDialog(url, dialogid, dialogtitle, widthsize) {
    var width = ModalConfig.getWidth(widthsize);

    // Forzar tamaño más amplio para este flujo (mejor UX dentro del formulario)
    if (dialogid === "FormReserva") {
        width = ModalConfig.getWidth("wide");
    }

    // Remover el diálogo si ya existe
    if ($("#" + dialogid).length > 0) {
        $("#" + dialogid).dialog("destroy").remove();
    }

    var divcontent = "<div id='" + dialogid + "' title='" + dialogtitle + "'></div>";
    $("body").append(divcontent);

    var divLoading = "<div class='modern-loading-container'><div class='modern-loading-spinner'></div><span class='modern-loading-text'>Cargando...</span></div>";
    $("#" + dialogid).html(divLoading);
    $("#" + dialogid).data("matBaseWidth", width);
    
    $.ajax({
        url: url,
        dataType: "html",
        success: function (data) {
            if (data && data.trim() !== '') {
                $("#" + dialogid).html(data);

                // UX: aplicar máscara de moneda automáticamente en el contenido cargado (si el plugin está disponible)
                try {
                    if ($.fn && typeof $.fn.mask === "function") {
                        $("#" + dialogid).find(".money").mask("#.##0,00", { reverse: true });
                    }
                } catch (e) {
                    // no-op
                }

                // UX: enfocar el primer campo visible del modal
                try {
                    var $first = $("#" + dialogid).find("input, textarea, select").filter(":visible").first();
                    if ($first && $first.length) $first.trigger("focus");
                } catch (e2) {
                    // no-op
                }
            } else {
                $("#" + dialogid).html('<div class="modern-alert-error"><i class="bi bi-exclamation-circle"></i><span>No se pudo cargar el contenido. Por favor, intente nuevamente.</span></div>');
            }
        },
        error: function (xhr, status, error) {
            var errorMsg = "Error al cargar el contenido.";
            if (xhr.status === 404) {
                errorMsg = "La página solicitada no fue encontrada.";
            } else if (xhr.status === 500) {
                errorMsg = "Error interno del servidor. Por favor, contacte al administrador.";
            } else if (xhr.responseText) {
                errorMsg = "Error: " + xhr.responseText.substring(0, 200);
            }
            $("#" + dialogid).html('<div class="modern-alert-error"><i class="bi bi-exclamation-circle"></i><span>' + errorMsg + '</span><br/><small>URL: ' + url + '</small></div>');
            console.error("Error en ShowFormDialog:", {url: url, status: status, error: error, xhr: xhr});
        }
    });

    $("#" + dialogid).dialog({
        autoOpen: false,
        modal: true,
        width: width,
        position: { my: 'center', at: 'center', of: window },
        closeOnEscape: true,
        resizable: false,
        draggable: false,
        dialogClass: "mat-dialog",
        open: function() {
            ModalConfig.setupOverlayClose(dialogid);

            // Asegurar que el botón de cerrar funcione correctamente para todos los diálogos
            var $dialog = $(this);
            var $closeBtn = $dialog.closest('.ui-dialog').find('.ui-dialog-titlebar-close');
            if ($closeBtn.length) {
                $closeBtn.off('click.matDialogClose').on('click.matDialogClose', function(e) {
                    e.preventDefault();
                    e.stopPropagation();
                    $dialog.dialog('close');
                });
            }

            // Fullscreen por defecto para SeleccionarPasajero (maximiza el popin)
            if (dialogid === "SeleccionPasajero") {
                ModalConfig.setupFullscreen(dialogid, true);
                var ensureBtn = $("#" + dialogid).data("matEnsureMaximizeButton");
                if (typeof ensureBtn === "function") ensureBtn();
                ModalConfig.setFullscreen(dialogid, true);
            }

            // Lista de Espera: +5% de ancho (sin afectar otros popins "min")
            if (dialogid === "divOffListPassengers") {
                try {
                    var $dlgWL = $("#" + dialogid);
                    var baseW = $dlgWL.data("matBaseWidth");
                    if (baseW && typeof baseW === "number") {
                        $dlgWL.dialog("option", "width", Math.round(baseW * 1.05));
                    }
                } catch (e) { }
            }

            // Mejor UX: limitar alto para que el scroll sea interno y no se corte el contenido
            if (dialogid === "FormReserva") {
                var $dlg = $("#" + dialogid);
                var maxH = Math.max(520, Math.floor($(window).height() - 80));
                $dlg.dialog("option", "maxHeight", maxH);
                $dlg.dialog("option", "height", "auto");

                var $w = $dlg.dialog("widget");
                if ($w && $w.length) $w.addClass("mat-dialog--formreserva");
            }

            // ReservaHabitacion: titlebar con estilo "Seleccionar pasajero" (brand header)
            if (dialogid === "idResHab") {
                var $wHab = $("#" + dialogid).dialog("widget");
                if ($wHab && $wHab.length) $wHab.addClass("mat-dialog--brandtitle");
            }

            // Nota de Crédito: UX consistente (titlebar blanco + scroll interno)
            if (dialogid === "NotaCredito") {
                var $wNc = $("#" + dialogid).dialog("widget");
                if ($wNc && $wNc.length) $wNc.addClass("mat-dialog--brandtitle");
            }

            // Pagos de Factura: aplicar estilos modernos
            if (dialogid === "PopinPagosFactura") {
                var $wPagos = $("#" + dialogid).dialog("widget");
                if ($wPagos && $wPagos.length) $wPagos.addClass("mat-pagos-dialog");
            }

            // Detalle Factura: agregar icono de copiar ID en titlebar
            // Nota: el contenido se carga de forma asíncrona, por lo que debemos esperar a que esté disponible
            if (dialogid === "Detalles") {
                try {
                    var $dlg = $("#" + dialogid);
                    var $widget = $dlg.dialog("widget");
                    var $titlebar = $widget.find(".ui-dialog-titlebar");
                    
                    // Remover icono anterior si existe (por si se reabre el dialog)
                    $titlebar.find(".ui-dialog-titlebar-copy-id").remove();
                    
                    // Variable de control para evitar agregar múltiples botones
                    var buttonAdded = false;
                    var observer = null;
                    var intervalId = null;
                    
                    // Función para limpiar observers/intervals
                    function cleanup() {
                        if (observer) {
                            observer.disconnect();
                            observer = null;
                        }
                        if (intervalId) {
                            clearInterval(intervalId);
                            intervalId = null;
                        }
                    }
                    
                    // Función para agregar el icono cuando el contenido esté disponible
                    function tryAddCopyButton() {
                        // Si ya se agregó el botón, no hacer nada
                        if (buttonAdded || $titlebar.find(".ui-dialog-titlebar-copy-id").length > 0) {
                            return true;
                        }
                        
                        var facturaId = null;
                        // Buscar en el contenedor principal
                        var $container = $dlg.find(".factura-detail-container");
                        if ($container.length && $container.data("facturaid")) {
                            facturaId = $container.data("facturaid");
                        } else {
                            // Buscar dentro de #divPopupDetalleFactura (donde se carga el contenido)
                            var $popupContainer = $dlg.find("#divPopupDetalleFactura .factura-detail-container");
                            if ($popupContainer.length && $popupContainer.data("facturaid")) {
                                facturaId = $popupContainer.data("facturaid");
                            } else {
                                // Fallback: buscar en data attributes de botones
                                var $btn = $dlg.find("[data-facturaid]").first();
                                if ($btn.length) {
                                    facturaId = $btn.data("facturaid");
                                }
                            }
                        }
                        
                        if (facturaId) {
                            // Ya tenemos el ID, agregar el botón
                            var $copyBtn = $("<button/>", {
                                type: "button",
                                class: "ui-dialog-titlebar-copy-id",
                                title: "Copiar ID de factura al portapapeles",
                                "aria-label": "Copiar ID de factura"
                            }).html('<i class="bi bi-clipboard"></i>');
                            
                            $copyBtn.on("click", function(e) {
                                e.preventDefault();
                                e.stopPropagation();
                                
                                var idToCopy = facturaId.toString();
                                var $btn = $(this);
                                
                                // Función para mostrar feedback visual
                                function showFeedback() {
                                    var $icon = $btn.find("i");
                                    var originalClass = $icon.attr("class");
                                    $icon.removeClass("bi-clipboard").addClass("bi-check");
                                    $btn.css("color", "#10b981");
                                    setTimeout(function() {
                                        $icon.attr("class", originalClass);
                                        $btn.css("color", "");
                                    }, 1500);
                                }
                                
                                // Función fallback para navegadores antiguos
                                function fallbackCopy(text) {
                                    var textArea = document.createElement("textarea");
                                    textArea.value = text;
                                    textArea.style.position = "fixed";
                                    textArea.style.opacity = "0";
                                    document.body.appendChild(textArea);
                                    textArea.select();
                                    try {
                                        document.execCommand('copy');
                                        showFeedback();
                                    } catch (err) {
                                        console.error("Error en fallback copy:", err);
                                    }
                                    document.body.removeChild(textArea);
                                }
                                
                                // Intentar usar Clipboard API moderna
                                if (navigator.clipboard && navigator.clipboard.writeText) {
                                    navigator.clipboard.writeText(idToCopy).then(function() {
                                        showFeedback();
                                    }).catch(function(err) {
                                        console.error("Error al copiar:", err);
                                        fallbackCopy(idToCopy);
                                    });
                                } else {
                                    // Fallback para navegadores antiguos
                                    fallbackCopy(idToCopy);
                                }
                            });
                            
                            // Insertar antes del botón cerrar
                            var $closeBtn = $titlebar.find(".ui-dialog-titlebar-close");
                            if ($closeBtn.length) {
                                $copyBtn.insertBefore($closeBtn);
                            } else {
                                $titlebar.append($copyBtn);
                            }
                            
                            buttonAdded = true;
                            cleanup(); // Limpiar observers/intervals
                            return true; // Éxito
                        }
                        return false; // Aún no está disponible
                    }
                    
                    // Intentar inmediatamente (por si el contenido ya está cargado)
                    if (!tryAddCopyButton()) {
                        // Si no está disponible, usar MutationObserver para detectar cuando se carga el contenido
                        observer = new MutationObserver(function(mutations) {
                            if (tryAddCopyButton()) {
                                cleanup(); // Ya encontramos el ID, limpiar todo
                            }
                        });
                        
                        // Observar cambios en el contenido del dialog
                        observer.observe($dlg[0], {
                            childList: true,
                            subtree: true
                        });
                        
                        // También intentar periódicamente como fallback (por si el observer no funciona)
                        var attempts = 0;
                        var maxAttempts = 50; // 5 segundos máximo (50 * 100ms)
                        intervalId = setInterval(function() {
                            attempts++;
                            if (tryAddCopyButton() || attempts >= maxAttempts) {
                                cleanup();
                            }
                        }, 100);
                    }
                } catch (e) {
                    console.error("Error al agregar icono de copiar ID:", e);
                }
            }
        },
        close: function () {
            $(document).off('click.modalOverlay');
            $(window).off("resize.matDialogFullscreen-" + dialogid);
            $("#" + dialogid).dialog("destroy").remove();
        }
    });
    $("#" + dialogid).dialog("open");
}
// ShowFormDialogCloseRefresh:
// Históricamente recargaba toda la página al cerrar, para “refrescar” datos.
// Ahora soporta refresh selectivo vía options:
// - reloadOnClose (default: true) mantiene compatibilidad
// - onClose: callback para refrescar un panel/tabla sin reload completo
function ShowFormDialogCloseRefresh(url, dialogid, dialogtitle, widthsize, options) {
    var width = ModalConfig.getWidth(widthsize);
    var opts = options || {};
    var reloadOnClose = (opts.reloadOnClose !== false); // default true (compat)

    var divcontent = "<div id='" + dialogid + "' title='" + dialogtitle + "'></div>";
    $("body").append(divcontent);
    $.ajax({
        url: url,
        dataType: "html",
        success: function (data) {
            $("#" + dialogid).html(data);
        }
    });

    $("#" + dialogid).dialog({
        autoOpen: false,
        modal: true,
        width: width,
        position: { my: 'center', at: 'center', of: window },
        closeOnEscape: true,
        open: function() {
            ModalConfig.setupOverlayClose(dialogid);
            
            // Asegurar que el botón de cerrar funcione correctamente
            var $dialog = $(this);
            var $closeBtn = $dialog.closest('.ui-dialog').find('.ui-dialog-titlebar-close');
            if ($closeBtn.length) {
                $closeBtn.off('click.matDialogClose').on('click.matDialogClose', function(e) {
                    e.preventDefault();
                    e.stopPropagation();
                    $dialog.dialog('close');
                });
            }
        },
        close: function () {
            $(document).off('click.modalOverlay');
            $("#" + dialogid).remove();

            // Permitir refresh selectivo / hooks por pantalla
            try {
                if (typeof opts.onClose === "function") opts.onClose();
                $(document).trigger("mat:dialogCloseRefresh", { dialogid: dialogid, url: url });
            } catch (e) {
                console.warn("ShowFormDialogCloseRefresh onClose error:", e);
            }

            if (reloadOnClose) {
                window.location.reload(true);
            }
        }
    });
    $("#" + dialogid).dialog("open");
}
function ShowFormDialogJson(url, dialogid, dialogtitle, widthsize, datasend) {
    var width = ModalConfig.getWidth(widthsize);

    var divcontent = "<div id='" + dialogid + "' title='" + dialogtitle + "'></div>";
    $("body").append(divcontent);
    $.ajax({
        type: "get",
        url: url,
        dataType: "json",
        data: { jsonobject: datasend },
        success: function (data) {
            $("#" + dialogid).html(data);
        }
    });

    $("#" + dialogid).dialog({
        autoOpen: false,
        modal: true,
        width: width,
        position: { my: 'center', at: 'center', of: window },
        closeOnEscape: true,
        open: function() {
            ModalConfig.setupOverlayClose(dialogid);
        },
        close: function () {
            $(document).off('click.modalOverlay');
            $("#" + dialogid).remove();
        }
    });
    $("#" + dialogid).dialog("open");
}

function ShowFormDialogHTML(html, dialogid, dialogtitle, widthsize) {
    var width = ModalConfig.getWidth(widthsize);

    // Forzar tamaño más amplio para este flujo (mejor UX dentro del formulario)
    if (dialogid === "FormReserva") {
        width = ModalConfig.getWidth("wide");
    }

    var divcontent = "<div id='" + dialogid + "' title='" + dialogtitle + "'></div>";
    $("body").append(divcontent);
    $("#" + dialogid).html(html);

    $("#" + dialogid).dialog({
        autoOpen: false,
        modal: true,
        width: width,
        position: { my: 'center', at: 'center', of: window },
        closeOnEscape: true,
        open: function() {
            ModalConfig.setupOverlayClose(dialogid);
            
            // Asegurar que el botón de cerrar funcione correctamente
            var $dialog = $(this);
            var $closeBtn = $dialog.closest('.ui-dialog').find('.ui-dialog-titlebar-close');
            if ($closeBtn.length) {
                $closeBtn.off('click.matDialogClose').on('click.matDialogClose', function(e) {
                    e.preventDefault();
                    e.stopPropagation();
                    $dialog.dialog('close');
                });
            }

            if (dialogid === "FormReserva") {
                var $dlg = $("#" + dialogid);
                var maxH = Math.max(520, Math.floor($(window).height() - 80));
                $dlg.dialog("option", "maxHeight", maxH);
                $dlg.dialog("option", "height", "auto");

                var $w = $dlg.dialog("widget");
                if ($w && $w.length) $w.addClass("mat-dialog--formreserva");
            }
        },
        close: function () {
            $(document).off('click.modalOverlay');
            $("#" + dialogid).remove();
        }
    });
    $("#" + dialogid).dialog("open");
}

function validarCTA(ClienteId, Estado) {

    $.ajax({
        url: "/PersonaCliente/validarCTA",
        type: "POST",
        data: { ClienteId: ClienteId, Estado: Estado },
        success: function (data) {
            var result = data == "True" ? true : false;
            if (result) {
                $('#btnActivar').attr('src', '/Images/Icons/icon-boton-verde.png');
                $('#btnDesactivar').attr('src', '/Images/Icons/icon-boton-rojo2.png');
                (window.alertSuccess || window.alert)('Se activó la cuenta corriente del cliente seleccionado.', 'Éxito');
            }
            else {
                $('#btnActivar').attr('src', '/Images/Icons/icon-boton-verde2.png');
                $('#btnDesactivar').attr('src', '/Images/Icons/icon-boton-rojo.png');
                (window.alertInfo || window.alert)('Se desactivó la cuenta corriente del cliente seleccionado.', 'Atención');
            }

        }
    });

};

function verificarCTA(ClienteId, Estado) {

    $.ajax({
        url: "/PersonaCliente/validarCTA",
        type: "POST",
        data: { ClienteId: ClienteId, Estado: Estado },
        success: function (data) {
            var result = data == "True" ? true : false;
            if (result) {
                $('#btnActivar').attr('src', '/Images/Icons/icon-boton-verde.png');
                $('#btnDesactivar').attr('src', '/Images/Icons/icon-boton-rojo2.png');

            }
            else {
                $('#btnActivar').attr('src', '/Images/Icons/icon-boton-verde2.png');
                $('#btnDesactivar').attr('src', '/Images/Icons/icon-boton-rojo.png');

            }

        }
    });

};

$(document).on("click", "#btnViajes", function () {
    var url = "/Viaje/PopPupViajes"
    var title = "Viajes";
    var id = "Viajes";
    ShowFormDialog(url, id, title);

});



$(document).on("click", "#btnFiltrarHabitacion", function () {
    var HotelID = $('#ddHotel').val();
    var PasajeroID = $("#hdnPasajeroID").val();
    var Fecha = $('#ddHotel option:selected').attr("fecha");
    $('#gridHabitacion').load('/ReservaHabitacion/GridHotelHabitacion?HotelID=' + HotelID + "&viajeid=" + $("#viaje-id").val() + "&PasajeroID=" + PasajeroID + "&Fecha=" + Fecha)

});


