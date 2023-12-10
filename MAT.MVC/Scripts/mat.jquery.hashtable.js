var Hashtable = (function () {
    /// <summary>Definición de la clase Hashtable que representa un diccionario de datos. Se emplea para el diccionario del sistema.</summary>
    var _hashtable = new Array();

    this.put = function (key, newValue) {
        /// <summary>Agrega un par clave/valor.</summary>
        /// <param name="key" type="String">Clave</param>
        /// <param name="newValue" type="String">Valor a agregar.</param>
        this.removeByKey(key);
        _hashtable.push({ name: key, value: newValue });
    };

    this.get = function (key) {
        /// <summary>Devuelve un valor a partir de su clave.</summary>
        /// <param name="key" type="String">Clave de identificación.</param>
        /// <returns type="String" />
        for (var i = 0; i < _hashtable.length; i++) {
            if (_hashtable[i].name == key) {
                var toReturn = new String;
                toReturn = _hashtable[i].value.toString();
                toReturn = toReturn.replace(/\+/g, " "); //- Hack for UrlEncodings
                return toReturn;
            }
        }
        return null;
    };

    this.remove = function (key) {
        /// <summary>Remueve una clave.</summary>
        /// <param name="key" type="String">Clave de identificación.</param>
        this.removeByKey(key);
    }

    this.removeByKey = function (key) {
        /// <summary>Remueve un valor a partir de su clave.</summary>
        /// <param name="key" type="String">Clave de identificación.</param>
        for (var i = 0; i < _hashtable.length; i++) {
            if (_hashtable[i].name == key) {
                _hashtable.splice(i, 1);
                break;
            }
        }
    };

    this.containsKey = function (key) {
        /// <summary>Verifica si existe una clave.</summary>
        /// <param name="key" type="String">Clave de identificación.</param>
        /// <returns type="Boolean" />
        for (var i = 0; i < _hashtable.length; i++) {
            if (_hashtable[i].name == key) {
                return true;
            }
        }
        return false;
    };

    this.entries = function () {
        /// <summary>Devuelve todos los valores serializados.</summary>
        /// <returns type="Object" />
        var $fakeForm = $("<form />");
        for (var i = 0; i < _hashtable.length; i++) {
            var $fakeInput = $("<input />");
            $fakeInput.attr("type", "hidden");
            $fakeInput.attr("name", _hashtable[i].name);
            $fakeInput.attr("value", _hashtable[i].value);
            $fakeForm.append($fakeInput);
        }
        //-
        $(document.body).append($fakeForm);
        var returnValue = $fakeForm.serializeArray();
        $fakeForm.remove();
        //-
        return $.param(returnValue);
    };

    this.init = function (entries) {
        /// <summary>Inicializa el diccionario con valores predeterminados.</summary>
        /// <param name="entries" type="String">Colección de clave/valor.</param>
        var entries = unescape(entries);
        var elements = entries.split('&');
        //-
        var _self = this;
        $.each(elements, function () {
            var pairs = this.split('=');
            _self.put(pairs[0], unescape(pairs[1]));
        });
    };

});