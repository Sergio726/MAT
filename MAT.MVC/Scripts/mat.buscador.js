/* Buscador global de la barra superior (Ctrl+K). Bloque D del plan del panel principal, etapa 1: módulos.
   Índice: catálogo de accesos (data-catalogo del <dialog>, desde QuickAccessCatalog) + ítems del menú lateral.
   Resultados agrupados por tipo (TIPOS): la etapa 2 suma clientes, viajes y facturas como tipos nuevos.
   Teclado: Ctrl+K (o Cmd+K) abre y cierra, flechas para moverse, Enter abre, Esc cierra.
   Accesibilidad: patrón combobox + listbox con aria-activedescendant; el <dialog> modal atrapa el foco. */
(function (window, document) {
    "use strict";

    var TIPOS = [
        { clave: "modulo", titulo: "Módulos" },
        { clave: "accion", titulo: "Acciones" }
    ];
    var MAX_POR_TIPO = 8;

    var dialogo, entrada, lista, vacio, estado, disparador;
    var indice = [];
    var visibles = [];
    var activo = -1;

    function normalizar(texto) {
        return (texto || "").toString().normalize("NFD").replace(/[̀-ͯ]/g, "")
            .toLowerCase().replace(/[^a-z0-9]+/g, " ").trim();
    }

    function iconoDe(elemento) {
        var i = elemento.querySelector("i.bi");
        var m = i ? /bi-[\w-]+/.exec(i.className) : null;
        return m ? m[0] : "bi-box-arrow-up-right";
    }

    function esAlta(url, titulo) {
        return /\/(create|abm)(\?|$)/i.test(url || "") || /^nuev[oa]\b/i.test(titulo || "");
    }

    function construirIndice() {
        var vistos = {};
        indice = [];

        function agregar(item) {
            var clave = (item.url || "").toLowerCase();
            if (!clave || clave === "#" || vistos[clave]) return;
            vistos[clave] = true;
            item.tipo = item.accion ? "accion" : "modulo";
            item.orden = indice.length;
            item.tTitulo = " " + normalizar(item.titulo);
            item.tResto = " " + normalizar((item.grupo || "") + " " + (item.descripcion || ""));
            indice.push(item);
        }

        var catalogo = [];
        try { catalogo = JSON.parse(dialogo.getAttribute("data-catalogo") || "[]"); } catch (e) { catalogo = []; }
        catalogo.forEach(function (c) {
            agregar({ titulo: c.titulo, descripcion: c.descripcion, url: c.url, icono: c.icono, accion: !!c.accion, frecuente: !!c.frecuente });
        });

        // Menú lateral: "Listado" o "Nuevo" solos no dicen nada, por eso el título lleva el grupo ("Hoteles › Nuevo")
        Array.prototype.forEach.call(document.querySelectorAll("#menu .menu-0 > li"), function (li) {
            var link = li.querySelector(":scope > a");
            if (!link) return;
            var grupo = (link.querySelector("span") || link).textContent.trim();
            var hijos = li.querySelectorAll(":scope > ul > li > a[href]");
            if (!hijos.length) {
                var href = link.getAttribute("href");
                if (href && href !== "#" && link.getAttribute("data-bs-toggle") !== "collapse") {
                    agregar({ titulo: grupo, descripcion: "", url: href, icono: iconoDe(link), accion: false });
                }
                return;
            }
            Array.prototype.forEach.call(hijos, function (a) {
                var texto = (a.querySelector("span") || a).textContent.trim();
                var url = a.getAttribute("href");
                agregar({ titulo: grupo + " › " + texto, grupo: grupo, descripcion: "", url: url, icono: iconoDe(a), accion: esAlta(url, texto) });
            });
        });
    }

    // Puntaje: cada palabra buscada tiene que aparecer; pesa más al comienzo de una palabra del título
    function puntuar(item, palabras) {
        var total = 0;
        for (var i = 0; i < palabras.length; i++) {
            var p = palabras[i];
            if (item.tTitulo.indexOf(" " + p) >= 0) total += 3;
            else if (item.tTitulo.indexOf(p) >= 0) total += 2;
            else if (item.tResto.indexOf(" " + p) >= 0) total += 1.5;
            else if (item.tResto.indexOf(p) >= 0) total += 1;
            else return -1;
        }
        return total + (item.frecuente ? 0.5 : 0);
    }

    function buscar(consulta) {
        var palabras = normalizar(consulta).split(" ").filter(Boolean);
        var candidatos;
        if (!palabras.length) {
            candidatos = indice.filter(function (it) { return it.frecuente; })
                .map(function (it) { return { item: it, puntos: 0 }; });
        } else {
            candidatos = indice.map(function (it) { return { item: it, puntos: puntuar(it, palabras) }; })
                .filter(function (c) { return c.puntos >= 0; });
        }
        candidatos.sort(function (a, b) { return (b.puntos - a.puntos) || (a.item.orden - b.item.orden); });

        // Los grupos se ordenan por su mejor resultado (con "fact", Módulos → Buscar facturas va primero);
        // si empatan, manda el orden de TIPOS
        return TIPOS.map(function (t, orden) {
            var delTipo = candidatos.filter(function (c) { return c.item.tipo === t.clave; }).slice(0, MAX_POR_TIPO);
            return {
                tipo: t,
                orden: orden,
                mejor: delTipo.length ? delTipo[0].puntos : -1,
                items: delTipo.map(function (c) { return c.item; })
            };
        }).filter(function (g) { return g.items.length; })
          .sort(function (a, b) { return (b.mejor - a.mejor) || (a.orden - b.orden); });
    }

    function crear(etiqueta, clase, texto) {
        var e = document.createElement(etiqueta);
        if (clase) e.className = clase;
        if (texto != null) e.textContent = texto;
        return e;
    }

    function pintar() {
        var grupos = buscar(entrada.value);
        lista.textContent = "";
        visibles = [];
        grupos.forEach(function (g, gi) {
            var bloque = crear("div", "bg-bloque");
            bloque.setAttribute("role", "group");
            bloque.setAttribute("aria-labelledby", "bg-grupo-" + gi);
            var cabecera = crear("div", "bg-grupo", g.tipo.titulo);
            cabecera.id = "bg-grupo-" + gi;
            bloque.appendChild(cabecera);
            g.items.forEach(function (it) {
                var n = visibles.length;
                var op = crear("div", "bg-opcion");
                op.id = "bg-opcion-" + n;
                op.setAttribute("role", "option");
                op.setAttribute("aria-selected", "false");
                op.setAttribute("data-indice", n);
                var icono = crear("i", "bi " + (it.icono || "bi-box-arrow-up-right"));
                icono.setAttribute("aria-hidden", "true");
                var textos = crear("span", "bg-opcion__textos");
                textos.appendChild(crear("span", "bg-opcion__titulo", it.titulo));
                if (it.descripcion) textos.appendChild(crear("span", "bg-opcion__desc", it.descripcion));
                var ir = crear("span", "bg-opcion__ir", "Abrir ↵");
                ir.setAttribute("aria-hidden", "true");
                op.appendChild(icono);
                op.appendChild(textos);
                op.appendChild(ir);
                bloque.appendChild(op);
                visibles.push(it);
            });
            lista.appendChild(bloque);
        });
        vacio.hidden = visibles.length > 0;
        estado.textContent = visibles.length ? visibles.length + (visibles.length === 1 ? " resultado" : " resultados") : "Sin resultados";
        marcar(visibles.length ? 0 : -1);
    }

    function marcar(n) {
        var previo = document.getElementById("bg-opcion-" + activo);
        if (previo) previo.setAttribute("aria-selected", "false");
        activo = n;
        var actual = document.getElementById("bg-opcion-" + n);
        if (actual) {
            actual.setAttribute("aria-selected", "true");
            entrada.setAttribute("aria-activedescendant", actual.id);
            actual.scrollIntoView({ block: "nearest" });
        } else {
            entrada.removeAttribute("aria-activedescendant");
        }
    }

    function ir(n) {
        var it = visibles[n];
        if (it && it.url) window.location.href = it.url;
    }

    function abrir() {
        if (dialogo.open) return;
        if (!indice.length) construirIndice();
        entrada.value = "";
        if (typeof dialogo.showModal === "function") dialogo.showModal(); else dialogo.setAttribute("open", "");
        disparador.setAttribute("aria-expanded", "true");
        pintar();
        entrada.focus();
    }

    function cerrar() {
        if (!dialogo.open) return;
        if (typeof dialogo.close === "function") dialogo.close(); else { dialogo.removeAttribute("open"); alCerrar(); }
    }

    function alCerrar() {
        disparador.setAttribute("aria-expanded", "false");
        disparador.focus();
    }

    function iniciar() {
        dialogo = document.getElementById("bgDialogo");
        disparador = document.getElementById("bgDisparador");
        if (!dialogo || !disparador) return;
        entrada = document.getElementById("bgEntrada");
        lista = document.getElementById("bgResultados");
        vacio = document.getElementById("bgVacio");
        estado = document.getElementById("bgEstado");
        disparador.setAttribute("aria-expanded", "false");

        disparador.addEventListener("click", abrir);
        dialogo.addEventListener("close", alCerrar);
        // Clic en el fondo (fuera de la caja): el evento llega con el <dialog> como destino
        dialogo.addEventListener("click", function (e) { if (e.target === dialogo) cerrar(); });

        entrada.addEventListener("input", pintar);
        entrada.addEventListener("keydown", function (e) {
            if (e.key === "ArrowDown" || e.key === "ArrowUp") {
                e.preventDefault();
                if (!visibles.length) return;
                var paso = e.key === "ArrowDown" ? 1 : -1;
                marcar((activo + paso + visibles.length) % visibles.length);
            } else if (e.key === "Enter") {
                e.preventDefault();
                ir(activo);
            }
        });

        lista.addEventListener("mousemove", function (e) {
            var op = e.target.closest(".bg-opcion");
            if (op && +op.getAttribute("data-indice") !== activo) marcar(+op.getAttribute("data-indice"));
        });
        lista.addEventListener("click", function (e) {
            var op = e.target.closest(".bg-opcion");
            if (op) ir(+op.getAttribute("data-indice"));
        });

        document.addEventListener("keydown", function (e) {
            if ((e.ctrlKey || e.metaKey) && !e.altKey && (e.key === "k" || e.key === "K")) {
                e.preventDefault();
                if (dialogo.open) cerrar(); else abrir();
            }
        });
    }

    if (document.readyState === "loading") document.addEventListener("DOMContentLoaded", iniciar); else iniciar();

    window.MatBuscador = { abrir: function () { abrir(); }, cerrar: function () { cerrar(); } };
})(window, document);
