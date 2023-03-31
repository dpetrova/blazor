// This is a JavaScript module that is loaded on demand. 
// It can export any number of functions, and may import other JavaScript modules if required.

window.components = (function () {
    return {
        chart: function (id, data, options) {
            var context = document.getElementById(id).getContext('2d');
            var chart = new Chart(context, {
                type: 'line',
                data: data,
                options: options
            });
        }
    };
})();
