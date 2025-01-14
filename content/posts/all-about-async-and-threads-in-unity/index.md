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

Основная мотивация использования асинхронного программирования это операции/задачи, которые занимают какое-то время для выполнения.
Такими могут быть:
1. Сетевые операции (GET, POST)
2. Работа с ресурсами (загрузка ассетов, бандлов)

В синхронном подходе, мы блокируем дальнейшее выполнение программы и ждем результата программы.

Асинхронный подход позволяет вместо блокирования потока, сообщить что мы ожидаем завершение операции после чего выполнить что-то еще и освободить поток для дальнейшего выполнения программы.

Давайте рассмотрим какой-то кодовый пример, мы же все таки программисты, ек-макарек:

/// Можно привести в пример что-то более осозннаное например вызов UnityWebRequest 

```csharp

public class WaiterClass : MonoBehaviour
{
    void Start()
    {
        await WaitSomeTimeAsync(10);
    }

    public async Task WaitSomeTimeAsync(float time)
    {
        Debug.Log("Wait starts now");
        await Task.Delay(time);
        Debug.Log("Wait ends now");
    }
}

```
/// Тут реально посидеть и потыкаться, как это в действительнсости работает, всякие // async void и всякие best practices

Как видите, у нас есть метод с добавление async и когда мы его вызываем, мы можем написать await 

    Точно что такое async:
        1. Работа с сетью
    https://docs.unity3d.com/6000.0/Documentation/Manual/async-await-support.html
    https://support.unity.com/hc/en-us/articles/208707516-Why-should-I-use-Threads-instead-of-Coroutines

    Я не буду сильно углубляться в то как это все работает, оставлю ссылку на хорошую статью про [habr](https://habr.com/ru/articles/470830/).

## Coroutine

Как говорил Пауло Коэльо, "Если вы думаете, что async опасны, попробуйте корутины – это смертельно"

Давайте также повторим, что Coroutine также выполняется в главном потоке.

Это уже фича непосредственно Unity Engine и они непосредственно связаны к классу MonoBehaviour'а.

(Не легко будет запустить его из других мест. Также отдельно для Editor'а есть свой [пакет](https://docs.unity3d.com/Packages/com.unity.editorcoroutines@1.0/manual/index.html))

Давайте же посмотрим, как нам съесть корутины по частям?

Начнем с того, чтобы запустить корутину, мы будем использовать команду **StartCoroutine()**.

Но как гласит правило автомобилиста, перед тем как поехать, узнай как остановиться – для этого нам нужна StopCoroutine();.

Все просто, две команда, газ и тормоз, все таки не rocket science, а всего лишь асинхронные операции – делов на пару минут.



Рути
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

