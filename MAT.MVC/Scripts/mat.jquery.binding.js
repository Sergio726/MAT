$(document).on("click", "#btn-resumen-linea", function () {
    var dataid = $(this).data("comprobanteid");
    var url = "/CuentaCorriente/DetalleComprobante?id=" + dataid;
    var dialogid = "DetalleComprobante";
    var dialogtitle = "Detalle de Comprobante";
    ShowFormDialog(url, dialogid, dialogtitle);
});
$(document).on("click", ".btn_viaje", function () {
    var viajeid = $(this).data("id");
    var url = "/Reserva/Index?viajeid=" + viajeid;
    document.location.href = url;
});

$(document).on("click", "#btn_crear_pasajes", function () {
    var viajeid = $(".field-id").val();
    var url = "/Viaje/CrearPasajes?id=" + viajeid;
    $.get(url, { id: viajeid }, function (data) {
        alert(data);
    });
});

$(document).on("click", "#btn-buscar-pasajero", function () {
    var url = "/Reserva/QuickSearch";
    var dialogid = "FormClientes";
    var dialogtitle = "Búsqueda Rápida - Pasajeros";
    ShowFormDialog(url, dialogid, dialogtitle);
});

$(document).on("click", "select[name='Pago.TipoPago']", function () {
    var tipopago = parseInt($(this).val());
    $("#total-credito-label").css("display", "none");
    $("#total-credito-field").css("display", "none");
    //TIPOS DE PAGO CREDITO, DEBITO Y TRANSFERENCIA

    //TIPO DE PAGO EN EFECTIVO
    if (tipopago == 1) {
        $("#transaccion-label").hide();
        $("#transaccion-field").hide();
    }
    else {
        $("#transaccion-label").css("display", "inline-block");
        $("#transaccion-field").css("display", "inline-block");
    }
    if (tipopago == 4) {
        $("#total-credito-label").css("display", "inline-block");
        $("#total-credito-field").css("display", "inline-block");
        var ClienteID = $("#Cliente").val();

        $.ajax({
            url: "/NotaCredito/getCreditoDisponible",
            data: { ClienteID: ClienteID },
            success: function (data) {
                var response = eval(data);
                if (response[0] == "Done.") {
                    var Cred = parseFloat(response[1]);
                    $("#total-credito").val(Cred);
                }
                else {
                    alert("Ocurrió un error a intentar calcular el crédito disponible.");
                    console.log(response[1]);
                }
            },
            error: function (e) {
                alert("Ocurrió un error a intentar calcular el crédito disponible.");
                console.log(e);
            }
        })
    }
});

$(document).on("click", "#btnReservarPasaje", function () {
    var clienteid = $("#Cliente").val();
    var condicion = $("input[name=condicion]:checked").val();

    if (clienteid == "") {
        return alert("Debe seleccionar un Cliente.");
    }

    if (ViajeMonedaTipo != "1" && condicion == "Efectivo") {
        if ($("#hdnCotizacion").val() == "") {
            $("#divError").html("Debe definir la cotizacion de la moneda para el pago de este viaje.")
            $("#divError").show().removeClass("hide");
            $("#divError").fadeOut(14000);

            return;
        }

    }

    var monto = $("#txtPago").val();
    var montoFactura = $("#hdnMontoFactura").val();
    var tipopago = parseInt($("select[name='Pago.TipoPago']").val());
    
    var descuento = $("input[name=Descuento]").val();
    var detalledescuento = $("#detalledescuento").val();
    var recibo = $("#txtRecibo").val();
    var TransaccionId = $("#TransaccionId").val();
    var nroFactura = $("#txtnroFactura").val();
    var observaciones = $("#Observaciones").val();

    var eListMenor = $("#hdnListMenor").data("complete");
    var listmenores = "";
    var tutormenor = "";
    var viajeid = $("#hdnViajeID").val();
    var jsonobject = $("#jsonobject").val();
    if (eListMenor) {
        listmenores = $("#hdnListMenor").val();
        tutormenor = $("#hdnListMenor").data("tutor");
    }

    if (montoFactura == monto) {
        if ($("input[name='comprobante']:checked").val() == "Recibo") {
            return alert("Esta realizando el pago total de la factura. Debe ingresar un número de factura.");
        }
    }

    if (tipopago == 4) {
        if (parseFloat(monto) > parseFloat($("#total-credito").val()))
        {
            return alert("Credito insufiente. Ingrese un monto menor o igual al Crédito Disponible.")
        }
    }

    if (descuento >0) {
        if (detalledescuento.trim() == '')
        {
            return alert('Por favor agregue una descripción para el descuento aplicado.');
        }
    }
    
    /*pago con cotizador*/
    var MontoRecibidoMonedaTipo = $("#sMonedaTipo").val();
    var MontoEquivalente = "";
    var MontoEquivalenteMonedaTipo = "";
    var MontoEquivalenteCotizacion = ""
    var ViajeMonedaTipo = $("#hdnViajeMonedaTipo").val();
    var MontoRecibido = monto;
          
    if ((ViajeMonedaTipo != "1") || (MontoRecibidoMonedaTipo != "1")) {

        
        MontoEquivalenteCotizacion = $("#hdnCotizacion").val();

        if (MontoRecibidoMonedaTipo == "1") {
            MontoEquivalenteMonedaTipo = "3"; //dolar
            MontoEquivalente = $("#MontoOut").val();
            
        }
        else {
            MontoEquivalenteMonedaTipo = "1"; //pesos
            MontoEquivalente = $("#MontoInt").val();
        }

        if (ViajeMonedaTipo != MontoRecibidoMonedaTipo) {
            monto = MontoEquivalente;//monto recibido, tabla pago
        }
    }

    var divLoading = "<div id='divFullLoading'></div>";
    $("#FormReserva").append(divLoading);
    $.ajax({
        url: "/Reserva/FormReserva",
        type: "POST",
        data: {
            cliente: clienteid,
            monto: monto,
            montoFactura: montoFactura,
            tipopago: tipopago,
            condicion: condicion,
            descuento: descuento,
            recibo: recibo,
            TransaccionId: TransaccionId,
            nroFactura: nroFactura,
            observaciones: observaciones,
            jsonobject: jsonobject,
            listmenores: listmenores,
            tutormenor: tutormenor,
            viajeid: viajeid,
            detalledescuento: detalledescuento,
            MontoRecibido: MontoRecibido,
            MontoRecibidoMonedaTipo: MontoRecibidoMonedaTipo,
            MontoEquivalente: MontoEquivalente,
            MontoEquivalenteMonedaTipo: MontoEquivalenteMonedaTipo,
            MontoEquivalenteCotizacion: MontoEquivalenteCotizacion,
            ViajeMonedaTipo: ViajeMonedaTipo
        },
        success: function (data) {

            $("#divFullLoading").remove();

            var result = data == "True" ? true : false;
            $("#FormReserva").dialog("close");
            if (data == "Pagado") {
                alert("Reserva realizada correctamente. Pago total recibido.");
                $(document).off("click", "#panel-bus a.selected", null);
                $("#panel-bus a.selected").addClass("reservado").removeClass("selected");
            } else if (data == "Señado") {
                alert("Reserva realizada correctamente. Seña parcial recibida.");
                $(document).off("click", "#panel-bus a.selected", null);
                $("#panel-bus a.selected").addClass("señado").removeClass("selected");
            } else if (data == "Pre-reserva") {
                alert("Pre-Reserva realizada correctamente.");
                $(document).off("click", "#panel-bus a.selected", null);
                $("#panel-bus a.selected").addClass("prereserva").removeClass("selected");
            } else if (data == "Error") {
                alert("Error de Sistema. Contacte con el Administrador.");
            }
        }
    });
});

// El filtrado ahora se maneja en SeleccionarPasajero.cshtml con autocomplete
// Este evento se mantiene por compatibilidad pero ya no se usa
// $(document).on("keypress", "#txt-filter-pasajero", function () {
//     var filter = $(this).val();
//     var viajeid = $("#id_viaje").val();
//     var url = "/Reserva/RenderGridPasajeros?viajeid=" + viajeid + "&filter=" + filter;
//     $("div#grid-pasajeros").load(url);
// });

$(document).on("keypress", "#txt-filter-cliente", function () {
    var filter = $(this).val();
    var url = "/PersonaCliente/RenderGridClientes?filter=" + filter;
    $("div#grid-clientes").load(url);
});

// Handler de DataTable eliminado - ahora se usa sistema de checkboxes en autocomplete
// $(document).on("click", "#grid-seleccionar-pasajero tbody tr.row-pasajero", function (e) { ... });

/*
$(document).on("click", "#btn-reservar", function () {
    if ($("#panel-bus a.selected").size() > 0) {
        var pasajesList = new Hashtable();
        
        var pasajes = [];
        $("#panel-bus a.selected").each(function () {
            var pasaje = {
                "adicionalesid": $(this).data("adicionales").trim(),
                "pasajeid": $(this).attr("id").trim(),
                "pasajeroid": $(this).data("pasajero").trim(),
                "precioid": $(this).data("precio").trim()
            }
            pasajes.push(pasaje);
            pasajesList.put($(this).attr("id").trim(), $(this).data("pasajero").trim());
        });
              
        var url = "/Reserva/FormReserva?jsonobject=" + JSON.stringify(pasajes); 
        
        var eListMenor = $("#hdnListMenor").data("complete");
        var montoSeguroMenor = $("#hdnListMenor").data("monto");
        
        if (eListMenor) {
            url = url + "&montoSeguroMenor=" + montoSeguroMenor;
        }

        var dialogid = "FormReserva";
        var dialogtitle = "Reserva de Pasajes";
        ShowFormDialog(url, dialogid, dialogtitle, "min");
        
    } else {
        alert("Sr. Usuario debe seleccionar al menos una butaca para la reserva.");
    }

});
*/

