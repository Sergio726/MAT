function SelectDate() {

}

// Función para mostrar mensaje de éxito de manera moderna
function ShowSuccessMessage(message, title, callback) {
    title = title || 'Atención';
    message = message || 'Operación realizada correctamente.';
    
    // Remover diálogo si ya existe
    if ($("#modern-success-dialog").length > 0) {
        $("#modern-success-dialog").dialog("destroy").remove();
    }
    
    var dialogContent = '<div id="modern-success-dialog" style="display: none;">' +
        '<div style="display: flex; align-items: flex-start; gap: 1.25rem;">' +
        '<i class="bi bi-check-circle-fill" style="font-size: 2.5rem; color: #10b981; flex-shrink: 0; margin-top: 0.125rem;"></i>' +
        '<span style="flex: 1; line-height: 1.6; font-size: 1rem; color: #1f2937;">' + message + '</span>' +
        '</div>' +
        '</div>';
    
    $("body").append(dialogContent);
    
    $("#modern-success-dialog").dialog({
        autoOpen: true,
        modal: true,
        width: 500,
        minWidth: 400,
        maxWidth: 600,
        title: '<i class="bi bi-check-circle"></i> ' + title,
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

    // Remover el diálogo si ya existe
    if ($("#" + dialogid).length > 0) {
        $("#" + dialogid).dialog("destroy").remove();
    }

    var divcontent = "<div id='" + dialogid + "' title='" + dialogtitle + "'></div>";
    $("body").append(divcontent);

    var divLoading = "<div class='modern-loading-container' style='padding: 2rem;'><div class='modern-loading-spinner'></div><span class='modern-loading-text'>Cargando...</span></div>";
    $("#" + dialogid).html(divLoading);
    
    $.ajax({
        url: url,
        dataType: "html",
        success: function (data) {
            if (data && data.trim() !== '') {
                $("#" + dialogid).html(data);
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


