+++
date = '2025-01-09T12:46:38+07:00'
draft = true
title = 'All About Async and Threads in Unity'
+++

# Серия статей All About:
На русском же будет что-то типа Хочу Знать: (забавно, типа ХЗ)

Прочитай ХЗ про асинки



### Цели статьи

Хочется начать с того, что я хочу в рамках этой статьи раскрыть:A

Что такое поток?

Что такое многопоточность и как он используется в игровых движках?

Точно что такое async:
    1. Работа с сетью

Что такое Corotuine:

    0. Что за глупое имя, корутина?
    1. Почему плохо использовать всякие строковые значения типа StartCorotuine("Heklki");
    2. Что они работают главный поток


## Resources


## Documentation


Дальше уже идет сам контент статьи.

# Что такое паралельность и последовательность? Concurrency and parallelism?

    https://stackoverflow.com/questions/1050222/what-is-the-difference-between-concurrency-and-parallelism

# Что такое поток и многопоточность?
// А вот стоит в такую базу уходить?
// Если только коротко и дать ссылки может на другие статьи
Как выполнять задачи?

# Что происходит в Unity?
    https://support.unity.com/hc/en-us/articles/208707516-Why-should-I-use-Threads-instead-of-Coroutines

## Single threads
    Sync context
    https://learn.microsoft.com/en-us/dotnet/api/system.threading.synchronizationcontext?view=net-5.0
    https://learn.microsoft.com/en-us/archive/msdn-magazine/2011/february/msdn-magazine-parallel-computing-it-s-all-about-the-synchronizationcontext

    Thread dispatcher
    Кто такой Thread Dispatcher и зачем мы убираем патчи?

## Coroutine
    https://discussions.unity.com/t/coroutine-vs-async-method/671201
    https://stackoverflow.com/questions/1934715/difference-between-a-coroutine-and-a-thread
## Async
    https://docs.unity3d.com/6000.0/Documentation/Manual/async-await-support.html
## Threads
    https://docs.unity3d.com/ru/2018.4/Manual/JobSystemMultithreading.html
## Awaitable
    https://docs.unity3d.com/6000.0/Documentation/ScriptReference/Awaitable.html