$(document).on("click", "#btn-reservar", function () {
    if ($("#panel-bus a.selected").size() > 0) {
        var pasajesList = new Hashtable();
        var pasajes = [];
        var butacasSinPasajero = [];

        // Validar que todas las butacas seleccionadas tengan pasajero asignado
        $("#panel-bus a.selected").each(function () {
            var pasajeroid = $(this).data("pasajero");
            var precioid = $(this).data("precio");
            
            if (!pasajeroid || pasajeroid.toString().trim() === "") {
                butacasSinPasajero.push($(this).attr("id"));
            }
        });

        if (butacasSinPasajero.length > 0) {
            alert("Hay " + butacasSinPasajero.length + " butaca(s) seleccionada(s) sin pasajero asignado. Por favor asigne un pasajero a cada butaca antes de reservar.");
            return;
        }

        // Recopilar información de todas las butacas seleccionadas
        $("#panel-bus a.selected").each(function () {
            var pasajeroid = $(this).data("pasajero");
            var precioid = $(this).data("precio");
            var adicionalesid = $(this).data("adicionales") || "";
            
            // Si no tiene precio, usar el del modal si está disponible
            if (!precioid && window.infoReservaModal && window.infoReservaModal.precioId) {
                precioid = window.infoReservaModal.precioId;
            }
            
            // Si no tiene adicionales, usar los del modal si está disponible
            if (!adicionalesid && window.infoReservaModal && window.infoReservaModal.adicionales) {
                adicionalesid = window.infoReservaModal.adicionales;
            }

            var pasaje = {
                "pasajeid": $(this).attr("id").trim(),
                "pasajeroid": pasajeroid.toString().trim(),
                "precioid": precioid ? precioid.toString().trim() : "",
                "adicionalesid": adicionalesid.toString().trim()
            };
            
            pasajes.push(pasaje);
            pasajesList.put($(this).attr("id").trim(), pasajeroid.toString().trim());
        });

        if (pasajes.length === 0) {
            alert("No hay pasajes válidos para reservar.");
            return;
        }

        // Manejar lógica de menores si aplica
        var nroMenores = window.infoReservaModal ? window.infoReservaModal.nroMenores : 0;
        if (nroMenores > 0) {
            var eListMenor = $("#hdnListMenor").data("complete");
            var montoSeguroMenor = $("#hdnListMenor").data("monto");
            
            if (!eListMenor && pasajes.length > 0) {
                // Asignar tutor al primer pasajero
                var primerPasajeroId = pasajes[0].pasajeroid;
                $("#hdnListMenor").data("tutor", primerPasajeroId);
                $("#hdnListMenor").data("monto", montoSeguroMenor || (nroMenores * ($("#numMenor").data("monto") || 0)));
                $("#hdnListMenor").data("complete", true);
            }
        }

        var _data = {"pasajes": pasajes};
        $.ajax({
            url: "/Reserva/ReservarPasajes",
            type: "POST",
            data: JSON.stringify({ "pasajes": pasajes }),
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (result) {
                var dialogid = "FormReserva";
                var dialogtitle = "Reserva de Pasajes";
                ShowFormDialogHTML(result, dialogid, dialogtitle, "min");
            },
            error: function(xhr, status, error) {
                alert("Error al procesar la reserva: " + error);
            }
        });

    } else {
        alert("Sr. Usuario debe seleccionar al menos una butaca para la reserva.");
    }
});

$(function () {
    $("#txt-pasajero").autocomplete({
        minLength: 3,
        source: function (request, response) {
            $.ajax({
                url: "/Reserva/QuickPasajeroSearch",
                data: { query: request.term },
                success: function (data) {
                    response($.map(data,
                                function (item) {
                                    return {
                                        label: item.Nombre + " " + item.Apellido,
                                        value: item.Nombre + " " + item.Apellido,
                                        id: item.PasajeroId,
                                        nrodocumento: item.NroDocumento,
                                        telefono: item.Telefono
                                    }
                                }));
                }
            })

        },
        select: function (event, ul) {
            $("#id_pasajero").val(ul.item.id);
            $("#resultado-pasajero").html("");
            var fieldSet = $("<fieldset><legend>Detalle de Pasajero</legend></fieldset>");
            var pNombre = $("<h2>Pasajero: " + ul.item.label + "</h2>");
            var pDni = $("<p>DNI: " + ul.item.nrodocumento + "</p>");
            var pTelefono = $("<p>Teléfono: " + ul.item.telefono + "</p>");
            fieldSet.append(pNombre).append(pDni).append(pTelefono);
            $("#resultado-pasajero").append(fieldSet);
        }
    });
});

$(function () {
    $("#txt-filter-persona").autocomplete({
        minLength: 3,
        source: function (request, response) {
            //$.ajax({
            //    url: "/Busqueda/PersonaAutocomplete",
            //    data: { query: request.term },
            //    success: function (data) {
            //        response($.map(data,
            //                    function (item) {
            //                        return {
            //                            label: item.Nombre + " " + item.Apellido,
            //                            value: item.Nombre + " " + item.Apellido,
            //                            id: item.PersonaId,
            //                            nrodocumento: item.NroDocumento,
            //                            telefono: item.Telefono
            //                        }
            //                    }));
            //    }
            //})
            $.ajax({
                url: "/Factura/PersonaAutocomplete",
                data: { sParam: request.term },
                success: function (data) {
                    response($.map(data,
                                function (item) {
                                    return {
                                        label: item.Nombre + " " + item.Apellido + " - Doc: " + item.NroDocumento,
                                        value: item.Nombre + " " + item.Apellido,
                                        id: item.PersonaId,
                                        //nrodocumento: item.NroDocumento,
                                        //telefono: item.Telefono
                                    }
                                }));
                }
            })
            
        },
        select: function (event, ul) {
            $(".card-perfil").load("/Busqueda/Card?personaid=" + ul.item.id);
            $("div.resumen-perfil").html("");
        }
    });
});

$(document).on("click", ".btn-detallefactura", function () {
    var url = "/PersonaCliente/PopupDetalleFactura?facturaid=" + $(this).data("facturaid");
    var title = "Detalle de Factura"; //Titulo Modificado por petición del cliente. Detalle de Factura -> Nota de Crédito
    var id = "Detalles";
    ShowFormDialog(url, id, title);
});


$(document).on("click", "#btn-cerrar-detalle-pago", function () {
    $("#DetallePago").dialog("close");
});

$(document).on("click", "#btn-buscar-persona", function () {
    $.ajax({
        url: "/Busqueda/Buscar",
        dataType: "text/html",
        data: { cliente: clienteid, estado: estado, monto: monto, tipopago: tipopago, transaccionid: transaccionid },
        success: function (data) {
            var result = data == "True" ? true : false;
            $("#FormReserva").dialog("close");
            if (result) {
                alert("Reserva realizada correctamente.");
                $("#panel-bus a.selected").addClass("reservado").removeClass("selected").off("click");
            } else {
                alert("Error de Sistema. Contacte con el Administrador.");
            }
        }
    });
});

$(document).on("click", "#btnEliminarVenta", function () {
    var facturaid = $("#FacturaId").val();
    var clienteid = $("#ClienteId").val();
    var code = $("#Codigo").val();
    $.ajax({
        url: "/PersonaCliente/EliminarVenta",
        type: "POST",
        data: { facturaid: facturaid, clienteid: clienteid, code: code },
        success: function (data) {
            $("#EliminarVenta").dialog("close");
            var result = data == "True" ? true : false;
            if (result) {
                alert("La venta ha sido eliminada correctamente.");
                setTimeout(function () { location.reload(); }, 1000);
            } else {
                alert("Error de Sistema. Verifique el código de seguridad o contacte con el Administrador de Sistema.");
            }
        }
    });
});

$(document).on("click", "#btn-perfil-cc", function () {
    $("div.resumen-perfil").load("/PersonaCliente/partialHistorialdePagos?ClienteID=" + $(this).data("personaid"));
});

$(document).on("click", "#btn-perfil-facturas", function () {
    $("div.resumen-perfil").load("/PersonaCliente/PartialFacturas?clienteid=" + $(this).data("personaid"));
});

$(document).on("click", "#btn-perfil-historialviaje", function () {
    $("div.resumen-perfil").load("/PersonaPasajero/PartialPasajerosHistorial?pasajeroid=" + $(this).data("personaid"));
});

$(document).on("click", "#btn-nomina", function () {
    var url = "/PasajeroViaje/Manifiesto?id=" + $(this).data("id");
    var title = "Manifiesto Previsorio";
    var id = "ManifiestoPrevisorio";
    window.open(url, '_blank').print();
});

$(document).on("click", "#btn-listasimple", function () {
    var url = "/PasajeroViaje/ListadoSimple?id=" + $(this).data("id");
    var title = "Listado de Pasajeros";
    var id = "ListadoPasajeros";
    window.open(url, '_blank').print().close();
});

