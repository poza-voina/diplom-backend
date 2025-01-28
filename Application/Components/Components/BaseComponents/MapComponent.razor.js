export function loadYandexMaps(apiKey) {
    return new Promise((resolve, reject) => {
        if (typeof ymaps !== 'undefined') {
            resolve();
        } else {
            const script = document.createElement('script');
            script.src = `https://api-maps.yandex.ru/2.1/?apikey=${apiKey}&lang=ru_RU`;
            script.async = true;
            script.onload = () => resolve();
            script.onerror = () => reject(new Error('Failed to load Yandex Maps'));
            document.head.appendChild(script);
        }
    });
}

export function initializeMap(containerId) {
    ymaps.ready(function () {
        new ymaps.Map(containerId, {
            center: [55.751574, 37.573856],
            zoom: 9
        });
    });
}
