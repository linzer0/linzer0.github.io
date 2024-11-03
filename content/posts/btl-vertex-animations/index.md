+++
date = '2024-11-03T15:09:15+07:00'
draft = true
title = 'Between The Lines: Vertex Animations'
hideSummary = true
[cover]
image = "0.png"
alt = "The alt text"
caption = ""
relative = false
+++

Здравствуйте, уважаемые господа. Меня зовут Линар Хилажев, я программирую логику, интерфейсы и игры.

Хочу кое-что интересное рассказать... значит начались эти события после того как была написана статья [Между строк: Создание элементов интерфейса](https://habr.com/ru/articles/803651/).

Значит, потом, ну наитие такое появилось, что нужно написать определенную статью, желательно про анимации.

Без лишних слов встречайте, анимации в **Unity UI Toolkit**.

## Chapter 1: Animation concept

Давайте начнем с объяснения того, как будет работать анимация для наших элементов интерфейса.

Здесь речь не идет о переходах/задержках и других встроенных возможностях анимации в UI Toolkit.

Из предыдущих статей мы уже научились создавать настраиваемые элементы пользовательского интерфейса. Теперь пришло время добавить немного динамичности.

Стоит отметить, что генерация анимации является довольно ресурсоемким процессом по сравнению с другими операциями, так как она будет нагружать процессор практически на каждом кадре.

Если разложить анимацию на базовые компоненты, то это будет изменение состояния объекта (размер, форма, цвет, положение и т.д.).

Первая задача, перед которой мы стоим, - научиться вызывать внутренние методы класса **VisualElement** с определенным интервалом времени.

Для этого мы будем использовать внутренюю реализацию интерфейса **IVisualElementScheduler**.

Из названия можно догадаться, что оно занимается планированием и выполнением каких-то действий, давайте разбираться как с ним работать.
```csharp
public RombElement()
{
    schedule.execute(MethodName);
}
```

В этом примере кода, при создание элемента мы обращаемся к методу **Execute** передавая в него **Action**.

Это значит, что этот метод будет вызван сразу после выполнения конструктора.

Стоит понимать, что выполнен он будет только один раз, а как нам сделать многократные вызовы?

– Я рад, что вы спросили, отвечаю:

```csharp
public RombElement()
{
    schedule.Execute(MethodName).Every(16);
}
```

Мы добавляем к нашему вызову **.Every**, что означает, наш метод будет вызываться каждые X милисекунд.

С базой разобрались, давайте усложнять.

## Chapter 2: Color animation

Под анимацией цвета мы будем обозначать изменение цвета/прозрачности и связанное с его составляющимим у **UI Element'a**

Давайте сделаем так, чтобы наш элемент проявлялся из прозрачности.

Как и всегда, приведу весь код, а потом будет разбирать детально.

```csharp
using UnityEngine;
using UnityEngine.UIElements;

namespace CustomElements
{
    [UxmlElement]
    public partial class RombElement : VisualElement
    {
        private float _timeLeft;
        
        private const float AlphaValue = 255;
        
        [UxmlAttribute] public float AnimationTime = 3f;

        public RombElement()
        {
            generateVisualContent += GenerateVisualContent;

            _timeLeft = AnimationTime;
            schedule.Execute(ChangeColorAnimation).Every(16);
        }

        private void ChangeColorAnimation()
        {
            _timeLeft -= Time.deltaTime;
            
            firstColor.a = (byte)Mathf.Lerp(firstColor.a, AlphaValue, Time.fixedDeltaTime / _timeLeft);
            secondColor.a = (byte)Mathf.Lerp(secondColor.a, AlphaValue, Time.deltaTime / _timeLeft);
            thirdColor.a = (byte)Mathf.Lerp(thirdColor.a, AlphaValue, Time.fixedDeltaTime / _timeLeft);
            fourColor.a = (byte)Mathf.Lerp(fourColor.a, AlphaValue, Time.deltaTime / _timeLeft);

            MarkDirtyRepaint();
        }

        Vertex[] vertices = new Vertex[4];
        ushort[] indices = { 0, 1, 2, 2, 3, 0 };

        private Color32 firstColor  = new (255, 0, 0, 0);
        private Color32 secondColor  = new (0, 255, 0, 0);
        private Color32 thirdColor  = new (0, 0, 255, 0);
        private Color32 fourColor  = new (17, 55, 55, 0);

        void GenerateVisualContent(MeshGenerationContext mgc)
        {
            vertices[0].tint = firstColor;
            vertices[1].tint = secondColor;
            vertices[2].tint = thirdColor;
            vertices[3].tint = fourColor;

            var top = 0;
            var left = 0f;
            var middleX = contentRect.width / 2;
            var middleY = contentRect.height / 2;
            var right = contentRect.width;
            var bottom = contentRect.height;

            vertices[0].position = new Vector3(left, middleY, Vertex.nearZ);
            vertices[1].position = new Vector3(middleX, top, Vertex.nearZ);
            vertices[2].position = new Vector3(right, middleY, Vertex.nearZ);
            vertices[3].position = new Vector3(middleX, bottom, Vertex.nearZ);

            MeshWriteData mwd = mgc.Allocate(vertices.Length, indices.Length);
            mwd.SetAllVertices(vertices);
            mwd.SetAllIndices(indices);
        }
    }
}
```

Обратим наше внимание на конструктор класса, мы там можем заметить вызов метода **scheduler'а**.

Мы хотим вызывать метод **ChangeColorAnimation** каждые 16 ms.

Дальше идет само тело метода анимации, в нем мы можем заметить **_timeLeft**, это переменная для отслеживания времени анимации, которое изначально равно **AnimationTime**.

Кстати, его можно настроить напрямую через **UI Builder**, благодаря новому аттрибуту **[UxmlAttribute]**.

Он появился в новой версии пакета, подробнее о нововведениях можно прочитать в моей [статье](https://dtf.ru/gamedev/2540623-chto-neset-nam-den-gryadushii-ili-novye-vozmozhnosti-ui-toolkita-s-kodom-i-kartinkami).

Мы немного отвлеклись, вернемся к нашим анимациям.

```csharp
private void ChangeColorAnimation()
{
    _timeLeft -= Time.deltaTime;
    
    firstColor.a = (byte)Mathf.Lerp(firstColor.a, AlphaValue, Time.fixedDeltaTime / _timeLeft);
    secondColor.a = (byte)Mathf.Lerp(secondColor.a, AlphaValue, Time.deltaTime / _timeLeft);
    thirdColor.a = (byte)Mathf.Lerp(thirdColor.a, AlphaValue, Time.fixedDeltaTime / _timeLeft);
    fourColor.a = (byte)Mathf.Lerp(fourColor.a, AlphaValue, Time.deltaTime / _timeLeft);

    MarkDirtyRepaint();
}
```

Дальше у нас идет четыре строки, где мы описываем, что оттенки наших вершин должны стремится от 0 к 255.

После чего, мы вызываем метод **MarkDirtyRepaint()**, для того чтобы спровоцировать вызов метода **GenerateVisualContent(...)**

{{< video src="firstColorAnimation.webm" type="video/webm" preload="auto" >}}

## Chapter 3: New Horizons
Давайте признаемся, анимация цвета - это самое простое, что можно придумать.

А что если у нас есть заявка на непосредственность? К примеру, мы хотим анимировать иконки/спрайты.
Иногда в голову прилетают интересные вещи, такие как мысли или более тяжелые, как анимация иконок без кадровой анимации.

В таком случае рассмотрим следующий пример, предположим, мы хотим чтобы у нас была анимация для колокола.

![](https://habrastorage.org/r/w1560/webt/ll/6q/uh/ll6quhcji_lsuigdyl9oucpgnbm.png)

Концептуально, мы не делаем ничего сложного, всего лишь меняем значение **rotate** у **VisualElement** и изменим центр масс.

Зачем менять центр у элемента? Чтобы логически было проще с этим работать и анимировать.

Для этого нам нужно изменить свойство **transform-Origin**, которое принимает два значения (X, Y) , мы это сделаем прямо в конструкторе класса.

![](transform-custom-position.png)

Обычно **transform-Origin** у элемента это **center-center**, то есть (X = 50%, Y = 50%), а мы хотим, чтобы он был **top-center** (X = 0, Y = 50%).

Более подробно можно посмотреть в [документации](https://docs.unity3d.com/Manual/UIE-Transform.html)

Теперь, приступаем к основной части нашего мероприятия, написание самой анимации.

```csharp
using UnityEngine.UIElements;

namespace CustomElements
{
    [UxmlElement]
    public partial class ImageAnimation : VisualElement
    {
        [UxmlAttribute] private float AngleLimit = 25;
        [UxmlAttribute] private bool AnimationIsStopped = true;
        
        private bool _rotationDirectionIsMinus = true;
        
        public ImageAnimation()
        {
            style.transformOrigin = new TransformOrigin( Length.Percent(50), 0);
            schedule.Execute(PlayAnimation).Every(16);
        }

        private void PlayAnimation()
        {
            if (AnimationIsStopped)
                return;
            
            RotateBell();
            MarkDirtyRepaint();
        }

        void RotateBell()
        {
            var newRotate = style.rotate;
            Angle rotateAngle = newRotate.value.angle;

            if (_rotationDirectionIsMinus)
            {
                rotateAngle.value--;
                if (rotateAngle.value < -AngleLimit)
                {
                    _rotationDirectionIsMinus = false;
                }
            }
            else if (_rotationDirectionIsMinus == false)
            {
                rotateAngle.value++;
                 if (rotateAngle.value > AngleLimit)
                 {
                     _rotationDirectionIsMinus = true;
                 }
            }
            
            newRotate.value = new Rotate(rotateAngle);
            style.rotate = newRotate;
        }
    }
}
```

Так выглядит весь класс, наш взор падает на метод **RotateBell()**

Основной составляющей анимации в данном случае является изменение поворота изображения.
Мы вращаем изображение влево и вправо до тех пор, пока не достигнем предела AngleLimit.

Для этого мы изменяем значение свойства **style.rotate** и затем вызываем метод **MarkDirtyRepaint()**, чтобы нам перерисовали **VisualElement**.

Получаем такой результат:

{{< video src="simple-rotate-animation.webm" type="video/webm" preload="auto" >}}


## Chapter 4: Между линий

На этой замечательной ноте, хочется завершить наши исследовательские работы по интерфейсам, это было хорошо пока продложалось.
Также хочется дождаться релиза интересных фичей из [UI Roadmap'а](https://unity.com/roadmap/unity-platform/ui) , например Advanced UI Animation
Думаю, в целом, было интересно позаниматься вопросами тулинга и автоматизации разного внутренних задач в Unity.

Это то самое, знаменитое **Tool Programmer**, было было славно охватить сторону CI/CD.

Да-да, это анонс серии статей "Я не буду это делать руками: Создаем первые инструменты в разработке игр"
