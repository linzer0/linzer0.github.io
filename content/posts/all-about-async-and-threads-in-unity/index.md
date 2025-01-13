+++
date = '2025-01-09T12:46:38+07:00'
draft = true
title = 'All About Async and Threads in Unity'
+++

# Want to Know : Async, Coroutine, Nuances

## Introduction

Всем привет! Хочу поздравить вас с началом нового цикла статей Хочу Знать, (сокращенно ХЗ).

Начнем мы с темы которая набила оскомину во многих местах, часто применяется и вообще, черт бы ногу сломал (кто-то начинает тащить еще и UniRX или другую хуйню), 

Представляю вашему вниманию Хочу Знать: Async, Coroutine, Threads, Nuances.

## Выдача базы


Перед тем как мы начнем говорить, давайте вспомним, что Unity API не потокобезопасно,
что означает не возможность вызывать его из других потоков.  (если вы попытаетесь это сделать, вас ударят по руке, ай-яй-яй, не надо так).

Но вы можете использовать потоки в Unity для других целей, вычисления, работа с нативными плагинами.

## Async
Что такое async? Как правило, этот вопрос начинают спрашивать где-то в середине собеседования на позицию разработчика, а потом еще спрашивают что такое await?

А если серьезно, то async/await это высшая ступень эволюции Паттернов асинхронного программирования из мира .Net.

Все это дело называется Task-based Asynchronous Pattern (TAP), которые как раз ввели нам Task<>.

Пусть вас не смущает слово async, оно также выполняется в главном потоке. 

https://docs.unity3d.com/6000.0/Documentation/Manual/async-await-support.html

    Точно что такое async:
        1. Работа с сетью
    https://docs.unity3d.com/6000.0/Documentation/Manual/async-await-support.html
    https://support.unity.com/hc/en-us/articles/208707516-Why-should-I-use-Threads-instead-of-Coroutines

Начнем мы с разбора async/await которая является инструментов языка C#,
Я не буду сильно углубляться в то как это все работает, оставлю ссылку на хорошую статью про [habr](https://habr.com/ru/articles/470830/).

## Coroutine
    https://discussions.unity.com/t/coroutine-vs-async-method/671201
    https://stackoverflow.com/questions/1934715/difference-between-a-coroutine-and-a-thread

    Что такое Corotuine:
        0. Что за глупое имя, корутина?
        1. Почему плохо использовать всякие строковые значения типа StartCorotuine("Heklki");
        2. Что они работают главный поток

## Nuances
    Разница, подводные камни. 
    Понимание что такое main thread unity. 
    Способы работы с ним в асинхронном/многопоточном контекстах.
    Sync context
    https://learn.microsoft.com/en-us/dotnet/api/system.threading.synchronizationcontext?view=net-5.0
    https://learn.microsoft.com/en-us/archive/msdn-magazine/2011/february/msdn-magazine-parallel-computing-it-s-all-about-the-synchronizationcontext

    Thread dispatcher
    Кто такой Thread Dispatcher и зачем мы убираем патчи?

    https://docs.unity3d.com/6000.0/Documentation/ScriptReference/Awaitable.html

