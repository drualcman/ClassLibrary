(function () {
    /**
     * Custom In page Message --independent
     * @param {object} target DOM element
     * @param {string} title notification title
     * @param {string} message notification message
     * @param {string} status notification status ex. is-danger
     * @param {number} timeout countdown time to remove the notification in seconds
     * @param {string} classes optional, additional classes
     */
    window.CustomMessage = (target, title, message, status, timeout, classes) => {
        target = document.querySelector(target) ?? '';
        title = title ?? '';
        message = message ?? '';
        status = status ?? 'is-default';
        timeout = parseInt(timeout) ?? 0;
        classes = classes ?? '';

        if (target) {
            let alert = document.createElement('div');
            alert.className = `ct-notification ${status} ${classes}`;

            let htmlContent = `<div class="close"><i class="fas fa-times"></i></div>
                    ${title ? `<div class="cst-title">${title}</div>` : ''}
                    ${message ? `<div class="cst-content">${message}</div>` : ''}`;

            alert.innerHTML = htmlContent;

            alert.querySelector('.close').addEventListener('click', function () {
                alert.remove();
            });

            if (timeout > 0) {
                let time = timeout * 1000;
                setTimeout(function () {
                    alert.remove();
                }, time);
            }

            target.appendChild(alert);
        }
    }
})();