$(document).on("click", "#btn-cnrt", function () {
    var url = "/PasajeroViaje/CNRT?id=" + $(this).data("id");
    var title = "Listado CNRT";
    var id = "ListadoCNRT";
    window.open(url, '_blank').print();
});

$(document).on("click", "#btn-cnrt-hojauno", function () {
    var url = "/PasajeroViaje/CNRTHojaUno";
    var title = "CNRT Hoja Uno";
    var id = "CNRTHojaUno";
    window.open(url, '_blank');
});

$(document).on("click", "#btn-distribucion-habitaciones", function () {
    var viajeid = $(this).data("viaje");
    var url = "/Hotel/Distribucion?viajeid=" + viajeid;
    var title = "Distribucion de Habitaciones";
    var id = "DistribucionHabitaciones";
    window.open(url, '_blank');
});

$(document).on("click", "#btn-imprimirvouchers", function () {
    var facturaId = $(this).data("facturaid");
    $("#divSelectTipoVoucher").dialog();
    //preguntar por grupal o individual

    //$.ajax({
    //    url: "/PersonaCliente/Voucher_GetNrPrintByFacturaID",
    //    data: { facturaId: facturaId },
    //    dataType: "html",
    //    success: function (data) {
    //        var response = eval(data);
    //        if (response[0] === "Done.") {
    //            if (parseInt(response[1]) > 0) {

    //                var msg = "<center><span><b>Este Voucher se imprimió " + response[1] + " veces.</b></br> Desea imprimirlo nuevamente?</span></center>";
    //                $("#Voucher_dialog-message").html(msg);
    //                $("#Voucher_dialog-message").dialog({
    //                    modal: true,
    //                    buttons: {
    //                        Ok: function () {
    //                            $(this).dialog("close");
    //                            showVoucher(facturaId);
    //                        },
    //                        Cancel: function () {
    //                            $(this).dialog("close");
    //                        }
    //                    }
    //                });
    //            }
    //            else { showVoucher(facturaId); }

    //        }
    //    }
    //});


    
});

function imprimirVoucher(facturaId, TipoVoucher) {
    var bInfoAdicional = $("#chkInfoAdicional").is(":checked");
    $.ajax({
        url: "/PersonaCliente/Voucher_GetNrPrintByFacturaID",
        data: { facturaId: facturaId },
        dataType: "html",
        success: function (data) {
            var response = eval(data);
            if (response[0] === "Done.") {
                if (parseInt(response[1]) > 0) {

                    var msg = "<center><span><b>Este Voucher se imprimió " + response[1] + " veces.</b></br> Desea imprimirlo nuevamente?</span></center>";
                    $("#Voucher_dialog-message").html(msg);
                    $("#Voucher_dialog-message").dialog({
                        modal: true,
                        buttons: {
                            Ok: function () {
                                $("#divSelectTipoVoucher").dialog("destroy").remove();
                                $(this).dialog("close");
                                showVoucher(facturaId, TipoVoucher, bInfoAdicional);
                            },
                            Cancel: function () {
                                $("#divSelectTipoVoucher").dialog("destroy").remove();
                                $(this).dialog("close");
                            }
                        }
                    });
                }
                else {
                    showVoucher(facturaId, TipoVoucher, bInfoAdicional);
                    $("#divSelectTipoVoucher").dialog("close");
                }

            }
        }
    });
}

function showVoucher(facturaId, TipoVoucher, bInfoAdicional)
{
    var url = "/PersonaCliente/Voucher?facturaid=" + facturaId + "&sTipoVoucher=" + TipoVoucher + "&bInfoAdicional=" + bInfoAdicional;
    var title = "Vouchers";
    var id = "Vouchers";
    window.open(url, '_blank').print();
}

$(document).on("click", "#btn-notacredito", function () {
    var facturaid = $(this).data("facturaid");
    var clienteid = $(this).data("clienteid");
    var Fecha = $(this).data("fecha");
    var Monto = $(this).data("monto");
    var TotalPagos = $("#hdnTotalPagos").val();
    
    // Validar que los datos necesarios estén presentes
    if (!facturaid || !clienteid) {
        alert("Error: Faltan datos necesarios para generar la nota de crédito.");
        console.error("Datos faltantes - facturaid:", facturaid, "clienteid:", clienteid);
        return;
    }
    
    var title = "Nota de Credito";
    var id = "NotaCredito";
    
    // Codificar correctamente los parámetros de la URL
    var url = "/PersonaCliente/RegistrarNotaCredito" +
        "?facturaid=" + encodeURIComponent(facturaid) +
        "&clienteid=" + encodeURIComponent(clienteid) +
        "&sFecha=" + encodeURIComponent(Fecha || "") +
        "&sMonto=" + encodeURIComponent(Monto || "") +
        "&sTotalPagos=" + encodeURIComponent(TotalPagos || "");
    
    ShowFormDialog(url, id, title, "min");
});

$(document).on("click", "#btn-retencion", function () {
    if ($("#NroNota").val().trim() == "") {
        return alert("El nro de nota es obligatorio.");
    }
    var fmontoRetencion = parseFloat($("#MontoRetencion").val().replace(".", "").replace(",", "."));
    var fmontohdnTotalPagos = parseFloat($("#hdnTotalPagos").val().replace(".", "").replace(",", "."));
    var fmontohdnMontoNota = parseFloat($("#MontoNota").val().replace(".", "").replace(",", "."));
    if (fmontoRetencion > fmontohdnTotalPagos) {
        return alert("El monto a retener no debe ser mayor al total de pagos.");
    }
    if (fmontohdnMontoNota > fmontohdnTotalPagos) {
        return alert("El monto de la Nota de Crédito no debe ser mayor al total de pagos.");
    }
    if ($("#DetalleNotaCredito").val().trim() == "") {
        return alert("Debe ingresar un detalle de Nota de Crédito.");
    }

    
    var _data = $("#formNotaCredito").serializeArray();
    $.ajax({
        url: "/PersonaCliente/RegistrarNotaCredito",
        type: "POST",
        data: _data,
        success: function (data) {
            var result = eval(data);
            $("#NotaCredito").dialog("close");
            $("#Detalles").dialog("close");
            if (result[0]== "Done.") {
                alert("Nota de Crédito realizada correctamente.");
            } else {
                console.log(result[1])
                alert("Error de Sistema. Contacte con el Administrador.");
            }
        }
    });
});

$(document).on("click", "#btn-retencion-cancelar", function () {
    $("#NotaCredito").dialog("close");
});

$(document).on("click", "input[name=condicion]", function () {
    var condicion = $("input[name=condicion]:checked").val();
    if (condicion == "Efectivo") {
        $("#efectivo").css("display", "inline-block");
    } else {
        $("#efectivo").hide();
    }
});

$(document).on("click", ".reservado", function () {
    var idPasaje = (this).getAttribute('id');
    var idPasajero = $(this).data("pasajero").trim();
    $("#hdnPasajeroID").val(idPasajero);
    var dialogtitle = "Reserva - Habitacion";

    //$.ajax({
    //    url: "/ReservaHabitacion/SetPasaje?PasajeId=" + idPasaje + "&PasajeroId=" + idPasajero,
    //    dataType: "text/html",
    //    data: { PasajeId: idPasaje, PasajeroId: idPasajero },
    //    success: function (data) {
    //        console.log("ok");
    //    }
    //});
    var idviaje = $("#id_viaje").val();
    var url = "/ReservaHabitacion/Index?id=" + idviaje + "&PasajeID=" + idPasaje + "&PasajeroID=" + idPasajero;
    console.log(url);
    ShowFormDialog(url, "idResHab", "Reserva - Habitacion");

});

$(document).on("click", ".prereserva", function () {
    
    var idPasaje = (this).getAttribute('id');
    var idPasajero = $(this).data("pasajero").trim();
    $("#hdnPasajeroID").val(idPasajero);
    var dialogtitle = "Reserva - Habitacion";

    //$.ajax({
    //    url: "/ReservaHabitacion/SetPasaje?PasajeId=" + idPasaje + "&PasajeroId=" + idPasajero,
    //    dataType: "text/html",
    //    data: { PasajeId: idPasaje, PasajeroId: idPasajero },
    //    success: function (data) {
    //        console.log("ok");
    //    }
    //});
    var idviaje = $("#id_viaje").val();
    var url = "/ReservaHabitacion/Index?id=" + idviaje + "&PasajeID=" + idPasaje + "&PasajeroID=" + idPasajero;
    console.log(url);
    ShowFormDialog(url, "idResHab", "Reserva - Habitacion");

});

$(document).on("click", ".señado", function () {
    var idPasaje = (this).getAttribute('id');
    var idPasajero = $(this).data("pasajero").trim();
    $("#hdnPasajeroID").val(idPasajero);
    var dialogtitle = "Reserva - Habitacion";

    //$.ajax({
    //    url: "/ReservaHabitacion/SetPasaje?PasajeId=" + idPasaje + "&PasajeroId=" + idPasajero,
    //    dataType: "text/html",
    //    data: { PasajeId: idPasaje, PasajeroId: idPasajero },
    //    success: function (data) {
    //        console.log("ok");
    //    }
    //});

    var idviaje = $("#id_viaje").val();
    var url = "/ReservaHabitacion/Index?id=" + idviaje + "&PasajeID=" + idPasaje + "&PasajeroID=" + idPasajero;
    console.log(url);
    ShowFormDialog(url, "idResHab", "Reserva - Habitacion", "medium");

});

