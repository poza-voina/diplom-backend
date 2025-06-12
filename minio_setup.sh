#!/bin/sh

# Устанавливаем alias для MinIO сервера
mc alias set local http://minio:9000 $MINIO_ROOT_USER $MINIO_ROOT_PASSWORD

# Создаем bucket, если нет
mc mb local/attachments || true

# Создаем пользователя для записи/чтения (замени на свои ключи)
mc admin user add local appuser apppassword

# Создаем политику с нужными правами (если нужно, создай json-файл с политикой и применяй)
mc admin policy add local apppolicy /scripts/minio-policy.json || true

# Назначаем политику пользователю
mc admin policy set local apppolicy user=appuser