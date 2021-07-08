// To set a function auto executable enclose with () and the second () is if need to receive parameters
(function () {
    window.$p = {
        /**
         * Load a javascript file in any time you need to load into the apge
         * @param {any} name name to identify the script
         * @param {any} scriptFile url with the javascript file
         */
        LoadJavascript: (name, scriptFile) => {
            let tag = document.getElementById(name);
            if (!tag) {
                tag = document.createElement('script');
                tag.id = name
                tag.src = scriptFile;
                var firstScriptTag = document.getElementsByTagName('script')[0];
                firstScriptTag.parentNode.insertBefore(tag, firstScriptTag);
            }
        },
        /**
         * Know when the page is full charge and can use all DOM objects
         * @param {any} CallBack function to execute when the page is full charge
         */
        Ready: function (CallBack) {
            /** Comprobar el estado de la pagina
             * Gracias a Jose Manuel Alarcon
             * http://www.jasoft.org/Blog/post/Detectar-que-la-pagina-actual-esta-lista.aspx
             */
            var tmpReady = setInterval(isPageFullyLoaded, 100);
            function isPageFullyLoaded() {
                try {
                    if (document.readyState === "complete") {
                        clearInterval(tmpReady);
                        if (CallBack !== undefined && CallBack !== null) ExternalFunc(CallBack, null);
                    }
                } catch (e) {
                    console.warn(e);
                }
            }
        },
        /**
         * Redirect to other url
         * @param {String} url url to redirect
         */
        Redirect: function (url) {
            window.location = url;
        },
        /**
         * Reload the page
         */
        Reload: function () {
            location.reload();
        },
        /**
         * Enable object
         * @param {String} idElemento selector or object
         */
        Enabled: function (idElemento) {
            $p.SetEnable(idElemento);
        },
        /**
         * Disable object
         * @param {String} idElemento selector or object
         */
        Disabled: function (idElemento) {
            $p.SetDisable(idElemento);
        },
        /**
         * Test if it's a variable or object and have a some value. Blank string return true.
         * @param {any} s variable a comprobar
         * @returns {boolean} retrun true or false
         */
        TestVar: function (s) {
            var resultado = false;
            if (s === undefined) resultado = false;
            else if (typeof s === 'undefined') resultado = false;
            else if (s === null) resultado = false;
            else resultado = true;
            return resultado;
        },
        /**
         * Send focus to the selector
         * @param {String} idElemento selector or object
         */
        Focus: function (idElemento) {
            try {
                $p.Element(idElemento).focus();
            } catch (e) {
                console.warn(e);
            }
        },
        /**
         * Insert a element aftert the other, not on the end
         * @param {any} e elemento padre
         * @param {any} i elemento a insertar
         */
        InsertAfter: function (e, i) {
            try {
                if (e.nextSibling) {
                    e.parentNode.insertBefore(i, e.nextSibling);
                } else {
                    e.parentNode.appendChild(i);
                }
            } catch (ex) {
                e.parentNode.appendChild(i);
            }
        },
        /**
         * Overwrites obj1's values with obj2's and adds obj2's if non existent in obj1
         * via: http://stackoverflow.com/questions/171251/how-can-i-merge-properties-of-two-javascript-objects-dynamically
         *
         * @param {object} obj1 origin
         * @param {object} obj2 new object to merge
         * @returns {object} obj3 a new object based on obj1 and obj2
         */
        MergeObjects: function (obj1, obj2) {
            var obj3 = {};
            for (var attrname in obj1) { obj3[attrname] = obj1[attrname]; }
            for (var attrname1 in obj2) { obj3[attrname1] = obj2[attrname1]; }
            return obj3;
        },
        /**
        * Control if have error on the images then show other error image
        * @param {String} urlErrorImage url for the image to use
        * @returns {void} no devuelve nada
        */
        ControlImgError: function (urlErrorImage) {
            if (urlErrorImage === null || urlErrorImage === undefined || urlErrorImage === '') urlErrorImage = '/img/nopicture.jpg';
            try {
                /*Sustituir las imagenes que no consigue cargar por una imagen de error*/
                var images = $p.GetByTag('img');
                for (var i = 0; i < images.length; i++) {
                    images[i].addEventListener('error', function () {
                        this.src = urlErrorImage;
                    });
                }
            } catch (e) {
                console.warn(e);
            }
        },
        /**
        * Get element from selector or DOM object
        * @param {any} element selector or object
        * @return {object} object
        */
        Element: function (element) {
            var elem;
            if (element !== null && element !== undefined && typeof element === 'string') {
                if (element.indexOf('#') >= 0 || element.indexOf('.') >= 0) elem = $p.GetBySel(element);
                else {
                    elem = $p.GetBySel(element);
                    if (elem === null || elem === undefined) elem = $p.GetById(element);
                }
            }
            else if (element !== null && element !== undefined) {
                try {
                    if (element.length > 0) elem = element[0];
                    else elem = element;
                } catch (e) {
                    elem = element;
                }
            }
            else elem = null;
            return elem;
        },
        /**
         * Get all the elements Ex: class, id, tag
         * @param {any} x id to search
         * @param {object} s object sender, document, window or any DOM element
         * @return {Array} object
         */
        Elements: function (x, s) {
            if (s !== null && s !== undefined && typeof s !== 'string') return s.querySelectorAll(x);
            else return document.querySelectorAll(x);
        },
        /**
         * Detect the browser in use. Return Object like:
         * Explorer: Name of the broser
         * Chrome: true or false
         * Firefox: true or false
         * Opera: true or false
         * MSIE: true or false
         * Safari: true or false
         * Edge: true or false
         * Trident: true or false
         * @returns {Object} devuelve objeto
         */
        GetBrowser: function () {
            var retorno = String('');
            var es_safari = navigator.userAgent.toLowerCase().indexOf('safari') > -1;
            if (es_safari) retorno = 'Safari';
            var es_chrome = navigator.userAgent.toLowerCase().indexOf('chrome') > -1;
            if (es_chrome) retorno = 'Chrome';
            var es_firefox = navigator.userAgent.toLowerCase().indexOf('firefox') > -1;
            if (es_firefox) retorno = 'Firefox';
            var es_opera = navigator.userAgent.toLowerCase().indexOf('opera') > -1;
            if (es_opera) retorno = 'Opera';
            var es_ie = navigator.userAgent.toLowerCase().indexOf('msie') > -1;
            if (es_ie) retorno = 'Explorer';
            var es_edge = navigator.userAgent.toLowerCase().indexOf('edge') > -1;
            if (es_edge) retorno = 'Edge';
            var es_trident = navigator.userAgent.toLowerCase().indexOf('trident') > -1;
            if (es_trident) retorno = 'Trident';
            if (retorno === '') retorno = navigator.userAgent;

            return { Explorer: retorno, MSIE: es_ie, Firefox: es_firefox, Chrome: es_chrome, Opera: es_opera, Safari: es_safari, Edge: es_edge, Trident: es_trident };
        },
        /**
        * Get element from the unique id
        * @param {string} x id to search
        * @param {object} s object sender, document, window or any DOM element
        * @return {object} object
        */
        GetById: function (x, s) {
            if (s !== null && s !== undefined && typeof s !== 'string') return s.getElementById(x);
            else return document.getElementById(x);
        },
        /**
         * Get first element Ex: class, id, tag
         * @param {any} x id to search
         * @param {object} s object sender, document, window or any DOM element
         * @return {object} object
         */
        GetBySel: function (x, s) {
            if (s !== null && s !== undefined && typeof s !== 'string') return s.querySelector(x);
            else return document.querySelector(x);
        },
        /**
        * Get all the elements from class name
        * @param {any} x id to search
        * @param {object} s object sender, document, window or any DOM element
        * @return {Array} object
        */
        GetByClass: function (x, s) {
            if (s !== null && s !== undefined && typeof s !== 'string') return s.getElementsByClassName(x);
            else return document.getElementsByClassName(x);
        },
        /**
         * Get all the elements from tag name
         * @param {string} x id to search
         * @param {object} s object sender, document, window or any DOM element
         * @return {Array} object
         */
        GetByTag: function (x, s) {
            // returns nodelist
            if (s !== null && s !== undefined && typeof s !== 'string') return s.getElementsByTagName(x);
            else return document.getElementsByTagName(x);
        },
        /**
         * Get Value from the element
         * @param {any} elemId selector object or DOM object
         * @returns {any} return valor
         */
        GetValue: function (elemId) {
            var retorno = String('');
            try {
                var elem = $p.Element(elemId);
                //check if it's a object or a string to get the element
                if (elem !== null) {
                    switch (elem.localName.toLowerCase()) {
                        case 'span':
                            /* innerHTML works in firefox, innerText works in others.*/
                            if ($p.GetBrowser().Firefox !== true) {
                                retorno = elem.innerText;
                            }
                            else {
                                retorno = elem.innerHTML;
                            }
                            break;
                        case 'div':
                        case 'label':
                            retorno = elem.innerHTML;
                            break;
                        default:
                            retorno = elem.value;
                    }
                }
                else return '';
            } catch (e) {
                retorno = '';
            }
            return retorno;
        },
        /**
         * Return date with format DD/MM/AAAA
         * @returns {String} devulve la fecha formato DD/MM/AAAA
         */
        GetDate: function () {
            var f = new Date();
            var fecha = f.getDate() + "/" + (f.getMonth() + 1) + "/" + f.getFullYear();
            return fecha;
        },
        /**
         * Returns Width and Height from the Window
         * @returns {Object} object {Width, Height}
         */
        GetWindowSize: function () {
            var myWidth = 0, myHeight = 0;
            if (typeof window.innerWidth === 'number') {
                /*No-IE */
                myWidth = window.innerWidth;
                myHeight = window.innerHeight;
            } else if (document.documentElement && (document.documentElement.clientWidth || document.documentElement.clientHeight)) {
                /*IE 6+ */
                myWidth = document.documentElement.clientWidth;
                myHeight = document.documentElement.clientHeight;
            } else if (document.body && (document.body.clientWidth || document.body.clientHeight)) {
                /*IE 4 compatible */
                myWidth = document.body.clientWidth;
                myHeight = document.body.clientHeight;
            } else if (typeof document.documentElement !== 'undefined' && typeof document.documentElement.clientWidth !== 'undefined' && document.documentElement.clientWidth !== 0) {
                /*Other IE compatible*/
                myWidth = document.documentElement.clientWidth;
                myHeight = document.documentElement.clientHeight;
            } else {
                /*other old*/
                myWidth = $p.GetByTag('body')[0].clientWidth;
                myHeight = $p.GetByTag('body')[0].clientHeight;
            }
            return { Width: myWidth, Height: myHeight };
        },
        /**
         * Get only all the numbers from the string
         * @param {string} txt texto alfanumerico
         * @returns {string} returns number like string
         */
        GetOnlyNumbers: function (txt) {
            return txt.replace(/\D/g, '');
        },
        /**
         * Get only letters from the string
         * @param {string} txt texto alfanumerico
         * @returns {string} return only letters
         */
        GetOnlyLetters: function (txt) {
            return txt.replace(/\W/g, '');
        },
        /**
         * Get X and Y exact for the element
         * Thanks to `meouw`: http://stackoverflow.com/a/442474/375966
         * @param {Object} el objeto
         * @returns {Object} devuelve objeto
         */
        GetOffsetAbs: function (el) {
            var el2 = el;
            var curleft = 0;
            var curtop = 0;

            if (document.getElementById || document.all) {
                do {
                    curleft += el.offsetLeft - el.scrollLeft;
                    curtop += el.offsetTop - el.scrollTop;
                    el = el.offsetParent;
                    el2 = el2.parentNode;
                    while (el2 !== el) {
                        curleft -= el2.scrollLeft;
                        curtop -= el2.scrollTop;
                        el2 = el2.parentNode;
                    }
                } while (el.offsetParent);

            } else if (document.layers) {
                curtop += el.y;
                curleft += el.x;
            }

            return { top: curtop, left: curleft };
        },
        /**
         * Get X e Y from the element
         * Thanks to `meouw`: http://stackoverflow.com/a/442474/375966
         * @param {Object} el objeto
         * @returns {Object} devuelve objeto
         */
        GetOffset: function (el) {
            var _x = 0;
            var _y = 0;
            var s = el.offsetParent;
            while (el && !isNaN(el.offsetLeft) && !isNaN(el.offsetTop)) {
                _x += el.offsetLeft - el.scrollLeft;
                _y += el.offsetTop - el.scrollTop;
                el = el.offsetParent;
            }

            return { top: _y, left: _x };
        },
        /**
         * Get the real id for the element in ASP pages.
         * @param {object} s DOM Object
         * @returns {object}  'Elementos': nombre, 'Id': id, 'Nombre': labelElemento
         */
        GetElementNameASP: function (s) {
            var nombre = s.id.split("_");
            var c = 0;
            c = nombre.length - 2;
            var labelElemento = '';
            for (var i = 0; i < c; i++) {
                labelElemento += nombre[i] + "_";
            }

            //saber el numero de elemento dentro de la lista
            var id = nombre[nombre.length - 1];

            return { 'Elementos': nombre, 'Id': id, 'Nombre': labelElemento };
        },
        /**
         * Get a value from the query string on the URL
         * @param {String} name Name of the value. Case sensitive
         * @return {String} null o string
         */
        GetQueryString: function (name) {
            name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
            var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
                results = regex.exec(location.search);
            return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
        },
        /**
        * Call web service URL pure JAVASCRIPT
        * @param {string} url ruta completa del webservice a llamar
        * @param {Function} success funcion a ejecutar. Siempre recibe data como parametro
        * @param {Function} error funcion a ejecutar en el error
        * @param {string} content content-type to send, default "application/json; charset=utf-8"
        */
        get: function (url, success, error, content) {
            _WebAPI('GET', url, success, error, content);
        },
        /**
         * Call web service URL pure JAVASCRIPT
         * @param {string} url ruta completa del webservice a llamar
         * @param {Function} success funcion a ejecutar. Siempre recibe data como parametro
         * @param {Function} error funcion a ejecutar en el error
         * @param {string} data datos a enviar
         * @param {string} content content-type to send, default "application/json; charset=utf-8"
         */
        post: function (url, success, error, data, content) {
            _WebAPI('POST', url, success, error, data, content);
        },
        /**
         * Load web page URL pure JAVASCRIPT
         * @param {string} url ruta completa del webservice a llamar
         * @param {any} target container use id or selector or DOM Object
         * @param {Function} success funcion a ejecutar. Siempre recibe data como parametro
         * @param {Function} error funcion a ejecutar en el error
         * @returns {void} almacena en el storageSession un item con el nombre ws_lr
         */
        load: function (url, target, success, error) {
            let myTarget = null;
            if (typeof target === 'string') {
                if (target.indexOf('#') >= 0 || target.indexOf('.') >= 0) myTarget = $p.GetBySel(target);
                else myTarget = $p.Element(target);
                if (myTarget !== null && myTarget !== undefined) {
                    //send the get action
                    $p.get(url, function (xhr) {
                        myTarget.innerHTML = xhr;
                        if (success !== undefined && success !== null) ExternalFunc(success, null);
                    }, function (ex) {
                        $p.MsgAlert(target, 'Error', 'Page cannot be load.', 5, true);
                        if (error !== undefined && error !== null) ExternalFunc(error, ex);
                    });
                }
                else console.warn(target + ' not found.', myTarget);
            }
            else throw new Error('Target should be a id or selector not a object or DOM object.', target);
        },
        /**
         * Delete a element from the document
         * @param {object} element DOM object to delete
         * @param {string} fadeClass animation effect css
         * @returns {boolean} return false if have error
         */
        Delete: function (element, fadeClass) {
            try {
                if (fadeClass === undefined || fadeClass === null) {
                    if ($p.GetBrowser().Trident) {
                        try {
                            document.body.removeChild(element);
                        } catch (e) {
                            element.style.display = 'none';
                        }
                    } else {
                        element.remove();
                    }
                }
                else {
                    element.classList.add(fadeClass);
                    setTimeout(function () {
                        if ($p.GetBrowser().Trident) {
                            try {
                                document.body.removeChild(element);
                            } catch (e) {
                                element.style.display = 'none';
                            }
                        } else {
                            element.remove();
                        }
                    }, 750);
                }
                return true;
            } catch (e) {
                return false;
            }
        },
        /**
         * Remove a event in all the elements inclusive in a new elements 
         * created dynamic with javascript on the same selector
         * @param {string} eventName event name
         * @param {string} selector selector to search
         * @param {function} handler call back to execute
         */
        DeteleEventAlways: function (eventName, selector, handler) {
            document.removeEventListener(
                eventName,
                function (event) {
                    let elements = $p.Elements(selector);
                    // if internet explorer use the polify composedPath() which is EventPath()
                    const path = $p.GetBrowser().Trident || $p.GetBrowser().Edge ? EventPath(event) : event.composedPath();
                    // if internet explorer slice the object into array
                    if ($p.GetBrowser().Trident) elements = Array.prototype.slice.call(elements);

                    path.forEach(function (node) {
                        elements.forEach(function (elem) {
                            if (node === elem) {
                                handler.call(elem, event);
                            }
                        });
                    });
                },
                true
            );
        },
        /**
        * Message box bootstrap.
        * @param {any} tarject selector object or DOM object
        * @param {any} title title to show
        * @param {any} message fecha a formatear
        * @param {any} timeout timeout to hide the div in seconds
        * @param {any} bulma indicates the message is for bulma css framework
        */
        MsgAlert: function (tarject, title, message, timeout, bulma) {
            var msg = new _Message();
            if (bulma) {
                let setup = {
                    Framework: 'bulma',
                    Title: title,
                    Message: message,
                    Class: 'is-Link',
                    Timeout: timeout,
                    Cleanup: false,
                    waiting: false
                };
                let msg = new _Message();
                msg.show(tarject, setup);
            }
            else msg.alert(tarject, title, message, timeout);
        },
        /**
        * Message box bootstrap.
        * @param {any} tarject selector object or DOM object
        * @param {any} title title to show
        * @param {any} message fecha a formatear
        * @param {any} timeout timeout to hide the div in seconds
        * @param {any} bulma indicates the message is for bulma css framework
        */
        MsgError: function (tarject, title, message, timeout, bulma) {
            var msg = new _Message();
            if (bulma) {
                let setup = {
                    Framework: 'bulma',
                    Title: title,
                    Message: message,
                    Class: 'is-danger',
                    Timeout: timeout,
                    Cleanup: false,
                    waiting: false
                };
                let msg = new _Message();
                msg.show(tarject, setup);
            }
            else msg.danger(tarject, title, message, timeout);
        },
        /**
        * Message box bootstrap.
        * @param {any} tarject selector object or DOM object
        * @param {any} title title to show
        * @param {any} message fecha a formatear
        * @param {any} timeout timeout to hide the div in seconds
        * @param {any} bulma indicates the message is for bulma css framework
        */
        MsgWarning: function (tarject, title, message, timeout, bulma) {
            var msg = new _Message();
            if (bulma) {
                let setup = {
                    Framework: 'bulma',
                    Title: title,
                    Message: message,
                    Class: 'is-warning',
                    Timeout: timeout,
                    Cleanup: false,
                    waiting: false
                };
                let msg = new _Message();
                msg.show(tarject, setup);
            }
            else msg.warning(tarject, title, message, timeout);
        },
        /**
        * Message box bootstrap. 
        * @param {any} tarject selector object or DOM object
        * @param {any} title title to show
        * @param {any} message fecha a formatear
        * @param {any} timeout timeout to hide the div in seconds
        * @param {any} bulma indicates the message is for bulma css framework
        */
        MsgInfo: function (tarject, title, message, timeout, bulma) {
            var msg = new _Message();
            if (bulma) {
                let setup = {
                    Framework: 'bulma',
                    Title: title,
                    Message: message,
                    Class: 'is-info',
                    Timeout: timeout,
                    Cleanup: false,
                    waiting: false
                };
                let msg = new _Message();
                msg.show(tarject, setup);
            }
            else msg.info(tarject, title, message, timeout);
        },
        /**
        * Message box bootstrap.
        * @param {any} tarject selector object or DOM object
        * @param {any} title title to show
        * @param {any} message fecha a formatear
        * @param {any} timeout timeout to hide the div in seconds
        * @param {any} bulma indicates the message is for bulma css framework
        */
        MsgSuccess: function (tarject, title, message, timeout, bulma) {
            var msg = new _Message();
            if (bulma) {
                let setup = {
                    Framework: 'bulma',
                    Title: title,
                    Message: message,
                    Class: 'is-success',
                    Timeout: timeout,
                    Cleanup: false,
                    waiting: false
                };
                let msg = new _Message();
                msg.show(tarject, setup);
            }
            else msg.success(tarject, title, message, timeout);
        },
        /**
        * Message box bootstrap. Class personalized
        * @param {any} tarject selector object or DOM object
        * @param {any} title title to show
        * @param {any} message fecha a formatear
        * @param {any} timeout timeout to hide the div in seconds
        * @param {any} cls only in other own class
        * @param {any} bulma indicates the message is for bulma css framework
        */
        MsgOther: function (tarject, title, message, timeout, cls, bulma) {
            var msg = new _Message();
            if (bulma) {
                let setup = {
                    Framework: 'bulma',
                    Title: title,
                    Message: message,
                    Class: 'is-Dark',
                    Timeout: timeout,
                    Cleanup: false,
                    waiting: false
                };
                let msg = new _Message();
                msg.show(tarject, setup);
            }
            else msg.other(tarject, title, message, timeout, cls);
        },
        /**
        * Message box bulma. After message show button with waiting spining.
        * Default Timeout 0. Never hide.
        * @param {any} tarject selector object or DOM object
        * @param {any} message message to show
        * @param {any} timeout timeout to hide the div in seconds
        */
        MsgWaiting: function (tarject, message, timeout) {
            if (timeout === undefined || timeout === null) timeout = 0;
            let setup = {
                Framework: 'bulma',
                Title: '',
                Message: message,
                Class: 'is-primary',
                Timeout: timeout,
                Cleanup: false,
                waiting: true
            };
            let msg = new _Message();
            msg.show(tarject, setup);
        },
        /**
        * Delete waiting spining.
        * @param {any} tarject selector object or DOM object content the waiting
        */
        MsgWaitingDelete: function (tarject) {
            let button = $p.Element(`${tarject} .button.is-loading`);
            if (button) {
                let message = button.parentElement.parentElement;
                $p.Delete(message);
            }
        },
        /**
        * Message box. You can use Bootsrap or Bulma framwork with this option.
        * Use is- or alert- plus class color. conf:
        * let setup = {
        *    Title: '',
        *    Message: '',
        *    Class: 'dark|primary|link|danger|info|success|warning|alert',
        *    Timeout: 0,  number in secconds
        *    Cleanup: false|true,
        *    Framework: 'bulma|bootsrap',
        *    waiting: true|false
        *  };
        * @param {any} tarject selector object or DOM object
        * @param {object} conf  setup = {Title: '',Message: '',Class: 'info',Timeout: 0,Cleanup: false};
        */
        MsgShow: function (tarject, conf) {
            var msg = new _Message();
            msg.show(tarject, conf);
        },
        /**
         * Open a page like a popup
         * @param {number} pagina URL
         * @param {number} width default 400
         * @param {number} height default 500
         * @param {number} top default 0
         * @param {number} left default 0
         * @param {string} id window id
         * @param {boolean} fullscreen id full screen unable Width and height
         * @param {boolean} scrollbars show scroll bars
         * @param {boolean} resizable enable resize
         * @param {boolean} toolbar show tool bar
         * @param {boolean} status show status bar
         * @returns {void} 
         */
        PopUpPage: function (pagina, width, height, top, left, id, fullscreen, scrollbars, resizable, toolbar, status) {
            try {
                if (!$p.isNum(width)) width = 400;
                if (!$p.isNum(height)) height = 500;
                if (width === 0) width = 400;
                if (height === 0) height = 500;
                if (!$p.isNum(top)) top = 0;
                if (!$p.isNum(left)) left = 0;
                if (isNaN(id)) id = '_blank';
                /*opciones*/
                if (isNaN(fullscreen)) fullscreen = false;
                if (isNaN(scrollbars)) scrollbars = false;
                if (isNaN(resizable)) resizable = false;
                if (isNaN(toolbar)) toolbar = false;
                if (isNaN(status)) status = false;
                var opciones = 'location=no, directories=no';
                if (fullscreen === false) {
                    opciones += ', width = ' + width + ', height=' + height + ', top=' + top + ', left=' + left;
                }
                else {
                    opciones += ', fullscreen=yes';
                }
                if (scrollbars === true) {
                    opciones = ', scrollbars = yes';
                }
                else {
                    opciones = ', scrollbars = no';
                }
                if (resizable === true) {
                    opciones = ', resizable = yes';
                }
                else {
                    opciones = ', resizable = no';
                }
                if (toolbar === true) {
                    opciones = ', toolbar = yes';
                }
                else {
                    opciones = ', toolbar = no';
                }
                if (status === true) {
                    opciones = ', status = yes';
                }
                else {
                    opciones = ', status = no';
                }
                window.open(pagina, id, opciones);
            } catch (e) {
                return e;
            }
        },
        /**
         * Create a double confirmation when click on a button
         * @param {any} selector selector to search the object or object
         * @param {any} options setup for the function
         */
        ConfirmationButton: function (selector, options) {
            var settings = {
                textConfirm: 'Sure?',
                textConfirmed: '<i class="fas fa-check"></i>',
                classConfirm: 'is-warning',
                classConfirmed: 'is-success',
                classStandby: 'is-danger',
                disableOnConfirm: false,
                cancelTime: 2000,
                onFirstClickBefore: function (btn) {
                    btn.innetHtml = settings.textConfirm;
                },
                onFirstClickAfter: null,
                onConfirm: null,
                allowFirstClick: true
            };
            settings = $p.MergeObjects(settings, options);
            var objects = $p.Elements(selector);
            for (var i = 0; i < objects.length; i++) {
                var btn = objects[i];
                btn.onclick = function () {
                    if ($p.ParseBoolean(this.dataset.clicked, false) === true) {
                        this.dataset.clicked = false;
                        this.innerHTML = settings.textConfirmed;
                        this.classList.remove(settings.classConfirm);
                        this.classList.add(settings.classConfirmed);
                        if (settings.disableOnConfirm) {
                            this.classList.add("disabled");
                            this.disabled = true;
                        }
                        if (settings.onConfirm !== null && settings.onConfirm !== undefined) ExternalFunc(settings.onConfirm, this);
                    }
                    else {
                        this.dataset.text = this.innerHTML;
                        ExternalFunc(settings.onFirstClickBefore, this);
                        if (settings.allowFirstClick) {
                            this.dataset.clicked = true;
                            this.innerHTML = settings.textConfirm;
                            this.classList.remove(settings.classStandby);
                            this.classList.add(settings.classConfirm);
                            setTimeout(function (me) {
                                if ($p.ParseBoolean(me.dataset.clicked, false) === true) {
                                    me.dataset.clicked = false;
                                    me.classList.remove(settings.classConfirm);
                                    me.classList.add(settings.classStandby);
                                    me.innerHTML = me.dataset.text;
                                }
                            }, settings.cancelTime, this);
                            if (settings.onFirstClickAfter !== null && settings.onFirstClickAfter !== undefined) ExternalFunc(settings.onFirstClickAfter, this);
                        }
                    }

                };
            }
        },
        /**
         * Set element value
         * @param {any} elemId selector object or DOM object
         * @param {String} text Value
         * @returns {void}
         */
        SetValue: function (elemId, text) {
            try {
                var elem = $p.Element(elemId);
                //check if it's a object or a string to get the element
                if (elem !== null) {
                    switch (elem.localName.toLowerCase()) {
                        case 'span':
                            /* innerHTML works in firefox, innerText works in others.*/
                            if ($p.GetBrowser().Firefox !== true)
                                elem.innerText = text;
                            else
                                elem.innerHTML = text;
                            break;
                        case 'div':
                        case 'label':
                        case 'p':
                            elem.innerHTML = text;
                            break;
                        default:
                            elem.value = text;
                    }
                }
                else return;
            } catch (e) {
                console.warn(e);
                return;
            }
        },
        /**
         * Set the checked on a check box or radio box
         * @param {any} s selector object or DOM object
         * @param {Boolean} v Value
         */
        SetChecked: function (s, v) {
            try {
                var check = $p.Element(s);
                var checked = $p.ParseBoolean(v);
                if (checked) {
                    check.setAttribute('checked', 'checked');      //check
                    check.value = true;
                    check.checked = true;
                }
                else {
                    check.checked = false;
                    check.removeAttribute('checked');                      //uncheck
                    check.value = false;
                }
            } catch (e) {
                console.error(e);
            }
        },
        /**
         * Enable element
         * @param {string} s selector object or DOM object
         */
        SetEnable: function (s) {
            try {
                var a = $p.Element(s);
                a.disabled = false;
                a.removeAttribute('disabled');
            } catch (e) {
                console.warn(e);
            }
        },
        /**
         * Unable element
         * @param {string} s selector object or DOM object
         */
        SetDisable: function (s) {
            try {
                var a = $p.Element(s);
                a.disabled = true;
                a.setAttribute('disabled', 'disabled');
            } catch (e) {
                console.warn(e);
            }
        },
        /**
         * Desplazamiento Vertical
         * @param {number} posY pixcels
         * @param {number} multipler multiplica
         * @returns {void} no devuelve nada
         */
        SetScrollY: function (posY, multipler) {
            if (multipler === null || multipler === undefined || multipler === '') multipler = 1;
            if ($p.GetBrowser().Chrome === true || $p.GetBrowser().Opera === true || $p.GetBrowser().Safari === true) {
                window.scrollBy(0, posY * multipler);
            }
            else {
                window.scrollTo(0, posY * multipler);
            }
        },
        /**
         * Desplazamiento Horizontal
         * @param {number} posX pixcels
         * @param {number} multipler multiplica
         */
        SetScrollX: function (posX, multipler) {
            if (multipler === null || multipler === undefined || multipler === '') multipler = 1;
            if ($p.GetBrowser().Chrome === true || $p.GetBrowser().Opera === true || $p.GetBrowser().Safari === true) {
                window.scrollBy(posX * multipler, 0);
            }
            else {
                window.scrollTo(posX * multipler, 0);
            }

        },
        /**
         * Desplazamiento vertical y horizontal
         * @param {number} posX pixcels
         * @param {number} posY pixcels
         * @param {number} multiplerX multiplica x
         * @param {number} multiplerY multiplica y
         */
        SetScrollXY: function (posX, posY, multiplerX, multiplerY) {
            if (multiplerX === null || multiplerX === undefined || multiplerX === '') multiplerX = 1;
            if (multiplerY === null || multiplerY === undefined || multiplerY === '') multiplerY = 1;
            if ($p.GetBrowser().Chrome === true || $p.GetBrowser().Opera === true || $p.GetBrowser().Safari === true) {
                window.scrollBy(posX * multiplerX, posY * multiplerY);
            }
            else {
                window.scrollTo(posX * multiplerX, posY * multiplerY);
            }
        },
        /**
         * Add event to the DOM. Compatibility with all browsers
         * @param {object} elemento selector object or DOM object
         * @param {string} evento nombre del evento a agregar
         * @param {object} funcion funcion a invocar
         */
        SetEvent: function (elemento, evento, funcion) {
            try {
                var elem = $p.Element(elemento);
                if (elem.addEventListener) {
                    elem.addEventListener(evento, funcion, false);
                } else {
                    elem.attachEvent("on" + evento, funcion);
                }
            } catch (e) {
                console.error(e);
            }
        },
        /**
         * Create a event in all the elements inclusive in a new elements 
         * created dynamic with javascript on the same selector
         * @param {string} eventName event name
         * @param {string} selector selector to search
         * @param {function} handler call back to execute
         */
        SetEventAlways: function (eventName, selector, handler) {
            document.addEventListener(
                eventName,
                function (event) {
                    let elements = $p.Elements(selector);
                    // if internet explorer use the polify composedPath() which is EventPath()
                    const path = $p.GetBrowser().Trident || $p.GetBrowser().Edge ? EventPath(event) : event.composedPath();
                    // if internet explorer slice the object into array
                    if ($p.GetBrowser().Trident) elements = Array.prototype.slice.call(elements);

                    path.forEach(function (node) {
                        elements.forEach(function (elem) {
                            if (node === elem) {
                                handler.call(elem, event);
                            }
                        });
                    });
                },
                true
            );
        },
        /**
         * cambiar la url de la pagina sin refrescar
         * @param {String} url direccion url
         */
        SetUrl: function (url, overwrite) {
            // Cambio el historial del navegador.
            let push;
            if (overwrite !== undefined && overwrite !== null && typeof overwrite === 'boolean') push = overwrite;
            else push = false;
            if (push) window.history.pushState('Object', 'Title', url);
            else window.history.replaceState('Object', 'Title', url);
        },
        /**
         * set the source on the image
         * @param {string} target selector object or DOM object
         * @param {string} src ruta de la imagen
         * @returns {Boolean} return false if have error
         */
        SetImgSrc: function (target, src) {
            try {
                if (src !== '') {
                    var img = $p.Element(target);
                    img.src = src;
                    try {
                        cerrar();
                        return true;
                    } catch (e) {
                        return true;
                    }
                }
                else return false;
            } catch (e) {
                console.warn(e);
                return false;
            }
        },
        /**
         * Change values between 2 elements
         * @param {any} sender sender's selector object or DOM object
         * @param {String} destination destination's selector object or DOM object
         */
        SwapValues: function (sender, destination) {
            $p.SetValue(destination, $p.GetValue(sender));
        },
        /**
         * Send the value from one check box to other checkbox
         * @param {any} sender selector object or DOM object
         * @param {String} target selector object or DOM object
         */
        ReplicateCheckBox: function (sender, target) {
            try {
                if ($p.IsChecked(sender) === true) $p.SetChecked(target, true);  /* para poner la marca*/
                else $p.SetChecked(target, false);  /* para poner la marca*/
            } catch (e) {
                console.warn(e);
            }
        },
        /**
         * Check if the element exist on the page
         * @param {String} elemId  selector object or DOM object
         * @returns {boolean} devuelve true o false
         */
        InPage: function (elemId) {
            try {
                var elem = $p.Element(elemId);
                if (elem === null || elem === undefined) { return false; } else { return true; }
            } catch (e) {
                return false;
            }
        },
        /**
         * Trigger the specified event on the specified element.
         * @param  {Object} elem  the target element.
         * @param  {String} event the type of the event (e.g. 'click').
         */
        TriggerEvent: function (elem, event) {
            try {
                var clickEvent = new Event(event); // Create the event.
                elem.dispatchEvent(clickEvent);    // Dispatch the event.
            } catch (e) {
                console.warn(e);
            }
        },
        /**
         * Open modal using bulma
         * @param {string} elemId id about the popup window
         */
        ModalOn: function (elemId) {
            var rootEl = document.documentElement;
            var $target = $p.GetById(elemId);
            rootEl.classList.add('is-clipped');
            $target.classList.add('is-active');
        },
        /**
         * Close the target modal
         * @param {string} elemId id about the popup window
         */
        ModalOff: function (elemId) {
            let rootEl = document.documentElement;
            let $el = this.GetById(elemId);
            rootEl.classList.remove('is-clipped');
            $el.classList.remove('is-active');
        },
        /**
         * Cambiar la visibilidad de un elemento
         * @param {any} elemId  selector object or DOM object
         * @param {string} className  selector object or DOM object
         * @returns {void} no devuelve nada
         */
        Visible: function (elemId, className) {
            try {
                var elem = $p.Element(elemId);
                if (className === undefined || className === null) className = 'inherit';
                var states = JSON.parse(sessionStorage.getItem('PropiosVisibleStates'));
                var pos = -1;
                if (states !== null && states !== undefined) {
                    if (states.length > 0) {
                        //check if the object is in the old states
                        for (var i = 0; i < states.length; i++) {
                            if (states[i].url === window.location.href) { //same page
                                if (states[i].name === elem.id) { //same object
                                    pos = i;
                                }
                            }
                        }
                    }
                }
                else {
                    states = [];
                }
                if (elem !== null && elem !== undefined) {
                    var state = {      //save the actual state for the button
                        url: window.location.href,
                        name: elem.id,
                        initial: elem.style.display
                    };
                    if (pos >= 0) {
                        if (elem.style.display !== 'none') {
                            elem.style.display = 'none';
                        }
                        else {
                            if (states[pos].initial === 'none') elem.style.display = className;
                            else elem.style.display = states[pos].initial;
                        }
                    }
                    else {
                        states.push(state);
                        sessionStorage.setItem('PropiosVisibleStates', JSON.stringify(states));
                        if (elem.style.display !== 'none') elem.style.display = 'none';
                        else elem.style.display = className;
                    }
                }
            } catch (e) {
                return false;
            }
        },
        /**
        * Know if the element is visible or not
        * @param {any} n  selector object or DOM object
        * @returns {boolean} devuelve true o false
        */
        IsVisible: function (n) {
            try {
                var elemId = $p.Element(n);
                if (elemId !== null && elemId !== undefined) {
                    if (elemId.style.display === 'none') {
                        return false;
                    }
                    else {
                        return true;
                    }
                }
                else return false;
            } catch (e) {
                return false;
            }
        },
        /**
         * Get the checked state
         * @param {anu} s selector object or DOM object
         * @returns {boolean} returns true o false
         */
        IsChecked: function (s) {
            try {
                var check = $p.Element(s);
                if (check.checked === true) return true;
                else {
                    var test = check.checked;
                    if (typeof test === 'string') {
                        if (check.checked.toLowerCase() === 'checked') return true;
                        else return $p.ParseBoolean(test, false);
                    }
                    else return false;
                }
            } catch (e) {
                return false;
            }
        },
        /**
         * Check if the url is found or not
         * IdActive.Then(callBack);
         * @param {string} address url a to check
         * @param {number} timehout max timeout to wait. Default 200
         * @return {boolean} boolean promise
         */
        IsUrlAlive: function (address, timehout) {
            if (timehout === undefined || timehout === null) timehout = 2000;
            return new Promise(function (resultado, error) {
                try {
                    var client = new XMLHttpRequest();
                    client.onreadystatechange = function () {
                        // in case of network errors this might not give reliable results
                        if (this.readyState === 4) {
                            if (this.status >= 200 || this.status < 300) resultado(true);
                            else resultado(false);
                        }
                    };
                    client.open("HEAD", address);
                    client.timeout = timehout;
                    client.send();
                } catch (e) {
                    error(e);
                }
            });
        },
        /**
         * Check if the text is a valid num
         * @param {string} s number to check
         * @returns {boolean} return true o false
         */
        isNum: function (s) {
            // NEW isNum function: 01/09/2010
            // Thanks to Emile Grau, GigaTecnologies S.L., www.gigatransfer.com, www.mygigamail.com
            // based on utility function isNum from xml2json plugin (http://www.fyneworks.com/ - diego@fyneworks.com)
            // few bugs corrected from original function :
            // - syntax error : regexp.test(string) instead of string.test(reg)
            // - regexp modified to accept  comma as decimal mark (latin syntax : 25,24 )
            // - regexp modified to reject if no number before decimal mark  : ".7" is not accepted
            // - string is "trimmed", allowing to accept space at the beginning and end of string
            var regexp = /^((-)?([0-9]+)(([\.\,]{0,1})([0-9]+))?$)/;
            try {
                return typeof s === "number" || regexp.test(String(s && typeof s === "string") ? s.trim() : '');
            } catch (e) {
                return false;
            }
        },
        /**
         * Check Email address
         * @param {String} email email
         * @returns {boolean}  return true o false
         */
        IsValidEmail: function (email) {
            var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
            if (regex.test(email)) return true;
            else return false;
        },
        /**
         * check url o ftp
         * @param {String} address url or ftp
         * @returns {boolean} devuelve true o false
         */
        IsValidUrl: function (address) {
            if (address === undefined || address === '' || address === null || typeof address !== 'string') return false;
            else {
                var pattern = /^(http|https|ftp)\:\/\/[a-z0-9\.-]+\.[a-z]{2,4}/gi;
                if (address.match(pattern)) return true;
                else return false;
            }
        },
        /**
         * Know is the number is decimal
         * @param {number} numero number to check
         * @returns {boolean} return true o false
         */
        IsDecimal: function (numero) {
            if ($p.isNum(numero)) {
                /*is a number*/
                if (numero % 1 === 0) return false;
                else return true;
            }
            else return false;
        },
        /**
         * Know if the number is odd or even
         * @param {number} numero number to check
         * @returns {boolean} truen true if odd
         */
        IsOdd: function (numero) {
            if ($p.isNum(numero)) {
                if (numero % 2 === 0) return false;
                else return true;
            }
            else return false;
        },
        /**
         * Select all text in a input[text] or input[textarea]
         * @param {object} s DOM Object Elemtent
         */
        SelectText: function (s) {
            try {
                var valor_input = s.value;
                var longitud = valor_input.length;

                if (s.setSelectionRange) {
                    s.focus();
                    s.setSelectionRange(0, longitud);
                }
                else {
                    if (s.createTextRange) {
                        var range = s.createTextRange();
                        range.collapse(true);
                        range.moveEnd('character', longitud);
                        range.moveStart('character', 0);
                        range.select();
                    }
                }
            } catch (e) {
                console.warn(e);
            }
        },
        /**
         * Search inside a table the rows with the text
         * @param {String} origin selector table or DOM object
         * @param {String} text Text to search
         * @param {String} className class to use for the table display
         * @returns {void} 
         */
        SearchInTable: function (origin, text, className) {
            try {
                var tableReg = $p.Element(origin);
                var searchText = text.toLowerCase();
                var cellsOfRow = '';
                var found = false;
                var compareWith = '';

                if (className === undefined || className === null || className === '') className = 'table-row';

                /* Recorremos todas las filas con contenido de la tabla*/
                for (var i = 1; i < tableReg.rows.length; i++) {
                    cellsOfRow = tableReg.rows[i].getElementsByTagName('td');
                    found = false;
                    /* Recorremos todas las celdas*/
                    for (var j = 0; j < cellsOfRow.length && !found; j++) {
                        compareWith = cellsOfRow[j].innerHTML.toLowerCase();
                        /* Buscamos el texto en el contenido de la celda*/
                        if (searchText.length === 0 || compareWith.indexOf(searchText) > -1) {
                            found = true;
                        }
                    }
                    if (found) {
                        tableReg.rows[i].style.display = className;
                    } else {
                        /* si no ha encontrado ninguna coincidencia, esconde la
                          fila de la tabla*/
                        tableReg.rows[i].style.display = 'none';
                    }
                }
            } catch (e) {
                console.warn(e);
            }
        },
        /**
         * Convert string into a decimal
         * @param {string} numero numero
         * @param {number} decimales decimal numbers to use. Default 2
         * @param {boolean} inCents default true. if true the number it's in cents and must be divided by 100
         * @returns {number} return decimal number
         */
        ConvertStringToDecimal: function (numero, decimales, inCents) {
            if (decimales === undefined) decimales = 2;
            if (inCents === undefined) inCents = true;
            if ($p.isNum(numero)) {
                /* is a number*/
                var num = 0.0;
                if (numero % 1 === 0) {
                    /* convert to decimal with cents*/
                    try {
                        if (inCents === true) num = parseFloat(numero / 100.0);
                        else num = parseFloat(numero / 1.0);
                    } catch (e) {
                        if (inCents === true) num = numero / 100.0;
                        else num = numero / 1.0;
                    }
                    num = parseFloat(Number(num).toFixed(decimales));
                }
                else {
                    num = parseFloat(Number(numero).toFixed(decimales));
                }
                return num * 1.0;
            }
            else {
                return $p.ConvertToDecimal($p.GetOnlyNumbers(numero), decimales, true);
            }
        },
        /**
         * Convert text into a integer
         * @param {string} numero number
         * @returns {number} return int number
         */
        ConvertStringToInteger: function (numero) {
            if ($p.isNum(numero)) {
                var num;
                try {
                    num = parseInt(Math.round(numero));
                } catch (e) {
                    num = Math.round(numero);
                }
                return num * 1;
            }
            else {
                return $p.ConvertToInteger($p.GetOnlyNumbers(numero), true);
            }
        },
        /**
         * Convert to decimal number
         * @param {any} numero number
         * @param {boolean} inCents Default true. If true the number must be divided by 100
         * @returns {number}  return decimal number
         */
        ConvertToDecimal: function (numero, inCents) {
            if (inCents === undefined) inCents = true;
            if ($p.isNum(numero)) {
                /* is a number */
                var num;
                if (numero % 1 === 0) {
                    /* convert to decimal with cents*/
                    try {
                        if (inCents === true) num = parseFloat(numero / 100.0);
                        else num = parseFloat(numero / 1.0);
                    } catch (e) {
                        if (inCents === true) num = numero / 100.0;
                        else num = numero / 1.0;
                    }
                }
                else {
                    num = numero / 1.0;
                }
                return num * 1.0;
            }
            else {
                return numero;
            }
        },
        /**
         * Convert a currency to number, decimal or integer
         * @param {string} txt number
         * @returns {number}  return decimal number
         */
        ConvertCurrencyToNumber: function (txt) {
            try {
                let number = txt.split(',');
                let punto = txt.indexOf('.');
                let coma = txt.indexOf(',');
                if (number.length > 1) {
                    //la coma era separador de miles, quitamos la coma
                    if (punto < 0) {
                        //no hay punto ver cuantas veces hay coma
                        if (number.length > 2) {
                            //la coma era separador de miles quitarla
                            txt = txt.replace(/,/g, '');
                        }
                        else {
                            //no hay punto decimal, problema, solo funciona para 1,000 (mil)
                            if (number[1].length === 3) {
                                //separaba miles quitamos la coma
                                txt = txt.replace(/,/g, '');
                            }
                            else {
                                //separaba decimales, cambiamos coma por punto
                                txt = txt.replace(/,/g, '.');
                            }
                        }
                    }
                    else {
                        //hay punto decimal, comprobamos quien estaba primero
                        if (punto < coma) {
                            //la coma era el decimal, intercambiamos la coma por el punto
                            txt = txt.replace(/./g, '');        //quitamos puntos
                            txt = txt.replace(/,/g, '.');        //cambiamos coma por punto
                        }
                        else {
                            //la coma era separador de miles, la quitamos
                            txt = txt.replace(/,/g, '');
                        }
                    }
                }
                else {
                    //solo hay una coma, comprobamos que no hay punto
                    if (punto < 0) {
                        //no hay punto decimal, problema, solo funciona para 1,000 (mil)
                        if (txt.length === 3) {
                            //separaba miles quitamos la coma
                            txt = txt.replace(/,/g, '');
                        }
                        else {
                            //separaba decimales, cambiamos coma por punto
                            txt = txt.replace(/,/g, '.');
                        }
                    }
                    else {
                        //hay punto decimal, comprobamos quien estaba primero
                        if (punto < coma) {
                            //la coma era el decimal, intercambiamos la coma por el punto
                            txt = txt.replace(/./g, '');        //quitamos puntos
                            txt = txt.replace(/,/g, '.');        //cambiamos coma por punto
                        }
                        else {
                            //la coma era separador de miles, la quitamos
                            txt = txt.replace(/,/g, '');
                        }
                    }
                }

                let num = Number(txt.replace(/[^0-9.-]+/g, ""));
                return num * 1.0;
            } catch (e) {
                console.warn('txt: ' + txt, e);
                return txt;         //is number already
            }
        },
        /**
         * Converto number into a integer
         * @param {number} numero numero
         * @param {boolean} inCents Default true. If true the number must be divided by 100
         * @returns {number} return integer number
         */
        ConvertToInteger: function (numero, inCents) {
            if (inCents === undefined) inCents = true;
            if ($p.isNum(numero)) {
                /*is a number*/
                var num;
                if (numero % 1 !== 0) {
                    try {
                        if (inCents === true) num = parseInt(Math.round(numero * 100));
                        else num = parseInt(Math.round(numero * 1));
                    } catch (e) {
                        if (inCents === true) num = numero * 100;
                        else num = numero * 1;
                    }
                }
                else {
                    try {
                        if (inCents === true) num = parseInt(Math.round(numero * 100));
                        else num = parseInt(Math.round(numero * 1));
                    } catch (e) {
                        if (inCents === true) num = numero * 100;
                        else num = numero * 1;
                    }
                }
                return num * 1;
            }
            else {
                return numero;
            }
        },
        /**
         * Parses values into booleans. 
         * @param  {any} value variable a comprobar
         * @return {Boolean} return true or false
         */
        ParseBoolean: function (value) {
            //https://stackoverflow.com/questions/263965/how-can-i-convert-a-string-to-boolean-in-javascript
            if (typeof value === 'string') {
                value = value.trim().toLowerCase();
            }
            switch (value) {
                case true:
                case 'true':
                case 1:
                case '1':
                case 'on':
                case 'yes':
                case 'ok':
                case 'si':
                    value = true;
                    break;
                default:
                    value = false;
                    break;
            }
            return value;
        },
        /**
         * Suportted Youtube URL and Vimeo URL
         * @param {URL} url ruta del video
         * @returns {Object} devuelve un objeto conlos datos del video
         */
        ParseVideo: function (url) {
            /* - Supported YouTube URL formats:
            //   - http://www.youtube.com/watch?v=My2FRPA3Gf8
            //   - http://youtu.be/My2FRPA3Gf8
            //   - https://youtube.googleapis.com/v/My2FRPA3Gf8
            // - Supported Vimeo URL formats:
            //   - http://vimeo.com/25451551
            //   - http://player.vimeo.com/video/25451551
            // - Also supports relative URLs:
            //   - //player.vimeo.com/video/25451551
            */
            url.match(/(http:|https:|)\/\/(player.|www.)?(vimeo\.com|youtu(be\.com|\.be|be\.googleapis\.com))\/(video\/|embed\/|watch\?v=|v\/)?([A-Za-z0-9._%-]*)(\&\S+)?/);
            var type = '';
            if (RegExp.$3.indexOf('youtu') > -1) {
                type = 'youtube';
            } else if (RegExp.$3.indexOf('vimeo') > -1) {
                type = 'vimeo';
            }

            return {
                type: type,
                id: RegExp.$6
            };
        },
        /**
         * Check if the text have HTML tags
         * @param {any} text test to test
         * @returns {boolean} tur or false
         */
        ContainsHtmlTag: function (text) {
            let tags = "a|abbr|acronym|address|area|b|base|bdo|big|blockquote|body|br|button|caption|cite|code|col|colgroup|dd|del|dfn|div|dl|DOCTYPE|dt|em|fieldset|form|h1|h2|h3|h4|h5|h6|head|html|hr|i|img|input|ins|kbd|label|legend|li|link|map|meta|noscript|object|ol|optgroup|option|p|param|pre|q|samp|script|select|small|span|strong|style|sub|sup|table|tbody|td|textarea|tfoot|th|thead|title|tr|tt|ul|var|header|article|footer".split('|');
            let retorno = false;
            let tag = '';
            let pattern = '';
            let c = 0;
            let texto = text.toLowerCase();
            do {
                tag = tags[c];
                pattern = new RegExp('<\s*' + tag + '\s*\/?>', 'g');
                retorno = pattern.test(texto);
                c++;
            } while (c < tags.length && retorno === false);
            return retorno;
        },
        /**
         * Only allow on the inputbox or textarea the characters setup
         * Only numbers <input type='text' id='texto' onkeypress='return $p.InputRuleset(event, 'num')' />
         * Only letters <input type='text' id='texto' onkeypress='return $p.InputRuleset(event, 'car')' />
         * Only numbers and letters <input type='text' id='texto' onkeypress='return $p.InputRuleset(event, 'num_car')' />
         * @param {Object} elEvento event sender
         * @param {String} permitidos must be select > num: numbers, car: letters, num_car: numbers and letters
         * @param {String} adicionales special characters is allowed also
         * @returns {boolean} return true o false
         */
        InputRuleset: function (elEvento, permitidos, adicionales) {
            /* Variables que definen los caracteres permitidos*/
            var numeros = '0123456789';
            var caracteres = ' abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ';
            var numeros_caracteres = numeros + caracteres;
            var teclas_especiales = [8, 13, 37, 39];
            /* 8 = BackSpace, 13 =  intro,  37 = flecha izquierda, 39 = flecha derecha
              Seleccionar los caracteres a partir del parámetro de la función*/
            switch (permitidos) {
                case 'num':
                    permitidos = numeros;
                    break;
                case 'car':
                    permitidos = caracteres;
                    break;
                case 'num_car':
                    permitidos = numeros_caracteres;
                    break;
            }
            if ($p.TestVar(adicionales) === true) {
                permitidos += adicionales;
            }
            /* Obtener la tecla pulsada */
            var evento = elEvento || window.event;
            var codigoCaracter = evento.charCode || evento.keyCode;
            if (codigoCaracter > 95 && codigoCaracter < 106) codigoCaracter = codigoCaracter - 48;
            var caracter = String.fromCharCode(codigoCaracter);
            /* Comprobar si la tecla pulsada es alguna de las teclas especiales
              (teclas de borrado y flechas horizontales)*/
            var tecla_especial = false;
            for (var i in teclas_especiales) {
                if (codigoCaracter === teclas_especiales[i]) {
                    tecla_especial = true;
                    break;
                }
            }
            if (codigoCaracter === 9) return true;
            else return permitidos.indexOf(caracter) !== -1 || tecla_especial;
        },
        /**
         * Blanck Spaces is not allowed on the inputbox or textarea
         * @param {string} sender  selector object or DOM object
         * @param {string} error not requiered. selector object or DOM object where show the error message
         * @returns {boolean} boolean
         */
        InputNotSpaces: function (sender, error) {
            var er = new RegExp(/\s/);
            var web = $p.Element(sender).value;
            if (er.test(web)) {
                if (error !== null && error !== undefined && error || '') {
                    $.MsgWarning(error, '<i class="fa fa-wheelchair" aria-hidden="true"></i>', 'Spaces are not allowed.', null, true);
                }
                return false;
            }
            else
                return true;
        },
        /**
        * Remove any spaces on the text
        * @param {string} text text to clean
        * @return {string} return clean string
        */
        StringRemoveSpaces: function (text) {
            if (typeof text === 'string') return text.replace(/\s/g, '');
            else return text;
        },
        /**
         * Return a string with the number formated
         * @param {Number} amount numero a formatear
         * @param {Number} decimals numero de decimales
         * @return {Number} devuelve el numero formateado a los decimales enviados
         */
        FormatNumber: function (amount, decimals) {
            //https://gist.github.com/jrobinsonc/5718959

            var sign = amount.toString().substring(0, 1) === "-";          //aportacion ceshuete

            amount += ''; // por si pasan un numero en vez de un string
            amount = parseFloat(amount.replace(/[^0-9\.]/g, '')); // elimino cualquier cosa que no sea numero o punto

            var dec = decimals || 2; // por si la variable no fue fue pasada por defecto 2 decimales

            // si no es un numero o es igual a cero retorno el mismo cero
            if (isNaN(amount) || amount === 0)
                return parseFloat(0).toFixed(dec);

            // si es mayor o menor que cero retorno el valor formateado como numero
            amount = '' + amount.toFixed(dec);

            var amount_parts = amount.split('.'), regexp = /(\d+)(\d{3})/;

            while (regexp.test(amount_parts[0]))
                amount_parts[0] = amount_parts[0].replace(regexp, '$1' + ',' + '$2');

            return sign ? '-' + amount_parts.join('.') : amount_parts.join('.');
        },
        /**
         * Set a format for a date
         * @param {any} fecha date
         * @param {any} locale format to use: en-AU, es-ES
         * @return {string} return string
         */
        FormatDate: function (fecha, locale) {
            var date = new Date(fecha);
            var options = {
                weekday: "long", year: "numeric", month: "long",
                day: "numeric", hour: "2-digit", minute: "2-digit"
            };
            return date.toLocaleDateString(locale, options);
        },
        /**
         * Add a day
         * @param {any} fecha date
         * @param {any} locale format to use: en-AU, es-ES
         * @return {string} return string
         */
        AddDays: function (days, selectedDate) {
            if (selectedDate === null || selectedDate === undefined) selectedDate = undefined;
            Date.prototype.addDays = function (days) {
                var date = new Date(this.valueOf());
                date.setDate(date.getDate() + days);
                return date;
            }

            let date;
            if (selectedDate) date = new Date(selectedDate);
            else date = new Date();

            date = date.addDays(days);
            let formats = {
                "fr-CA": "yyyy-MM-dd",
            };
            return date.toLocaleDateString('fr-CA', formats);
        }
    };

    try {
        /*Sustituir las imagenes que no consigue cargar por una imagen de error*/
        $p.Ready(function () {
            var images = $p.GetByTag('img');
            for (var i = 0; i < images.length; i++) {
                images[i].addEventListener('error', function () {
                    this.src = '/img/nopicture.jpg';
                });
            }
        });
    } catch (e) {
        console.warn(e);
    }


    /**
     * Selecciona solo un radio dentro de un grupo de radios
     * @param {Object} s objeto donde se hace click
     * @returns {void} no devuelve nada
     */
    function SelectRadio(s) {
        var elemento = $p.Element('#' + s.id).parent().parent();
        var elementos = $p.Elements("input[type=radio]", elemento);
        //desmarcar todos los radio buttons
        for (var i = 0; i < elementos.length; i++) {
            try {
                elementos[i].checked = false;
            } catch (e) {
                return e;
            }
        }
        //marcar el seleccionado
        var Type;
        var selElemento = $p.Elements("input[type=radio]", $p.Element('#' + s.id));
        try {
            Type = selElemento[0].type.toLowerCase();
            selElemento[0].checked = true;
        } catch (e) {
            Type = '';
        }
        return Type;
    }

    /**
     * Enviar un mensaje al usuario utilizando la ventana personalizada
     * @param {String} titulo titulo de la ventana
     * @param {String} texto mensaje a mostrar
     */
    function sms(titulo, texto) {
        MyConfirm({
            'title': titulo,
            'message': texto,
            'buttons': {
                'OK': {
                    'class': 'blue',     /*'gray' o la clase que designe en la hora de estilos para el boton*/
                    'action': function () { return true; }
                }
            }
        });
    }

    /**
     * Mostrar un aviso por pantalla:
     * OnClientClick='javascript:if(!pregunta(sms))return false'
     * @param {String} sms Text to show
     * @returns {boolean} devuelve true o false
     */
    function pregunta(sms) {
        if (confirm(sms)) return true;
        else return false;
    }

    /**
     * comprobar que un valor es numerico
     * @param {String} numero numero a comprobar
     * @returns {boolean} devuelve true o false
     */
    function esNumero(numero) {
        return !isNaN(numero);
    }

    /**
     * Create pretty confirmacion box. Always return false. Need the function for true or false option or any acction in the buttons
                MyConfirm({
                    'title': 'Titulo',
                    'message': 'Mensaje',
                    'buttons': {
                        'THANKS': {
                            'class': 'blue',     //'gray' o la clase que designe en la hora de estilos para el boton
                            'action': function () { return true; }
                        }
                    }
                });
     * @param {object} params title, message, buttons { buttonText { class, action [function to execute] } }
     * @returns {void} no devuelve nada
     */
    function MyConfirm(params) { /*http: www.ajaxshake.com/plugin/ES/666/cb8f6825/cuadro-de-confirmacion-jquery-confirmdialog.html*/
        //comprbar que no existe ya una ventana con el mismo nombre
        var miDiv = $p.GetById('confirmOverlay');
        if (miDiv) {
            $p.Delete(miDiv);
        }

        if (params === undefined) {
            params = {
                'title': 'Title',
                'message': 'Message',
                'buttons': {
                    'OK': {
                        'class': 'blue',
                        'action': function () { }	/* Nothing to do in this case. You can as well omit the action property.*/
                    }
                }
            };
        }

        if ($p.InPage('confirmOverlay')) {
            /* A confirm is already shown on the page:*/
            return false;
        }

        var buttonHTML = '';

        Object.keys(params.buttons).forEach(function (key) {
            let button = params.buttons[key];
            buttonHTML += '<a class="button ' + button['class'] + '" style="cursor: pointer">' + key + '</a>';
            if (!button.action) {
                button.action = function () { };
            }
        });


        var css = "<style>#confirmOverlay {width: 100%;height: 100%;position: fixed;top: 0;left: 0;background: url('/js/ie.png');background: -moz-linear-gradient(rgba(11, 11, 11, 0.1), rgba(11, 11, 11, 0.6)) repeat-x rgba(11, 11, 11, 0.2);background: -webkit-gradient(linear, 0% 0%, 0% 100%, from(rgba(11, 11, 11, 0.1)), to(rgba(11, 11, 11, 0.6))) repeat-x rgba(11, 11, 11, 0.2);z-index: 100000;} " +
            "#confirmBox {background: url('/js/body_bg.jpg') repeat-x left bottom #e5e5e5;position: fixed;margin: 10px;border: 1px solid rgba(33, 33, 33, 0.6);-moz-box-shadow: 0 0 2px rgba(255, 255, 255, 0.6) inset;-webkit-box-shadow: 0 0 2px rgba(255, 255, 255, 0.6) inset;box-shadow: 0 0 2px rgba(255, 255, 255, 0.6) inset;} " +
            "#confirmBox h1, #confirmBox h2 {margin: 0;padding: 3px;text-shadow: 1px 1px 0 rgba(255, 255, 255, 0.6);letter-spacing: 0.4px;text-align: center;} " +
            "#confirmBox h1 {font: 36px 'Cuprum', 'Lucida Sans Unicode', 'Lucida Grande', sans-serif;color: #353535;} " +
            "#confirmBox h1.title {text-transform: uppercase;padding: 20px;background: url('/js/header_bg.jpg') repeat-x left bottom #f5f5f5;} " +
            "#confirmBox h2 {font: 26px 'Cuprum', 'Lucida Sans Unicode', 'Lucida Grande', sans-serif;color: #666;} " +
            "#confirmBox h3, #confirmBox h4, #confirmBox h5, #confirmBox h6 {text-align: center;letter-spacing: 0.3px;text-align: center;color: #888;} " +
            "#confirmBox p {overflow: auto;max-height: 350px;background: none;font-size: 16px;line-height: 1.4;padding: 5px 15px 5px 15px;text-shadow: 1px 1px 0 rgba(255, 255, 255, 0.6);color: #555;} " +
            "#confirmButtons {padding: 15px 15px 25px;text-align: center;} " +
            "#confirmBox .button {display: inline-block;position: relative;height: 33px;width: 200px;font: 17px/33px 'Cuprum', 'Lucida Sans Unicode', 'Lucida Grande', sans-serif;margin-right: 15px;text-decoration: none;border: none;margin-top: 3px;} " +
            "#confirmBox .blue {background: url('/js/buttons.png') no-repeat;color: white;background-position: left top;text-shadow: 1px 1px 0 #5889a2;} " +
            "#confirmBox .blue:hover {background-position: left bottom;} " +
            "#confirmBox .gray {background: url('/js/buttons.png') no-repeat;color: white;background-position: -200px top;text-shadow: 1px 1px 0 #707070;} " +
            "#confirmBox .gray:hover {background-position: -200px bottom;}</style> ";

        let markup = document.createElement('div');
        markup.id = 'confirmOverlay';
        markup.innerHTML = css +
            '<div id="confirmBox">' +
            '<h1 class="title">' + params.title + '</h1>' +
            '<p>' + params.message + '</p>' +
            '<div id="confirmButtons">' +
            buttonHTML +
            '</div></div>';

        $p.Element('body').appendChild(markup);

        var buttons = $p.Elements('#confirmBox .button'),
            i = 0;

        Object.keys(params.buttons).forEach(function (key) {
            let obj = params.buttons[key];
            $p.SetEvent(buttons[i], 'click', function () {
                /* Calling the action attribute when a
                  click occurs, and hiding the confirm.*/
                $p.Delete($p.Element('#confirmOverlay'));
                if (obj.action) {
                    ExternalFunc(obj.action);
                }
            });
            i++;
        });
    }

    /**
     * Ejecutar una funcion externa
     * @param {any} func nombre de la funcion
     * @param {any} data variables de la funcion
     */
    function ExternalFunc(func, data) {
        if (data === undefined) data = null;
        if (typeof func === 'string') eval(func, data);
        if (typeof func === 'function') func(data);
        else return;
    }

    /**
     * Activar pantalla completa
     * @param {object} element elemento a poner en pantalla completa. Usar 'document.documentElement' para todo el documento
     */
    function launchFullScreen(element) {
        /* Encuentra el método correcto, llama al elemento correcto*/
        if (element.requestFullScreen) {
            element.requestFullScreen();
        } else if (element.mozRequestFullScreen) {
            element.mozRequestFullScreen();
        } else if (element.webkitRequestFullScreen) {
            element.webkitRequestFullScreen();
        }
    }

    /**
     * Cambiar la pantalla completa
     */
    function cancelFullscreen() {
        if (document.cancelFullScreen) {
            document.cancelFullScreen();
        } else if (document.mozCancelFullScreen) {
            document.mozCancelFullScreen();
        } else if (document.webkitCancelFullScreen) {
            document.webkitCancelFullScreen();
        }
    }

    /**
     * Send Tracking to Google Analitics
     * @param {string} category nombre de la categoria
     * @param {string} action nomrbe de la accion
     * @param {string} description descripcion 
     * @param {number} valor valor a enviar
     * @returns {boolean} boolean
     */
    function SendGA(category, action, description, valor) {
        if ($p.TestVar(category) === false) category = 'No Category';
        if ($p.TestVar(action) === false) action = 'No Action';
        if ($p.TestVar(description) === false) description = '';
        if ($p.TestVar(valor) === false) valor = 0;

        try {
            ga('send', {
                hitType: 'event',
                eventCategory: category,
                eventAction: action,
                eventLabel: description,
                eventValue: valor
            });
            return true;
        } catch (e) {
            return false;
        }
    }

    /**
     * Polify function of composedPath
     * @param {any} evt event
     * @returns {array} array of events
     */
    function EventPath(evt) {
        var path = evt.composedPath && evt.composedPath() || evt.path, target = evt.target;

        if (path) {
            // Safari doesn't include Window, and it should.
            path = path.indexOf(window) < 0 ? path.concat([window]) : path;
            return path;
        }

        if (target === window) {
            return [window];
        }

        function getParents(node, memo) {
            memo = memo || [];
            var parentNode = node.parentNode;

            if (!parentNode) {
                return memo;
            }
            else {
                return getParents(parentNode, memo.concat([parentNode]));
            }
        }

        return [target]
            .concat(getParents(target))
            .concat([window]);
    }

    /**
     * show loading
     */
    function cargando() {
        try {
            loadingJS.show();
        } catch (e) {
            console.error(e);
        }
    }
    /**
     * hide loading
     */
    function cerrar() {
        try {
            loadingJS.hide();
        } catch (e) {
            console.error(e);
        }
    }


    /**
     * Selecciona solo un radio dentro de un grupo de radios
     * @param {Object} s objeto donde se hace click
     * @returns {void} no devuelve nada
     */
    function SelectRadio(s) {
        var elemento = $p.Element('#' + s.id).parent().parent();
        var elementos = $p.Elements("input[type=radio]", elemento);
        //desmarcar todos los radio buttons
        for (var i = 0; i < elementos.length; i++) {
            try {
                elementos[i].checked = false;
            } catch (e) {
                return e;
            }
        }
        //marcar el seleccionado
        var Type;
        var selElemento = $p.Elements("input[type=radio]", $p.Element('#' + s.id));
        try {
            Type = selElemento[0].type.toLowerCase();
            selElemento[0].checked = true;
        } catch (e) {
            Type = '';
        }
        return Type;
    }


    /**
     * Enviar un mensaje al usuario utilizando la ventana personalizada
     * @param {String} titulo titulo de la ventana
     * @param {String} texto mensaje a mostrar
     */
    function sms(titulo, texto) {
        MyConfirm({
            'title': titulo,
            'message': texto,
            'buttons': {
                'OK': {
                    'class': 'blue',     /*'gray' o la clase que designe en la hora de estilos para el boton*/
                    'action': function () { return true; }
                }
            }
        });
    }


    /**
     * Mostrar un aviso por pantalla:
     * OnClientClick='javascript:if(!pregunta(sms))return false'
     * @param {String} sms Text to show
     * @returns {boolean} devuelve true o false
     */
    function pregunta(sms) {
        if (confirm(sms)) return true;
        else return false;
    }

    /**
     * comprobar que un valor es numerico
     * @param {String} numero numero a comprobar
     * @returns {boolean} devuelve true o false
     */
    function esNumero(numero) {
        return !isNaN(numero);
    }

    /**
     * Create pretty confirmacion box. Always return false. Need the function for true or false option or any acction in the buttons
                MyConfirm({
                    'title': 'Titulo',
                    'message': 'Mensaje',
                    'buttons': {
                        'THANKS': {
                            'class': 'blue',     //'gray' o la clase que designe en la hora de estilos para el boton
                            'action': function () { return true; }
                        }
                    }
                });
     * @param {object} params title, message, buttons { buttonText { class, action [function to execute] } }
     * @returns {void} no devuelve nada
     */
    function MyConfirm(params) { /*http: www.ajaxshake.com/plugin/ES/666/cb8f6825/cuadro-de-confirmacion-jquery-confirmdialog.html*/
        //comprbar que no existe ya una ventana con el mismo nombre
        var miDiv = $p.GetById('confirmOverlay');
        if (miDiv) {
            $p.Delete(miDiv);
        }

        if (params === undefined) {
            params = {
                'title': 'Title',
                'message': 'Message',
                'buttons': {
                    'OK': {
                        'class': 'blue',
                        'action': function () { }	/* Nothing to do in this case. You can as well omit the action property.*/
                    }
                }
            };
        }

        if ($p.InPage('confirmOverlay')) {
            /* A confirm is already shown on the page:*/
            return false;
        }

        var buttonHTML = '';

        Object.keys(params.buttons).forEach(function (key) {
            let button = params.buttons[key];
            buttonHTML += '<a class="button ' + button['class'] + '" style="cursor: pointer">' + (key ? key.replace(/_/g, ' ') : '') + '</a>';
            if (!button.action) {
                button.action = function () { };
            }
        });


        var css = "<style>#confirmOverlay {width: 100%;height: 100%;position: fixed;top: 0;left: 0;background: url('/js/ie.png');background: -moz-linear-gradient(rgba(11, 11, 11, 0.1), rgba(11, 11, 11, 0.6)) repeat-x rgba(11, 11, 11, 0.2);background: -webkit-gradient(linear, 0% 0%, 0% 100%, from(rgba(11, 11, 11, 0.1)), to(rgba(11, 11, 11, 0.6))) repeat-x rgba(11, 11, 11, 0.2);z-index: 100000;} " +
            "#confirmBox {background: url('/js/body_bg.jpg') repeat-x left bottom #e5e5e5;position: fixed;margin: 10px;border: 1px solid rgba(33, 33, 33, 0.6); box-shadow: 0px 1px 6px rgba(0, 0, 0, 0.41);}" +
            "#confirmBox h1, #confirmBox h2 {margin: 0;padding: 3px;letter-spacing: 0.4px;text-align: center;} " +
            "#confirmBox h1 {font: 36px 'Cuprum', 'Lucida Sans Unicode', 'Lucida Grande', sans-serif;color: #353535;} " +
            "#confirmBox h1.title {text-transform: uppercase; margin: -16px -16px 0; background: #faa02f; border-radius: 4px 4px 0 0; border: 1px solid #faa02f; border-bottom: 0; padding: 16px 0;} " +
            "#confirmBox h2 {font: 26px 'Cuprum', 'Lucida Sans Unicode', 'Lucida Grande', sans-serif;color: #666;} " +
            "#confirmBox h3, #confirmBox h4, #confirmBox h5, #confirmBox h6 {text-align: center;letter-spacing: 0.3px;text-align: center;color: #888;} " +
            "#confirmBox p {overflow: auto;max-height: 400px;background: none;font-size: 15px;line-height: 1.4;text-shadow: 1px 1px 0 rgba(255, 255, 255, 0.6);color: #555;} " +
            "#confirmButtons {padding: 15px 15px 25px;text-align: center;} " +
            "#confirmBox .button {display: inline-block;position: relative;height: 33px;width: 200px;font: 17px/33px 'Cuprum', 'Lucida Sans Unicode', 'Lucida Grande', sans-serif;margin-right: 15px;text-decoration: none;border: none;margin-top: 3px;} " +
            "#confirmBox .blue {background: url('/js/buttons.png') no-repeat;color: white;background-position: left top;text-shadow: 1px 1px 0 #5889a2;} " +
            "#confirmBox .blue:hover {background-position: left bottom;} " +
            "#confirmBox .gray {background: url('/js/buttons.png') no-repeat;color: white;background-position: -200px top;text-shadow: 1px 1px 0 #707070;} " +
            "#confirmBox .gray:hover {background-position: -200px bottom;}" +
            "#confirmOverlay{display:flex;justify-content:center;align-items:center;padding:0 15px;}#confirmOverlay #confirmBox{background:#fff;margin:0;max-width: 750px;padding:15px;border-radius: 5px; border: 1px solid #fff;}#confirmOverlay #confirmBox #confirmButtons{padding:15px}#confirmOverlay #confirmBox p{padding:15px}#confirmOverlay #confirmBox h1.title{background:#faa02f; font-weight:400;font-size:1.3em;font-family:Roboto;color:#fff;}" +
            "</style> ";

        let markup = document.createElement('div');
        markup.id = 'confirmOverlay';
        markup.innerHTML = css +
            '<div id="confirmBox">' +
            '<h1 class="title">' + params.title + '</h1>' +
            '<p>' + params.message + '</p>' +
            '<div id="confirmButtons">' +
            buttonHTML +
            '</div></div>';

        $p.Element('body').appendChild(markup);

        var buttons = $p.Elements('#confirmBox .button'),
            i = 0;

        Object.keys(params.buttons).forEach(function (key) {
            let obj = params.buttons[key];
            $p.SetEvent(buttons[i], 'click', function () {
                /* Calling the action attribute when a
                  click occurs, and hiding the confirm.*/
                $p.Delete($p.Element('#confirmOverlay'));
                if (obj.action) {
                    ExternalFunc(obj.action);
                }
            });
            i++;
        });
    }

    /**
     * Ejecutar una funcion externa
     * @param {any} func nombre de la funcion
     * @param {any} data variables de la funcion
     */
    function ExternalFunc(func, data) {
        if (data === undefined) data = null;
        if (typeof func === 'string') eval(func, data);
        if (typeof func === 'function') func(data);
        else return;
    }

    /**
     * Activar pantalla completa
     * @param {object} element elemento a poner en pantalla completa. Usar 'document.documentElement' para todo el documento
     */
    function launchFullScreen(element) {
        /* Encuentra el método correcto, llama al elemento correcto*/
        if (element.requestFullScreen) {
            element.requestFullScreen();
        } else if (element.mozRequestFullScreen) {
            element.mozRequestFullScreen();
        } else if (element.webkitRequestFullScreen) {
            element.webkitRequestFullScreen();
        }
    }

    /**
     * Cambiar la pantalla completa
     */
    function cancelFullscreen() {
        if (document.cancelFullScreen) {
            document.cancelFullScreen();
        } else if (document.mozCancelFullScreen) {
            document.mozCancelFullScreen();
        } else if (document.webkitCancelFullScreen) {
            document.webkitCancelFullScreen();
        }
    }

    /**
     * Send Tracking to Google Analitics
     * @param {string} category nombre de la categoria
     * @param {string} action nomrbe de la accion
     * @param {string} description descripcion 
     * @param {number} valor valor a enviar
     * @returns {boolean} boolean
     */
    function SendGA(category, action, description, valor) {
        if ($p.TestVar(category) === false) category = 'No Category';
        if ($p.TestVar(action) === false) action = 'No Action';
        if ($p.TestVar(description) === false) description = '';
        if ($p.TestVar(valor) === false) valor = 0;

        try {
            ga('send', {
                hitType: 'event',
                eventCategory: category,
                eventAction: action,
                eventLabel: description,
                eventValue: valor
            });
            return true;
        } catch (e) {
            return false;
        }
    }


    /**
     * Polify function of composedPath
     * @param {any} evt event
     * @returns {array} array of events
     */
    function EventPath(evt) {
        var path = evt.composedPath && evt.composedPath() || evt.path, target = evt.target;

        if (path) {
            // Safari doesn't include Window, and it should.
            path = path.indexOf(window) < 0 ? path.concat([window]) : path;
            return path;
        }

        if (target === window) {
            return [window];
        }

        function getParents(node, memo) {
            memo = memo || [];
            var parentNode = node.parentNode;

            if (!parentNode) {
                return memo;
            }
            else {
                return getParents(parentNode, memo.concat([parentNode]));
            }
        }

        return [target]
            .concat(getParents(target))
            .concat([window]);
    }

    /**
     * Llamar a un web service de URL estilo MVC con puro JAVASCRIPT
     * @param {Function} type tipo: GET o POST
     * @param {string} url ruta completa del webservice a llamar
     * @param {Function} success funcion a ejecutar. Siempre recibe data como parametro
     * @param {Function} error funcion a ejecutar en el error
     * @param {string} data datos a enviar
     * @param {string} content content-type to send, default "application/json; charset=utf-8"
     * @param {string} upload callback function to know the process of the upload files
     * @returns {void} almacena en el storageSession un item con el nombre ws_lr
     */
    function _WebAPI(type, url, success, error, data, content, upload) {

        let xhr;            //control compatibilities
        if (window.XMLHttpRequest) {
            xhr = new XMLHttpRequest();
        }
        else {
            xhr = new ActiveXObject("Microsoft.XMLHTTP");
        }
        if (type === null || type === undefined) type = "POST";
        if (content === null || content === undefined) {
            if (data instanceof FormData) content = "";
            else content = "application/json; charset=utf-8";
        }

        xhr.open(type, url, true);
        if (type === 'POST') {
            if (content !== '') xhr.setRequestHeader("Content-Type", content);
        }
        xhr.setRequestHeader("Access-Control-Allow-Origin", "*");
        xhr.setRequestHeader("Access-Control-Allow-Method", type);
        xhr.setRequestHeader("Access-Control-Allow-Headers", "*");
        xhr.onloadend = function () {
            sessionStorage.setItem('ws_response', xhr.response);
            if (xhr.status >= 200 && xhr.status <= 299) {
                let dat = '';
                try {
                    dat = JSON.parse(xhr.response);
                } catch (e) {
                    if (dat !== null && dat !== undefined && xhr.responseXML !== null) {
                        dat = xml2json(xhr.responseXML, false);
                    }
                    else {
                        dat = xhr.response;
                    }
                }
                sessionStorage.setItem('ws_json', JSON.stringify(dat));
                if (success !== undefined && success !== null) ExternalFunc(success, dat);
            }
            else {
                try {
                    let err = SetError(xhr, null);
                    sessionStorage.setItem('ws_response', JSON.stringify(err));
                    if (error !== undefined && error !== null) ExternalFunc(error, err);
                } catch (e) {
                    if (error !== undefined && error !== null) ExternalFunc(error, SetError(xhr, e));
                }
            }
        };
        xhr.onerror = function (e) {
            console.warn(e);
            if (error !== undefined && error !== null) ExternalFunc(error, SetError(xhr, e));
        };
        if (upload !== undefined && upload !== null) {
            xhr.upload.onprogress = function (e) {
                if (e.lengthComputable) {
                    let progress = {
                        length: e.lengthComputable,
                        loaded: e.loaded,
                        total: e.total
                    };
                    ExternalFunc(upload, progress);
                }
            };
        }
        xhr.ontimeout = function (t) {
            console.warn(t);
            if (error !== undefined && error !== null) ExternalFunc(error, SetError(xhr, t));
        };
        if (type === 'POST') {
            if (data === null || data === undefined || data === '') xhr.send();
            else {
                xhr.send(data);
            }
        }
        else xhr.send();

        function SetError(request, e) {
            let message = request.status >= 400 ? url + ' replied ' + request.status : request.statusText
            return {
                Status: request.status,
                Message: message,
                Stack: request.responseText,
                ex: e
            };
        }
    }

    /**
     * Convertir xml a json
     * Tries to convert a given XML data to a native JavaScript object by traversing the DOM tree.
     * If a string is given, it first tries to create an XMLDomElement from the given string.
     * https://stackoverflow.com/questions/1199180/read-xml-file-using-javascript
     * 
     * @param {XMLDomElement|String} source The XML string or the XMLDomElement prefreably which containts the necessary data for the object.
     * @param {Boolean} [includeRoot] Whether the "required" main container node should be a part of the resultant object or not.
     * @return {Object} The native JavaScript object which is contructed from the given XML data or false if any error occured.
     */
    function xml2json(source, includeRoot) {
        var original = source;
        if (typeof source === 'string') {
            try {
                if (window.DOMParser)
                    source = (new DOMParser()).parseFromString(source, "application/xml");
                else if (window.ActiveXObject) {
                    var xmlObject = new ActiveXObject("Microsoft.XMLDOM");
                    xmlObject.async = false;
                    xmlObject.loadXML(source);
                    source = xmlObject;
                    xmlObject = undefined;
                }
                else
                    throw new Error("Cannot find an XML parser!");
            }
            catch (error) {
                return false;
            }
        }

        var result = {};

        if (source !== null) {

            if (source.nodeType === 9)
                source = source.firstChild;
            if (!includeRoot)
                source = source.firstChild;

            while (source) {
                if (source.childNodes.length) {
                    if (source.tagName in result) {
                        if (result[source.tagName].constructor !== Array)
                            result[source.tagName] = [result[source.tagName]];
                        result[source.tagName].push(xml2json(source));
                    }
                    else
                        result[source.tagName] = xml2json(source);
                } else if (source.tagName)
                    result[source.tagName] = source.nodeValue;
                else if (!source.nextSibling) {
                    if (source.nodeValue.clean() !== "") {
                        result = source.nodeValue.clean();
                    }
                }
                source = source.nextSibling;
            }
            return result;
        }
        else return original;
    }

    String.prototype.clean = function () {
        var self = this;
        return this.replace(/(\r\n|\n|\r)/gm, "").replace(/^\s+|\s+$/g, "");
    };

    //Eliminar el valor pasado de un array
    Array.prototype.remove = function (deleteValue) {
        for (var i = 0, j = this.length; i < j; i++) {
            if (this[i] === deleteValue) {
                this.splice(i, 1);
                i--;
            }
        }
        return this;
    };

    //eliminar los valores nulos de un array
    Array.prototype.clean = function () {
        var deleteValue = "";
        for (var i = 0, j = this.length; i < j; i++) {
            if (this[i] === deleteValue) {
                this.splice(i, 1);
                i--;
            }
        }
        return this;
    };


    /**
     * Check if the string is booleans. 
     * @return {Boolean} devulve true, false o null
     */
    String.prototype.maybeBool = function () {
        return $p.ParseBoolean(this);
    };
    /**
     * Message box like bootstrap. If don't use bootstrap must be to create the clases:
     * alert and danger/alert/info/warning/success or also can send yout own class
     * @param {any} tarject selector object or DOM object
     * @param {any} title title to show
     * @param {any} message fecha a formatear
     * @param {any} timeout timeout to hide the div in seconds
     * @param {any} cls only in other own class
     * @param {object} conf  setup = {Title: '',Message: '',Class: 'info',Timeout: 0,Cleanup: false};
     */
    function _Message() {
        /** Setup the window */
        let setup = {
            Title: '',
            Message: '',
            Class: 'is-info',
            Timeout: 0,
            Cleanup: false,
            Framework: 'bulma',
            waiting: false
        };

        /**
         * Create a div with the messate
         * @returns {HTMLDivElement} div with the message window
         */
        function myMessage() {
            let myDiv = document.createElement('div');
            var html = '';
            if (setup.Framework.toLowerCase() === 'bootstrap') {
                myDiv.className = 'alert ' + setup.Class;
                html = '<button type="button" class="close" aria-hidden="true">&times;</button>';
                if (setup.Title !== '' && setup.Title !== null && setup.Title !== undefined) html += '<h4>' + setup.Title + '</h4>';
                html += '<span>' + setup.Message + '</span>';
                myDiv.innerHTML = html;
                //set the evento on the close button to hide the message from the user
                let close = $p.GetBySel('.close', myDiv);
                close.addEventListener('click', function () {
                    let parent = $p.GetBySel('.alert.' + setup.Class);
                    $p.Delete(parent);
                }, true);
            }
            else if (setup.Framework.toLowerCase() === 'bulma') {
                myDiv.className = 'message ' + setup.Class;
                if (setup.Title !== '' && setup.Title !== null && setup.Title !== undefined) html += '<div class="message-header"><p>' + setup.Title + '</p><button class="delete" aria-label="delete"></button></div>';
                html += '<div class="message-body">' + setup.Message;
                if (setup.waiting) html += '<button class="button is-loading is-pulled-right ' + setup.Class + '">Loading</button>';
                html += '</div>';
                myDiv.innerHTML = html;
                if (setup.Title !== '' && setup.Title !== null && setup.Title !== undefined) {
                    //set the evento on the close button to hide the message from the user
                    let close = $p.GetBySel('.delete', myDiv);
                    close.addEventListener('click', function () {
                        let parent = $p.GetBySel('.message.' + setup.Class);
                        $p.Delete(parent);
                    }, true);
                }
            }
            else {
                if (setup.Title !== '' && setup.Title !== null && setup.Title !== undefined) html += '<h3>' + setup.Title + '</h3>';
                html += '<p>' + setup.Message + '</p>';
                myDiv.innerHTML = html;
            }

            if (setup.Timeout !== null && setup.Timeout !== undefined && setup.Timeout > 0) {
                let time = $p.ConvertToInteger(setup.Timeout, false) * 1000;
                setTimeout($p.Delete, time, myDiv);
            }
            return myDiv;
        }
        /**
         * Show a message 
         * @param {any} tarject id or selector
         */
        function ShowMessage(tarject) {
            if (setup.Cleanup) {
                $p.Element(tarject).innerHTML = '';
                $p.Element(tarject).appendChild(myMessage());
            }
            else $p.Element(tarject).appendChild(myMessage());
        }
        _Message.prototype.alert = function (tarject, title, message, timeout) {
            setup = {
                Title: title,
                Message: message,
                Class: 'alert-danger',
                Timeout: timeout,
                Cleanup: true,
                Framework: 'bootstrap',
                waiting: false
            };
            ShowMessage(tarject);
        };
        _Message.prototype.danger = function (tarject, title, message, timeout) {
            setup = {
                Title: title,
                Message: message,
                Class: 'alert-danger',
                Timeout: timeout,
                Cleanup: true,
                Framework: 'bootstrap',
                waiting: false
            };
            ShowMessage(tarject);
        };
        _Message.prototype.warning = function (tarject, title, message, timeout) {
            setup = {
                Title: title,
                Message: message,
                Class: 'alert-warning',
                Timeout: timeout,
                Cleanup: true,
                Framework: 'bootstrap',
                waiting: false
            };
            ShowMessage(tarject);
        };
        _Message.prototype.info = function (tarject, title, message, timeout) {
            setup = {
                Title: title,
                Message: message,
                Class: 'alert-info',
                Timeout: timeout,
                Cleanup: true,
                Framework: 'bootstrap',
                waiting: false
            };
            ShowMessage(tarject);
        };
        _Message.prototype.success = function (tarject, title, message, timeout) {
            setup = {
                Title: title,
                Message: message,
                Class: 'alert-success',
                Timeout: timeout,
                Cleanup: true,
                Framework: 'bootstrap',
                waiting: false
            };
            ShowMessage(tarject);
        };
        _Message.prototype.other = function (tarject, title, message, timeout, cls) {
            setup = {
                Title: title,
                Message: message,
                Class: cls,
                Timeout: timeout,
                Cleanup: true,
                Framework: 'bootstrap',
                waiting: false
            };
            ShowMessage(tarject);
        };
        _Message.prototype.show = function (tarject, conf) {
            if (typeof conf !== 'undefined' && conf !== '') {
                if (typeof conf !== 'object') {
                    try {
                        let lookJson = JSON.parse(conf);
                        if (lookJson) {
                            setup = $p.MergeObjects(setup, conf);
                        }
                    } catch (e) {
                        console.error(e);
                        setup.Title = 'Error';
                        setup.Message = 'When parse a message.';
                        setup.Timeout = 5;
                        setup.Class = '';
                        setup.Cleanup = true;
                        setup.Framework = '';
                        setup.waiting = true;
                    }
                }
                else {
                    setup = $p.MergeObjects(setup, conf);
                }
            }
            ShowMessage(tarject);
        };
    }


    /**
     * Convertir xml a json
     * Tries to convert a given XML data to a native JavaScript object by traversing the DOM tree.
     * If a string is given, it first tries to create an XMLDomElement from the given string.
     * https://stackoverflow.com/questions/1199180/read-xml-file-using-javascript
     * 
     * @param {XMLDomElement|String} source The XML string or the XMLDomElement prefreably which containts the necessary data for the object.
     * @param {Boolean} [includeRoot] Whether the "required" main container node should be a part of the resultant object or not.
     * @return {Object} The native JavaScript object which is contructed from the given XML data or false if any error occured.
     */
    function xml2json(source, includeRoot) {
        var original = source;
        if (typeof source === 'string') {
            try {
                if (window.DOMParser)
                    source = (new DOMParser()).parseFromString(source, "application/xml");
                else if (window.ActiveXObject) {
                    var xmlObject = new ActiveXObject("Microsoft.XMLDOM");
                    xmlObject.async = false;
                    xmlObject.loadXML(source);
                    source = xmlObject;
                    xmlObject = undefined;
                }
                else
                    throw new Error("Cannot find an XML parser!");
            }
            catch (error) {
                return false;
            }
        }

        var result = {};

        if (source !== null) {

            if (source.nodeType === 9)
                source = source.firstChild;
            if (!includeRoot)
                source = source.firstChild;

            while (source) {
                if (source.childNodes.length) {
                    if (source.tagName in result) {
                        if (result[source.tagName].constructor !== Array)
                            result[source.tagName] = [result[source.tagName]];
                        result[source.tagName].push(xml2json(source));
                    }
                    else
                        result[source.tagName] = xml2json(source);
                } else if (source.tagName)
                    result[source.tagName] = source.nodeValue;
                else if (!source.nextSibling) {
                    if (source.nodeValue.clean() !== "") {
                        result = source.nodeValue.clean();
                    }
                }
                source = source.nextSibling;
            }
            return result;
        }
        else return original;
    }

    String.prototype.clean = function () {
        var self = this;
        return this.replace(/(\r\n|\n|\r)/gm, "").replace(/^\s+|\s+$/g, "");
    };

    //Eliminar el valor pasado de un array
    Array.prototype.remove = function (deleteValue) {
        for (var i = 0, j = this.length; i < j; i++) {
            if (this[i] === deleteValue) {
                this.splice(i, 1);
                i--;
            }
        }
        return this;
    };

    //eliminar los valores nulos de un array
    Array.prototype.clean = function () {
        var deleteValue = "";
        for (var i = 0, j = this.length; i < j; i++) {
            if (this[i] === deleteValue) {
                this.splice(i, 1);
                i--;
            }
        }
        return this;
    };

    /**
     * Check if the string is booleans. 
     * @return {Boolean} devulve true, false o null
     */
    String.prototype.maybeBool = function () {
        return $p.ParseBoolean(this);
    };

    /**
     * Message box like bootstrap. If don't use bootstrap must be to create the clases:
     * alert and danger/alert/info/warning/success or also can send yout own class
     * @param {any} tarject selector object or DOM object
     * @param {any} title title to show
     * @param {any} message fecha a formatear
     * @param {any} timeout timeout to hide the div in seconds
     * @param {any} cls only in other own class
     * @param {object} conf  setup = {Title: '',Message: '',Class: 'info',Timeout: 0,Cleanup: false};
     */
    function _Message() {
        /** Setup the window */
        let setup = {
            Title: '',
            Message: '',
            Class: 'is-info',
            Timeout: 0,
            Cleanup: false,
            Framework: 'bulma',
            waiting: false
        };

        /**
         * Create a div with the messate
         * @returns {HTMLDivElement} div with the message window
         */
        function myMessage() {
            let myDiv = document.createElement('div');
            var html = '';
            if (setup.Framework.toLowerCase() === 'bootstrap') {
                myDiv.className = 'alert ' + setup.Class;
                html = '<button type="button" class="close" aria-hidden="true">&times;</button>';
                if (setup.Title !== '' && setup.Title !== null && setup.Title !== undefined) html += '<h4>' + setup.Title + '</h4>';
                html += '<span>' + setup.Message + '</span>';
                myDiv.innerHTML = html;
                //set the evento on the close button to hide the message from the user
                let close = $p.GetBySel('.close', myDiv);
                close.addEventListener('click', function () {
                    let parent = $p.GetBySel('.alert.' + setup.Class);
                    $p.Delete(parent);
                }, true);
            }
            else if (setup.Framework.toLowerCase() === 'bulma') {
                myDiv.className = 'message ' + setup.Class;
                if (setup.Title !== '' && setup.Title !== null && setup.Title !== undefined) html += '<div class="message-header"><p>' + setup.Title + '</p><button class="delete" aria-label="delete"></button></div>';
                html += '<div class="message-body">' + setup.Message;
                if (setup.waiting) html += '<button class="button is-loading is-pulled-right ' + setup.Class + '">Loading</button>';
                html += '</div>';
                myDiv.innerHTML = html;
                if (setup.Title !== '' && setup.Title !== null && setup.Title !== undefined) {
                    //set the evento on the close button to hide the message from the user
                    let close = $p.GetBySel('.delete', myDiv);
                    close.addEventListener('click', function () {
                        let parent = $p.GetBySel('.message.' + setup.Class);
                        $p.Delete(parent);
                    }, true);
                }
            }
            else {
                if (setup.Title !== '' && setup.Title !== null && setup.Title !== undefined) html += '<h3>' + setup.Title + '</h3>';
                html += '<p>' + setup.Message + '</p>';
                myDiv.innerHTML = html;
            }

            if (setup.Timeout !== null && setup.Timeout !== undefined && setup.Timeout > 0) {
                let time = $p.ConvertToInteger(setup.Timeout, false) * 1000;
                setTimeout($p.Delete, time, myDiv);
            }
            return myDiv;
        }
        /**
         * Show a message 
         * @param {any} tarject id or selector
         */
        function ShowMessage(tarject) {
            if (setup.Cleanup) {
                $p.Element(tarject).innerHTML = '';
                $p.Element(tarject).appendChild(myMessage());
            }
            else $p.Element(tarject).appendChild(myMessage());
        }
        _Message.prototype.alert = function (tarject, title, message, timeout) {
            setup = {
                Title: title,
                Message: message,
                Class: 'alert-danger',
                Timeout: timeout,
                Cleanup: true,
                Framework: 'bootstrap',
                waiting: false
            };
            ShowMessage(tarject);
        };
        _Message.prototype.danger = function (tarject, title, message, timeout) {
            setup = {
                Title: title,
                Message: message,
                Class: 'alert-danger',
                Timeout: timeout,
                Cleanup: true,
                Framework: 'bootstrap',
                waiting: false
            };
            ShowMessage(tarject);
        };
        _Message.prototype.warning = function (tarject, title, message, timeout) {
            setup = {
                Title: title,
                Message: message,
                Class: 'alert-warning',
                Timeout: timeout,
                Cleanup: true,
                Framework: 'bootstrap',
                waiting: false
            };
            ShowMessage(tarject);
        };
        _Message.prototype.info = function (tarject, title, message, timeout) {
            setup = {
                Title: title,
                Message: message,
                Class: 'alert-info',
                Timeout: timeout,
                Cleanup: true,
                Framework: 'bootstrap',
                waiting: false
            };
            ShowMessage(tarject);
        };
        _Message.prototype.success = function (tarject, title, message, timeout) {
            setup = {
                Title: title,
                Message: message,
                Class: 'alert-success',
                Timeout: timeout,
                Cleanup: true,
                Framework: 'bootstrap',
                waiting: false
            };
            ShowMessage(tarject);
        };
        _Message.prototype.other = function (tarject, title, message, timeout, cls) {
            setup = {
                Title: title,
                Message: message,
                Class: cls,
                Timeout: timeout,
                Cleanup: true,
                Framework: 'bootstrap',
                waiting: false
            };
            ShowMessage(tarject);
        };
        _Message.prototype.show = function (tarject, conf) {
            if (typeof conf !== 'undefined' && conf !== '') {
                if (typeof conf !== 'object') {
                    try {
                        let lookJson = JSON.parse(conf);
                        if (lookJson) {
                            setup = $p.MergeObjects(setup, conf);
                        }
                    } catch (e) {
                        console.warn(e);
                        setup.Title = 'Error';
                        setup.Message = 'When parse a message.';
                        setup.Timeout = 5;
                        setup.Class = '';
                        setup.Cleanup = true;
                        setup.Framework = '';
                        setup.waiting = true;
                    }
                }
                else {
                    setup = $p.MergeObjects(setup, conf);
                }
            }
            ShowMessage(tarject);
        };
    }
})();
