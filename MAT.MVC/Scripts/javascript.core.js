// Guardar confirm nativo (para fallback y compatibilidad)
this._nativeConfirm = this._nativeConfirm || this.confirm;

this.confirm = function (message, title, ok_function, cancel_function) {
    /// <summary>Redefine la ventana modal de alerta</summary>
    // Si se usa como confirm síncrono (sin callbacks), fallback al confirm nativo.
    // Esto evita perder el botón de aceptación y mantiene compatibilidad con "if (!confirm(...))".
    if (typeof ok_function !== "function" && typeof cancel_function !== "function") {
        return this._nativeConfirm ? this._nativeConfirm(message) : window.confirm(message);
    }

    var $div = $("<div />");
    $div.attr("title", typeof title !== "string" ? "Atención" : title);
    //-
    var $p1 = $("<p />");
    var $span = $("<span />");
    $span.addClass("bi bi-info-circle");
    $span.css({ float: "left", margin: "0 12px 20px 0", fontSize: "24px", color: "var(--brand-primary, #e91e63)" });
    $p1.append($span);
    $p1.append(message);
    $div.append($p1);
    //-
    $(document.body).append($div);
    $div.dialog({
        modal: true,
        closeOnEscape: true,
        resizable: false,
        draggable: false,
        dialogClass: "modern-confirm-dialog",
        open: function () {
            // Asegurar que el botón X cierre (algunas combinaciones de jQuery UI + estilos pueden interferir)
            var $dlg = $(this);
            try {
                var $w = $dlg.dialog("widget");
                $w.find(".ui-dialog-titlebar-close")
                    .off("click.matConfirmClose")
                    .on("click.matConfirmClose", function (e) {
                        e.preventDefault();
                        $dlg.dialog("close");
                    });

                // Dar estilo bootstrap al botón Aceptar (jQuery UI puede ignorar "class" en versiones legacy)
                var $btns = $w.find(".ui-dialog-buttonpane button");
                $btns.each(function () {
                    var $b = $(this);
                    var txt = ($b.text() || "").trim().toLowerCase();
                    if (txt === "aceptar") $b.addClass("btn btn-primary");
                    if (txt === "cancelar") $b.addClass("btn btn-outline-secondary");
                });
            } catch (e) { }
        },
        close: function () {
            // Limpieza segura (incluye cierre por X)
            var $dlg = $(this);
            try { $dlg.dialog("destroy"); } catch (e) { }
            try { $dlg.remove(); } catch (e) { }
        },
        buttons: [
            {
                text: "Cancelar",
                click: (typeof cancel_function === "function") ? cancel_function : function () {
                    var $dlg = $(this);
                    $dlg.dialog("close");
                    $dlg.remove();
                }
            },
            {
                text: "Aceptar",
                "class": "btn btn-primary",
                click: (typeof ok_function === "function") ? ok_function : function () {
                    var $dlg = $(this);
                    $dlg.dialog("close");
                    $dlg.remove();
                }
            }
        ]
    });
}

this.alert = function (message, title) {
    /// <summary>Redefine la ventana modal de alerta</summary>
    // Normalización: intenta inferir tipo (success/error/info) para icono/colores consistentes.
    var msg = (message || "").toString();
    var ttl = (typeof title === "string" ? title : "");
    var isError = /(^|\b)(error|fall[oó]|no se pudo|no se puede|exception|excepci[oó]n)\b/i.test(msg) || /^error/i.test(ttl);
    var isSuccess = /\b(correctamente|ok\b|exito|éxito|realizada|guardad[oa]|eliminad[oa])\b/i.test(msg) || /\b(exito|éxito)\b/i.test(ttl);

    var iconClass = "bi bi-info-circle";
    var iconColor = "var(--brand-primary, #e91e63)";
    if (isError) {
        iconClass = "bi bi-exclamation-triangle-fill";
        iconColor = "#ef4444";
    } else if (isSuccess) {
        iconClass = "bi bi-check-circle-fill";
        iconColor = "#10b981";
    }

    var $div = $("<div />");
    $div.attr("title", typeof title !== "string" ? "Atención" : title);
    //-
    var $p1 = $("<p />");
    var $span = $("<span />");
    $span.addClass(iconClass);
    $span.css({ float: "left", margin: "0 12px 20px 0", fontSize: "24px", color: iconColor });
    $p1.append($span);
    $p1.append(message);
    $div.append($p1);
    //-
    $(document.body).append($div);
    $div.dialog({
        modal: true,
        closeOnEscape: true,
        resizable: false,
        draggable: false,
        dialogClass: "modern-alert-dialog",
        open: function () {
            var $dlg = $(this);
            try {
                var $w = $dlg.dialog("widget");
                // Asegurar que el botón X cierre correctamente
                $w.find(".ui-dialog-titlebar-close")
                    .off("click.matAlertClose")
                    .on("click.matAlertClose", function (e) {
                        e.preventDefault();
                        $dlg.dialog("close");
                    });
                // Dar estilo al botón Aceptar
                var $btns = $w.find(".ui-dialog-buttonpane button");
                $btns.addClass("btn btn-primary");
            } catch (e) { }
        },
        close: function () {
            var $dlg = $(this);
            try { $dlg.dialog("destroy"); } catch (e) { }
            try { $dlg.remove(); } catch (e) { }
        },
        buttons: {
            "Aceptar": function () {
                var $dlg = $(this);
                $dlg.dialog("close");
            }
        }
    });
}

// Helpers explícitos para evitar ambigüedad (nuevo estándar)
this.alertError = function (message, title) {
    this.alert(message, title || "Error");
}

this.alertSuccess = function (message, title) {
    this.alert(message, title || "Éxito");
}

this.alertInfo = function (message, title) {
    this.alert(message, title || "Atención");
}
