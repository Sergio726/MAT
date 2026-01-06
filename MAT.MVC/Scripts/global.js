$(document).ready(function(){
		
	

});

function ElegirButaca(elto) {
    //Para la nueva reserva:
    //if (elto.closest(".right-container").length > 0)
    //    return false;

	//$('.piso_superior a').removeClass('selected');
	//$('.piso_inferior a').removeClass('selected');
	// Asign value of the link target
	var thisTarget = elto.attr('href');
	var title = elto.attr('title');
	var butacaId = elto.attr('id');
	$("#id_pasaje").val(butacaId);
	var paqueteid = $("#id_paquete").val();
	var piso = elto.data("piso");
	
	// Verificar si la butaca ya tiene un pasajero asignado
	// Primero verificar el atributo HTML directamente, luego el data()
	var pasajeroAsignadoAttr = elto.attr("data-pasajero");
	var pasajeroAsignado = elto.data("pasajero");
	var estadoButaca = parseInt(elto.data("estado") || elto.attr("data-estado") || "0");
	
	// Una butaca tiene pasajero asignado si:
	// 1. Tiene un valor en data-pasajero que no sea vacío ni GUID vacío
	// 2. Y el estado no es 1 (disponible) - porque las disponibles no tienen pasajero
	// 3. O si tiene la clase butaca-asignada que agregamos programáticamente
	var tienePasajero = false;
	
	if (pasajeroAsignadoAttr || pasajeroAsignado) {
	    var pasajeroId = (pasajeroAsignadoAttr || pasajeroAsignado || "").toString().trim();
	    var guidVacio = "00000000-0000-0000-0000-000000000000";
	    
	    // Verificar que no sea vacío ni GUID vacío
	    if (pasajeroId !== "" && pasajeroId !== guidVacio) {
	        tienePasajero = true;
	    }
	}
	
	// También verificar si tiene la clase butaca-asignada (asignada en esta sesión)
	if (elto.hasClass("butaca-asignada")) {
	    tienePasajero = true;
	}
	
	// Si el estado es 1 (disponible), no debería tener pasajero
	if (estadoButaca === 1) {
	    tienePasajero = false;
	}
	
	if (tienePasajero) {
	    // Butaca ya tiene pasajero - mostrar panel de opciones
	    var pasajeroIdFinal = (pasajeroAsignadoAttr || pasajeroAsignado || "").toString().trim();
	    mostrarPanelButacaAsignada(elto, butacaId, pasajeroIdFinal);
	} else {
	    // Butaca vacía - verificar si hay pasajeros pendientes de asignar
	    var pasajerosPendientes = obtenerPasajerosPendientes();
	    
	    if (pasajerosPendientes && pasajerosPendientes.length > 0) {
	        // Hay pasajeros pendientes - mostrar panel de selección
	        mostrarPanelSeleccionPasajeroPendiente(elto, butacaId);
	    } else {
	        // No hay pasajeros pendientes - abrir modal completo para seleccionar pasajeros
	        var url = "/Reserva/SeleccionarPasajero?paqueteid=" + paqueteid + "&piso=" + piso;
	        var dialogid = "SeleccionPasajero";
	        var dialogtitle = "Seleccionar Pasajero";
	        
	        // Guardar el ID de la butaca para asignación posterior
	        window.butacaActualSeleccionada = butacaId;
	        
	        ShowFormDialog(url, dialogid, dialogtitle, "default");
	        
	        // El modal ya maneja la asignación cuando se hace clic en "Continuar Reserva"
	    }
	}
	
	elto.toggleClass('selected');
	this.blur();
	return false;
}

