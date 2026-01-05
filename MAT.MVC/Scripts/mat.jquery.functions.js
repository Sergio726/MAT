function SelectDate() {

}

function ShowFormDialog(url, dialogid, dialogtitle, widthsize) {
    
    var width = "80%";
    if (widthsize == "min") {
        width = "50%";
    } else if (widthsize == "medium") {
        width = "65%";
    } else if (widthsize == "default") {
        width = "80%";
    } else if (widthsize == "max") {
        width = "100%";
    } else if (widthsize == null) {
        width = "80%";
    }
    var divcontent = "<div id='" + dialogid + "' title='" + dialogtitle + "'></div>";
    $("body").append(divcontent);

    var divLoading = "<div class='d-flex justify-content-center align-items-center' style='min-height: 200px;'><div class='spinner-border text-primary' role='status'><span class='visually-hidden'>Cargando...</span></div></div>";
    $("#" + dialogid).html(divLoading);
    $.ajax({
        url: url,
        dataType: "html",
        success: function (data) {
            $("#" + dialogid).html(data);
           
        }
    });
    //$("#" + dialogid).load(url);
    $("#" + dialogid).dialog({
        autoOpen: false,
        modal: true,
        width: width,
        position: { my: 'top', at: 'top', of: window },
        closeOnEscape: true,
        close: function () {
            $("#" + dialogid).dialog("destroy").remove();
            //$("#" + dialogid).remove();
        }
    });
    $("#" + dialogid).dialog("open");
}
function ShowFormDialogCloseRefresh(url, dialogid, dialogtitle, widthsize) {
    var width = "80%";
    if (widthsize == "min") {
        width = "50%";
    } else if (widthsize == "medium") {
        width = "65%";
    } else if (widthsize == "default") {
        width = "80%";
    } else if (widthsize == null) {
        width = "80%";
    }
    var divcontent = "<div id='" + dialogid + "' title='" + dialogtitle + "'></div>";
    $("body").append(divcontent);
    $.ajax({
        url: url,
        dataType: "html",
        success: function (data) {
            $("#" + dialogid).html(data);
        }
    });
    //$("#" + dialogid).load(url);
    $("#" + dialogid).dialog({
        autoOpen: false,
        modal: true,
        width: width,
       // position: { my: 'top', at: 'top', of: window },
        closeOnEscape: true,
        close: function () {
            $("#" + dialogid).remove();
            window.location.reload(true);
        }
    });
    $("#" + dialogid).dialog("open");

    $("#" + dialogid).parent().css("top", "100px")
}
function ShowFormDialogJson(url, dialogid, dialogtitle, widthsize, datasend) {
    var width = "80%";
    if (widthsize == "min") {
        width = "50%";
    } else if (widthsize == "medium") {
        width = "65%";
    } else if (widthsize == "default") {
        width = "80%";
    } else if (widthsize == null) {
        width = "80%";
    }
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
    //$("#" + dialogid).load(url);
    $("#" + dialogid).dialog({
        autoOpen: false,
        modal: true,
        width: width,
        position: { my: 'top', at: 'top', of: window },
        closeOnEscape: true,
        close: function () {
            $("#" + dialogid).remove();
        }
    });
    $("#" + dialogid).dialog("open");
}

function ShowFormDialogHTML(html, dialogid, dialogtitle, widthsize) {
    var width = "80%";
    if (widthsize == "min") {
        width = "50%";
    } else if (widthsize == "medium") {
        width = "65%";
    } else if (widthsize == "default") {
        width = "80%";
    } else if (widthsize == null) {
        width = "80%";
    }
    else if (widthsize == "30%") {
        width = "30%";
    }
    var divcontent = "<div id='" + dialogid + "' title='" + dialogtitle + "'></div>";
    $("body").append(divcontent);
    $("#" + dialogid).html(html);
    
    $("#" + dialogid).dialog({
        autoOpen: false,
        modal: true,
        width: width,
        position: { my: 'top', at: 'top', of: window },
        closeOnEscape: true,
        close: function () {
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
                alert('Se activo la cuenta corriente del el cliente seleccionado');
            }
            else {
                $('#btnActivar').attr('src', '/Images/Icons/icon-boton-verde2.png');
                $('#btnDesactivar').attr('src', '/Images/Icons/icon-boton-rojo.png');
                alert('Se desactivo la cuenta corriente del cliente seleccionado');
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


