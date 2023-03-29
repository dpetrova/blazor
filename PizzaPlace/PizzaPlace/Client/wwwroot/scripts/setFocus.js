//automatically focus on element
(function () {
    window.blazorFocus = {
        set: (element) => { element.focus(); }
    };
})();