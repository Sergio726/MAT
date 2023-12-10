this.confirm = function (message, title, ok_function, cancel_function) {
    /// <summary>Redefine la ventana modal de alerta</summary>
    var $div = $("<div />");
    $div.attr("title", typeof title !== "string" ? "Atenci&oacute;n" : title);
    //-
    var $p1 = $("<p />");
    var $span = $("<span />");
    $span.addClass("ui-icon");
    $span.addClass("ui-icon-circle-check");
    $span.css({ float: "left", margin: "0 7px 50px 0" });
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
	var $div = $("<div />");
	$div.attr("title", typeof title !== "string" ? "Atenci&oacute;n" : title);
	//-
	var $p1 = $("<p />");
	var $span = $("<span />");
	$span.addClass("ui-icon");
	$span.addClass("ui-icon-circle-check");
	$span.css({ float: "left", margin: "0 7px 50px 0" });
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

