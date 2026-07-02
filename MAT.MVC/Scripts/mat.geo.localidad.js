/**
 * MAT — selección y alta unificada de localidades (normalización L3).
 * Depende de: jQuery, jQuery UI autocomplete, ShowFormDialog (mat.jquery.functions.js).
 */
(function (window, $) {
    'use strict';

    if (!$) {
        return;
    }

    var ENDPOINTS = {
        provincia: '/Localidad/GetProvincia',
        departamento: '/Localidad/GetDepartamento',
        localidad: '/Localidad/GetLocalidad',
        search: '/Localidad/Search',
        create: '/Localidad/Create'
    };

    var RESULT_DONE = 'Done.';
    var DEFAULT_MIN_SEARCH = 3;

    function resolve$el(selectorOrJq) {
        if (!selectorOrJq) {
            return $();
        }
        return selectorOrJq.jquery ? selectorOrJq : $(selectorOrJq);
    }

    function parseJsonList(jsonString) {
        if (!jsonString) {
            return [];
        }
        try {
            return JSON.parse(jsonString);
        } catch (e) {
            console.error('matGeo: error al parsear JSON de cascada', e);
            return [];
        }
    }

    function fillSelect($select, items, options) {
        options = options || {};
        var valueKey = options.valueKey || 'Id';
        var textKey = options.textKey || 'Nombre';
        var placeholder = options.placeholder;
        var placeholderValue = options.placeholderValue != null ? String(options.placeholderValue) : '0';
        var selectedId = options.selectedId != null ? String(options.selectedId) : null;

        $select.empty();
        if (placeholder) {
            $select.append($('<option>', { value: placeholderValue, text: placeholder }));
        }

        (items || []).forEach(function (item) {
            var value = item[valueKey];
            if (value == null) {
                return;
            }
            $select.append($('<option>', { value: String(value), text: item[textKey] || '' }));
        });

        if (selectedId) {
            $select.val(selectedId);
        }
    }

    function clearSelect($select, placeholder, placeholderValue) {
        fillSelect($select, [], {
            placeholder: placeholder,
            placeholderValue: placeholderValue != null ? placeholderValue : '0'
        });
    }

    function cascadeListFromResponse(response, keys) {
        keys = keys || ['LProvincia', 'LDepartamento', 'LLocalidad'];
        for (var i = 0; i < keys.length; i++) {
            if (response && response[keys[i]]) {
                return parseJsonList(response[keys[i]]);
            }
        }
        return [];
    }

    function handleJsonResult(response, context) {
        if (response && response.Result === RESULT_DONE) {
            return true;
        }
        var msg = (response && response.Result) ? response.Result : 'Error inesperado en la operación.';
        handleJsonError(msg, context);
        return false;
    }

    function postJson(url, data) {
        return $.ajax({
            type: 'POST',
            url: url,
            data: data,
            dataType: 'json'
        });
    }

    var matGeo = {
        ENDPOINTS: ENDPOINTS,

        /**
         * Carga provincias por país en un &lt;select&gt;.
         * @param {string} paisId - GUID del país
         * @param {object} opts - provinciaSelect, placeholder, selectedId, valueKey (default ID), onDone, onError
         */
        loadProvincias: function (paisId, opts) {
            opts = opts || {};
            var $provincia = resolve$el(opts.provinciaSelect);
            if (!$provincia.length || !paisId) {
                return $.Deferred().resolve().promise();
            }

            return postJson(ENDPOINTS.provincia, { sIdPais: paisId })
                .done(function (response) {
                    if (!handleJsonResult(response, opts.context || 'Provincias')) {
                        if (typeof opts.onError === 'function') {
                            opts.onError(response);
                        }
                        return;
                    }
                    var list = cascadeListFromResponse(response, ['LProvincia', 'LDepartamento']);
                    fillSelect($provincia, list, {
                        valueKey: opts.valueKey || 'ID',
                        textKey: opts.textKey || 'Nombre',
                        placeholder: opts.placeholder || 'SELECCIONE UNA PROVINCIA',
                        placeholderValue: opts.placeholderValue,
                        selectedId: opts.selectedId
                    });
                    if (typeof opts.onDone === 'function') {
                        opts.onDone(list, response);
                    }
                })
                .fail(function (xhr) {
                    matGeo.handleAjaxError(xhr, opts.context || 'Provincias');
                    if (typeof opts.onError === 'function') {
                        opts.onError(xhr);
                    }
                });
        },

        /**
         * Carga departamentos por provincia.
         */
        loadDepartamentos: function (provinciaId, opts) {
            opts = opts || {};
            var $departamento = resolve$el(opts.departamentoSelect);
            var id = parseInt(provinciaId, 10);

            if (!$departamento.length || !(id > 0)) {
                if ($departamento.length && opts.clearOnEmpty !== false) {
                    clearSelect($departamento, opts.placeholder || 'SELECCIONE UN DEPARTAMENTO', opts.placeholderValue);
                }
                if (opts.localidadSelect) {
                    clearSelect(resolve$el(opts.localidadSelect), opts.localidadPlaceholder || 'SELECCIONE UNA LOCALIDAD', opts.localidadPlaceholderValue);
                }
                return $.Deferred().resolve().promise();
            }

            return postJson(ENDPOINTS.departamento, { sIdProvincia: id })
                .done(function (response) {
                    if (!handleJsonResult(response, opts.context || 'Departamentos')) {
                        if (typeof opts.onError === 'function') {
                            opts.onError(response);
                        }
                        return;
                    }
                    var list = cascadeListFromResponse(response, ['LDepartamento']);
                    fillSelect($departamento, list, {
                        valueKey: opts.valueKey || 'IdDepartamento',
                        textKey: opts.textKey || 'Nombre',
                        placeholder: opts.placeholder || 'SELECCIONE UN DEPARTAMENTO',
                        placeholderValue: opts.placeholderValue,
                        selectedId: opts.selectedId
                    });
                    if (opts.localidadSelect && opts.clearLocalidad !== false) {
                        clearSelect(resolve$el(opts.localidadSelect), opts.localidadPlaceholder || 'SELECCIONE UNA LOCALIDAD', opts.localidadPlaceholderValue);
                    }
                    if (typeof opts.onDone === 'function') {
                        opts.onDone(list, response);
                    }
                })
                .fail(function (xhr) {
                    matGeo.handleAjaxError(xhr, opts.context || 'Departamentos');
                    if (typeof opts.onError === 'function') {
                        opts.onError(xhr);
                    }
                });
        },

        /**
         * Carga localidades por departamento.
         */
        loadLocalidades: function (deptoId, opts) {
            opts = opts || {};
            var $localidad = resolve$el(opts.localidadSelect);
            var id = parseInt(deptoId, 10);

            if (!$localidad.length || !(id > 0)) {
                if ($localidad.length && opts.clearOnEmpty !== false) {
                    clearSelect($localidad, opts.placeholder || 'SELECCIONE UNA LOCALIDAD', opts.placeholderValue);
                }
                return $.Deferred().resolve().promise();
            }

            return postJson(ENDPOINTS.localidad, { sIdDepartamento: id })
                .done(function (response) {
                    if (!handleJsonResult(response, opts.context || 'Localidades')) {
                        if (typeof opts.onError === 'function') {
                            opts.onError(response);
                        }
                        return;
                    }
                    var list = cascadeListFromResponse(response, ['LLocalidad']);
                    fillSelect($localidad, list, {
                        valueKey: opts.valueKey || 'Id',
                        textKey: opts.textKey || 'Nombre',
                        placeholder: opts.placeholder || 'SELECCIONE UNA LOCALIDAD',
                        placeholderValue: opts.placeholderValue,
                        selectedId: opts.selectedId
                    });
                    if (typeof opts.onDone === 'function') {
                        opts.onDone(list, response);
                    }
                })
                .fail(function (xhr) {
                    matGeo.handleAjaxError(xhr, opts.context || 'Localidades');
                    if (typeof opts.onError === 'function') {
                        opts.onError(xhr);
                    }
                });
        },

        /**
         * Autocomplete unificado contra /Localidad/Search.
         * @param {jQuery|string} $input - campo visible
         * @param {jQuery|string} $hidden - campo hidden con LocalidadId
         */
        initAutocomplete: function ($input, $hidden, opts) {
            opts = opts || {};
            $input = resolve$el($input);
            $hidden = resolve$el($hidden);

            if (!$input.length) {
                return;
            }

            var minLength = opts.minLength != null ? opts.minLength : DEFAULT_MIN_SEARCH;

            if ($input.hasClass('ui-autocomplete-input')) {
                try {
                    $input.autocomplete('destroy');
                } catch (e) { /* no-op */ }
            }

            $input.autocomplete({
                minLength: minLength,
                source: function (request, response) {
                    var data = { term: request.term };
                    if (opts.idProvincia != null) {
                        data.idProvincia = opts.idProvincia;
                    } else if (opts.provinciaSelect) {
                        var provVal = resolve$el(opts.provinciaSelect).val();
                        if (provVal && parseInt(provVal, 10) > 0) {
                            data.idProvincia = provVal;
                        }
                    }
                    if (opts.idDepartamento != null) {
                        data.idDepartamento = opts.idDepartamento;
                    } else if (opts.departamentoSelect) {
                        var depVal = resolve$el(opts.departamentoSelect).val();
                        if (depVal && parseInt(depVal, 10) > 0) {
                            data.idDepartamento = depVal;
                        }
                    }

                    $.ajax({
                        url: opts.url || ENDPOINTS.search,
                        dataType: 'json',
                        data: data,
                        success: function (items) {
                            response($.map(items || [], function (item) {
                                return {
                                    label: item.Nombre,
                                    value: item.Nombre,
                                    id: item.LocalidadId
                                };
                            }));
                        },
                        error: function (xhr) {
                            matGeo.handleAjaxError(xhr, opts.context || 'Búsqueda de localidad');
                            response([]);
                        }
                    });
                },
                select: function (event, ui) {
                    if ($hidden.length) {
                        $hidden.val(ui.item.id);
                    }
                    if (typeof opts.onSelect === 'function') {
                        opts.onSelect(ui.item);
                    }
                },
                change: function (event, ui) {
                    if (!ui.item && $hidden.length) {
                        $hidden.val('');
                    }
                }
            });
        },

        /**
         * Abre popup de alta. Si se pasan selects padre, refresca localidades al crear.
         */
        openCreateDialog: function (opts) {
            opts = opts || {};
            var dialogId = opts.dialogId || 'AgregarLocalidad';
            var title = opts.title || 'Agregar una Localidad';
            var url = opts.url || ENDPOINTS.create;

            if (typeof window.ShowFormDialog === 'function') {
                window.ShowFormDialog(url, dialogId, title, opts.width || 'min');
            } else {
                console.error('matGeo: ShowFormDialog no está disponible');
                return;
            }

            if (opts.idDepartamentoSelect && opts.idLocalidadSelect) {
                $(document).one('mat:localidad-created.matGeoOpenDialog', function (e, data) {
                    if (!data || !data.localidadId) {
                        return;
                    }
                    var $depto = resolve$el(opts.idDepartamentoSelect);
                    var deptoId = data.idDepartamento || $depto.val();
                    if (data.idDepartamento) {
                        $depto.val(String(data.idDepartamento));
                    }
                    matGeo.loadLocalidades(deptoId, {
                        localidadSelect: opts.idLocalidadSelect,
                        selectedId: data.localidadId,
                        onDone: function () {
                            if (typeof opts.onCreated === 'function') {
                                opts.onCreated(data);
                            }
                        }
                    });
                });
            } else if (typeof opts.onCreated === 'function') {
                $(document).one('mat:localidad-created.matGeoOpenDialog', function (e, data) {
                    opts.onCreated(data);
                });
            }
        },

        /**
         * Suscripción al evento global de alta exitosa.
         */
        onLocalidadCreated: function (handler) {
            $(document).on('mat:localidad-created.matGeo', handler);
        },

        offLocalidadCreated: function (handler) {
            $(document).off('mat:localidad-created.matGeo', handler);
        },

        /**
         * Dispara el evento global (usado desde popup Create en L4).
         */
        triggerLocalidadCreated: function (data) {
            $(document).trigger('mat:localidad-created', [data || {}]);
        },

        /**
         * Error de respuesta JSON legacy (Result !== Done.).
         */
        handleJsonError: function (messageOrResponse, context) {
            var msg = 'Error inesperado en la operación.';
            if (typeof messageOrResponse === 'string') {
                msg = messageOrResponse;
            } else if (messageOrResponse && messageOrResponse.Result) {
                msg = messageOrResponse.Result;
            } else if (messageOrResponse && messageOrResponse.message) {
                msg = messageOrResponse.message;
            }
            (window.alertError || window.alert)(msg, context || 'Error');
        },

        /**
         * Error HTTP AJAX (incluye GlobalExceptionFilter: ok/message).
         */
        handleAjaxError: function (xhr, context) {
            var msg = 'Error de comunicación con el servidor.';
            if (xhr && xhr.responseJSON) {
                if (xhr.responseJSON.message) {
                    msg = xhr.responseJSON.message;
                } else if (xhr.responseJSON.Result) {
                    msg = xhr.responseJSON.Result;
                }
            }
            (window.alertError || window.alert)(msg, context || 'Error');
        }
    };

    window.matGeo = matGeo;

}(window, window.jQuery));