$(document).on("click", "#btnReservarHabitacion", function () {


    //*************************************Validar campos  *************************************
    $(".date").css("background-color", "#fff");
    $(".time").css("background-color", "#fff");
    var iFecha = 0;
    //$.each($(".date"), function (i, l) {
    //    if (!validarFecha($(this).val().trim())) {
    //        $(this).css("background-color", "#E83A61");
    //        iFecha = iFecha + 1;
    //    }

    //    if (!existeFecha($(this).val().trim())) {
    //        $(this).css("background-color", "#E83A61");
    //        iFecha = iFecha + 1;
    //    }

    //});
    if (iFecha > 0) {
        return alert("Por favor verifique que las fechas ingresadas no esten en blanco y sean correctas dd/mm/yyyy.", "Fecha Incorrecta")

    }

    var iHora = 0;
    $.each($(".time"), function (i, l) {
        if (!validarHora($(this).val().trim())) {
            $(this).css("background-color", "#E83A61");
            iHora = iHora + 1;
        }

    });
    if (iHora > 0) {
        return alert("Por favor verifique que las horas ingresadas no esten en blanco y sean correctas hh:mm.", "Hora Incorrecta")

    }

    //************************************* fin Validar campos  *************************************


    var Desde = $("#txtFechaDesde").val().trim();
    var Hasta = $("#txtFechaHasta").val().trim();
    var HoraDesde = $("#txtHoraDesde").val();
    var HoraHasta = $("#txtHoraHasta").val();
    var dialogtitle = "Reserva - Habitacion";
    var viajeid = $("#reserva-viaje-id").val();
    var HabitacionID = $("#habitacionid").val();
    var PasajeroID = $("#hdnPasajeroID").val();
    var PasajeID = $("#hdnPasajeID").val();

    
    var divLoading = "<div id='divFullLoading'></div>";
    $("#divReservaHabitacion").append(divLoading);
    //debugger
    $.ajax({
        url: "/ReservaHabitacion/SetReserva",
        data: { Desde: Desde, Hasta: Hasta, horadesde: HoraDesde, horahasta: HoraHasta, viajeid: viajeid, HabitacionID: HabitacionID, PasajeroID: PasajeroID, PasajeID:PasajeID },
        dataType: "text",
        success: function (data) {
           
            var resultado = "";
            var IdPasaje = "";
            //var countData = data.length;

            for (var i = 0; i < 4; i++) {
                resultado = resultado + data[i];
            }

            for (var i = 6; i <= 41; i++) {
                IdPasaje = IdPasaje + data[i];
            }
           
            $('#divReserva').dialog('close');

            if (resultado == "true") {
                fnFiltrarHabitacionCambio();
                setTimeout(5000);

                var pathname = window.location.pathname;

                if (pathname == "/Reserva/Index") {
                    alert('Reserva Exitosa');
                }
                else {
                    PopupDetalleFactura_Load();
                    alert('Reserva Exitosa');
                }
                window.location.reload(true);
            }
            else {
                alert('No se pudo realizar la reserva');
            }

            
            $('#idResHab').dialog('close');
        }
    });

});

$(document).on("click", '#coche51_61 .piso_superior_edicion a.disponible', function () {
    ElegirCambioButaca($(this));
});

$(document).on("click", '#coche51_61 .piso_inferior_edicion a.disponible', function () {
    ElegirCambioButaca($(this));
});




$(document).on("click", '#coche51_61 .piso_superior a.disponible', function () {
    ElegirButaca($(this));
});

$(document).on("click", '#coche51_61 .piso_inferior a.disponible', function () {
    ElegirButaca($(this));
});

$(document).on("click", '#coche50 .piso_superior a.disponible', function () {
    ElegirButaca($(this));
});

$(document).on("click", '#coche50 .piso_inferior a.disponible', function () {
    ElegirButaca($(this));
});

//minibus
//$(document).on("click", '#coche_minibus .piso_superior_edicion a.disponible', function () {
//    ElegirCambioButaca($(this));
//});

$(document).on("click", '#coche_minibus .piso_superior a.disponible', function () {
    ElegirButaca($(this));
});

$(document).on("click", '#coche_minibus .piso_superior_edicion a.disponible', function () {
    ElegirCambioButaca($(this));
});

//PISO ELEVADO
$(document).on("click", '#coche_pisoelevado .piso_inferior a.disponible', function () {
    ElegirButaca($(this));
});

$(document).on("click", '#coche_pisoelevado .piso_inferior_edicion a.disponible', function () {
    ElegirCambioButaca($(this));
});


$(document).on("click", "input[name='chk_transporte']", function () {
    if ($(this).is(":checked")) {
        $("select[name='TransporteId']").attr("disabled", false);
    } else {
        $("select[name='TransporteId']").attr("disabled", true);
    }
});

$(document).on("click", "input[name='chk_hotel']", function () {
    if ($(this).is(":checked")) {
        $("select[name='HotelId']").attr("disabled", false);
    } else {
        $("select[name='HotelId']").attr("disabled", true);
    }
});


$(document).on("keyup.autocomplete", "#txt-busqueda-localidad", function () {
    $(this).autocomplete({
        minLength: 3,
        source: function (request, response) {
            var IdProvincia = $("#Provincia").val();
            $.ajax({
                url: "/Home/QuickLocalidadSearch",
                data: {
                    "query": request.term,
                    "sIdProvincia": IdProvincia
                },
                success: function (data) {
                    response($.map(data,
                                function (item) {
                                    return {
                                        label: item.Nombre,
                                        value: item.Nombre,
                                        id: item.Id
                                    }
                                }));
                }
            })

        },
        select: function (event, ul) {
            $("#LocalidadId").val(ul.item.id);
        }
    });
});

$(document).on("keyup.autocomplete", "#txt-busqueda-localidad-empresa", function () {
    $(this).autocomplete({
        minLength: 3,
        source: function (request, response) {
            $.ajax({
                url: "/Home/QuickLocalidadSearch",
                data: { query: request.term },
                success: function (data) {
                    response($.map(data,
                                function (item) {
                                    return {
                                        label: item.Nombre,
                                        value: item.Nombre,
                                        id: item.Id
                                    }
                                }));
                }
            })

        },
        select: function (event, ul) {
            $("#LocalidadEmpresa").val(ul.item.id);
        }
    });
});

$(document).on("keyup.autocomplete", "#txt-busqueda-destino", function () {
    $(this).autocomplete({
        minLength: 3,
        source: function (request, response) {
            $.ajax({
                url: "/Home/QuickLocalidadSearch",
                data: { query: request.term },
                success: function (data) {
                    response($.map(data,
                                function (item) {
                                    return {
                                        label: item.Nombre,
                                        value: item.Nombre,
                                        id: item.Id
                                    }
                                }));
                }
            })

        },
        select: function (event, ul) {
            $("#DestinoID").val(ul.item.id);
        }
    });
});

$(document).on("click", "#chkDescuento", function () {
    if ($(this).is(":checked")) {
        $("#txtDescuento").attr("disabled", false);
        $("#detalledescuento").attr("disabled", false);
    } else {
        $("#txtDescuento").attr("disabled", true);
        $("#detalledescuento").attr("disabled", true);
    }
});

$(document).on("click", "#btnCancelarReserva", function () {
    $("#FormReserva").dialog("close");
    $("a.selected").removeClass("selected").addClass("disponible");
});

$(document).on("keyup", "#frmCreateCliente #NroDocumento", function () {
    if ($("#NroDocumento").val().trim().length = 10 && $.isNumeric($("#NroDocumento").val().replace('.', '').replace('.', ''))) {
        var nrodoc = $("#NroDocumento").val().trim();

        $.ajax({
            type: "POST",
            url: "/PersonaCliente/ExistDni",
            data: { "dni": nrodoc },
            dataType: "json",
            success: function (response) {
               
                if (response.Result == "Done.") {
                    
                    if (response.Data == "True") {
                        $("#NroDocumento").val("");
                        alert("El DNI ingresado ya existe en el Sistema. Por favor ingrese otro DNI o contacte con el administrador del Sistema.");
                    }
                }
                else {
                    alert(response.Result);
                }

            }
        });

        //$.ajax({
        //    url: "/PersonaCliente/ExistDni",
        //    type: "POST",
        //    data: { dni: nrodoc },
        //    success: function (data) {
        //        if (data == "True") {
        //            $("#NroDocumento").val("");
        //            alert("El DNI ingresado ya existe en el Sistema. Por favor ingrese otro DNI o contacte con el administrador del Sistema.");
        //        }
        //    }
        //});
    }
});

$(document).on("keyup", "#frmEditCliente #NroDocumento", function () {
    if ($("#NroDocumento").val().trim().length = 10 && $.isNumeric($("#NroDocumento").val().replace('.', '').replace('.', ''))) {
        var nrodocOld = $("#hdnNroDocumento").val().trim();
        var nrodoc = $("#NroDocumento").val().trim();

        $.ajax({
            type: "POST",
            url: "/PersonaCliente/ExistDni",
            data: { "dni": nrodoc },
            dataType: "json",
            success: function (response) {
                
                if (response.Result == "Done.") {

                    if (response.Data == "True") {
                        $("#NroDocumento").val("");
                        alert("El DNI ingresado ya existe en el Sistema. Por favor ingrese otro DNI o contacte con el administrador del Sistema.");
                    }
                  
                }
                else {
                    alert(response.Result);
                }

            }
        });

    }
});

