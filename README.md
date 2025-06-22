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
**Сборка minio/mc с sh**
```
docker build -t minio-setup -f Dockerfile.minio-setup
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
docker run --rm --network diplom-backend_default --env-file .env diplom-backend-migration-runner
```

# Конфигурация Minio
```
docker run --network diplom-backend_default -e MINIO_ROOT_USER=minioadmin -e MINIO_ROOT_PASSWORD=minioadmin -v %cd%/minio_setup.sh:/minio_setup.sh:ro -v %cd%/minio-policy.json:/scripts/minio-policy.json:ro minio-setup sh /minio_setup.sh
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