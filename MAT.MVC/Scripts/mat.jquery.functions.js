function SelectDate() {

}

// Configuración centralizada de modales
var ModalConfig = {
    getWidth: function(size) {
        // Usar anchos en píxeles para mejor centrado con sidebar fijo
        var sizes = { 'min': 600, 'medium': 800, 'default': 1000, 'max': '95%', '30%': 400 };
        return sizes[size] || 1000;
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
            $("#" + dialogid).dialog("destroy").remove();
        }
    });
    $("#" + dialogid).dialog("open");
}
function ShowFormDialogCloseRefresh(url, dialogid, dialogtitle, widthsize) {
    var width = ModalConfig.getWidth(widthsize);

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
        },
        close: function () {
            $(document).off('click.modalOverlay');
            $("#" + dialogid).remove();
            window.location.reload(true);
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


