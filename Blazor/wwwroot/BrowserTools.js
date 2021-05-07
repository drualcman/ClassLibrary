// To set a function auto executable enclose with () and the second () is if need to receive parameters
(function () {
    window.browserJsFunctions = {
        getLanguage: () => {
            return (navigator.languages && navigator.languages.length) ? navigator.languages[0] :
                navigator.userLanguage || navigator.language || navigator.browserLanguage || 'ES'
        },
        getBrowserTimeZoneOffset: () => {
            return new Date().getTimezoneOffset();
        },
        getBrowserTimeZoneIdentifier: () => {
            return Intl.DateTimeFormat().resolvedOptions().timeZone;
        },
        Width: () => window.innerWidth,
        Height: () => window.innerHeight
    };
})();
