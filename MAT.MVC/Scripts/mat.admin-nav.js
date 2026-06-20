/**
 * Panel Admin: navegación lateral, estado activo, colapsado y offcanvas.
 */
(function (window, document) {
    'use strict';

    window.MatAdminNav = window.MatAdminNav || {};

    var STORAGE_SECTIONS = 'mat.admin.nav.sections';
    var STORAGE_COLLAPSED = 'mat.admin.sidebar.collapsed';

    function normalizePath(path) {
        if (!path) return '/';
        var p = path.split('?')[0].trim().toLowerCase();
        if (!p.startsWith('/')) p = '/' + p;
        return p.replace(/\/+$/, '') || '/';
    }

    function readJson(key, fallback) {
        try {
            var raw = window.localStorage.getItem(key);
            return raw ? JSON.parse(raw) : fallback;
        } catch (e) {
            return fallback;
        }
    }

    function writeJson(key, value) {
        try {
            window.localStorage.setItem(key, JSON.stringify(value));
        } catch (e) { /* ignore */ }
    }

    function isLinkActive(path, href) {
        var linkPath = normalizePath(href);
        if (!linkPath) return false;

        if (linkPath === '/admin/reportes' && path.indexOf('/admin/reportes') === 0) {
            return true;
        }

        if (linkPath === '/admin/usuarios'
            && (path.indexOf('/admin/usuarioeditar') === 0 || path.indexOf('/admin/usuarioresetpassword') === 0)) {
            return true;
        }

        return path === linkPath || path.indexOf(linkPath + '/') === 0;
    }

    function markActiveLinks() {
        var path = normalizePath(window.location.pathname);
        var links = document.querySelectorAll('.admin-nav .nav-link[data-nav-url]');

        for (var i = 0; i < links.length; i++) {
            var link = links[i];
            var href = link.getAttribute('data-nav-url') || link.getAttribute('href') || '';
            var active = isLinkActive(path, href);

            link.classList.toggle('active', active);
            if (active) {
                link.setAttribute('aria-current', 'page');
            } else {
                link.removeAttribute('aria-current');
            }
        }

        // Expandir sección con ítem activo
        var activeLink = document.querySelector('.admin-nav .nav-link.active');
        if (activeLink) {
            var section = activeLink.closest('.admin-nav-section');
            if (section) {
                setSectionExpanded(section, true, true);
            }
        }
    }

    function getSectionId(sectionEl) {
        return sectionEl ? sectionEl.getAttribute('data-nav-section') : null;
    }

    function setSectionExpanded(sectionEl, expanded, persist) {
        if (!sectionEl) return;
        var panel = sectionEl.querySelector('.admin-nav-section-panel');
        var toggle = sectionEl.querySelector('.admin-nav-section-toggle');
        if (!panel || !toggle) return;

        sectionEl.classList.toggle('admin-nav-section--collapsed', !expanded);
        toggle.setAttribute('aria-expanded', expanded ? 'true' : 'false');
        panel.hidden = !expanded;

        if (persist) {
            var id = getSectionId(sectionEl);
            if (!id) return;
            var state = readJson(STORAGE_SECTIONS, {});
            state[id] = expanded;
            writeJson(STORAGE_SECTIONS, state);
        }
    }

    function bindSectionToggles() {
        var toggles = document.querySelectorAll('[data-nav-section-toggle]');
        var saved = readJson(STORAGE_SECTIONS, {});

        for (var i = 0; i < toggles.length; i++) {
            (function (toggle) {
                var section = toggle.closest('.admin-nav-section');
                var id = getSectionId(section);
                var hasActive = section && section.querySelector('.nav-link.active');
                var expanded = hasActive ? true : (id && saved.hasOwnProperty(id) ? !!saved[id] : true);
                setSectionExpanded(section, expanded, false);

                toggle.addEventListener('click', function () {
                    var isCollapsed = section.classList.contains('admin-nav-section--collapsed');
                    setSectionExpanded(section, isCollapsed, true);
                });
            })(toggles[i]);
        }
    }

    function bindOffcanvasClose() {
        var offcanvasEl = document.getElementById('adminOffcanvas');
        if (!offcanvasEl || !window.bootstrap || !window.bootstrap.Offcanvas) return;

        var links = offcanvasEl.querySelectorAll('.nav-link');
        for (var i = 0; i < links.length; i++) {
            links[i].addEventListener('click', function () {
                var instance = window.bootstrap.Offcanvas.getInstance(offcanvasEl);
                if (instance) instance.hide();
            });
        }
    }

    function bindOffcanvasActions() {
        var searchBtn = document.getElementById('adminOffcanvasSearchBtn');
        if (searchBtn) {
            searchBtn.addEventListener('click', function () {
                if (window.MatAdminHelp && window.MatAdminHelp.openSearch) {
                    window.MatAdminHelp.openSearch();
                }
                var offcanvasEl = document.getElementById('adminOffcanvas');
                if (offcanvasEl && window.bootstrap && window.bootstrap.Offcanvas) {
                    var instance = window.bootstrap.Offcanvas.getInstance(offcanvasEl);
                    if (instance) instance.hide();
                }
            });
        }

        var tourBtn = document.getElementById('adminOffcanvasTourBtn');
        if (tourBtn) {
            tourBtn.addEventListener('click', function (ev) {
                ev.preventDefault();
                if (window.MatAdminHelp && window.MatAdminHelp.startTour) {
                    window.MatAdminHelp.startTour({ force: true, persist: false });
                }
                var offcanvasEl = document.getElementById('adminOffcanvas');
                if (offcanvasEl && window.bootstrap && window.bootstrap.Offcanvas) {
                    var instance = window.bootstrap.Offcanvas.getInstance(offcanvasEl);
                    if (instance) instance.hide();
                }
            });
        }
    }

    function applySidebarCollapsed(collapsed) {
        document.body.classList.toggle('admin-sidebar-collapsed', collapsed);
        var btn = document.getElementById('adminSidebarCollapse');
        if (btn) {
            btn.setAttribute('aria-pressed', collapsed ? 'true' : 'false');
            btn.title = collapsed ? 'Expandir menú lateral' : 'Colapsar menú lateral';
        }
    }

    function bindSidebarCollapse() {
        var btn = document.getElementById('adminSidebarCollapse');
        if (!btn) return;

        var collapsed = readJson(STORAGE_COLLAPSED, false) === true;
        applySidebarCollapsed(collapsed);

        btn.addEventListener('click', function () {
            collapsed = !document.body.classList.contains('admin-sidebar-collapsed');
            applySidebarCollapsed(collapsed);
            try {
                window.localStorage.setItem(STORAGE_COLLAPSED, JSON.stringify(collapsed));
            } catch (e) { /* ignore */ }
        });
    }

    function bindTopbarCompact() {
        var topbar = document.getElementById('adminTopbar');
        if (!topbar) return;

        var reducedMotion = false;
        try {
            reducedMotion = window.matchMedia && window.matchMedia('(prefers-reduced-motion: reduce)').matches;
        } catch (e) { /* ignore */ }

        function onScroll() {
            var compact = window.scrollY > 48;
            topbar.classList.toggle('admin-topbar--compact', compact);
        }

        window.addEventListener('scroll', onScroll, { passive: true });
        onScroll();

        if (reducedMotion) {
            topbar.classList.add('admin-topbar--no-motion');
        }
    }

    window.MatAdminNav.init = function () {
        markActiveLinks();
        bindSectionToggles();
        bindOffcanvasClose();
        bindOffcanvasActions();
        bindSidebarCollapse();
        bindTopbarCompact();
    };

    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', window.MatAdminNav.init);
    } else {
        window.MatAdminNav.init();
    }
})(window, document);
