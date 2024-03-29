﻿(function () {
    /**
     * Show popup message on the bottom right on the screen (No dependencies)
     * @param {string} message message shows on popup
     * @param {boolean} status status of popup
     * @param {number} time time limit to show
     * @param {string} additional additional data
     */
    window.PopupResponse = (message, status, time, additional) => window.PopupResponseMessage(message, status, time, additional),
    /**
     * Show popup message on the bottom right on the screen (No dependencies)
     * @param {string} message message shows on popup
     * @param {boolean} status status of popup
     * @param {number} time time limit to show
     * @param {string} additional additional data
     */
    window.PopupResponseMessage = (message, status, time, additional) => {
        if (!message) return false; // No need to run
        else {
            if (typeof (message) !== typeof ('')) {
                let msg = '';
                try {
                    message.forEach(function (notif) {
                        msg += notif.message
                    });
                } catch (e) {
                    console.warn(e);
                    msg = '';
                }
                message = msg;
            }
        }

        status = status ?? true;
        time = ((time ?? 5) * 1000);
        additional = additional ?? '';
        message += additional;

        let isContainerExist = true;
        let container = document.querySelector('.popup-response-wp'); 
        let items;

        if (!container) {
            isContainerExist = false;
            container = document.createElement('div');
            container.className = 'popup-response-wp';

            items = document.createElement('div');
            items.className = 'items';

            container.appendChild(items);
        }
        else items = container.querySelector('.items');

        let alertIconCSS;
        let alertIcon;
        if (status) {
            alertIconCSS = 'is-success';
            alertIcon = '<i id="icon" class="fas fa-check-circle" style="font-size: 2em; margin-right: 0.75em;"></i>';
        }
        else {
            alertIconCSS = 'is-danger';
            alertIcon = '<i id="icon" class="fas fa-exclamation-circle" style="font-size: 2em; margin-right: 0.75em;"></i>';
        }        

        let item = document.createElement('div');
        item.className = `item ${alertIconCSS}`;
        item.innerHTML = `<div class="close-btn"><i class="fas fa-times"></i></div>
                          <div class="content">
                              ${alertIcon}
                              ${message}
                          </div>`;

        items.appendChild(item);
        item.querySelector('.close-btn').addEventListener('click', function () {
            Close(this.closest('.item'));
        });

        if (!isContainerExist) document.body.appendChild(container);
        setTimeout(Close, time, item);

        function Close(item) {
            if (!item) return false;
            let items = item.closest('.items').children.length,
                container = item.closest('.popup-response-wp');
            items > 1 ? item.remove() : container.remove();
        }
    }
})();

