# Дипломная работа (backend)
---
# Сборка Docker Image
**Сборка приложения**
```
docker build -t diplom-backend -f Dockerfile.app
```
**Сборка консольного приложения для миграций**
```
docker build -t diplom-backend-migration-runner -f Dockerfile.migration-runner 
```
---
# Запуск контейнеров Docker
**Запуск контейнера с приложением**
```
docker compose -f docker-compose.app.yml up -d
```
**Запуск контейнера с Базой данных и S3**
```
docker compose -f docker-compose.infrastructure.yml up -d
````
---
# Принять миграции
```
docker run --rm --network diplombackend_default --env-file .env diplom-backend-migration-runner
```
---
# Локально
Строка подключения берется из appsettings.json
## Cоздание миграций
```
dotnet ef migrations add InitialMigration --project Infrastructure --startup-project Application
```
## Локальное приминение миграций
```
dotnet ef database update --project Infrastructure --startup-project Application
```