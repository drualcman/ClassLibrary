// To set a function auto executable enclose with () and the second () is if need to receive parameters
(function () {
    window.MyLocalStorage = {                  //manage local store
        Get: key => key in localStorage ? JSON.parse(localStorage[key]) : null,     //recovery data
        Set: (key, value) => localStorage[key] = JSON.stringify(value),             //set data
        Del: key => delete localStorage[key]                                     //delete data
    };
    window.MySessionStorage = {                  //manage session store
        Get: key => key in sessionStorage ? JSON.parse(sessionStorage[key]) : null,     //recovery data
        Set: (key, value) => sessionStorage[key] = JSON.stringify(value),             //set data
        Del: key => delete sessionStorage[key]                                     //delete data
    };
})();