// Función para mostrar panel cuando butaca ya tiene pasajero asignado
function mostrarPanelButacaAsignada(elto, butacaId, pasajeroId) {
    // Crear panel flotante con información y opciones
    var panel = $('<div class="panel-butaca-asignada"></div>');
    var pasajeroInfo = obtenerInfoPasajero(pasajeroId);
    
    panel.html(
        '<div class="panel-header">' +
            '<h4>Butaca Asignada</h4>' +
            '<button type="button" class="btn-cerrar-panel">&times;</button>' +
        '</div>' +
        '<div class="panel-content">' +
            '<p><strong>Pasajero asignado:</strong></p>' +
            '<p>' + (pasajeroInfo.nombre || 'Pasajero ID: ' + pasajeroId) + '</p>' +
            '<div class="panel-acciones">' +
                '<button type="button" class="btn-cambiar-pasajero" data-butaca-id="' + butacaId + '">Cambiar Pasajero</button>' +
                '<button type="button" class="btn-liberar-butaca" data-butaca-id="' + butacaId + '">Liberar Butaca</button>' +
            '</div>' +
        '</div>'
    );
    
    // Posicionar panel cerca de la butaca
    var pos = elto.offset();
    panel.css({
        position: 'absolute',
        top: pos.top + elto.outerHeight() + 10,
        left: pos.left,
        zIndex: 10000
    });
    
    $('body').append(panel);
    
    // Cerrar panel
    panel.find('.btn-cerrar-panel, .btn-cambiar-pasajero, .btn-liberar-butaca').on('click', function() {
        if ($(this).hasClass('btn-liberar-butaca')) {
            liberarButaca($(this).data('butaca-id'));
        } else if ($(this).hasClass('btn-cambiar-pasajero')) {
            cambiarPasajeroButaca($(this).data('butaca-id'));
        }
        panel.remove();
    });
    
    // Cerrar al hacer clic fuera
    setTimeout(function() {
        $(document).one('click', function(e) {
            if (!panel.is(e.target) && !panel.has(e.target).length) {
                panel.remove();
            }
        });
    }, 100);
}

// Función para obtener información del pasajero (búsqueda en pasajeros disponibles)
function obtenerInfoPasajero(pasajeroId) {
    // Intentar buscar en pasajeros seleccionados del modal
    if (window.pasajerosSeleccionadosModal && window.pasajerosSeleccionadosModal.length > 0) {
        var pasajero = window.pasajerosSeleccionadosModal.find(function(p) {
            return p.id === pasajeroId;
        });
        if (pasajero) {
            return {
                nombre: pasajero.apellido + ', ' + pasajero.nombre,
                dni: pasajero.nrodoc
            };
        }
    }
    
    // Buscar en infoReservaModal si existe
    if (window.infoReservaModal && window.infoReservaModal.pasajeros && window.infoReservaModal.pasajeros.length > 0) {
        var pasajero = window.infoReservaModal.pasajeros.find(function(p) {
            return p.id === pasajeroId;
        });
        if (pasajero) {
            return {
                nombre: pasajero.apellido + ', ' + pasajero.nombre,
                dni: pasajero.nrodoc || ''
            };
        }
    }
    
    // Si no se encuentra, buscar en el DOM de las butacas
    var butaca = $('#panel-bus a[data-pasajero="' + pasajeroId + '"]').first();
    if (butaca.length) {
        var title = butaca.attr('title') || butaca.attr('data-original-title');
        return {
            nombre: title || 'Pasajero asignado',
            dni: ''
        };
    }
    
    return { nombre: '', dni: '' };
}

// Función para obtener pasajeros pendientes de asignar
function obtenerPasajerosPendientes() {
    if (!window.infoReservaModal || !window.infoReservaModal.pasajeros || window.infoReservaModal.pasajeros.length === 0) {
        return [];
    }
    
    var pasajerosPendientes = [];
    var guidVacio = "00000000-0000-0000-0000-000000000000";
    
    // Para cada pasajero en infoReservaModal, verificar si ya está asignado a alguna butaca
    for (var i = 0; i < window.infoReservaModal.pasajeros.length; i++) {
        var pasajero = window.infoReservaModal.pasajeros[i];
        var pasajeroId = pasajero.id;
        var estaAsignado = false;
        
        // Buscar si este pasajero ya está asignado a alguna butaca
        $('#panel-bus a').each(function() {
            var butacaPasajero = $(this).attr('data-pasajero') || $(this).data('pasajero');
            if (butacaPasajero && butacaPasajero.toString().trim() === pasajeroId.toString().trim() && 
                butacaPasajero.toString().trim() !== "" && butacaPasajero.toString().trim() !== guidVacio) {
                estaAsignado = true;
                return false; // break
            }
        });
        
        if (!estaAsignado) {
            pasajerosPendientes.push(pasajero);
        }
    }
    
    return pasajerosPendientes;
}