$(document).on("click", "#btnAgregarCliente", function () {
    var nrodoc = $("input[name=NroDocumento]").val();
    if ($("#Apellido").val().trim() == "") {
        return alert("Por favor ingrese el apellido de la persona.");
    }
    if ($("#Nombre").val().trim() == "") {
        return alert("Por favor ingrese el nombre de la persona.");
    }
    if ($("#NroDocumento").val().trim() == "") {
        return alert("Por favor ingrese el número de documento de la persona.");
    }
    if ($("#Domicilio").val().trim() == "") {
        return alert("Por favor ingrese el domicilio de la persona.");
    }
    if ($("#Telefono").val().trim() == "") {
        return alert("Por favor ingrese el télefono de la persona.");
    }
    if ($("#FechaNacimiento").val().trim() == "") {
        return alert("Por favor ingrese la fecha de nacimiento.");
    }
    if ($("#Provincia").val().trim() == "0") {
        return alert("Por favor seleccione una Provincia.");
    }
    if ($("#ddDepartamento").val().trim() == "0") {
        return alert("Por favor seleccione un Departamento.");
    }
    if ($("#ddLocalidad").val().trim() == "0") {
        return alert("Por favor seleccione una localidad.");
    }
    if ($("#Nacionalidad").val().trim() == "") {
        return alert("Por favor ingrese la nacionalidad de la persona.");
    }
    if ($("#PaisResidencia").val().trim() == "") {
        return alert("Por favor ingrese un pais de residencia.");
    }
    //if ($("#Cuit").val().trim() == "") {
    //    return alert("Por favor ingrese el CUIL o CUIL de la persona.");
    //}
    $.ajax({
        type: "POST",
        url: "/PersonaCliente/ExistDni",
        data: { "dni": nrodoc },
        dataType: "json",
        success: function (response) {
            
            if (response.Result == "Done.") {

                if (response.Data == "True") {
                    $("#NroDocumento").val("");
                    alert("El DNI ingresado ya existe en el Sistema. Por favor ingrese otro DNI o contacte con el administrador del Sistema.");
                }
                else {
                    $("#frmCreateCliente").submit();
                }
            }
            else {
                alert(response.Result);
            }

        }
    });
});

$(document).on("click", "#btn-cancelar-pago", function () {
    var comprobanteid = $(this).data("comprobanteid");
    confirm("¿Está seguro de cancelar el presente pago?", "Confirmación de pago",
    function () {
        var $dlg = $(this);
        $.ajax({
            url: "/PersonaCliente/CancelarPago",
            data: { movimientoid: comprobanteid },
            success: function (data) {
                console.log(data);
                if (data == "True") {
                    window.location.reload(true);
                } else {
                    alert("Error en la Operación. Contacte con el Administrador del Sistema.");
                }
            }
        });
        $dlg.dialog("close");
        $dlg.remove();
    },
    function () {
        var $dlg = $(this);
        $dlg.dialog("close");
        $dlg.remove();
    })
});

$(document).on("click", "#btn-cancelar-nota", function () {
    var comprobanteid = $(this).data("comprobanteid");
    confirm("¿Está seguro de cancelar la nota de crédito?", "Confirmación Baja Nota",
    function () {
        var $dlg = $(this);
        $.ajax({
            url: "/PersonaCliente/CancelarNota",
            data: { movimientoid: comprobanteid },
            success: function (data) {
                console.log(data);
                if (data == "True") {
                    window.location.reload(true);
                } else {
                    alert("Error en la Operación. Contacte con el Administrador del Sistema.");
                }
            }
        });
        $dlg.dialog("close");
        $dlg.remove();
    },
    function () {
        var $dlg = $(this);
        $dlg.dialog("close");
        $dlg.remove();
    })
});

$(document).on("click", "#btn-eliminar-servicio", function () {
    var id = $(this).data("id");
    confirm("¿Está seguro que desea eliminar este servicio?", "Confirmación Eliminar Servicio",
    function () {
        var $dlg = $(this);
        $.ajax({
            url: "/Servicio/Delete",
            data: { id: id },
            success: function (data) {
                if (data == "True") {
                    alert("Servicio Eliminado Correctamente.");
                    setTimeout(5000);
                    window.location.reload();
                } else {
                    alert("Error en la Operación. Contacte con el Administrador del Sistema.");
                }
            }
        });
        $dlg.dialog("close");
        $dlg.remove();

    },
    function () {
        var $dlg = $(this);
        $dlg.dialog("close");
        $dlg.remove();
    })
});

$(document).on("click", "#btn-eliminar-servicio-admin", function () {
    var id = $(this).data("id");
    confirm("¿Está seguro que desea eliminar este servicio?", "Confirmación Eliminar Servicio",
    function () {
        var $dlg = $(this);
        $.ajax({
            url: "/Admin/ServiciosAdminDelete",
            data: { id: id },
            success: function (data) {
                if (data == "True") {
                    alert("Servicio Eliminado Correctamente.");
                    setTimeout(5000);
                    window.location.reload();
                } else {
                    alert("Error en la Operación. Contacte con el Administrador del Sistema.");
                }
            }
        });
        $dlg.dialog("close");
        $dlg.remove();

    },
    function () {
        var $dlg = $(this);
        $dlg.dialog("close");
        $dlg.remove();
    })
});

$(document).on("click", "#btn-eliminar-excursion", function () {
    var id = $(this).data("id");
    confirm("¿Está seguro que desea eliminar esta excursión?", "Confirmación Eliminar Excursion",
    function () {
        var $dlg = $(this);
        $.ajax({
            url: "/Excursion/Delete",
            data: { id: id },
            success: function (data) {
                if (data == "True") {
                    alert("Excursión Eliminada Correctamente.");
                    setTimeout(5000);
                    window.location.reload();
                } else {
                    alert("Error en la Operación. Contacte con el Administrador del Sistema.");
                }
            }
        });
        $dlg.dialog("close");
        $dlg.remove();

    },
    function () {
        var $dlg = $(this);
        $dlg.dialog("close");
        $dlg.remove();
    })
});

$(document).on("click", "#btn-eliminar-adicional", function () {
    var id = $(this).data("id");
    confirm("¿Está seguro que desea eliminar este adicional?", "Confirmación Eliminar Adicional",
    function () {
        var $dlg = $(this);
        $.ajax({
            url: "/Adicional/Delete",
            data: { id: id },
            success: function (data) {
                if (data == "True") {
                    alert("Adicional Eliminado Correctamente.");
                    setTimeout(5000);
                    window.location.reload();
                } else {
                    alert("Error en la Operación. Contacte con el Administrador del Sistema.");
                }
            }
        });
        $dlg.dialog("close");
        $dlg.remove();

    },
    function () {
        var $dlg = $(this);
        $dlg.dialog("close");
        $dlg.remove();
    })
});

$(document).on("click", "#btn-eliminar-precio", function () {
    var id = $(this).data("id");
    confirm("¿Está seguro que desea eliminar este precio?", "Confirmación Eliminar Precio",
    function () {
        var $dlg = $(this);
        $.ajax({
            url: "/Precio/Delete",
            data: { id: id },
            success: function (data) {
                if (data == "True") {
                    alert("Precio Eliminado Correctamente.");
                    setTimeout(5000);
                    window.location.reload();
                } else {
                    alert("Error en la Operación. Contacte con el Administrador del Sistema.");
                }
            }
        });
        $dlg.dialog("close");
        $dlg.remove();

    },
    function () {
        var $dlg = $(this);
        $dlg.dialog("close");
        $dlg.remove();
    })
});

$(document).on("click", "#btn-eliminar-viaje", function () {
    var ViajeId = $(this).data("id");
    confirm("¿Está seguro que desea eliminar este viaje?, se perderan los datos permanentemente.", "Confirmación Eliminar Viaje",
    function () {
        var $dlg = $(this);
        $.ajax({
            url: "/Viaje/DeleteViaje",
            data: { ViajeId: ViajeId },
            success: function (data) {
                console.log(data);
                if (data.Mensaje == "Done") {
                    alert("Viaje eliminado Correctamente.");
                    setTimeout(5000);
                    window.location.reload();
                } else {
                    alert("Error en la Operación. Contacte con el Administrador del Sistema.");
                }
            }
        });
        $dlg.dialog("close");
        $dlg.remove();

    },
    function () {
        var $dlg = $(this);
        $dlg.dialog("close");
        $dlg.remove();
    })
});



$(document).on("click", ".ui-dialog-titlebar-close", function () {
    window.location.reload();
});

$(document).on("click", "#btn-cancelar-seleccion", function () {
    $("#panel-bus a.selected").removeClass("selected");
    $("#hdnListMenor").val("");
    $("#SeleccionPasajero").dialog("close");
});

