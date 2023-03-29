//function allow you to access the localStorage object from the browser,
//which allows you to store data on the client’s computer so you can access it later, even after the user has restarted the browser
(function () {
    window.blazorLocalStorage = {
        get: key => key in localStorage ? JSON.parse(localStorage[key]) : null,
        set: (key, value) => { localStorage[key] = JSON.stringify(value); },
        delete: key => { delete localStorage[key]; },
        // invoke .NET instance method
        watch: async (instance) => {
            window.addEventListener('storage', (e) => {
                instance.invokeMethodAsync('UpdateCounter');
            });
        }
    };
})();