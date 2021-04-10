// To set a function auto executable enclose with () and the second () is if need to receive parameters
(function () {
    window.Cookies = {
        /**
         * Get a cookie
         * @param {String} nombre name of the cookie to read
         * @returns {String} returns value or null if can't find
         */
        Get: function (nombre) {
            var nombreIgual = nombre + "=";
            var numeroCookies = document.cookie.split(';');
            for (var i = 0; i < numeroCookies.length; i++) { //Recorremos todas las cookies
                var valorCookie = numeroCookies[i]; //Analizamos la cookie actual
                while (valorCookie.charAt(0) === ' ') { valorCookie = valorCookie.substring(1, valorCookie.length); } //Eliminamos espacios
                if (valorCookie.indexOf(nombreIgual) === 0) {
                    return decodeURIComponent(valorCookie.substring(nombreIgual.length, valorCookie.length));
                } //Devolvemos el valor
            }
            return null; //Si numeroCookies es cero se devuelve null
        },
        /**
         * Set a Cookie
         * @param {String} name name
         * @param {String} value value
         * @param {number} days expires in x days
         * @param {String} path path
         */
        Set: function (name, value, days, path) {
            var expires;
            if (path === undefined) path = '/';
            if (days) {
                var date = new Date();
                date.setTime(date.getTime() + days * 24 * 60 * 60 * 1000);
                expires = "; expires=" + date.toGMTString();
            }
            else expires = "";
            document.cookie = name + "=" + encodeURIComponent(value) + expires + "; path=" + path;
        },
        /**
         * Delete a Cookie
         * @param {String} name Nombre de la cookie
         * @param {String} path ruta de la cookie
         */
        Del: function (name, path) {
            if (path === undefined) path = '/';
            document.cookie = name + "=''; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=" + path;
        },
        /**
         * Check if the cookie exits
         * @param {String} clave nombre de la cookie
         * @returns {Boolean} return true o false
         */
        Check: function (clave) {
            var miclave = $Cookie.Get(clave);
            if (miclave !== "" || miclave !== null) {
                return true;
            } else {
                return false;
            }
        }
    };
})();