// Función para mostrar panel de selección de pasajeros pendientes
function mostrarPanelSeleccionPasajeroPendiente(elto, butacaId) {
    var pasajerosPendientes = obtenerPasajerosPendientes();
    
    if (!pasajerosPendientes || pasajerosPendientes.length === 0) {
        // Si no hay pendientes, abrir modal completo
        var paqueteid = $("#id_paquete").val();
        var piso = elto.data("piso");
        var url = "/Reserva/SeleccionarPasajero?paqueteid=" + paqueteid + "&piso=" + piso;
        var dialogid = "SeleccionPasajero";
        var dialogtitle = "Seleccionar Pasajero";
        window.butacaActualSeleccionada = butacaId;
        ShowFormDialog(url, dialogid, dialogtitle, "default");
        return;
    }
    
    // Cerrar cualquier panel existente
    $('.panel-seleccion-pasajero-pendiente').remove();
    
    // Crear panel flotante con lista de pasajeros pendientes
    var panel = $('<div class="panel-seleccion-pasajero-pendiente"></div>');
    
    var html = '<div class="panel-header">' +
        '<h4>Seleccionar Pasajero</h4>' +
        '<button type="button" class="btn-cerrar-panel">&times;</button>' +
        '</div>' +
        '<div class="panel-content">' +
        '<p class="panel-subtitle">Pasajeros pendientes de asignar:</p>' +
        '<div class="lista-pasajeros-pendientes">';
    
    // Agregar cada pasajero pendiente
    for (var i = 0; i < pasajerosPendientes.length; i++) {
        var pasajero = pasajerosPendientes[i];
        var nombreCompleto = (pasajero.apellido || '') + ', ' + (pasajero.nombre || '');
        var dni = pasajero.nrodoc || 'N/A';
        var telefono = pasajero.telefono || 'Sin teléfono';
        
        html += '<div class="pasajero-pendiente-item" data-pasajero-id="' + pasajero.id + '">' +
            '<div class="pasajero-info">' +
            '<div class="pasajero-nombre"><strong>' + nombreCompleto + '</strong></div>' +
            '<div class="pasajero-detalles">' +
            '<span><i class="bi bi-card-text"></i> DNI: ' + dni + '</span>' +
            '<span><i class="bi bi-telephone"></i> ' + telefono + '</span>' +
            '</div>' +
            '</div>' +
            '</div>';
    }
    
    html += '</div>' +
        '<div class="panel-footer">' +
        '<button type="button" class="btn-agregar-pasajero-nuevo" data-butaca-id="' + butacaId + '">Agregar otro pasajero</button>' +
        '</div>' +
        '</div>';
    
    panel.html(html);
    
    // Posicionar panel cerca de la butaca
    var pos = elto.offset();
    var panelWidth = 350;
    var panelLeft = pos.left;
    
    // Ajustar posición para que no se salga de la pantalla
    if (panelLeft + panelWidth > $(window).width()) {
        panelLeft = $(window).width() - panelWidth - 20;
    }
    
    panel.css({
        position: 'absolute',
        top: pos.top + elto.outerHeight() + 10,
        left: panelLeft,
        zIndex: 10000
    });
    
    $('body').append(panel);
    
    // Event handler para seleccionar pasajero
    panel.find('.pasajero-pendiente-item').on('click', function() {
        var pasajeroId = $(this).data('pasajero-id');
        var precioId = window.infoReservaModal.precioId;
        var adicionales = window.infoReservaModal.adicionales || '';
        
        // Asignar pasajero a la butaca
        asignarPasajeroAButaca(butacaId, pasajeroId, precioId, adicionales);
        
        // Remover el pasajero de la lista de pendientes
        removerPasajeroDePendientes(pasajeroId);
        
        // Cerrar el panel
        panel.remove();
    });
    
    // Event handler para agregar otro pasajero (abrir modal completo)
    panel.find('.btn-agregar-pasajero-nuevo').on('click', function() {
        var paqueteid = $("#id_paquete").val();
        var piso = elto.data("piso");
        var url = "/Reserva/SeleccionarPasajero?paqueteid=" + paqueteid + "&piso=" + piso;
        var dialogid = "SeleccionPasajero";
        var dialogtitle = "Seleccionar Pasajero";
        window.butacaActualSeleccionada = butacaId;
        panel.remove();
        ShowFormDialog(url, dialogid, dialogtitle, "default");
    });
    
    // Cerrar panel
    panel.find('.btn-cerrar-panel').on('click', function() {
        panel.remove();
    });
    
    // Cerrar al hacer clic fuera
    setTimeout(function() {
        $(document).one('click', function(e) {
            if (!panel.is(e.target) && !panel.has(e.target).length) {
                panel.remove();
            }
        });
    }, 100);
}

// Función para remover un pasajero de la lista de pendientes
function removerPasajeroDePendientes(pasajeroId) {
    if (!window.infoReservaModal || !window.infoReservaModal.pasajeros) {
        return;
    }
    
    // No removemos de la lista, solo marcamos como asignado
    // La función obtenerPasajerosPendientes() ya verifica si está asignado
    // Esto permite que el usuario pueda ver qué pasajeros ya fueron asignados
}