$(document).on("click", "#btn-agregar-servicio", function () {
    var paqueteid = $(this).data("paquete");
    var title = "Lista de Servicios";
    var id = "Servicios";
    var url = "/Paquete/Servicios?id=" + paqueteid;
    ShowFormDialogCloseRefresh(url, id, title, "medium");
});
$(document).on("click", "#btn-agregar-excursion", function () {
    var paqueteid = $(this).data("paquete");
    var title = "Lista de Excursiones";
    var id = "Excursiones";
    var url = "/Paquete/Excursiones?id=" + paqueteid;
    ShowFormDialogCloseRefresh(url, id, title, "medium");
});
$(document).on("click", "#btn-agregar-precio", function () {
    var paqueteid = $(this).data("paquete");
    var title = "Lista de Precios";
    var id = "Precios";
    var url = "/Paquete/Precios?id=" + paqueteid;
    ShowFormDialogCloseRefresh(url, id, title, "medium");
});
$(document).on("click", "#btn-agregar-adicional", function () {
    var paqueteid = $(this).data("paquete");
    var title = "Lista de Adicionales";
    var id = "Adicionales";
    var url = "/Paquete/Adicionales?id=" + paqueteid;
    ShowFormDialogCloseRefresh(url, id, title, "medium");
});
$(document).on("click", "#btn-agregar-hotel", function () {
    var viajeid = $(this).data("viaje");
    var title = "Lista de Hoteles";
    var id = "Hoteles";
    var url = "/Viaje/HotelesDisponibles?id=" + viajeid;
    ShowFormDialogCloseRefresh(url, id, title, "medium");
});

//- VINCULAR SERVICIOS
$(document).on("click", "#btn-vincular-servicio", function () {
    var servicioid = $(this).data("servicio");
    var paqueteid = $("#guid-paquete").val();
    var btn = $(this);
    $.ajax({
        url: "/Paquete/VincularServicio",
        data: { servicioid: servicioid, paqueteid: paqueteid },
        success: function (data) {
            if (data == "True") {
                btn.parent().parent().hide();
            } else {
                alert("Error en la Operación. Contacte con el Administrador del Sistema.");
            }
        }
    });
});
$(document).on("click", "#btn-desvincular-servicio", function () {
    var servicioid = $(this).data("servicio");
    var paqueteid = $(this).data("paquete");
    var btn = $(this);
    confirm("¿Está seguro que desea desvincular el servicio seleccionado?", "Confirmación de pago",
    function () {
        var $dlg = $(this);
        $.ajax({
            url: "/Paquete/DesvincularServicio",
            data: { servicioid: servicioid, paqueteid: paqueteid },
            success: function (data) {
                if (data == "True") {
                    btn.parent().parent().remove();
                } else {
                    alert("Error en la Operación. Contacte con el Administrador del Sistema.");
                }
            }
        });
        $dlg.dialog("close");
        $dlg.remove();
    },
    function () {
        var $dlg = $(this);
        $dlg.dialog("close");
        $dlg.remove();
    });
});

//- VINCULAR EXCURSIONES
$(document).on("click", "#btn-vincular-excursion", function () {
    var id = $(this).data("excursion");
    var paqueteid = $("#guid-paquete").val();
    var IsOpcional = $(this).parent().find("input[name='IsOpcional']").is(":checked")
    var btn = $(this);
    $.ajax({
        url: "/Paquete/VincularExcursion",
        data: { excursionid: id, paqueteid: paqueteid, IsOpcional: IsOpcional },
        success: function (data) {
            if (data == "True") {
                btn.parent().parent().parent().hide();
            } else {
                alert("Error en la Operación. Contacte con el Administrador del Sistema.");
            }
        }
    });
});
$(document).on("click", "#btn-desvincular-excursion", function () {
    var PaqueteExcursionID = $(this).data("paqueteexcursionid");
    var btn = $(this);
    confirm("¿Está seguro que desea desvincular la excursión seleccionado?", "Atención!",
    function () {
        var $dlg = $(this);
        $.ajax({
            url: "/Paquete/DesvincularExcursion",
            data: { PaqueteExcursionID: PaqueteExcursionID },
            success: function (data) {
                if (data == "True") {
                    btn.parent().parent().remove();
                } else {
                    alert("Error en la Operación. Contacte con el Administrador del Sistema.");
                }
            }
        });
        $dlg.dialog("close");
        $dlg.remove();
    },
    function () {
        var $dlg = $(this);
        $dlg.dialog("close");
        $dlg.remove();
    });
});

//- VINCULAR PRECIOS
$(document).on("click", "#btn-vincular-precio", function () {
    var id = $(this).data("precio");
    var paqueteid = $("#guid-paquete").val();
    var btn = $(this);
    $.ajax({
        url: "/Paquete/VincularPrecio",
        data: { precioid: id, paqueteid: paqueteid },
        success: function (data) {
            if (data == "True") {
                btn.parent().parent().hide();
            } else {
                alert("Error en la Operación. Contacte con el Administrador del Sistema.");
            }
        }
    });
});
$(document).on("click", "#btn-desvincular-precio", function () {
    var id = $(this).data("precio");
    var paqueteid = $(this).data("paquete");
    var btn = $(this);
    confirm("¿Está seguro que desea desvincular el precio seleccionado?", "Confirmación de pago",
    function () {
        var $dlg = $(this);
        $.ajax({
            url: "/Paquete/DesvincularPrecio",
            data: { precioid: id, paqueteid: paqueteid },
            success: function (data) {
                if (data == "True") {
                    btn.parent().parent().remove();
                } else {
                    alert("Error en la Operación. Contacte con el Administrador del Sistema.");
                }
            }
        });
        $dlg.dialog("close");
        $dlg.remove();
    },
    function () {
        var $dlg = $(this);
        $dlg.dialog("close");
        $dlg.remove();
    });
});

//- VINCULAR ADICIONALES
$(document).on("click", "#btn-vincular-adicional", function () {
    var id = $(this).data("adicional");
    var paqueteid = $("#guid-paquete").val();
    var btn = $(this);
    $.ajax({
        url: "/Paquete/VincularAdicional",
        data: { adicionalid: id, paqueteid: paqueteid },
        success: function (data) {
            if (data == "True") {
                btn.parent().parent().hide();
            } else {
                alert("Error en la Operación. Contacte con el Administrador del Sistema.");
            }
        }
    });
});
$(document).on("click", "#btn-desvincular-adicional", function () {
    var id = $(this).data("adicional");
    var paqueteid = $(this).data("paquete");
    var btn = $(this);
    confirm("¿Está seguro que desea desvincular el adicional seleccionado?", "Confirmación de pago",
    function () {
        var $dlg = $(this);
        $.ajax({
            url: "/Paquete/DesvincularAdicional",
            data: { adicionalid: id, paqueteid: paqueteid },
            success: function (data) {
                if (data == "True") {
                    btn.parent().parent().remove();
                } else {
                    alert("Error en la Operación. Contacte con el Administrador del Sistema.");
                }
            }
        });
        $dlg.dialog("close");
        $dlg.remove();
    },
    function () {
        var $dlg = $(this);
        $dlg.dialog("close");
        $dlg.remove();
    });

});

//- VINCULAR HOTELES
$(document).on("click", "#btn-vincular-hotel", function () {
    var viajeid = $(this).data("viaje");
    var hotelid = $(this).data("hotel");
    var btn = $(this);
    $.ajax({
        url: "/Viaje/VincularHotel",
        data: { hotelid: hotelid, viajeid: viajeid },
        success: function (data) {
            if (data == "True") {
                btn.parent().parent().hide();
            } else {
                alert("Error en la Operación. Contacte con el Administrador del Sistema.");
            };
        }
    });
});
$(document).on("click", "#btn-desvincular-hotel", function () {
    var viajeid = $(this).data("viaje");
    var hotelid = $(this).data("hotel");
    var btn = $(this);
    confirm("¿Está seguro que desea desvincular el hotel seleccionado?", "Confirmación Desvincular Hotel",
    function () {
        var $dlg = $(this);
        $.ajax({
            url: "/Viaje/DesvincularHotel",
            data: { hotelid: hotelid, viajeid: viajeid },
            success: function (data) {
                if (data == "True") {
                    btn.parent().parent().remove();
                } else {
                    alert("Error en la Operación. Contacte con el Administrador del Sistema.");
                }
            }
        });
        $dlg.dialog("close");
        $dlg.remove();
    },
    function () {
        var $dlg = $(this);
        $dlg.dialog("close");
        $dlg.remove();
    });
});


$(document).on("click", "#btn-cerrar-ventana-servicios", function () {
    $("#Servicios").dialog("close");
    window.location.reload();
});
$(document).on("click", "#btn-cerrar-ventana-excursiones", function () {
    $("#Excursiones").dialog("close");
    window.location.reload();
});
$(document).on("click", "#btn-cerrar-ventana-precios", function () {
    $("#Precios").dialog("close");
    window.location.reload();
});
$(document).on("click", "#btn-cerrar-ventana-adicionales", function () {
    $("#Adicionales").dialog("close");
    window.location.reload();
});

$(document).on("click", "#btn-document-help", function () {
    var url = "/Documents/Manual.pdf";
    window.open(url, '_blank');
});

$(document).on("click", ".paging-habitaciones", function () {
    var search = $(this).data("search");
    var page = $(this).data("page");
    var viajeid = $("#input-viaje-id").val();
    $('#gridHabitacion').load('/ReservaHabitacion/GridHotelHabitacion?searchString=' + search + "&page=" + page + "&viajeid=" + viajeid);
});

$(document).on("click", ".paging-habitaciones-cambio", function () {
    var search = $(this).data("search");
    var page = $(this).data("page");
    var viajeid = $("#input-viaje-id").val();
    $('#gridHabitacion').load('/PersonaCliente/GridHabitaciones?searchString=' + search + "&page=" + page + "&viajeid=" + viajeid);
});

