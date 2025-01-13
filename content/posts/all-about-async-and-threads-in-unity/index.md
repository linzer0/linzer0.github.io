+++
date = '2025-01-09T12:46:38+07:00'
draft = true
title = 'All About Async and Threads in Unity'
+++

# Want to Know : Async, Coroutine, Threads,

# Серия статей Want to Know / Хочу знать:
На русском же будет что-то типа Хочу Знать: (забавно, типа ХЗ)

## Цели статьи

Хочется начать с того, что я хочу в рамках этой статьи раскрыть:

# Что такое поток и многопоточность?
// А вот стоит в такую базу уходить?
// Если только коротко и дать ссылки может на другие статьи
Как выполнять задачи?


## Introduction

Всем привет! Хочу поздравить вас с началом нового цикла статей Хочу Знать, (сокращенно ХЗ).

Начнем мы с темы которая набила оскомину во многих местах, часто применяется и вообще, черт бы ногу сломал (кто-то начинает тащить еще и UniRX или другую хуйню), 

Представляю вашему вниманию Хочу Знать: Async, Coroutine, Threads, Nuances.

## Async Content Plan
    https://docs.unity3d.com/6000.0/Documentation/Manual/async-await-support.html

    Точно что такое async:
        1. Работа с сетью
    https://docs.unity3d.com/6000.0/Documentation/Manual/async-await-support.html
    https://support.unity.com/hc/en-us/articles/208707516-Why-should-I-use-Threads-instead-of-Coroutines

Начнем мы с разбора async/await которая является инструментов языка C#,
Я не буду сильно углубляться в то как это все работает, оставлю ссылку на хорошую статью про [habr](https://habr.com/ru/articles/470830/).

# XD
Асинхронное программирование позволяет вашему коду выполнять длительные задачи, не блокируя основной поток. Это позволяет вашему приложению оставаться отзывчивым и выполнять другие задачи, ожидая завершения асинхронной задачи.

Unity поддерживает упрощенную модель асинхронного программирования с использованием ключевого слова .NET async и оператора await.

Перед чтением об асинхронном программировании в Unity убедитесь, что вы понимаете основные элементы асинхронного программирования в .NET. 

Для важного контекста обратитесь к разделам Асинхронное программирование с async и await и Модель асинхронного программирования задач.

Как правило, это связано с кодом который работает 

## Coroutine
    https://discussions.unity.com/t/coroutine-vs-async-method/671201
    https://stackoverflow.com/questions/1934715/difference-between-a-coroutine-and-a-thread

    Что такое Corotuine:
        0. Что за глупое имя, корутина?
        1. Почему плохо использовать всякие строковые значения типа StartCorotuine("Heklki");
        2. Что они работают главный поток

## Threads
    https://docs.unity3d.com/ru/2018.4/Manual/JobSystemMultithreading.html

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

