/* =====================================================================
   Modulo Paquete - logica de UI (Index, Edit, Vinculos).
   Namespace: window.MatPaquete
   Depende de: jQuery, Bootstrap 5, MatAdmin (mat.admin-utils.js).
   ===================================================================== */
(function (window, $) {
    'use strict';

    var MatPaquete = window.MatPaquete || {};
    window.MatPaquete = MatPaquete;

    function toast(message, type) {
        if (window.MatAdmin && window.MatAdmin.toast) {
            window.MatAdmin.toast(message, type || 'info');
        }
    }

    function confirmDialog(options) {
        if (window.MatAdmin && window.MatAdmin.confirm) {
            return window.MatAdmin.confirm(options);
        }
        return Promise.resolve(window.confirm((options && options.message) || '¿Confirmar?'));
    }

    function debounce(fn, wait) {
        var t;
        return function () {
            var ctx = this, args = arguments;
            clearTimeout(t);
            t = setTimeout(function () { fn.apply(ctx, args); }, wait);
        };
    }

    /* ------------------------------------------------------------------
       INDEX: barra de filtros + eliminacion
       ------------------------------------------------------------------ */
    MatPaquete.initIndex = function () {
        var $form = $('#pqFiltrosForm');

        if ($form.length) {
            // Auto-submit al cambiar cualquier select de filtro (conserva el texto de busqueda).
            $form.on('change', 'select', function () {
                $form.trigger('submit');
            });

            // La busqueda por texto filtra de forma instantanea en cliente (ver Index.cshtml);
            // Enter no recarga la pagina.
            $form.on('keydown', '#pqSearch', function (e) {
                if (e.key === 'Enter') { e.preventDefault(); }
            });

            // Limpiar filtros: vuelve al anio actual sin texto.
            $('#pqLimpiarFiltros').on('click', function (e) {
                e.preventDefault();
                window.location.href = '/Paquete/Index';
            });
        }

        // Eliminar paquete (POST AJAX + confirmacion).
        $(document).on('click', '.pq-btn-eliminar', function (e) {
            e.preventDefault();
            var $btn = $(this);
            var id = $btn.data('id');
            var nombre = $btn.data('nombre') || 'este paquete';

            confirmDialog({
                title: 'Eliminar paquete',
                message: '¿Está seguro que desea eliminar "' + nombre + '"? Esta acción no se puede deshacer.',
                confirmLabel: 'Eliminar',
                confirmClass: 'btn-danger'
            }).then(function (ok) {
                if (!ok) { return; }
                $.ajax({
                    url: '/Paquete/Delete',
                    type: 'POST',
                    data: { id: id },
                    success: function (res) {
                        if (res && res.success) {
                            toast(res.message || 'Paquete eliminado.', 'success');
                            setTimeout(function () { window.location.reload(); }, 600);
                        } else {
                            toast((res && res.message) || 'No se pudo eliminar el paquete.', 'danger');
                        }
                    },
                    error: function () {
                        toast('Error al eliminar el paquete.', 'danger');
                    }
                });
            });
        });
    };

    /* ------------------------------------------------------------------
       EDIT: guardar (alta/edicion) via AJAX
       ------------------------------------------------------------------ */
    MatPaquete.initEdit = function (options) {
        options = options || {};
        var action = options.action;      // "new" | "edit" | "detail"
        var paqueteId = options.paqueteId;

        function validar() {
            var ok = true;
            $('.valtxt').each(function () {
                var val = ($(this).val() || '').replace(/^\s+|\s+$/g, '');
                if (val === '') {
                    $(this).addClass('fieldError');
                    ok = false;
                } else {
                    $(this).removeClass('fieldError');
                }
            });
            return ok;
        }

        $(document).on('keypress input', '.valtxt', function () {
            $(this).removeClass('fieldError');
        });

        $('#btn_guardar_paquete').on('click', function (e) {
            e.preventDefault();

            if (!validar()) {
                toast('Complete los campos obligatorios remarcados.', 'warning');
                return;
            }

            var $btn = $(this);
            var data = $('#formPaquete').serialize();
            var url = action === 'new' ? '/Paquete/InsertPaquete' : '/Paquete/UpdatePaquete';

            if (window.MatAdmin) { window.MatAdmin.showLoading($btn[0], { loadingText: 'Guardando...' }); }

            $.ajax({
                url: url,
                data: data,
                dataType: 'json',
                success: function (res) {
                    if (res && res.success) {
                        toast(res.message || 'Guardado correctamente.', 'success');
                        var destId = action === 'new' ? res.id : paqueteId;
                        setTimeout(function () {
                            window.location.href = '/Paquete/Edit/?sAction=detail&sPaqueteID=' + destId;
                        }, 800);
                    } else {
                        if (window.MatAdmin) { window.MatAdmin.hideLoading($btn[0]); }
                        toast((res && res.message) || 'No se pudo guardar el paquete.', 'danger');
                    }
                },
                error: function () {
                    if (window.MatAdmin) { window.MatAdmin.hideLoading($btn[0]); }
                    toast('Error al guardar el paquete.', 'danger');
                }
            });
        });
    };

    /* ------------------------------------------------------------------
       VINCULOS: modal BS5 generico + vincular / desvincular
       ------------------------------------------------------------------ */
    var vincularCfg = {
        servicio:  { grid: '/Paquete/RenderGridServicios',   vincular: '/Paquete/VincularServicio',   idParam: 'servicioid',  titulo: 'Vincular Servicio' },
        excursion: { grid: '/Paquete/RenderGridExcursiones', vincular: '/Paquete/VincularExcursion',  idParam: 'excursionid', titulo: 'Vincular Excursión' },
        precio:    { grid: '/Paquete/RenderGridPrecios',      vincular: '/Paquete/VincularPrecio',     idParam: 'precioid',    titulo: 'Vincular Precio' },
        adicional: { grid: '/Paquete/RenderGridAdicionales',  vincular: '/Paquete/VincularAdicional',  idParam: 'adicionalid', titulo: 'Vincular Adicional' }
    };

    var desvincularCfg = {
        servicio:  '/Paquete/DesvincularServicio',
        excursion: '/Paquete/DesvincularExcursion',
        precio:    '/Paquete/DesvincularPrecio',
        adicional: '/Paquete/DesvincularAdicional'
    };

    MatPaquete.initVinculos = function (paqueteId) {
        var modalEl = document.getElementById('modalVincular');
        var modal = modalEl && window.bootstrap ? new bootstrap.Modal(modalEl) : null;
        var currentTipo = null;
        var dirty = false;

        function loadGrid(filter) {
            var cfg = vincularCfg[currentTipo];
            if (!cfg) { return; }
            var $grid = $('#vincularGrid');
            $grid.html('<div class="pq-loading"><span class="pq-spinner"></span> Cargando...</div>');
            $grid.load(cfg.grid + '?id=' + encodeURIComponent(paqueteId) + '&filter=' + encodeURIComponent(filter || ''));
        }

        function refreshSections() {
            $('#vinculos-sections').load('/Paquete/VinculosContent?id=' + encodeURIComponent(paqueteId));
        }

        // Abrir modal segun tipo.
        $(document).on('click', '.pq-btn-abrir-vincular', function (e) {
            e.preventDefault();
            currentTipo = $(this).data('tipo');
            var cfg = vincularCfg[currentTipo];
            if (!cfg) { return; }
            $('#modalVincularTitle').text(cfg.titulo);
            $('#vincularSearch').val('');
            loadGrid('');
            if (modal) { modal.show(); }
        });

        // Busqueda dentro del modal (debounce).
        $('#vincularSearch').on('input', debounce(function () {
            loadGrid($(this).val());
        }, 300));

        // Vincular un item del grid.
        $(document).on('click', '.btn-vincular-item', function (e) {
            e.preventDefault();
            var cfg = vincularCfg[currentTipo];
            if (!cfg) { return; }
            var $btn = $(this);
            var id = $btn.data('id');
            var payload = { paqueteid: paqueteId };
            payload[cfg.idParam] = id;

            if (currentTipo === 'excursion') {
                payload.IsOpcional = $btn.closest('tr').find('input[name="IsOpcional"]').is(':checked');
            }

            $.ajax({
                url: cfg.vincular,
                type: 'POST',
                data: payload,
                success: function (res) {
                    if (res && res.success) {
                        $btn.closest('tr').fadeOut(150, function () { $(this).remove(); });
                        dirty = true;
                        toast(res.message || 'Vinculado.', 'success');
                    } else {
                        toast((res && res.message) || 'No se pudo vincular.', 'danger');
                    }
                },
                error: function () { toast('Error al vincular.', 'danger'); }
            });
        });

        // Refrescar secciones al cerrar el modal si hubo cambios.
        if (modalEl) {
            modalEl.addEventListener('hidden.bs.modal', function () {
                if (dirty) { refreshSections(); dirty = false; }
            });
        }

        // Desvincular desde las secciones.
        $(document).on('click', '.btn-desvincular', function (e) {
            e.preventDefault();
            var $btn = $(this);
            var tipo = $btn.data('tipo');
            var url = desvincularCfg[tipo];
            if (!url) { return; }

            confirmDialog({
                title: 'Desvincular',
                message: '¿Está seguro que desea desvincular este elemento?',
                confirmLabel: 'Desvincular',
                confirmClass: 'btn-danger'
            }).then(function (ok) {
                if (!ok) { return; }

                var data;
                if (tipo === 'excursion') {
                    data = { PaqueteExcursionID: $btn.data('rowid') };
                } else {
                    data = { paqueteid: paqueteId };
                    data[vincularCfg[tipo].idParam] = $btn.data('id');
                }

                $.ajax({
                    url: url,
                    type: 'POST',
                    data: data,
                    success: function (res) {
                        if (res && res.success) {
                            toast(res.message || 'Desvinculado.', 'success');
                            refreshSections();
                        } else {
                            toast((res && res.message) || 'No se pudo desvincular.', 'danger');
                        }
                    },
                    error: function () { toast('Error al desvincular.', 'danger'); }
                });
            });
        });
    };

})(window, jQuery);