$(document).on("click", ".aReserva", function () {
    var viajeid = $(this).data("viaje");
    var title = "Reservar Habitacion";
    var id = "divReserva";
    var fecha = $(this).attr('fecha');
    var habitacionId = $(this).attr('id');
    $("#hdnHabitacionID").val(habitacionId);
    var url = "/ReservaHabitacion/ReservaHabitacion?HabitacionId=" + habitacionId + "&viajeid=" + viajeid + "&fecha=" + fecha;
    ShowFormDialog(url, id, title);

});

$(document).on("click", "#btn-editar-butaca", function () {
    var idanterior = $(this).data("anterior");
    var pasajeid = $(this).data("pasaje");
    var title = "Elegir Nueva Butaca";
    var id = "ElegirNuevaButaca";
    var url = "/PersonaCliente/ElegirNuevaButaca?anteriorid=" + idanterior + "&pasajeid=" + pasajeid;
    ShowFormDialog(url, id, title, "default");
});

$(document).on("click", "#btn-aceptar-cambio-butaca", function () {
  
        //var anteriorid = $("#input-anterior").val();
        var pasajeid = $("#input-pasaje").val();
        var piso = $("#input-piso").val();
        var nuevopasaje = $("#input-nuevopasaje").val();
       // var precio = $("input[name=radio_precio]:checked").data("id");
        var adicionales = new Array();
        $("input[name=check_adicional]:checked").each(function () {
            adicionales.push($(this).data("id"));
        });
        var adicional = adicionales.join(";");

        $.ajax({
            url: "/PersonaCliente/ConfirmarCambioButaca",
            data: { pasajeid: pasajeid, piso: piso, adicional: adicional, nuevopasaje: nuevopasaje },
            success: function (data) {
                if (data == "True") {
                    $("#SeleccionImportes").dialog("close");
                    $("#ElegirNuevaButaca").dialog("close");
                    $("#DetalleFactura").dialog("close");
                    //$("#btn-detallefactura").click();
                    PopupDetalleFactura_Load();
                } else {
                    alert("Error en la Operación. Contacte con el Administrador del Sistema.");
                }
            }
        });

        $("#SeleccionarImportes").dialog("close");
   
});

$(document).on("click", "#btn-editar-habitacion", function () {
    //var idanterior = $(this).data("anterior");
    var pasajeid = $(this).data("pasaje");
    var viajeid = $(this).data("viaje");
    var title = "Elegir Nueva Habitacion";
    var pasajeroid = $(this).data("pasajero");
    var facturaid = $(this).data("factura");
    $("#hdnPasajeroID").val(pasajeroid);
    $("#hdnPasajeID").val(pasajeid);
    var id = "ElegirNuevaHabitacion";
    var url = "/PersonaCliente/PrincipalHabitaciones?pasajeid=" + pasajeid + "&viajeid=" + viajeid + "&facturaid" + facturaid;
  
    var pasajero = $(this).data("pasajero");
    $("#hdnClienteID").val(pasajero);
    //console.log(url);
    ShowFormDialog(url, id, title, "default");
});

$(document).on("click", ".btn-cambio-habitacion", function () {
    var idanterior = $("#input-anterior").val();
    var pasajeid = $("#input-pasaje").val();
    var viajeid = $("#input-viaje").val();
    var nuevahabitacion = $(this).attr("id");
    $.ajax({
        url: "/PersonaCliente/ConfirmarCambioHabitacion",
        //data: { anteriorid: idanterior, pasajeid: pasajeid, viajeid: viajeid, nuevahabitacion: nuevahabitacion },
        data: { pasajeid: pasajeid, viajeid: viajeid, nuevahabitacion: nuevahabitacion },
        success: function (data) {
            if (data == "True") {
                $("#ElegirNuevaHabitacion").dialog("close");
                $("#DetalleFactura").dialog("close");
                //$("#btn-detallefactura").click();
                PopupDetalleFactura_Load();
            } else {
                alert("Error en la Operación. Contacte con el Administrador del Sistema.");
            }
        }
    });
});


function fnFiltrarHabitacionCambio() {
    var HotelID = $('#ddHotel').val();
    var PasajeroID = $("#hdnPasajeroID").val();
    var Fecha = $('#ddHotel option:selected').attr("fecha");
    $('#gridHabitacion').load('/ReservaHabitacion/GridHotelHabitacion?HotelID=' + HotelID + "&viajeid=" + $("#viaje-id").val() + "&PasajeroID=" + PasajeroID + "&Fecha=" + Fecha)
}

$(document).on("click", ".btn-eliminar-precio-habitacion", function () {
    var id = $(this).data("id");
    confirm("¿Está seguro que desea eliminar este precio?", "Confirmación Eliminar Precio",
    function () {
        var $dlg = $(this);
        $.ajax({
            url: "/Admin/PrecioHotelDelete",
            data: { preciohabitacionid: id },
            success: function (data) {
                if (data == "True") {
                    alert("Precio Eliminado Correctamente.");
                    setTimeout(5000);
                    window.location.reload();
                } else {
                    alert("Error en la Operación. Contacte con el Administrador del Sistema.");
                }
            }
        });
        $dlg.dialog("close");
        $dlg.remove();

    },
    function () {
        var $dlg = $(this);
        $dlg.dialog("close");
        $dlg.remove();
    })
});

$(document).on("click", ".btn-eliminar-precio-servicio", function () {
    var id = $(this).data("id");
    confirm("¿Está seguro que desea eliminar este precio?", "Confirmación Eliminar Precio",
    function () {
        var $dlg = $(this);
        $.ajax({
            url: "/Admin/ServiciosAdminPrecioDelete",
            data: { id: id },
            success: function (data) {
                if (data == "True") {
                    alert("Precio Eliminado Correctamente.");
                    setTimeout(5000);
                    window.location.reload();
                } else {
                    alert("Error en la Operación. Contacte con el Administrador del Sistema.");
                }
            }
        });
        $dlg.dialog("close");
        $dlg.remove();

    },
    function () {
        var $dlg = $(this);
        $dlg.dialog("close");
        $dlg.remove();
    })
});

$(document).on("click", "#AgregarPlanillaServicioItem", function () {
    var url = "/Admin/PartialGridServiciosAdmin";
    var id = "GridServiciosAdmin";
    var title = "Seleccionar Servicio de Administrador";
    ShowFormDialog(url, id, title, "default");
});

$(document).on("click", "#btn-seleccionar-servicio-admin", function () {
    var servicioid = $(this).data("id");
    var viajeid = $("#Viajes option:selected").val();
    $.ajax({
        url: "/Admin/AgregarPlanillaServicioItem",
        data: { servicioid: servicioid, viajeid: viajeid },
        success: function (data) {
            if (data == "True") {
                $("#GridServiciosAdmin").dialog("close");
                $("#grid_items").load("/Admin/GridPlanillaServicioItemContext");
            } else {
                alert("Error en la Operación. Contacte con el Administrador del Sistema.");
            }
        }
    });
});

$(document).on("blur", "input.cantidad", function () {
    var planillaservicioitemid = $(this).parent().parent().find("td:eq(0)").text();
    var precio = $(this).parent().parent().find("td:eq(2)").text();
    var cantidad = $(this).val();
    var subtotal = parseFloat(precio) * cantidad;
    $(this).parent().parent().find("td:eq(4)").text(subtotal);

    $.ajax({
        url: "/Admin/ActualizarPlanillaServicioItem",
        data: { planillaservicioitemid: planillaservicioitemid, cantidad: cantidad, subtotal: subtotal },
        success: function (data) {
            if (data == "True") {
                console.log(data);
            } else {
                alert("Error en la Operación. Contacte con el Administrador del Sistema.");
            }
        }
    });

    ActualizarTotal();
});

$(document).on("blur", "input.cantidad-servicio-item", function () {
    var planillaservicioitemid = $(this).parent().parent().find("td:eq(0)").text();
    var precio = $(this).parent().parent().find("td:eq(2)").text();
    var cantidad = $(this).val();
    var subtotal = parseFloat(precio) * cantidad;
    console.log("precio: " + precio + " - cantidad: " + cantidad);
    $(this).parent().parent().find("td:eq(4)").text(subtotal);
    var _totalservicios = 0;
    $("table.servicios tbody tr td.subtotal").each(function () {
        _totalservicios += parseFloat($(this).text());
    });
    $(".text-total-servicios").val(_totalservicios);
    ActualizarTotal();
});

function ActualizarTotal() {
    var total = 0;
    $("table tbody tr").each(function () {
        if (!isNaN(parseFloat($(this).find("td:eq(4)").text()))) {
            total += parseFloat($(this).find("td:eq(4)").text());
        }
    });
    $("#total").text("Total: $ " + total);
}

function CalcularTotal() {
    var total = 0;
    $("table tbody tr").each(function () {
        if (!isNaN(parseFloat($(this).find("td:eq(4)").text()))) {
            total += parseFloat($(this).find("td:eq(4)").text());
        }
    });
    return total;
}

$(document).on("click", "#btn-generar-planilla", function () {
    var total = CalcularTotal();
    var viajeid = $("#Viajes option:selected").val();
    console.log(total);
    console.log(viajeid);
    $.ajax({
        url: "/Admin/GenerarPlanillaServicio",
        data: { viajeid: viajeid, total: total },
        success: function (data) {
            if (data == "True") {
                alert("Planilla Generada Correctamente");
            } else {
                alert("Error en la Operación. Contacte con el Administrador del Sistema.");
            }
        }
    });
});

