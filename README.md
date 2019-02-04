# Security.V2.1

Система управления доступом. Версия 2.1

## Назначение системы

1. Организация защиты доступа к ресурсам приложения путем аутентификации пользователя при помощи логина и пароля
2. Защита от несанкционированного использования ресурсов приложения на основе ролей и путем назначения политик безопасности
3. Логирование действий пользователя

## Где можно использовать

1. Все приложения .Net Framework
2. Поддержка .Net Core пока отсутствует

## Компоненты системы на всех уровнях

1. База данных (MSSQL)
2. Основные программно-функциональные блоки (контекст, работающий непосредственно с БД и контекст, работающий на основе RESTfull API)
3. Приложение WebApi
4. Система управления настройками безопасности, представляющее собой одностраничное UI-приложение, которое позволяет полностью настраивать систему безопасности, списки пользователей, групп, ролей и политик безопасности

## Схема взаимодействия компонентов

![Imgur](https://i.imgur.com/pMVClGs.png)
