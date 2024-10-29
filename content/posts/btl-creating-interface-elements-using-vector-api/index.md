+++
date = '2024-10-29T11:05:11+07:00'
draft = true
title = 'Between The Lines: Creating Interface Elements Using Vector Api'
hideSummary = true
[cover]
    image = "0.png"
    alt = "The alt text"
    caption = ""
    relative = false
+++

In the recent past, I've talked about [how you can create your own custom elements through mash generation](https://dtf.ru/gamedev/2215784-razdelyaem-i-vlastvuem-nad-interfeisami-v-unity), it will be useful to read to understand many aspects of this article.

And today, we will be creating custom elements for interfaces, but using the **VectorApi** in the **UI Toolkit** of the **Unity** engine.

After reading this article you will learn:

1.  What is **painter2D**

2.  How to create elements using it

3.  How to surprise your colleagues with your knowledge of interface creation



## Chapter 1: Lines!

Just like in the previous part, we will write our own classes, inheriting from the [VisualElement](https://docs.unity3d.com/ScriptReference/UIElements.VisualElement.html) _(is the base class for creating a custom interface element)_ class.

Let's go from simple to complex, using a code example:


```csharp
using UnityEngine;
using UnityEngine.UIElements;

namespace CustomElements
{
    public class EmojiIconElement : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<EmojiIconElement> { }

        public EmojiIconElement()
        {
            generateVisualContent += GenerateVisualContent;
        }
        
        private void GenerateVisualContent(MeshGenerationContext mgc)
        {
            var top = 0;
            var left = 0f;
            var right = contentRect.width;
            var bottom = contentRect.height;
            
            var painter2D = mgc.painter2D;
            painter2D.lineWidth = 10.0f;
            painter2D.strokeColor = Color.white;
            painter2D.lineJoin = LineJoin.Bevel;
            painter2D.lineCap = LineCap.Round;
            

            painter2D.BeginPath();
            
            painter2D.MoveTo(new Vector2((float)(left + (contentRect.width * 0.2)), (float)(top + contentRect.height * 0.1)));
            painter2D.LineTo(new Vector2((float)(left + (contentRect.width * 0.2)), (float)(bottom * 0.7)));
            
            painter2D.MoveTo(new Vector2((float)(right - (contentRect.width * 0.2)), (float)(top + contentRect.height * 0.1)));
            painter2D.LineTo(new Vector2((float)(right - (contentRect.width * 0.2)), (float)(bottom * 0.7)));
            
            painter2D.MoveTo(new Vector2((float)(left + (contentRect.width * 0.3)), (float)(bottom * 0.8)));
            painter2D.LineTo(new Vector2((float)(right - (contentRect.width * 0.3)), (float)(bottom * 0.8)));
            
            painter2D.Stroke();

       }
    }
}
```
In this case, we will consider the **EmojiIconElement** class.

![alt](https://habrastorage.org/getpro/habr/upload_files/955/f5f/527/955f5f527d7ecf12c07471704a8aeeff.png)

Yes, that's the interpretation of this smiley face – |_|

Within this chapter, the `GenerateVisualContent(...)` method and its internals will be covered, and I have already covered the constructor, base class and `UxmlFactory` in the previous article, in the [**Mesh and Triangle**](https://dtf.ru/gamedev/2215784-razdelyaem-i-vlastvuem-nad-interfeisami-v-unity) chapter!

I won't drag on with the introduction, let's look at our code.

```csharp
private void GenerateVisualContent(MeshGenerationContext mgc)
{
    var top = 0;
    var left = 0f;
    var right = contentRect.width;
    var bottom = contentRect.height;
            
    var painter2D = mgc.painter2D;
    painter2D.lineWidth = 10.0f;
    painter2D.strokeColor = Color.white;
    painter2D.lineJoin = LineJoin.Bevel;
    painter2D.lineCap = LineCap.Round;
            

    painter2D.BeginPath();
            
    painter2D.MoveTo(new Vector2((float)(left + (contentRect.width * 0.2)), (float)(top + contentRect.height * 0.1)));
    painter2D.LineTo(new Vector2((float)(left + (contentRect.width * 0.2)), (float)(bottom * 0.7)));
            
    painter2D.MoveTo(new Vector2((float)(right - (contentRect.width * 0.2)), (float)(top + contentRect.height * 0.1)));
    painter2D.LineTo(new Vector2((float)(right - (contentRect.width * 0.2)), (float)(bottom * 0.7)));
            
    painter2D.MoveTo(new Vector2((float)(left + (contentRect.width * 0.3)), (float)(bottom * 0.8)));
    painter2D.LineTo(new Vector2((float)(right - (contentRect.width * 0.3)), (float)(bottom * 0.8)));
            
    painter2D.Stroke();
}
```

As I said earlier, we are interested in the GenerateVisualContent method.

Briefly, it is activated when our **VisualElement** needs to display our element or regenerate itself _(this usually happens if there have been changes to the UI element).

The four variables `top, left, right, bottom` are needed to simplify the handling of positions within our UI element.

It is also important to note that these variable values will change depending on the size of the UI element (in UI Builder / build), and because of this our UI element will scale relative to the size of the screen and the element itself.

![alt](https://habrastorage.org/getpro/habr/upload_files/f1c/1f7/d0a/f1c1f7d0a185fc888afbd63acdd9ac07.png "Именно на этом самом contentRect'e и будет происходить наша генерация.")

It is on this very contentRect that our generation will take place.

Next we have the description of our [**painter2D**](https://docs.unity3d.com/ScriptReference/UIElements.Painter2D.html) and here we will stop in detail.

In official language, this is a class that allows you to draw vector graphics.

_- How exactly do you draw vector graphics?_

_- Good question!_

It provides various API calls that can be used to draw lines, arcs, curves.

Also, it has various properties that affect the result of sketching:

- lineWidth - responsible for the line width

- strokeColor - stroke color

- fillColor - fill color

- lineJoin - how the line will look like when connected

- lineCap - how the line ends will look like


![alt](https://habrastorage.org/getpro/habr/upload_files/8d4/8a3/b35/8d48a3b35d97b1e76172bef6706b4ad1.png "Более наглядно как это выглядит")

More clearly how it looks like

We have decided how to prepare our **painter2D**, now let's see how paths are handled in graphical programming.

In the context of graphical programming, a “path” is a sequence of geometric shapes such as lines, curves, rectangles and circles that define the shape or contour of an object.

Путь может быть открытым или закрытым.

-   **Открытый путь:** Начинается и заканчивается без соединения конечных точек.

-   **Закрытый путь:** Конечные точки пути соединены, образуя замкнутую форму.


1.  **Начало нового пути**: Этот шаг определяет начало нового векторного пути. При вызове `BeginPath()` вы начинаете записывать команды рисования для нового пути.

2.  **Добавление команд рисования:** После начала нового пути вы добавляете команды рисования, такие как `ArcTo()`, `LineTo()`, и другие, для создания форм и геометрических объектов в вашем пути.

3.  **Завершение пути**: После того как вы нарисовали все необходимые фигуры и геометрические объекты для текущего пути, вы вызываете `ClosePath()` для завершения этого пути.  
    Это указывает графическому движку на то, что вы закончили рисование этого пути, и что он должен его отрисовать.


Давайте теперь вернемся к нашему примеру и разберем, что мы делаем:

```csharp
painter2D.BeginPath();
            
painter2D.MoveTo(new Vector2((float)(left + (contentRect.width * 0.2)), (float)(top + contentRect.height * 0.1)));
painter2D.LineTo(new Vector2((float)(left + (contentRect.width * 0.2)), (float)(bottom * 0.7)));
            
painter2D.MoveTo(new Vector2((float)(right - (contentRect.width * 0.2)), (float)(top + contentRect.height * 0.1)));
painter2D.LineTo(new Vector2((float)(right - (contentRect.width * 0.2)), (float)(bottom * 0.7)));
            
painter2D.MoveTo(new Vector2((float)(left + (contentRect.width * 0.3)), (float)(bottom * 0.8)));
painter2D.LineTo(new Vector2((float)(right - (contentRect.width * 0.3)), (float)(bottom * 0.8)));
            
painter2D.Stroke();
```

Мы начинаем с команды `BeginPath()` , после чего вызываем метод `MoveTo(Vector2 pos)` – который перемещает точку рисования на новую позицию, от которой будут выполняться следующие команды.

Следом за ней идет метод `LineTo(Vector2 pos)` , как можно понять из названия, оно проводит прямую линию из текущей позицию `painter2D` до позиции заданный в аргументе метода.

Далее идет две пачки команды, которые перемещают курсор рисования и чертят линию.

В конце, мы можем заметить метод `Stroke()` – который непосредственно отрисовывает контур текущего пути, который мы определили ранее.

После вызова метода текущий контур будет отрисован на холсте с помощью текущего стиля обводки, такого как цвет и толщина линии.

Поздравляю, теперь у вас есть кастомный UI элемент!

## Глава 2: Кривые!

У нас есть два разных варианта возможности нарисовать кривые линии:

1.  Метод `BezierCurveTo()` генерирует кубическую кривую Безье по двум контрольным точкам и конечному положению кубической кривой Безье.

2.  Метод `QuadraticCurveTo()` генерирует квадратичную кривую Безье по контрольной точке и конечному положению квадратичной кривой Безье.


Рассмотрим их использование:

```csharp
painter2D.BeginPath();
painter2D.MoveTo(new Vector2(100, 100));
painter2D.BezierCurveTo(new Vector2(150, 150), new Vector2(200, 50), new Vector2(250, 100));
painter2D.Stroke();
```

![Кривая Безье](https://habrastorage.org/getpro/habr/upload_files/e96/5dc/4a2/e965dc4a2213dba9f3d19973cc4a23cd.png "Кривая Безье")

Кривая Безье

И также приведем код для второго примера:

```csharp
painter2D.BeginPath();
painter2D.MoveTo(new Vector2(100, 100));
painter2D.QuadraticCurveTo(new Vector2(150, 150), new Vector2(250, 100));
painter2D.Stroke();
```

![Квадратичная кривая Безье](https://habrastorage.org/getpro/habr/upload_files/058/b08/55a/058b0855a5dbdb1b7628dc1b816dd59a.png "Квадратичная кривая Безье")

Квадратичная кривая Безье

Для более глубокого понимания кривых Безье, читаем [Wikipedia](https://ru.wikipedia.org/wiki/%D0%9A%D1%80%D0%B8%D0%B2%D0%B0%D1%8F_%D0%91%D0%B5%D0%B7%D1%8C%D0%B5).

## Глава 3: Дуги!

Для рисования дуг можно использовать следующие методы:

1.  Метод `Arc()` создает дугу на основе предоставленного центра дуги, радиуса, а также начального и конечного углов.

2.  Метод `ArcTo()`создает дугу между двумя прямыми сегментами.


В рамках рисования дуг, так же стоит рассказать про заливку пути.

Когда в конце пути мы получаем [замкнутую фигуру](closedPath), тогда мы можем ее покрасить в какой-то цвет, вызвав метод `painter2D.Fill()`.

Рассмотрим пример построения дуги используя метод `Arc()` .

```csharp
painter2D.lineWidth = 2.0f;
painter2D.strokeColor = Color.red;
painter2D.fillColor = Color.blue;

painter2D.BeginPath();

painter2D.MoveTo(new Vector2(100, 100));


painter2D.Arc(new Vector2(100, 100), 50.0f, 10.0f, 95.0f);
painter2D.ClosePath();


painter2D.Fill();
painter2D.Stroke();
```

И как раз, параметр `painter2D.FillColor` отвечает какой цвет будет у залитой области.

![Красная обводка и синяя заливка](https://habrastorage.org/getpro/habr/upload_files/609/08c/193/60908c1935182d6b22f1d237fd11ffb4.png "Красная обводка и синяя заливка")

Красная обводка и синяя заливка

А используя метод `painter2D.ArcTo()`, можно нарисовать кривую:

```csharp
painter2D.BeginPath();
painter2D.MoveTo(new Vector2(100, 100));
painter2D.ArcTo(new Vector2(150, 150), new Vector2(200, 100), 20.0f);
painter2D.LineTo(new Vector2(200, 100));
painter2D.Stroke();
```

![Кривая через дугу](https://habrastorage.org/getpro/habr/upload_files/07f/7b8/629/07f7b862986f28c95b92f16678f1cc67.png "Кривая через дугу")

Кривая через дугу

Вы наверное могли задуматься, а можно ли используя метод `Arc()` , построить окружность или круговую диаграмму.

_– Ну, конечно, можно._

С ней вы можете ознакомиться из [официальной документации](https://docs.unity3d.com/2022.3/Documentation/Manual/UIE-pie-chart.html) от Unity.

## Глава 4: Остальное?

Здесь хочу рассмотреть, что не вошло в другие главы, и другие комментарии по отрисовке.

Первое, о чем хочется поговорить, это про дыры в заливке.

Когда вы вызываете `Fill()` для закраски области, содержащейся внутри пути, вы также можете создать "дыры" в этой закрашенной области, используя дополнительные подпути.

Чтобы создать дыру, вы должны создать дополнительный подпуть с помощью `MoveTo()`, а затем использовать правило заливки (fill rule), чтобы определить, какие области будут закрашены, а какие нет.

Вот два основных правила заливки:

1.  **OddEven (Нечетное/Четное):** Отрисовывается луч из данной точки в бесконечность в любом направлении и подсчитываются количество пересечений сегментов пути. Если количество пересечений нечетное, то точка считается внутри пути, если четное - снаружи.

2.  **NonZero (Не нуль):** Отрисовывается луч из данной точки в бесконечность в любом направлении, и подсчитываются пересечения сегментов пути. При этом, когда сегменты пересекают луч справа налево, счетчик уменьшается, а когда слева направо - увеличивается. Если счетчик равен нулю, то точка считается снаружи пути, иначе - внутри.


Таким образом, вы можете использовать эти правила, чтобы создать дыру в заполненной области, описав подпуть, который определяет контур этой дыры, и используя правило заливки для указания, как области должны быть закрашены.

В приведенном коде создается прямоугольник с дополнительным подпутем, который определяет форму ромба (бриллианта) внутри прямоугольника. Этот ромб будет являться "дырой" в заполненной области прямоугольника.

```csharp
painter2D.BeginPath();
painter2D.MoveTo(new Vector2(10, 10));
painter2D.LineTo(new Vector2(300, 10));
painter2D.LineTo(new Vector2(300, 150));
painter2D.LineTo(new Vector2(10, 150));
painter2D.ClosePath();

painter2D.MoveTo(new Vector2(150, 50));
painter2D.LineTo(new Vector2(175, 75));
painter2D.LineTo(new Vector2(150, 100));
painter2D.LineTo(new Vector2(125, 75));
painter2D.ClosePath();

painter2D.Fill(FillRule.OddEven);
```

![Прямоугольник с отверстием внутри](https://habrastorage.org/getpro/habr/upload_files/da7/a20/d44/da7a20d448dc1c34ff18dd5cc4ee6b0a.png "Прямоугольник с отверстием внутри")

Прямоугольник с отверстием внутри

Второе, это возможность настраивать стили в каждом подпути.

Для этого нужно использовать методы `BeginPath()` и `ClosePath()` и между ними менять значения у `painter2D`.

```csharp
private void GenerateVisualContent(MeshGenerationContext mgc)
{
    var painter2D = mgc.painter2D;
    painter2D.lineWidth = 10.0f;

    // Начало первого подпути
    painter2D.BeginPath();
    painter2D.strokeColor = Color.red;
  
    painter2D.MoveTo(new Vector2(50, 50));
    painter2D.LineTo(new Vector2(100, 100));
  
    painter2D.Stroke();
    painter2D.ClosePath();
    // Конец первого подпутя

    // Начало второго подпути
    painter2D.BeginPath();
    painter2D.strokeColor = Color.blue;
  
    painter2D.MoveTo(new Vector2(20, 20));
    painter2D.LineTo(new Vector2(60, 60));
    
    painter2D.Stroke();
    painter2D.ClosePath();
    // Конец второго подпутя

    // Начало третьего подпути
    painter2D.BeginPath();
    painter2D.strokeGradient = new Gradient()
    {
        colorKeys = new GradientColorKey[]
        {
            new() { color = Color.red, time = 0.0f },
            new() { color = Color.blue, time = 1.0f }
        }
    };
    painter2D.fillColor = Color.green;
    
    painter2D.MoveTo(new Vector2(50, 150));
    painter2D.LineTo(new Vector2(100, 200));
    painter2D.LineTo(new Vector2(150, 150));
    
    painter2D.Fill();
    painter2D.Stroke();
            
    painter2D.ClosePath();
    // Конец третьего подпутя
}
```

![Три разных стиля](https://habrastorage.org/getpro/habr/upload_files/378/bf4/404/378bf4404732002951c86d2ad1e39220.png "Три разных стиля")

Три разных стиля

Внимательный зритель уже заметил следующую фишку – поддержка градиента для обводки.

```csharp
painter2D.strokeGradient = new Gradient()
{
    colorKeys = new GradientColorKey[]
    {
        new() { color = Color.red, time = 0.0f },
        new() { color = Color.blue, time = 1.0f }
    }
};
```

Используя свойство `strokeGradient` можно рисовать обводку через градиент.

## Глава 5: Финал!

Поздравляю с тем, что вы дочитали эту статья до конца.

Сегодня мы разобрались как можно рисовать кастомные элементы в интерфейсах, какой инструментарий для этого имеется в движке Unity.

Для более глубокого погружения и изучения создания элементов интерфейса, рекомендую официальную [документацию](https://docs.unity3d.com/2022.3/Documentation/Manual/UIE-ui-renderer.html).

Если у вас остались вопросы или не поняли какую-то часть, напишите в комментариях, постараюсь объяснить, что да как :)

Спасибо за внимание и до скорых встреч