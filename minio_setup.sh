#!/bin/sh

# Устанавливаем alias для MinIO сервера
mc alias set local http://minio:9000 $MINIO_ROOT_USER $MINIO_ROOT_PASSWORD

# Создаем bucket (если ещё не создан)
mc mb local/attachments || true

# Создаем пользователя (если ещё не создан)
mc admin user add local appuser apppassword || true

# Добавляем политику (если ещё не добавлена)
mc admin policy create local appolicy /scripts/minio-policy.json || true

# Назначаем политику пользователю
mc admin policy attach local appolicy --user appuser

# Создаем сервисный аккаунт с фиксированными ключами
mc admin user svcacct add local appuser \
  --access-key appaccesskey \
  --secret-key appsecretkey || true
