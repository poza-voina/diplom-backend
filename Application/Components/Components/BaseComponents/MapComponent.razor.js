export class YandexMap {
	constructor(apiKey, containerId, routeCoords = []) {
		this.apiKey = apiKey;
		this.containerId = containerId;
		this.routeCoords = routeCoords;
		this.map = null;
		this.route = null;
	}

	loadYandexMaps() {
		return new Promise((resolve, reject) => {
			if (typeof ymaps !== 'undefined') {
				resolve();
			} else {
				const script = document.createElement('script');
				script.src = `https://api-maps.yandex.ru/2.1/?apikey=${this.apiKey}&lang=ru_RU`;
				script.async = true;
				script.onload = () => resolve();
				script.onerror = () => reject(new Error('Failed to load Yandex Maps'));
				document.head.appendChild(script);
			}
		});
	}

	initializeMap() {
		this.loadYandexMaps().then(() => {
			ymaps.ready(() => {
				this.map = new ymaps.Map(this.containerId, {
					center: this.routeCoords.length ? this.routeCoords[0] : [55.751574, 37.573856],
					zoom: 10
				});
				this.buildRoute();
			});
		}).catch((error) => {
			console.error("Ошибка загрузки API Яндекс.Карт:", error);
		});
	}

	buildRoute() {
		if (!this.map || this.routeCoords.length < 2) return;

		ymaps.route(this.routeCoords).then((route) => {
			if (this.route) {
				this.map.geoObjects.remove(this.route);
			}

			this.route = route;
			this.map.geoObjects.add(route);
			this.map.setCenter(this.routeCoords[0]);
		}).catch((error) => {
			console.error("Ошибка при построении маршрута:", error);
		});
	}

	// Метод для добавления точки
	addPoint(newPoint) {
		this.routeCoords.push(newPoint);
		this.buildRoute();  // Перестроить маршрут
	}

	addPoints(points) {
		points.forEach(x => this.routeCoords.push(x))
	}
}

// Функция для создания объекта карты
window.createYandexMapObject = function (apiKey, containerId) {
	const mapObject = new YandexMap(apiKey, containerId);
	mapObject.initializeMap();
	return mapObject;
};

// Функция для добавления точки на карту
window.addPointToMap = function (mapObject, point) {
	mapObject.addPoint([point.latitude, point.longitude]);
};


window.addPointsToMap = function (mapObject, points) {
	mapObject.addPoints(points.map(x => [x.latitude, x.longitude]));
}

window.updateMap = function (mapObject) {
	mapObject.buildRoute();
}