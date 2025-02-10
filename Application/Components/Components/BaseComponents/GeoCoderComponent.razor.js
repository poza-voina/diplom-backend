window.yandexGeocoder = {
    geocodeAddress: function (address) {
        return new Promise((resolve, reject) => {
            ymaps.geocode(address).then(function (res) {
                var firstGeoObject = res.geoObjects.get(0);
                if (firstGeoObject) {
                    resolve({
                        latitude: firstGeoObject.geometry.getCoordinates()[0],
                        longitude: firstGeoObject.geometry.getCoordinates()[1]
                    });
                } else {
                    reject('Адрес не найден');
                }
            }).catch(function (error) {
                reject('Ошибка геокодирования: ' + error);
            });
        });
    }
};
