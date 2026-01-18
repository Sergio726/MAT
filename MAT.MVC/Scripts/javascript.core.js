this.confirm = function (message, title, ok_function, cancel_function) {
    /// <summary>Redefine la ventana modal de alerta</summary>
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
        buttons: {
            Ok: ok_function,
            Cancel: cancel_function
        }
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
        buttons: {
            Ok: function () {
                var $dlg = $(this);
                $dlg.dialog("close");
                $dlg.remove();
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
