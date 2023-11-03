# Otus.MqRabbit
## Идея
Программа рандомно генерирует число, пользователь должен угадать это число.
При каждом вводе числа программа пишет больше или меньше отгадываемого.
Кол-во попыток отгадывания и диапазон чисел должен задаваться из настроек.

## Функционал
Программа реализована в чистой архитектуре с разделением на микросервисы.
Микросервис клиентской части реализован в виде консольного приложения.
Микросервис серверной части реализован в виде проекта Asp.net Core WebApi.
Общение между сервисами строится на шине сообщений RabbitMq (библиотека MassTransit).

## Общий вид
Главное меню:
![alt text](https://github.com/E-A-Volobuev/Otus.MqRabbit/blob/master/%D0%93%D0%BB%D0%B0%D0%B2%D0%BD%D0%BE%D0%B5%20%D0%BC%D0%B5%D0%BD%D1%8E.png)

Игра:
![alt text](https://github.com/E-A-Volobuev/Otus.MqRabbit/blob/master/%D0%98%D0%B3%D1%80%D0%B0.png)

Меню настроек:
![alt text](https://github.com/E-A-Volobuev/Otus.MqRabbit/blob/master/%D0%9C%D0%B5%D0%BD%D1%8E%20%D0%BD%D0%B0%D1%81%D1%82%D1%80%D0%BE%D0%B5%D0%BA.png)

Редактор настроек:
![alt text](https://github.com/E-A-Volobuev/Otus.MqRabbit/blob/master/%D0%A0%D0%B5%D0%B4%D0%B0%D0%BA%D1%82%D0%BE%D1%80%20%D0%BD%D0%B0%D1%81%D1%82%D1%80%D0%BE%D0%B5%D0%BA.png)

## Установка и использование
Клонируем репозиторий, открываем любой IDE (Visual Studio, Rider и т.д.) и запускаем несколько проектов (ProducerApp и WebApi).
