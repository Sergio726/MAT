/**
 * Panel Admin: tour guiado (onboarding), tours contextuales y buscador de funciones.
 * Depende de: Bootstrap 5, MatAdmin.toast (mat.admin-utils.js), jQuery opcional para POST.
 */
(function (window, document) {
    'use strict';

    window.MatAdminHelp = window.MatAdminHelp || {};

    var state = {
        catalog: [],
        contextualTours: [],
        tourSteps: [],
        tourIndex: 0,
        tourActive: false,
        tourMode: 'hub',
        contextualPageId: null,
        onboardingCompleted: false,
        onboardingCompleteUrl: '/Admin/OnboardingComplete',
        onboardingStatusUrl: '/Admin/OnboardingStatus',
        onboardingResetUrl: '/Admin/OnboardingReset',
        userId: 0,
        isIndexPage: false,
        searchModal: null,
        resizeHandler: null,
        scrollHandler: null,
        tourKeyHandler: null,
        tourPersistOnEnd: true,
        tourControlsBound: false,
        helpDropdownOpenedForTour: false,
        modalSearchActiveIndex: -1,
        prefersReducedMotion: false
    };

    var SEARCH_EMPTY_HINT = 'Escriba para buscar herramientas del panel Admin o módulos de la intranet (presupuestos, viajes, clientes). Use <strong>Ctrl+K</strong> en cualquier pantalla Admin.';

    function escapeHtml(str) {
        if (str == null) return '';
        return String(str)
            .replace(/&/g, '&amp;')
            .replace(/</g, '&lt;')
            .replace(/>/g, '&gt;')
            .replace(/"/g, '&quot;');
    }

    function normalizeText(str) {
        if (!str) return '';
        return String(str)
            .toLowerCase()
            .normalize('NFD')
            .replace(/[\u0300-\u036f]/g, '');
    }

    function normalizePath(path) {
        var p = (path || '').split('?')[0].trim().toLowerCase();
        if (!p) return '/';
        if (p.charAt(0) !== '/') p = '/' + p;
        if (p.length > 1 && p.charAt(p.length - 1) === '/') p = p.slice(0, -1);
        return p;
    }

    function getUserId() {
        var shell = document.querySelector('.admin-shell[data-admin-user-id]');
        if (!shell) return 0;
        var id = parseInt(shell.getAttribute('data-admin-user-id'), 10);
        return isNaN(id) ? 0 : id;
    }

    function getAntiforgeryToken() {
        var input = document.querySelector('#adminAntiforgeryForm input[name="__RequestVerificationToken"]');
        return input ? input.value : '';
    }

    function parseCatalog() {
        var el = document.getElementById('adminHelpCatalogJson');
        if (!el || !el.textContent) return [];
        try {
            return JSON.parse(el.textContent);
        } catch (e) {
            return [];
        }
    }

    function parseContextualTours() {
        var el = document.getElementById('adminContextualToursJson');
        if (!el || !el.textContent) return [];
        try {
            return JSON.parse(el.textContent);
        } catch (e) {
            return [];
        }
    }

    function prefersReducedMotion() {
        try {
            return window.matchMedia && window.matchMedia('(prefers-reduced-motion: reduce)').matches;
        } catch (e) {
            return false;
        }
    }

    function contextualStorageKey(pageId) {
        return 'mat.admin.contextual.' + (state.userId || 0) + '.' + pageId;
    }

    function isContextualTourComplete(pageId) {
        try {
            return localStorage.getItem(contextualStorageKey(pageId)) === '1';
        } catch (e) {
            return false;
        }
    }

    function markContextualTourComplete(pageId) {
        try {
            localStorage.setItem(contextualStorageKey(pageId), '1');
        } catch (e) {
            // ignore
        }
    }

    function resolveContextualTour(pathname) {
        var path = normalizePath(pathname);
        var tours = state.contextualTours || [];
        var i;

        for (i = 0; i < tours.length; i++) {
            if (tours[i].exactMatch && normalizePath(tours[i].pathMatch) === path) {
                return tours[i];
            }
        }

        var best = null;
        var bestLen = 0;
        for (i = 0; i < tours.length; i++) {
            var match = normalizePath(tours[i].pathMatch);
            if (!tours[i].exactMatch && path.indexOf(match) === 0 && match.length > bestLen) {
                best = tours[i];
                bestLen = match.length;
            }
        }
        return best;
    }

    function buildDefaultTourSteps() {
        return [
            {
                selector: '#admin-tour-header',
                title: 'Bienvenido al panel de administración',
                text: 'Este es su centro de control exclusivo: reportes, usuarios y herramientas de sistema en un solo lugar. En pantallas grandes también puede usar el menú lateral.'
            },
            {
                selector: '#admin-tour-section-reportes',
                title: 'Reportes y consultas',
                text: 'Consulte pagos por viaje o fecha, audite facturas y acceda a reportes operativos de ventas, pagos y ranking.'
            },
            {
                selector: '#admin-tour-section-usuarios',
                title: 'Gestión de usuarios',
                text: 'Administre cuentas, roles, contraseñas y dé de alta vendedores sin cerrar su sesión.'
            },
            {
                selector: '#admin-tour-section-sistema',
                title: 'Sistema y diagnóstico',
                text: 'Revise errores, logs en memoria y configure los códigos de confirmación para operaciones sensibles.'
            },
            {
                selector: '#adminTopbarSearchBtn',
                title: 'Buscar funciones',
                text: 'Use este botón o el atajo Ctrl+K en cualquier pantalla Admin para encontrar reportes, usuarios, módulos de intranet y más.'
            },
            {
                selector: '#admin-tour-user-menu',
                openHelpDropdown: true,
                title: 'Menú de usuario',
                text: 'Desde aquí puede volver a ver la guía de inicio, la guía de la pantalla actual, reiniciar el tour del panel o ir a la intranet.'
            }
        ];
    }

    function isIntranetItem(item) {
        return item && normalizeText(item.category) === 'intranet';
    }

    function filterCatalog(query) {
        var q = normalizeText(query).trim();
        if (!q) return [];

        var scored = [];
        for (var i = 0; i < state.catalog.length; i++) {
            var item = state.catalog[i];
            var title = normalizeText(item.title);
            var desc = normalizeText(item.description);
            var cat = normalizeText(item.category);
            var kw = (item.keywords || []).map(normalizeText).join(' ');
            var blob = title + ' ' + desc + ' ' + cat + ' ' + kw;
            if (blob.indexOf(q) === -1) continue;

            var score = 0;
            if (title.indexOf(q) === 0) score += 100;
            else if (title.indexOf(q) !== -1) score += 50;
            if (cat.indexOf(q) !== -1) score += 20;
            if (desc.indexOf(q) !== -1) score += 10;
            if (kw.indexOf(q) !== -1) score += 5;
            if (isIntranetItem(item) && (kw.indexOf(q) !== -1 || title.indexOf(q) !== -1)) score += 15;
            scored.push({ item: item, score: score });
        }

        scored.sort(function (a, b) { return b.score - a.score; });
        return scored.map(function (x) { return x.item; });
    }

    function renderSearchEmptyState(container, hasQuery) {
        if (!container) return;
        if (!hasQuery) {
            container.innerHTML = '<div class="admin-help-search-empty text-secondary small py-3 px-2">' + SEARCH_EMPTY_HINT + '</div>';
            return;
        }
        container.innerHTML = '<div class="admin-help-search-empty text-secondary small py-3 px-2">No se encontraron resultados. Pruebe con otras palabras.</div>';
    }

    function setSearchActiveItem(container, index) {
        if (!container) return;
        var items = container.querySelectorAll('.admin-help-search-item');
        for (var i = 0; i < items.length; i++) {
            items[i].classList.toggle('admin-help-search-item--active', i === index);
            if (i === index) items[i].setAttribute('aria-selected', 'true');
            else items[i].removeAttribute('aria-selected');
        }
        state.modalSearchActiveIndex = index;
        if (index >= 0 && items[index]) {
            items[index].scrollIntoView({ block: 'nearest', behavior: state.prefersReducedMotion ? 'auto' : 'smooth' });
        }
    }

    function renderCategoryBadge(category) {
        var cat = category || '';
        var norm = normalizeText(cat);
        if (norm === 'intranet') {
            return '<span class="badge admin-help-search-badge-intranet mt-1">' + escapeHtml(cat) + '</span>';
        }
        return '<span class="badge bg-light text-secondary border mt-1">' + escapeHtml(cat) + '</span>';
    }

    function renderSearchResults(container, query, onNavigate, options) {
        options = options || {};
        if (!container) return;

        var trimmed = (query || '').trim();
        var results = filterCatalog(trimmed);

        if (!trimmed) {
            renderSearchEmptyState(container, false);
            state.modalSearchActiveIndex = -1;
            return;
        }

        if (results.length === 0) {
            renderSearchEmptyState(container, true);
            state.modalSearchActiveIndex = -1;
            return;
        }

        var html = '<ul class="list-group list-group-flush admin-help-search-list">';
        for (var i = 0; i < results.length; i++) {
            var r = results[i];
            var activeClass = options.enableKeyboardNav && i === state.modalSearchActiveIndex ? ' admin-help-search-item--active' : '';
            var intranetClass = isIntranetItem(r) ? ' admin-help-search-item--intranet' : '';
            var externalIcon = isIntranetItem(r) && (r.url || '').toLowerCase().indexOf('/admin/') !== 0
                ? '<i class="bi bi-box-arrow-up-right admin-help-search-external-icon" aria-hidden="true"></i>'
                : '';
            html += '<li class="list-group-item admin-help-search-item' + activeClass + intranetClass + '" role="option" tabindex="0" data-url="' + escapeHtml(r.url) + '" data-index="' + i + '">' +
                '<div class="d-flex align-items-start gap-3">' +
                '<span class="admin-help-search-item-icon"><i class="bi ' + escapeHtml(r.icon || 'bi-link-45deg') + '"></i></span>' +
                '<div class="flex-grow-1 min-w-0">' +
                '<div class="fw-semibold text-truncate d-flex align-items-center gap-1">' + escapeHtml(r.title) + externalIcon + '</div>' +
                '<div class="small text-secondary">' + escapeHtml(r.description) + '</div>' +
                renderCategoryBadge(r.category) +
                '</div></div></li>';
        }
        html += '</ul>';
        container.innerHTML = html;

        var items = container.querySelectorAll('.admin-help-search-item');
        for (var j = 0; j < items.length; j++) {
            (function (node) {
                function go() {
                    var url = node.getAttribute('data-url');
                    if (url) {
                        if (typeof onNavigate === 'function') onNavigate();
                        window.location.href = url;
                    }
                }
                node.addEventListener('click', go);
                node.addEventListener('keydown', function (ev) {
                    if (ev.key === 'Enter' || ev.key === ' ') {
                        ev.preventDefault();
                        go();
                    }
                });
            })(items[j]);
        }

        if (options.enableKeyboardNav && state.modalSearchActiveIndex < 0 && items.length > 0) {
            setSearchActiveItem(container, 0);
        }
    }

    function applyOnboardingHubUi(completed) {
        state.onboardingCompleted = !!completed;
        if (completed) {
            document.body.classList.add('admin-onboarding-done');
        } else {
            document.body.classList.remove('admin-onboarding-done');
        }
        updateHelpMenuVisibility();
    }

    function updateHelpMenuVisibility() {
        var resetWrap = document.getElementById('adminHelpMenuResetOnboardingWrap');
        if (resetWrap) {
            resetWrap.classList.toggle('d-none', !state.onboardingCompleted);
        }

        var ctxWrap = document.getElementById('adminHelpMenuContextualTourWrap');
        var tour = resolveContextualTour(window.location.pathname);
        if (ctxWrap) {
            ctxWrap.classList.toggle('d-none', !tour);
        }
    }

    function bindHubSearch() {
        var input = document.getElementById('adminHelpHubSearchInput');
        var results = document.getElementById('adminHelpHubSearchResults');
        if (!input || !results) return;

        function update() {
            renderSearchResults(results, input.value, null);
        }

        input.addEventListener('input', update);
        input.addEventListener('keydown', function (ev) {
            if (ev.key === 'Enter') {
                ev.preventDefault();
                var first = results.querySelector('.admin-help-search-item');
                if (first) first.click();
            }
        });
        update();
    }

    function bindModalSearch() {
        var input = document.getElementById('adminHelpSearchModalInput');
        var results = document.getElementById('adminHelpSearchModalResults');
        if (!input || !results) return;

        function update() {
            renderSearchResults(results, input.value, function () {
                if (state.searchModal) state.searchModal.hide();
            }, { enableKeyboardNav: true });
        }

        input.addEventListener('input', function () {
            state.modalSearchActiveIndex = -1;
            update();
        });

        var modalEl = document.getElementById('adminHelpSearchModal');
        if (modalEl) {
            modalEl.addEventListener('shown.bs.modal', function () {
                input.value = '';
                state.modalSearchActiveIndex = -1;
                update();
                setTimeout(function () { input.focus(); }, 50);
            });
            input.addEventListener('keydown', function (ev) {
                var items = results.querySelectorAll('.admin-help-search-item');
                if (ev.key === 'ArrowDown') {
                    ev.preventDefault();
                    if (items.length === 0) return;
                    var next = state.modalSearchActiveIndex < items.length - 1 ? state.modalSearchActiveIndex + 1 : 0;
                    setSearchActiveItem(results, next);
                    return;
                }
                if (ev.key === 'ArrowUp') {
                    ev.preventDefault();
                    if (items.length === 0) return;
                    var prev = state.modalSearchActiveIndex > 0 ? state.modalSearchActiveIndex - 1 : items.length - 1;
                    setSearchActiveItem(results, prev);
                    return;
                }
                if (ev.key === 'Enter') {
                    ev.preventDefault();
                    var active = results.querySelector('.admin-help-search-item--active') || results.querySelector('.admin-help-search-item');
                    if (active) active.click();
                }
            });
        }
    }

    window.MatAdminHelp.openSearch = function () {
        var modalEl = document.getElementById('adminHelpSearchModal');
        if (!modalEl || !window.bootstrap || !window.bootstrap.Modal) return;
        if (!state.searchModal) state.searchModal = new bootstrap.Modal(modalEl);
        state.searchModal.show();
    };

    function getHelpDropdownToggle() {
        return document.getElementById('adminUserDropdown');
    }

    function openHelpDropdownForTour() {
        var toggle = getHelpDropdownToggle();
        if (!toggle || !window.bootstrap || !window.bootstrap.Dropdown) return;
        var dd = window.bootstrap.Dropdown.getOrCreateInstance(toggle);
        dd.show();
        state.helpDropdownOpenedForTour = true;
        document.body.classList.add('admin-tour-help-open');
    }

    function closeHelpDropdownForTour() {
        if (!state.helpDropdownOpenedForTour) return;
        var toggle = getHelpDropdownToggle();
        if (toggle && window.bootstrap && window.bootstrap.Dropdown) {
            var dd = window.bootstrap.Dropdown.getInstance(toggle);
            if (dd) dd.hide();
        }
        state.helpDropdownOpenedForTour = false;
        document.body.classList.remove('admin-tour-help-open');
    }

    function getTourSpotlightTarget(step) {
        if (!step) return null;
        if (step.openHelpDropdown) {
            var menu = document.querySelector('#admin-tour-user-menu .dropdown-menu');
            if (menu && menu.classList.contains('show')) return menu;
            return document.querySelector('#admin-tour-user-menu');
        }
        return document.querySelector(step.selector);
    }

    function positionSpotlight(target) {
        var spot = document.getElementById('adminTourSpotlight');
        if (!spot || !target) return;

        var rect = target.getBoundingClientRect();
        var pad = 8;
        spot.style.top = Math.max(0, rect.top - pad) + 'px';
        spot.style.left = Math.max(0, rect.left - pad) + 'px';
        spot.style.width = (rect.width + pad * 2) + 'px';
        spot.style.height = (rect.height + pad * 2) + 'px';
        spot.classList.remove('d-none');

        if (state.prefersReducedMotion) {
            spot.style.transition = 'none';
        } else {
            spot.style.transition = '';
        }
    }

    function positionTourCard(target) {
        var card = document.getElementById('adminTourCard');
        if (!card) return;

        card.classList.remove('admin-tour-card--top', 'admin-tour-card--bottom');

        var cardRect = card.getBoundingClientRect();
        var cardH = cardRect.height || 220;
        var viewportH = window.innerHeight || document.documentElement.clientHeight;
        var placeTop = false;

        if (target && target !== document.body) {
            var targetRect = target.getBoundingClientRect();
            var spaceBelow = viewportH - targetRect.bottom;
            var spaceAbove = targetRect.top;
            if (spaceBelow < cardH + 24 && spaceAbove > spaceBelow) {
                placeTop = true;
            }
            if (targetRect.bottom > viewportH * 0.55) {
                placeTop = true;
            }
        }

        if (placeTop) {
            card.classList.add('admin-tour-card--top');
        } else {
            card.classList.add('admin-tour-card--bottom');
        }
    }

    function updateTourProgressBar() {
        var bar = document.getElementById('adminTourProgressBar');
        if (!bar || !state.tourSteps.length) return;
        var pct = ((state.tourIndex + 1) / state.tourSteps.length) * 100;
        bar.style.width = pct + '%';
        bar.setAttribute('aria-valuenow', String(Math.round(pct)));
    }

    function removeTourKeyHandler() {
        // El handler global verifica state.tourActive; no se remueve entre tours.
    }

    function hideTourUi() {
        closeHelpDropdownForTour();
        removeTourKeyHandler();

        var root = document.getElementById('adminTourRoot');
        if (root) {
            root.classList.add('d-none');
            root.setAttribute('aria-hidden', 'true');
        }
        state.tourActive = false;
        document.body.classList.remove('admin-tour-active');

        if (state.resizeHandler) {
            window.removeEventListener('resize', state.resizeHandler);
            state.resizeHandler = null;
        }
        if (state.scrollHandler) {
            window.removeEventListener('scroll', state.scrollHandler, true);
            state.scrollHandler = null;
        }
    }

    function focusTourPrimaryAction() {
        var nextBtn = document.getElementById('adminTourNext');
        if (nextBtn) nextBtn.focus();
    }

    function updateTourStep() {
        var steps = state.tourSteps;
        var idx = state.tourIndex;
        var step = steps[idx];
        if (!step) return;

        if (idx !== steps.length - 1 || !step.openHelpDropdown) {
            closeHelpDropdownForTour();
        }

        if (step.openHelpDropdown) {
            openHelpDropdownForTour();
        }

        var target = getTourSpotlightTarget(step);
        var titleEl = document.getElementById('adminTourTitle');
        var textEl = document.getElementById('adminTourText');
        var badgeEl = document.getElementById('adminTourStepBadge');
        var prevBtn = document.getElementById('adminTourPrev');
        var nextBtn = document.getElementById('adminTourNext');

        if (titleEl) titleEl.textContent = step.title;
        if (textEl) textEl.textContent = step.text;
        if (badgeEl) badgeEl.textContent = (idx + 1) + ' / ' + steps.length;
        if (prevBtn) prevBtn.disabled = idx === 0;
        if (nextBtn) nextBtn.textContent = idx === steps.length - 1 ? 'Finalizar' : 'Siguiente';

        updateTourProgressBar();

        var scrollBehavior = state.prefersReducedMotion ? 'auto' : 'smooth';

        function applyPositions() {
            target = getTourSpotlightTarget(step);
            if (target) {
                positionSpotlight(target);
                positionTourCard(target);
            } else {
                positionSpotlight(document.body);
                positionTourCard(document.body);
            }
            focusTourPrimaryAction();
        }

        if (target) {
            target.scrollIntoView({ behavior: scrollBehavior, block: 'nearest', inline: 'nearest' });
            var delay = step.openHelpDropdown ? 120 : (state.prefersReducedMotion ? 0 : 280);
            setTimeout(applyPositions, delay);
        } else {
            applyPositions();
        }
    }

    function markOnboardingComplete(persist) {
        if (!persist) return Promise.resolve();

        var token = getAntiforgeryToken();
        if (window.jQuery) {
            return window.jQuery.ajax({
                url: state.onboardingCompleteUrl,
                type: 'POST',
                data: { __RequestVerificationToken: token }
            }).then(function () {
                applyOnboardingHubUi(true);
                return null;
            }).catch(function () { return null; });
        }

        return fetch(state.onboardingCompleteUrl, {
            method: 'POST',
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
            body: '__RequestVerificationToken=' + encodeURIComponent(token)
        }).then(function () {
            applyOnboardingHubUi(true);
        }).catch(function () { return null; });
    }

    function endTour(options) {
        options = options || {};
        var wasContextual = state.tourMode === 'contextual';
        var pageId = state.contextualPageId;

        hideTourUi();

        if (wasContextual && pageId) {
            if (options.persist !== false) {
                markContextualTourComplete(pageId);
            }
            state.tourMode = 'hub';
            state.contextualPageId = null;
            if (window.MatAdmin && window.MatAdmin.toast) {
                window.MatAdmin.toast('Guía de pantalla completada.', 'success');
            }
            return;
        }

        state.tourMode = 'hub';
        state.contextualPageId = null;

        if (options.persist) {
            markOnboardingComplete(true).then(function () {
                if (window.MatAdmin && window.MatAdmin.toast) {
                    window.MatAdmin.toast('Guía completada. Puede volver a verla desde el menú de usuario.', 'success');
                }
            });
        }
    }

    function tourSkip() {
        endTour({ persist: state.tourPersistOnEnd });
    }

    function tourNext() {
        if (state.tourIndex < state.tourSteps.length - 1) {
            state.tourIndex++;
            updateTourStep();
        } else {
            endTour({ persist: state.tourPersistOnEnd });
        }
    }

    function tourPrev() {
        if (state.tourIndex > 0) {
            state.tourIndex--;
            updateTourStep();
        }
    }

    function initTourControlsOnce() {
        if (state.tourControlsBound) return;
        state.tourControlsBound = true;

        state.tourKeyHandler = function (ev) {
            if (!state.tourActive) return;
            if (ev.key === 'Escape') {
                ev.preventDefault();
                tourSkip();
            }
        };
        document.addEventListener('keydown', state.tourKeyHandler);

        var nextBtn = document.getElementById('adminTourNext');
        var prevBtn = document.getElementById('adminTourPrev');
        var skipBtn = document.getElementById('adminTourSkip');
        var skipTop = document.getElementById('adminTourSkipTop');

        if (nextBtn) nextBtn.addEventListener('click', tourNext);
        if (prevBtn) prevBtn.addEventListener('click', tourPrev);
        if (skipBtn) skipBtn.addEventListener('click', tourSkip);
        if (skipTop) skipTop.addEventListener('click', tourSkip);
    }

    function bindTourControls() {
        if (state.resizeHandler) {
            window.removeEventListener('resize', state.resizeHandler);
            window.removeEventListener('scroll', state.resizeHandler, true);
        }

        state.resizeHandler = function () {
            var step = state.tourSteps[state.tourIndex];
            if (!step) return;
            var target = getTourSpotlightTarget(step);
            if (target) {
                positionSpotlight(target);
                positionTourCard(target);
            }
        };
        state.scrollHandler = state.resizeHandler;

        window.addEventListener('resize', state.resizeHandler);
        window.addEventListener('scroll', state.scrollHandler, true);
    }

    function beginTour(steps, options) {
        options = options || {};
        state.prefersReducedMotion = prefersReducedMotion();
        initTourControlsOnce();

        var root = document.getElementById('adminTourRoot');
        if (!root || !steps || !steps.length) return;

        state.tourMode = options.mode || 'hub';
        state.contextualPageId = options.pageId || null;
        state.tourPersistOnEnd = options.persistOnEnd !== false;
        state.tourSteps = steps;
        state.tourIndex = 0;
        state.tourActive = true;
        root.classList.remove('d-none');
        root.setAttribute('aria-hidden', 'false');
        document.body.classList.add('admin-tour-active');

        bindTourControls();
        updateTourStep();
    }

    function cleanTourQueryFromUrl() {
        try {
            var params = new URLSearchParams(window.location.search || '');
            if (params.get('tour') !== '1') return;
            params.delete('tour');
            var qs = params.toString();
            var newUrl = window.location.pathname + (qs ? '?' + qs : '') + window.location.hash;
            window.history.replaceState({}, document.title, newUrl);
        } catch (e) {
            // ignore
        }
    }

    window.MatAdminHelp.startTour = function (options) {
        options = options || {};

        var path = (window.location.pathname || '').toLowerCase();
        var onIndex = path === '/admin/index' || path === '/admin' || path === '/admin/';

        if (!onIndex && options.force) {
            window.location.href = '/Admin/Index?tour=1';
            return;
        }

        if (onIndex) {
            cleanTourQueryFromUrl();
        }

        var persistOnEnd = options.persist !== false && !options.force;
        beginTour(buildDefaultTourSteps(), { mode: 'hub', persistOnEnd: persistOnEnd });
    };

    window.MatAdminHelp.startContextualTour = function (options) {
        options = options || {};
        var tour = resolveContextualTour(window.location.pathname);
        if (!tour || !tour.steps || !tour.steps.length) return;

        if (!options.force && isContextualTourComplete(tour.pageId)) return;

        var persistOnEnd = options.persist !== false;
        beginTour(tour.steps, {
            mode: 'contextual',
            pageId: tour.pageId,
            persistOnEnd: persistOnEnd
        });
    };

    function postOnboardingReset() {
        var token = getAntiforgeryToken();

        function onSuccess() {
            applyOnboardingHubUi(false);
            if (window.MatAdmin && window.MatAdmin.toast) {
                window.MatAdmin.toast('Guía de inicio reiniciada. Se mostrará al entrar al panel principal.', 'success');
            }
            if (state.isIndexPage) {
                setTimeout(function () {
                    window.MatAdminHelp.startTour({ force: false, persist: true });
                }, 400);
            }
        }

        function onError(msg) {
            if (window.MatAdmin && window.MatAdmin.toast) {
                window.MatAdmin.toast(msg || 'No se pudo reiniciar la guía de inicio.', 'danger');
            }
        }

        if (window.jQuery) {
            return window.jQuery.ajax({
                url: state.onboardingResetUrl,
                type: 'POST',
                data: { __RequestVerificationToken: token }
            }).then(function (resp) {
                if (resp && resp.ok) onSuccess();
                else onError(resp && resp.message);
            }).catch(function () {
                onError();
            });
        }

        return fetch(state.onboardingResetUrl, {
            method: 'POST',
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
            body: '__RequestVerificationToken=' + encodeURIComponent(token)
        }).then(function (r) { return r.json(); })
            .then(function (resp) {
                if (resp && resp.ok) onSuccess();
                else onError(resp && resp.message);
            })
            .catch(function () { onError(); });
    }

    function fetchOnboardingStatus() {
        var url = state.onboardingStatusUrl;

        function handleResponse(resp) {
            if (resp && resp.ok && resp.data) {
                applyOnboardingHubUi(!!resp.data.completed);
                return !!resp.data.completed;
            }
            if (window.MatAdmin && window.MatAdmin.toast) {
                window.MatAdmin.toast('No se pudo verificar el estado de la guía de inicio.', 'info');
            }
            updateHelpMenuVisibility();
            return null;
        }

        if (window.jQuery) {
            return window.jQuery.getJSON(url).then(function (resp) {
                return handleResponse(resp);
            }).catch(function () {
                if (window.MatAdmin && window.MatAdmin.toast) {
                    window.MatAdmin.toast('No se pudo verificar el estado de la guía de inicio.', 'info');
                }
                updateHelpMenuVisibility();
                return null;
            });
        }

        return fetch(url, { credentials: 'same-origin' })
            .then(function (r) { return r.json(); })
            .then(handleResponse)
            .catch(function () {
                if (window.MatAdmin && window.MatAdmin.toast) {
                    window.MatAdmin.toast('No se pudo verificar el estado de la guía de inicio.', 'info');
                }
                updateHelpMenuVisibility();
                return null;
            });
    }

    function maybeAutoStartTour() {
        if (!state.isIndexPage) return;

        fetchOnboardingStatus().then(function (completed) {
            if (completed === false) {
                setTimeout(function () {
                    window.MatAdminHelp.startTour({ force: false, persist: true });
                }, 400);
            }
        });
    }

    function maybeAutoStartContextualTour() {
        if (state.isIndexPage || state.tourActive) return;

        var tour = resolveContextualTour(window.location.pathname);
        if (!tour || isContextualTourComplete(tour.pageId)) return;

        setTimeout(function () {
            if (!state.tourActive) {
                window.MatAdminHelp.startContextualTour({ force: false, persist: true });
            }
        }, 500);
    }

    function bindKeyboardShortcuts() {
        document.addEventListener('keydown', function (ev) {
            if (state.tourActive && ev.key === 'Escape') return;

            var tag = (ev.target && ev.target.tagName) ? ev.target.tagName.toLowerCase() : '';
            var isEditable = tag === 'input' || tag === 'textarea' || tag === 'select' || (ev.target && ev.target.isContentEditable);

            if (ev.ctrlKey && (ev.key === 'k' || ev.key === 'K')) {
                ev.preventDefault();
                window.MatAdminHelp.openSearch();
                return;
            }

            if (ev.key === '/' && !isEditable && !ev.ctrlKey && !ev.metaKey && !ev.altKey) {
                ev.preventDefault();
                window.MatAdminHelp.openSearch();
            }
        });
    }

    function bindSearchTriggers() {
        var el = document.getElementById('adminTopbarSearchBtn');
        if (!el) return;
        el.addEventListener('click', function (ev) {
            ev.preventDefault();
            window.MatAdminHelp.openSearch();
        });
    }

    function bindHelpMenu() {
        var tourLink = document.getElementById('adminHelpMenuTour');
        if (tourLink) {
            tourLink.addEventListener('click', function (ev) {
                ev.preventDefault();
                window.MatAdminHelp.startTour({ force: true, persist: false });
            });
        }

        var ctxLink = document.getElementById('adminHelpMenuContextualTour');
        if (ctxLink) {
            ctxLink.addEventListener('click', function (ev) {
                ev.preventDefault();
                window.MatAdminHelp.startContextualTour({ force: true, persist: true });
            });
        }

        var resetLink = document.getElementById('adminHelpMenuResetOnboarding');
        if (resetLink) {
            resetLink.addEventListener('click', function (ev) {
                ev.preventDefault();
                if (!window.MatAdmin || !window.MatAdmin.confirm) return;
                window.MatAdmin.confirm({
                    title: 'Reiniciar guía de inicio',
                    message: '¿Desea volver a ver el tour del panel principal? La próxima vez que entre al panel se mostrará la guía automáticamente.',
                    confirmLabel: 'Reiniciar',
                    confirmClass: 'btn-primary'
                }).then(function (ok) {
                    if (ok) postOnboardingReset();
                });
            });
        }
    }

    window.MatAdminHelp.init = function (options) {
        options = options || {};
        state.catalog = parseCatalog();
        state.contextualTours = parseContextualTours();
        state.userId = getUserId();
        state.isIndexPage = !!options.isIndexPage;
        state.prefersReducedMotion = prefersReducedMotion();
        if (options.onboardingCompleteUrl) state.onboardingCompleteUrl = options.onboardingCompleteUrl;
        if (options.onboardingStatusUrl) state.onboardingStatusUrl = options.onboardingStatusUrl;
        if (options.onboardingResetUrl) state.onboardingResetUrl = options.onboardingResetUrl;

        bindHubSearch();
        bindModalSearch();
        bindKeyboardShortcuts();
        bindHelpMenu();
        bindSearchTriggers();
        updateHelpMenuVisibility();

        if (state.isIndexPage) {
            var params = new URLSearchParams(window.location.search || '');
            if (params.get('tour') !== '1') {
                maybeAutoStartTour();
            } else {
                fetchOnboardingStatus();
            }
        } else {
            fetchOnboardingStatus();
            maybeAutoStartContextualTour();
        }
    };

    function onReady(fn) {
        if (document.readyState === 'loading') {
            document.addEventListener('DOMContentLoaded', fn);
        } else {
            fn();
        }
    }

    onReady(function () {
        var path = (window.location.pathname || '').toLowerCase();
        var isIndex = path === '/admin/index' || path === '/admin' || path === '/admin/';
        var params = new URLSearchParams(window.location.search || '');
        var tourQuery = params.get('tour') === '1';

        window.MatAdminHelp.init({ isIndexPage: isIndex });

        if (tourQuery && isIndex) {
            setTimeout(function () {
                window.MatAdminHelp.startTour({ force: true, persist: false });
            }, 500);
        }
    });
})(window, document);
