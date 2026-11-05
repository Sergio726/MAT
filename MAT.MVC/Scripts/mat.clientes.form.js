/**
 * Formulario de cliente (Views/PersonaCliente/_ClienteForm.cshtml), alta y edición.
 * Las reglas de validación propias espejan los atributos de MAT.MVC/Infrastructure/Validation
 * (mismos mensajes que el servidor). Expone window.MatClienteForm.init(opciones).
 */
(function (window, $) {
    'use strict';

    // ------------------------------------------------------------ Reglas (se registran antes de que
    // la validación discreta de MVC lea el formulario al cargar la página)
    function parseFecha(texto) {
        var m = /^(\d{1,2})\/(\d{1,2})\/(\d{4})$/.exec($.trim(texto || ''));
        if (!m) return null;
        var d = new Date(+m[3], +m[2] - 1, +m[1]);
        return d.getFullYear() === +m[3] && d.getMonth() === +m[2] - 1 && d.getDate() === +m[1] ? d : null;
    }

    function hoy() {
        var d = new Date();
        d.setHours(0, 0, 0, 0);
        return d;
    }

    function digitos(valor) {
        return String(valor || '').replace(/\D/g, '').length;
    }

    function valorDe(elemento, nombre) {
        return $(elemento.form).find('[name="' + nombre + '"]').val();
    }

    var v = $.validator, adaptadores = v.unobtrusive.adapters;

    v.addMethod('fechavalida', function (valor, el) { return this.optional(el) || parseFecha(valor) !== null; });
    v.addMethod('fechanofutura', function (valor, el) {
        var d = parseFecha(valor);
        return this.optional(el) || d === null || d <= hoy();
    });
    v.addMethod('fechaminima', function (valor, el, anio) {
        var d = parseFecha(valor);
        return this.optional(el) || d === null || d.getFullYear() >= +anio;
    });
    v.addMethod('telefonocompleto', function (valor, el, minimo) {
        var n = digitos(valor);
        return n === 0 ? $.trim(valor) === '' : n >= +minimo;
    });
    v.addMethod('telefonoocelular', function (valor, el, p) {
        return digitos(valor) >= +p.minimo || digitos(valorDe(el, p.otro)) >= +p.minimo;
    });
    v.addMethod('documento', function (valor, el, tipoPropiedad) {
        if (this.optional(el)) return true;
        var tipo = valorDe(el, tipoPropiedad), limpio = $.trim(valor), mensaje, ok;
        if (tipo === '2') { // Pasaporte
            var alfa = limpio.replace(/[^A-Za-z0-9]/g, '').length;
            ok = alfa >= 5 && alfa <= 20 && /^[A-Za-z0-9\- ]+$/.test(limpio);
            mensaje = 'El pasaporte debe tener entre 5 y 20 letras o números.';
        } else if (/[^\d.\- ]/.test(limpio)) {
            ok = false;
            mensaje = 'El documento solo puede tener números.';
        } else {
            var n = limpio.replace(/\D/g, '').replace(/^0+/, '').length, esDni = tipo === '1' || !tipo;
            ok = esDni ? n >= 7 && n <= 8 : n >= 6 && n <= 10;
            mensaje = esDni ? 'El DNI debe tener 7 u 8 números.' : 'El documento debe tener entre 6 y 10 números.';
        }
        // Mismo mensaje que ClienteValidacion.ValidarDocumento según el tipo elegido.
        if (!ok) {
            var mensajes = this.settings.messages;
            mensajes[el.name] = $.extend(mensajes[el.name] || {}, { documento: mensaje });
        }
        return ok;
    });

    adaptadores.addBool('fechavalida');
    adaptadores.addBool('fechanofutura');
    adaptadores.addSingleVal('fechaminima', 'anio');
    adaptadores.addSingleVal('telefonocompleto', 'minimo');
    adaptadores.add('telefonoocelular', ['otro', 'minimo'], function (o) {
        o.rules.telefonoocelular = { otro: o.params.otro, minimo: o.params.minimo };
        o.messages.telefonoocelular = o.message;
    });
    adaptadores.addSingleVal('documento', 'tipo');

    // ------------------------------------------------------------ Pantalla
    // Corre al terminar la carga: la validación discreta de MVC lee el formulario en ese momento y
    // tiene que crear el validador con sus reglas antes de que esta pantalla le agregue las suyas.
    function init(opciones) {
        $(function () { iniciar(opciones); });
    }

    function iniciar(opciones) {
        var $form = $('#frmCliente');
        if (!$form.length) return;

        var validador = $form.data('validator') || $form.validate();
        var $prov = $form.find('#Provincia'), $depto = $('#ddDepartamento'), $loc = $('#ddLocalidad');

        // Cambiar uno de los teléfonos revalida el otro ("al menos uno").
        $('#Telefono, #Celular').on('input', function () {
            var otro = this.id === 'Telefono' ? '#Celular' : '#Telefono';
            if ($(otro).val() || $(otro).hasClass('input-validation-error')) validador.element(otro);
            if (this.id === 'Telefono' && $('#Celular').hasClass('input-validation-error')) validador.element('#Celular');
        });
        $('#TipoDocumento').on('change', function () { if ($('#NroDocumento').val()) validador.element('#NroDocumento'); });

        // ---------------- Documento repetido: aviso bajo el campo con enlace al cliente existente
        var $existente = $('#documento-existente'), ultimoConsultado = null;
        function mostrarExistente(datos) {
            if (!datos || datos.disponible) { $existente.addClass('d-none').empty(); return; }
            var texto = $('<span>').text((datos.esCliente ? 'Ya existe un cliente con este documento: ' : 'Ya existe una persona con este documento: ') + datos.nombre + '. ');
            $existente.empty().append($('<i class="bi bi-exclamation-triangle" aria-hidden="true"></i> '), texto);
            if (datos.esCliente) $existente.append($('<a>').attr('href', opciones.urlFicha + '/' + encodeURIComponent(datos.personaId)).text('Ver su ficha'));
            $existente.removeClass('d-none');
        }
        $('#NroDocumento').on('blur', function () {
            var numero = $.trim($(this).val());
            if (!numero || numero === ultimoConsultado || !validador.element(this)) {
                if (!numero) mostrarExistente(null);
                return;
            }
            ultimoConsultado = numero;
            $.getJSON(opciones.urlDocumento, { nroDocumento: numero, personaId: opciones.personaId || '' })
                .done(mostrarExistente);
        });

        // ---------------- País → Provincia → Departamento → Localidad
        if (window.MatCombo) MatCombo.mejorar($form.find('select.js-combo'));

        var $pais = $form.find('#PaisId');
        var provincias = JSON.parse(document.getElementById('clienteProvincias').textContent || '[]');
        var paisDeProvincia = {};
        provincias.forEach(function (p) { paisDeProvincia[p.id] = p.paisId; });

        function vaciar($select, texto) {
            $select.empty().append($('<option>').val('').text(texto)).trigger('mat:sincronizar');
        }
        function llenarProvincias(paisId, seleccionada) {
            $prov.empty().append($('<option>').val('').text('Seleccioná la provincia…'));
            provincias.filter(function (p) { return p.paisId === paisId; }).forEach(function (p) {
                $prov.append($('<option>').val(String(p.id)).text(p.nombre));
            });
            $prov.val(seleccionada ? String(seleccionada) : '').trigger('mat:sincronizar');
        }
        /** Elige la provincia y, si es de otro país, también el país. */
        function seleccionarProvincia(idProv) {
            var paisId = paisDeProvincia[idProv];
            if (paisId && paisId !== $pais.val()) {
                $pais.val(paisId).trigger('mat:sincronizar');
                llenarProvincias(paisId, idProv);
            } else {
                $prov.val(String(idProv)).trigger('mat:sincronizar');
            }
        }
        // "Nueva localidad" puede cambiar el departamento con .val(): el combo se pone al día
        $(document).on('mat:localidad-created', function () {
            setTimeout(function () { $depto.trigger('mat:sincronizar'); $loc.trigger('mat:sincronizar'); }, 0);
        });
        $pais.on('change', function () {
            llenarProvincias($(this).val());
            vaciar($depto, 'Seleccioná el departamento…');
            vaciar($loc, 'Seleccioná la localidad…');
        });

        var geoTextos = { placeholder: 'Seleccioná el departamento…', placeholderValue: '', localidadPlaceholder: 'Seleccioná la localidad…', localidadPlaceholderValue: '' };
        function cargarLocalidades(idDepto, seleccionada) {
            if (!window.matGeo || !(parseInt(idDepto, 10) > 0)) return;
            matGeo.loadLocalidades(idDepto, $.extend({}, geoTextos, {
                localidadSelect: $loc, placeholder: geoTextos.localidadPlaceholder, placeholderValue: '',
                selectedId: seleccionada || null, context: 'Localidades (cliente)'
            }));
        }
        function cargarDepartamentos(idProv, deptoSel, locSel) {
            if (!window.matGeo || !(parseInt(idProv, 10) > 0)) return;
            matGeo.loadDepartamentos(idProv, $.extend({}, geoTextos, {
                departamentoSelect: $depto, localidadSelect: $loc, selectedId: deptoSel || null,
                context: 'Departamentos (cliente)',
                onDone: function () { if (deptoSel) cargarLocalidades(deptoSel, locSel); }
            }));
        }
        $prov.on('change', function () {
            if ($(this).val()) { cargarDepartamentos($(this).val()); return; }
            vaciar($depto, 'Seleccioná el departamento…');
            vaciar($loc, 'Seleccioná la localidad…');
        });
        $depto.on('change', function () { cargarLocalidades($(this).val()); });

        var deptoInicial = $depto.data('seleccionado'), locInicial = $loc.data('seleccionado');
        if (locInicial && !deptoInicial) {
            // Edición: el departamento sale de la localidad guardada.
            $.post(opciones.urlInfoLocalidad, { sLocalidaId: locInicial }, null, 'json').done(function (r) {
                if (r && r.Result === 'Done.') {
                    seleccionarProvincia(parseInt(r.IdProvincia, 10));
                    cargarDepartamentos(r.IdProvincia, parseInt(r.IdDepartamento, 10), locInicial);
                } else {
                    cargarDepartamentos($prov.val());
                }
            }).fail(function () { cargarDepartamentos($prov.val()); });
        } else {
            cargarDepartamentos($prov.val(), deptoInicial, locInicial);
        }

        // ---------------- Fecha, teclado y máscaras
        if ($.fn.datepicker) {
            $('#FechaNacimiento').datepicker({
                changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy',
                yearRange: '1910:+0', maxDate: 0, defaultDate: '-40y',
                onSelect: function () { validador.element(this); }
            });
        }
        if ($.fn.mask) $('#Cuit').mask('99-99999999-9');
        $('#Telefono, #Celular, #Fax').on('keypress', function (e) {
            return e.which === 0 || e.which === 8 || /[\d\s()+\-*]/.test(String.fromCharCode(e.which));
        });

        // ---------------- Guardar: botón ocupado, sin doble envío, foco en el primer error
        $form.find('button[type="submit"]').on('click', function () {
            $('#accion').val($(this).data('accion') || 'guardar');
        });
        $form.on('submit', function () {
            if (!$form.valid()) return false;
            var $botones = $form.find('button[type="submit"]').prop('disabled', true);
            $botones.filter('[data-accion="' + $('#accion').val() + '"]')
                .html('<span class="spinner-border spinner-border-sm" aria-hidden="true"></span> <span>Guardando…</span>');
            return true;
        });

        // Errores que vienen del servidor: llevar el foco al primero.
        var $primerError = $form.find('.input-validation-error').first();
        if ($primerError.length) $primerError.trigger('focus');
        else if ($form.find('.cliente-form-resumen li').length) $form.find('.cliente-form-resumen')[0].scrollIntoView({ block: 'center' });
    }

    window.MatClienteForm = { init: init };
})(window, jQuery);
