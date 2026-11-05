/**
 * Combo con búsqueda sobre un <select> existente (jQuery UI Autocomplete, ya cargado por _Layout).
 * El <select> sigue siendo el que se envía y valida: queda oculto a la vista pero no para jQuery Validate,
 * y el texto se mantiene sincronizado cuando cambian sus opciones (cascadas) o su valor.
 * Busca sin importar mayúsculas ni acentos ("guemes" encuentra "GÜEMES"; "rio" encuentra "Río").
 * Uso: MatCombo.mejorar($('select.js-combo'))
 */
(function (window, $) {
    'use strict';

    var MAXIMO = 80;

    function normalizar(s) {
        return String(s || '').normalize('NFD').replace(/[̀-ͯ]/g, '').toLowerCase().trim();
    }

    function opciones($select) {
        return $select.find('option').filter(function () { return this.value !== ''; }).map(function () {
            return { label: $(this).text(), value: $(this).text(), id: this.value, clave: normalizar($(this).text()) };
        }).get();
    }

    function mejorar($selects) {
        $selects.each(function () {
            var select = this, $select = $(select);
            if ($select.data('matCombo')) return;
            $select.data('matCombo', true);

            var idInput = select.id + '_buscar';
            var placeholder = $select.find('option[value=""]').first().text() || 'Buscá…';
            var $envoltura = $('<div class="mat-combo"></div>').insertAfter($select);
            var $input = $('<input type="text" class="modern-form-input mat-combo-input" autocomplete="off" spellcheck="false" />')
                .attr({ id: idInput, placeholder: placeholder, role: 'combobox', 'aria-autocomplete': 'list', 'aria-expanded': 'false' })
                .appendTo($envoltura);
            var $boton = $('<button type="button" class="mat-combo-boton" tabindex="-1" aria-hidden="true"><i class="bi bi-chevron-down"></i></button>')
                .appendTo($envoltura);
            $select.addClass('mat-combo-select').attr({ tabindex: -1, 'aria-hidden': 'true' }).appendTo($envoltura);

            // La etiqueta apunta al campo que se escribe
            $('label[for="' + select.id + '"]').attr('for', idInput);
            var describe = $select.attr('aria-describedby');
            if (describe) $input.attr('aria-describedby', describe);

            function textoSeleccionado() {
                var $op = $select.find('option:selected');
                return $op.length && $op.val() !== '' ? $op.text() : '';
            }
            function sincronizar() {
                if (document.activeElement !== $input[0]) $input.val(textoSeleccionado());
                var deshabilitado = $select.prop('disabled');
                $input.prop('disabled', deshabilitado);
                $envoltura.toggleClass('is-deshabilitado', deshabilitado);
                $envoltura.toggleClass('is-error', $select.hasClass('input-validation-error'));
                $input.attr('aria-invalid', $select.hasClass('input-validation-error') ? 'true' : null);
            }
            function elegir(id) {
                if ($select.val() !== id) $select.val(id).trigger('change');
                $input.val(textoSeleccionado());
                // Revalida el select (el error queda bajo el campo como siempre)
                var validador = $select.closest('form').data('validator');
                if (validador && $select.attr('data-val') === 'true') validador.element(select);
                sincronizar();
            }

            $input.autocomplete({
                minLength: 0,
                delay: 0,
                appendTo: $envoltura,
                source: function (pedido, responder) {
                    var termino = normalizar(pedido.term), lista = opciones($select);
                    if (termino && termino !== normalizar(textoSeleccionado())) {
                        var empiezan = [], contienen = [];
                        lista.forEach(function (o) {
                            var i = o.clave.indexOf(termino);
                            if (i === 0) empiezan.push(o); else if (i > 0) contienen.push(o);
                        });
                        lista = empiezan.concat(contienen);
                    }
                    responder(lista.length ? lista.slice(0, MAXIMO) : [{ label: 'Sin coincidencias', value: '', id: null }]);
                },
                select: function (e, ui) {
                    e.preventDefault();
                    if (ui.item.id === null) return;
                    elegir(ui.item.id);
                },
                focus: function (e) { e.preventDefault(); },
                open: function () { $input.attr('aria-expanded', 'true'); },
                close: function () { $input.attr('aria-expanded', 'false'); }
            });

            // Al salir: si lo escrito coincide exacto con una opción se elige; si quedó vacío se limpia;
            // si no, vuelve a lo que estaba elegido (nunca queda un texto que no se va a guardar).
            $input.on('blur', function () {
                var texto = normalizar($input.val());
                if (!texto) { if ($select.val()) elegir(''); else sincronizar(); return; }
                var exacta = opciones($select).filter(function (o) { return o.clave === texto; })[0];
                if (exacta) elegir(exacta.id); else $input.val(textoSeleccionado());
            });
            $input.on('focus click', function () {
                if (!$input.autocomplete('widget').is(':visible')) $input.autocomplete('search', '');
            });
            $input.on('keydown', function (e) {
                if (e.key === 'Escape') { $input.val(textoSeleccionado()); }
            });
            $boton.on('mousedown', function (e) {
                e.preventDefault();
                if ($input.prop('disabled')) return;
                var abierto = $input.autocomplete('widget').is(':visible');
                $input.trigger('focus');
                if (abierto) $input.autocomplete('close'); else $input.autocomplete('search', '');
            });

            // jQuery Validate enfoca el select con error: se pasa el foco al campo visible
            $select.on('focus', function () { $input.trigger('focus'); });
            // "mat:sincronizar": para quien cambia el valor con .val() sin disparar change
            $select.on('change mat:sincronizar', sincronizar);
            new MutationObserver(sincronizar).observe(select, { childList: true, subtree: true, attributes: true, attributeFilter: ['disabled', 'class'] });
            sincronizar();
        });
    }

    window.MatCombo = { mejorar: mejorar, normalizar: normalizar };
})(window, jQuery);