// Función para asignar pasajeros a butacas después de cerrar el modal
function asignarPasajerosAButacas() {
    if (!window.infoReservaModal || !window.infoReservaModal.pasajeros || window.infoReservaModal.pasajeros.length === 0) {
        return;
    }
    
    var butacaId = window.butacaActualSeleccionada;
    if (!butacaId) {
        return;
    }
    
    // Si hay butaca específica seleccionada, asignar el primer pasajero disponible
    var butaca = $('#' + butacaId);
    if (butaca.length && !butaca.data('pasajero')) {
        var primerPasajero = window.infoReservaModal.pasajeros[0];
        asignarPasajeroAButaca(butacaId, primerPasajero.id, window.infoReservaModal.precioId, window.infoReservaModal.adicionales);
        
        // Si hay más pasajeros, mostrar mensaje
        if (window.infoReservaModal.pasajeros.length > 1) {
            alert('Pasajero asignado a la butaca. ' + (window.infoReservaModal.pasajeros.length - 1) + ' pasajero(s) disponible(s) para asignar a otras butacas.');
        }
    }
    
    // Limpiar variables temporales
    window.butacaActualSeleccionada = null;
}

// Función para asignar un pasajero específico a una butaca
function asignarPasajeroAButaca(butacaId, pasajeroId, precioId, adicionales) {
    var butaca = $('#' + butacaId);
    if (!butaca.length) {
        return false;
    }
    
    // Buscar información del pasajero
    var pasajeroInfo = null;
    
    // Buscar primero en infoReservaModal (pasajeros seleccionados en el modal)
    if (window.infoReservaModal && window.infoReservaModal.pasajeros) {
        pasajeroInfo = window.infoReservaModal.pasajeros.find(function(p) {
            return p.id === pasajeroId;
        });
    }
    
    // Si no se encuentra, buscar en pasajerosSeleccionadosModal (fallback)
    if (!pasajeroInfo && window.pasajerosSeleccionadosModal) {
        pasajeroInfo = window.pasajerosSeleccionadosModal.find(function(p) {
            return p.id === pasajeroId;
        });
    }
    
    // Guardar datos en la butaca usando attr para que sea persistente
    butaca.attr("data-pasajero", pasajeroId);
    butaca.data("pasajero", pasajeroId);
    butaca.attr("data-precio", precioId || '');
    butaca.data("precio", precioId);
    butaca.attr("data-adicionales", adicionales || '');
    butaca.data("adicionales", adicionales || '');
    
    // Remover clase disponible, agregar clase asignada
    butaca.removeClass('disponible');
    butaca.addClass('butaca-asignada');
    
    // Actualizar tooltip/title
    var nombreCompleto = pasajeroInfo ? (pasajeroInfo.apellido + ', ' + pasajeroInfo.nombre) : 'Pasajero asignado';
    butaca.attr('title', nombreCompleto);
    
    // Agregar badge con nombre (solo si hay espacio)
    butaca.find('.badge-pasajero-nombre').remove(); // Remover badge anterior si existe
    if (pasajeroInfo) {
        var badge = $('<span class="badge-pasajero-nombre">' + pasajeroInfo.apellido + ', ' + pasajeroInfo.nombre + '</span>');
        butaca.append(badge);
    }
    
    return true;
}

// Función para liberar una butaca
function liberarButaca(butacaId) {
    var butaca = $('#' + butacaId);
    if (!butaca.length) {
        return;
    }
    
    butaca.removeClass('butaca-asignada');
    butaca.addClass('disponible');
    butaca.removeData('pasajero');
    butaca.removeData('precio');
    butaca.removeData('adicionales');
    butaca.attr('title', '');
    butaca.find('.badge-pasajero-nombre').remove();
}

// Función para cambiar pasajero de una butaca
function cambiarPasajeroButaca(butacaId) {
    // Abrir modal para seleccionar nuevo pasajero
    var butaca = $('#' + butacaId);
    var paqueteid = $("#id_paquete").val();
    var piso = butaca.data("piso");
    
    window.butacaActualSeleccionada = butacaId;
    
    var url = "/Reserva/SeleccionarPasajero?paqueteid=" + paqueteid + "&piso=" + piso;
    var dialogid = "SeleccionPasajero";
    var dialogtitle = "Cambiar Pasajero";
    
    ShowFormDialog(url, dialogid, dialogtitle, "default");
    
    // Al cerrar, asignar el primer pasajero seleccionado
    $('#' + dialogid).on('dialogclose', function() {
        asignarPasajerosAButacas();
    });
}

