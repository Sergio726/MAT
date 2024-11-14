$(document).ready(function(){
		
	

});

function ElegirButaca(elto) {

    if (elto.closest(".right-container").length > 0)
        return false;
	//$('.piso_superior a').removeClass('selected');
	//$('.piso_inferior a').removeClass('selected');
	// Asign value of the link target
	var thisTarget = elto.attr('href');
	var title = elto.attr('title');
	$("#id_pasaje").val(elto.attr('id'));
	var paqueteid = $("#id_paquete").val();
	var piso = elto.data("piso");
    //var url = "/Reserva/FormReserva?pasajeroid=" + $("#id_pasajero").val() + "&pasajeid=" + elto.attr("id");
	var url = "/Reserva/SeleccionarPasajero?paqueteid=" + paqueteid + "&piso=" + piso;
	var dialogid = "SeleccionPasajero";
	var dialogtitle = "Seleccionar Pasajero";
	ShowFormDialog(url, dialogid, dialogtitle, "default");
	elto.toggleClass('selected');
	this.blur();
	return false;
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
            return number === 1 ? 'er' : 'ème';
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