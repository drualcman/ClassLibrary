export const AudioPlayer = {
    /**
     * Play a sound
     * @param {any} src source of the audio
     * @param {any} container player name
     */
    Play: (src, container) => {
        if (container && container.length > 0) {            //have container then get element
            var audio = document.getElementById(container);
            if (audio != null) {
                if (src && src.length > 0) {                //sent file then load it
                    audio.src = src;
                    audio.load();
                }
                try {
                    audio.play();
                } catch (e) {
                    console.warn(e);
                }
            }
        }
        else {
            if (src && src.length > 0) {                //is no file don't do nothing
                var audio = document.createElement('audio');
                try {
                    audio.id = 'audio' + (Math.floor(Math.random() * 100) + 1);
                    audio.src = src;
                    document.body.appendChild(audio);
                    audio.load();
                    audio.play();
                } catch (e) {
                    console.warn(e);
                }
                setTimeout(document.body.removeChild, 60000, audio);
            }
        }
    },
    /**
     * Stop a sound
     * @param {any} container player name
     */
    Stop: (container) => {
        var audio = document.getElementById(container);
        if (audio != null) {
            audio.pause();
        }
    }
}