$(document).on("blur", ".txt-dias", function () {
    var total = 0;
    $(this).parent().parent().parent().parent().parent().find("table.planilla-habitaciones tbody tr td.subtotal").each(function () {
        var precio = parseFloat($(this).parent().find("td.precio").text());
        var preciomenor = parseFloat($(this).parent().find("td.preciomenor").text());
        var cantidad = parseInt($(this).parent().find("td.cantidad_pasajeros").text());
        var cantidadmenor = parseInt($(this).parent().find("td.cantidad_menores").text());
        var dias = parseInt($(this).parent().find("td.dias input.txt-dias").val());
        //console.log("precio: " + precio + " - cantidad: " + cantidad + " - preciomenor: " + preciomenor + " - cantidadmenor: " + cantidadmenor + " - dias: " + dias);
        var subtotal = (precio * cantidad + preciomenor * cantidadmenor) * dias;
        $(this).parent().find("td.subtotal").text(subtotal);
        total += subtotal;
    });
    var _total = 0;
    $(".subtotales").each(function () {
        _total += parseFloat($(this).val());
    });
    $("#total-planilla").val(_total);
    $(this).parent().parent().parent().parent().parent().find("input.text-total-hotel").val(total);
});

$(document).on("blur", "#cantidad_dias", function () {
    $(this).parent().find("table.planilla-habitaciones tbody tr td.dias input.txt-dias").val($(this).val());
    var singletotal = 0;
    var dobletotal = 0;
    var matrimonialtotal = 0;
    var tripletotal = 0;
    var cuadrupletotal = 0;
    var total = 0;
    $(this).parent().find("table.planilla-habitaciones tbody tr td.subtotal").each(function () {
        var precio = parseFloat($(this).parent().find("td.precio").text());
        var preciomenor = parseFloat($(this).parent().find("td.preciomenor").text());
        var cantidad = parseInt($(this).parent().find("td.cantidad_pasajeros").text());
        var cantidadmenor = parseInt($(this).parent().find("td.cantidad_menores").text());
        var dias = parseInt($(this).parent().find("td.dias input.txt-dias").val());
        
        var subtotal = (precio * cantidad + preciomenor * cantidadmenor) * dias;
        total += subtotal;
        var tipo = parseInt($(this).parent().find("td.tipo").data("valor"));
        $(this).parent().find("td.subtotal").text(subtotal);

        switch (tipo) {
            case 1:
                singletotal += subtotal;
                break;
            case 2:
                dobletotal += subtotal;
                break;
            case 3:
                matrimonialtotal += subtotal;
                break;
            case 4:
                tripletotal += subtotal;
                break;
            case 5:
                cuadrupletotal += subtotal;
                break;
            default:

        }

        //$(this).parent().find("td.subtotal").text("$" + parseFloat(subtotal, 10).toFixed(2).replace(/(\d)(?=(\d{3})+\,)/g, "$1,").toString().replace(".", ","));

    });
    console.log("singletotal: " + singletotal);
    console.log("dobletotal: " + dobletotal);
    console.log("matrimonialtotal: " + matrimonialtotal);
    console.log("tripletotal: " + tripletotal);
    console.log("cuadrupletotal: " + cuadrupletotal);
    var nuevototal = singletotal + dobletotal + matrimonialtotal + tripletotal + cuadrupletotal;
    $(this).parent().find("input.text-total-hotel").val(total);
});

$(document).on("click", "#btn-guardar-planilla", function () {
    var planillaid = $(this).data("planilla");
    var fecha = $("#fecha-planilla").val();
    var total = $("#total-planilla").val();
    var hecho = true;
    // CREAR PLANILLA
    $.ajax({
        url: "/Admin/GuardarDatosPlanilla",
        data: { planillaid: planillaid, fecha: fecha, total: total},
        success: function (data) {
            if (data == "True") {
                //alert("Planilla Generada Correctamente");
            } else {
                hecho = false;
                alert("Error en la Operación. Contacte con el Administrador del Sistema.");
            }
        }
    });

    //ACTUALIZAR HABITACION ITEMS
    $("table.planilla-habitaciones").each(function () {
        //AGREGAR HABITACIONES ITEM
        $(this).find("tbody tr").each(function () {
            var id = $(this).find("td.item-id").text();
            var dias = $(this).find("td.dias input.txt-dias").val();
            var subtotal = $(this).find("td.subtotal").text();
            $.ajax({
                url: "/Admin/GuardarDatosItem",
                data: { itemid: id, dias: dias, subtotal: subtotal },
                success: function (data) {
                    if (data == "True") {
                        //alert("Planilla Generada Correctamente");
                    } else {
                        hecho = false;
                        alert("Error en la Operación Actualizar Habitaciones Planilla Item. Contacte con el Administrador del Sistema.");
                    }
                }
            });
        });
    });

    //ACTUALIZAR SERVICIOS ITEMS
    $("table.servicios tbody tr").each(function () {
        var id = $(this).find("td.item-id").text();
        var cantidad = $(this).find("td.cantidad input.txt-cantidad").val();
        var subtotal = $(this).find("td.subtotal").text();
        $.ajax({
            url: "/Admin/GuardarDatosServiciosItem",
            data: { id: id, cantidad: cantidad, subtotal: subtotal },
            success: function (data) {
                if (data == "True") {
                    //alert("Planilla Generada Correctamente");
                } else {
                    hecho = false;
                    alert("Error en la Operación Actualizar Servicios Planilla Item. Contacte con el Administrador del Sistema.");
                }
            }
        });
    });
    if (hecho) alert("Datos guardados correctamente");
});

$(document).on("click", "#btn-siguiente-paso-planilla", function () {
    var viajeid = $("#Viajes option:selected").val();
    // CREAR PLANILLA
    $.ajax({
        url: "/Admin/GenerarPlanilla",
        data: { viajeid: viajeid },
        success: function (data) {
            if (data == "True") {
                //alert("Planilla Generada Correctamente");
            } else {
                alert("Error en la Operación. Contacte con el Administrador del Sistema.");
            }
        }
    });
    var total = 0;
    //AGREGAR LISTA
    $("table.planilla-habitaciones").each(function () {
        total += parseFloat($(this).parent().find("input.text-total-hotel").val());
        //AGREGAR HABITACIONES ITEM
        $(this).find("tbody tr").each(function () {
            var id = $(this).find("td.id").text();
            var dias = $(this).find("td.dias input.txt-dias").val();
            var subtotal = $(this).find("td.subtotal").text();
            $.ajax({
                url: "/Admin/AgregarHabitacionesItem",
                data: { habitacionid: id, dias: dias, subtotal: subtotal},
                success: function (data) {
                    if (data == "True") {
                        //alert("Planilla Generada Correctamente");
                    } else {
                        alert("Error en la Operación Agregar Habitaciones Planilla Item. Contacte con el Administrador del Sistema.");
                    }
                }
            });
        });
    });

    $.ajax({
        url: "/Admin/AgregarTotalHabitaciones",
        data: { total: total },
        success: function (data) {
            if (data == "True") {
                //alert("Planilla Generada Correctamente");
            } else {
                alert("Error en la Operación Agregar Total. Contacte con el Administrador del Sistema.");
            }
        }
    });

    //SIGUIENTE PASO
    $("#planilla-tabs").find("ul li a:eq(1)").trigger("click");
});

$(document).on("click", "#btn-imprimir-planilla", function () {
    var planillaid = $(this).data("planilla");
    var url = "/Admin/ImprimirPlanilla?planillaid=" + planillaid;
    var title = "Planilla";
    var id = "Planilla";
    window.open(url, '_blank');
});

$(document).on("click", "#btn-imprimir-planilla-detalle", function () {
    var planillaid = $(this).data("planilla");
    var url = "/Admin/ImprimirPlanillaDetalle?planillaid=" + planillaid;
    var title = "Planilla";
    var id = "Planilla";
    window.open(url, '_blank');
});

$(document).on("click", "#btn-distribucion-coche", function () {
    var viajeid = $(this).data("viaje");
    var url = "/Reserva/DistribucionCoche?viajeid=" + viajeid;
    var title = "Distribucion de Coche";
    var id = "DistribucionCoche";
    window.open(url, '_blank');
});

$(document).on("click", "#lnk-Agregar-Localidad", function () {
    AgregarLocalidad();
});


function eliminarReservaHotel(PasajeroID, HabitacionID, ViajeID) {
    var divLoading = "<div id='divFullLoading'></div>";
    $("#gridHabitacion").append(divLoading);

    $.ajax({
        url: "/PersonaCliente/EliminarReservaHotel",
        data: { "sPasajeroID": PasajeroID, "sHabitacionID": HabitacionID, "sViajeID": ViajeID },
        dataType: "json",
        success: function (data) {

            if (data.Estado === "Done.") {
                var pathname = window.location.pathname;
                if (pathname != "/Reserva/Index") {
                    PopupDetalleFactura_Load();
                }
                fnFiltrarHabitacionCambio();

            }
            else {
                alert("Ha ocurrido un error, por favor intente nuevamente.", "Error");
            }
            $("#divFullLoading").remove();
        }

    });
}

function showCotizador() {
    var title = "Cotizar Divisas";
    var id = "Cotizador";
    var url = "/Herramientas/Cotizador"
    ShowFormDialog(url, id, title, "min");
}


