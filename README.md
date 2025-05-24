# Прототип социальной сети

## Архитектура системы
Система состоит из двух микросервисов, реализующих паттерн CQRS:

### 1. Topic.CommandService
**Назначение:**
- Обработка операций записи (CUD-операции)
- Генерация событий изменений
- Публикация событий в Kafka

**Технологический стек:**
- .NET 9
- PostgreSQL + Marten (Event Store)
- Kafka (брокер сообщений)
- Minimal API
- Carter (для организации endpoints)

**Основные функции:**
- Создание/обновление/удаление топиков
- Управление комментариями
- Генерация событий Domain Events

### 2. Topic.QueryService (Сервис запросов)
**Назначение:**
- Фоновая обработка событий из Kafka
- Построение проекций данных
- Обслуживание read-запросов

**Технологический стек:**
- .NET 9
- PostgreSQL (реляционная модель)
- Kafka Consumer
- Entity Framework Core
- Minimal API

## Общий технологический стек

**Базы данных:**
- `CommandService`: PostgreSQL + Marten
- `QueryService`: PostgreSQL + EF Core

## Инфраструктура

Проект использует следующие инфраструктурные компоненты:

*   **Docker Compose:** для локальной разработки и развертывания используется Docker Compose, что позволяет описать и запустить все необходимые сервисы (PostgreSQL, Kafka, сами микросервисы)
*   **PostgreSQL:** основная СУБД
*   **Kafka:** брокер сообщений, обеспечивающий асинхронное взаимодействие между `CommandService` и `QueryService`. Используется для публикации событий, отражающих изменения
*   **.NET Minimal API:** используется для создания API, предоставляемых сервисами `CommandService` и `QueryService`.
*   **Marten:** библиотека, упрощающая работу с PostgreSQL
*   **Carter:** фреймворк, облегчающий организацию endpoints в Minimal API

## Список используемых библиотек:

- `Carter` - минималистичный фреймворк для роутинга
- `Confluent.Kafka` - клиент для Apache Kafka
- `Microsoft.Extensions.Configuration.Binder` - биндинг конфигурации
- `Marten` - документная БД для PostgreSQL
- `Npgsql.EntityFrameworkCore.PostgreSQL` - PostgreSQL провайдер для EF Core
- `Microsoft.EntityFrameworkCore.Proxies` - ленивая загрузка
- `Microsoft.Extensions.Options` - конфигурация и DI
- `Microsoft.EntityFrameworkCore.Design` - миграции EF Core
- `Microsoft.EntityFrameworkCore.Tools` - CLI инструменты EF

## Порты для взаимодействия

| Сервис                | dev  | debug | prod | docker |
|-----------------------|------|-------|------|--------|
| social_write_db       | 6001 |       | 6201 |        |
| social_read_db        | 6002 |       | 6202 |        |
| zookeeper             | 2181 |       | 2181 |        |
| kafka                 | 9092 |       | 9092 |        |
| topic_command_service | 6010 | 6110  | 6210 | 6310   |
| topic_query_service   | 6020 | 6120  | 6220 | 6320   |

## Удаление папок bin и obj

```zsh
find . -type d \( -name "bin" -o -name "obj" \) -exec rm -rf {} +
```

## Проверка топиков

```zsh
kafka-topics.sh --list --bootstrap-server localhost:9092
```

## Сообщение топика `TopicKafkaDev`

```zsh
kafka-console-consumer.sh --bootstrap-server localhost:9092 --topic TopicKafkaDev --from-beginning
```