function ElegirCambioButaca(elto) {
	// Asign value of the link target
	var thisTarget = elto.attr('href');
	var title = elto.attr('title');
	var paqueteid = $("#id_paquete").val();
	var pasajeid = $("#id_pasaje").val();
	var anteriorid = $("#id_anterior").val();
	var nuevopasaje = elto.attr('id');
	var piso = elto.data("piso");
	var url = "/PersonaCliente/SeleccionarImportes?paqueteid=" + paqueteid + "&piso=" + piso + "&anteriorid=" + anteriorid + "&pasajeid=" + pasajeid + "&nuevopasaje=" + nuevopasaje;
	var dialogid = "SeleccionImportes";
	var dialogtitle = "Seleccionar Pasajero";
    //debugger
	if ($("#hdnCambiarButaca").val() === "true") {
	    dialogtitle = "Confirmar Datos del Pasaje";
	}
	ShowFormDialog(url, dialogid, dialogtitle, "medium");
	elto.toggleClass('selected');
	this.blur();
	return false;
}

function ElegirButacaMulti(elto) {
    // Asign value of the link target
    var thisTarget = elto.attr('href');
    var title = elto.attr('title');
    elto.toggleClass('selected');

    $(".selected").each(function () {
        alert($(this).attr('title'));
    });


    this.blur();
    return false;
}

function AgregarLocalidad() {
    var url = "/Localidad/Create";
    var dialogid = "AgregarLocalidad";
    var dialogtitle = "Agregar una Localidad";
    ShowFormDialog(url, dialogid, dialogtitle, "min");
   // elto.toggleClass('selected');
    this.blur();
    return false;
}

function validarFecha(idCampo){ 
    var datePat = /^((([0][1-9]|[12][\d])|[3][01])[-\/]([0][13578]|[1][02])[-\/][1-9]\d\d\d)|((([0][1-9]|[12][\d])|[3][0])[-\/]([0][13456789]|[1][012])[-\/][1-9]\d\d\d)|(([0][1-9]|[12][\d])[-\/][0][2][-\/][1-9]\d([02468][048]|[13579][26]))|(([0][1-9]|[12][0-8])[-\/][0][2][-\/][1-9]\d\d\d)$/;
         
    var matchArray = idCampo.match(datePat);

    if (matchArray != null)
    {
        return true
    }
    else
    {
        return false
    }


}

function existeFecha(fecha) {
    var fechaf = fecha.split("/");
    var day = fechaf[0];
    var month = fechaf[1];
    var year = fechaf[2];
    var date = new Date(year, month, '0');
    if ((day - 0) > (date.getDate() - 0)) {
        return false;
    }
    return true;
}

function validarHora(idCampo) {
    var datePat = /^([0-9]|0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$/;

    var matchArray = idCampo.match(datePat);

    if (matchArray != null) {
        return true
    }
    else {
        return false
    }
}

function validate_fechaMayorQue(fechaInicial, fechaFinal) {
    valuesStart = fechaInicial.split("/");
    valuesEnd = fechaFinal.split("/");

    // Verificamos que la fecha no sea posterior a la actual
    var dateStart = new Date(valuesStart[2], (valuesStart[1] - 1), valuesStart[0]);
    var dateEnd = new Date(valuesEnd[2], (valuesEnd[1] - 1), valuesEnd[0]);
    if (dateStart >= dateEnd) {
        return false;
    }
    return true;
}

String.prototype.render = (function () {
    var re = /\{{(.+?)\}}/g;
    return function (o) {
        return this.replace(re, function (_, k) {
            return typeof o[k] != 'undefined' ? o[k] : '';
        });
    }
}());

var divLoading = "<div class='iconLoading'></div>";

//#format #decimal #formatdecimal
function registerFormatNumber() {
    //activate format number
    if (numeral.locale() == 'fr')
        return;

    numeral.register('locale', 'fr', {
        delimiters: {
            thousands: '.',
            decimal: ','
        },
        abbreviations: {
            thousand: 'k',
            million: 'm',
            billion: 'b',
            trillion: 't'
        },
        ordinal: function (number) {
            return number === 1 ? 'er' : '�me';
        },
        currency: {
            symbol: '$'
        }
    });

    numeral.locale('fr');

}


function formatNumberMoney(n) {

    var nReturn = numeral(n);
    numeral.defaultFormat('$0,0.00');
    return nReturn.format();
}

function showLoading() {
    var divLoading = "<div id='divFullLoading'></div>";
    $("body").append(divLoading);
}

function showLoading_div(div) {
    var divLoading = "<div id='divFullLoading'></div>";
    $("#" + div).append(divLoading);
}

function hideLoading() {
    $("#divFullLoading").remove();
}

function changeSeparatorDecimal(input) {
    return input.replaceAll(".", "").replaceAll(",", ".");
}