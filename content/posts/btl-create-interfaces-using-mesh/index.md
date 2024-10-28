+++
date = '2024-10-28T11:12:23+07:00'
draft = false 
title = 'Between The Lines: Create Interfaces Using Mesh'
hideSummary = true
[cover]
    image = "0.png"
    alt = "The alt text"
    caption = ""
    relative = false
+++

# Divide and conquer User Interface in Unity

I tell you how to create custom UI elements through Mesh creation using **Unity** and **UI Toolkit**.

## What do we do?
Within the framework of this article, we will analyze the possibilities of generating custom UI elements using the **UI Toolkit**.

First, we'll take a closer look at creating a triangle based on mash generation, and second - a gradient rhombus!

## Chapter 1: Mesh and Triangle!
Within this chapter, let's look at how to make a custom triangle element, as well as tell you in general how this system works.

So, read the code and comments to it, so you can repeat it at home: ).

![alt](1.png)

First, the code will be attached (for the smartest and most intricate) and then there will be a detailed breakdown of most of the points, here we go!

```csharp
using UnityEngine;
using UnityEngine.UIElements;

namespace CustomUI
{
    public class TriangleElement : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<TriangleElement>
        {
        };

        public TriangleElement()
        {
            generateVisualContent += GenerateVisualContent;
        }

        Vertex[] vertices = new Vertex[3];
        ushort[] indices = { 0, 1, 2 };

        void GenerateVisualContent(MeshGenerationContext mgc)
        {
            vertices[0].tint = Color.red;
            vertices[1].tint = Color.red;
            vertices[2].tint = Color.red;

            var leftCorner = 0f;
            var rightCorner = contentRect.width;
            var top = 0;
            var bottom = contentRect.height;
            var middleX = contentRect.width / 2;

            vertices[0].position = new Vector3(leftCorner, bottom, Vertex.nearZ);
            vertices[1].position = new Vector3(middleX, top, Vertex.nearZ);
            vertices[2].position = new Vector3(rightCorner, bottom, Vertex.nearZ);

            MeshWriteData mwd = mgc.Allocate(vertices.Length, indices.Length);
            mwd.SetAllVertices(vertices);
            mwd.SetAllIndices(indices);
        }
    }
}
```


This is what the code looks like, now let's see what's what!

Let's start with the base, the declaration of our class - **TriangleElement**

```csharp
public class TriangleElement : VisualElement
{
    ....
}
```

To create a custom UI element, inheritance of the **VisualElement** class is used. (There are other ways, but I won't go into details right now).

We will use some methods and data from the parent class, such as **contentRect** and **generateVisualElement**;

Let's move on to **UxmlFactory** and displaying the element in the hierarchy:
```csharp
public new class UxmlFactory : UxmlFactory<TriangleElement> { };
```

This is a feature of the **UI Toolkit**, it is necessary for our **TriangleElement** to be added to the general list of UI elements.

After that, we can easily drag it to the layout window.

![alt](2.png)

More details about creating custom elements can be found in the [documentation](https://docs.unity3d.com/Packages/com.unity.ui.builder@1.0/manual/uib-structuring-ui-custom-elements.html)


That's taken care of, let's move on:
```csharp
public TriangleElement()
{
    generateVisualContent += GenerateVisualContent;
}
```

As many of you know, the method we call the constructor is called when our object is created.

Specifically for us, it subscribes to the **Action** of the parent class (the one mentioned above)

What Action of the parent class?

```csharp
/// <summary>
/// <para>
/// Called when the VisualElement visual contents need to be (re)generated.
/// </para>
/// </summary>
public Action<MeshGenerationContext> generateVisualContent { get; set; }
```

Briefly, it is activated when our **VisualElement** needs to regenerate itself, this usually happens if there were changes in the UI element (change of properties, dimensions) or if the **MarkDirtyRepaint()** method was called

I will also leave a link to the [documentation](https://docs.unity3d.com/ScriptReference/UIElements.VisualElement-generateVisualContent.html)

When we catch this **Action**, we also get **MeshGenerationContext** and thanks to it, we can additionally render geometry on the element.

The method returns us a value of type - **MeshGenerationContext**, we will understand it later.

Let's make a small digression and make an introduction to game engines - let's talk about Mesh.

Mesh is a set of vertices, edges and polygons that define the shape and structure of an object.

To create a custom element, we create this very mash, but only at the interface level and in 2D space.

Next we will analyze the minimum data needed to create such an object:

```csharp
Vertex[] vertices = new Vertex[3];
ushort[] indices = { 0, 1, 2 };
```

Meet our valiant elephants, gentlemen — 'Vertices' and 'Indices'; henceforth, we'll refer to them as vertices and indices.

A Vertex is an internal structure of Unity UI Toolkit, employed in rendering UI elements. The key aspect for us is:

```csharp
public struct Vertex
{
    ...
    public Vector3 position; 
    public Color32 tint; 
    ...
}
```

Under the hood, it utilizes Vector3 for the vertex position and Color32 (32-bit RGBA representation) for the color shade.

In essence, vertices define their spatial coordinates and color.

Next up are the indices, responsible for connecting the mesh vertices.

The majority of game engines and 3D modeling tools employ a triangular mesh approach, where objects are comprised of triangles.

Similarly, in our case, when we state that the indices array is filled – {0, 1, 2}, we imply that the output will be a triangle formed by these vertices.

Hence, the minimum description of a shape involves three numbers to outline a single triangle.

![alt](3.png)

You can read more about the anatomy of the mesh and its structure [here](https://docs.unity3d.com/2022.2/Documentation/Manual/AnatomyofaMesh.html).

Moving along in the code, we've reached the body of our method, which will be invoked upon regenerating our UI element:

```csharp
void GenerateVisualContent(MeshGenerationContext mgc)
{
    vertices[0].tint = Color.red;
    vertices[1].tint = Color.red;
    vertices[2].tint = Color.red;
}
```

Keep in mind that our vertices have a 'tint' field (vertex color), and right here is where we've defined them, setting all to red

Next, we'll determine their positions (who is where):

```csharp
var leftCorner = 0f;
var rightCorner = contentRect.width;
var top = 0;
var bottom = contentRect.height;
var middleX = contentRect.width / 2;

vertices[0].position = new Vector3(leftCorner, bottom, Vertex.nearZ);
vertices[1].position = new Vector3(middleX, top, Vertex.nearZ);
vertices[2].position = new Vector3(rightCorner, bottom, Vertex.nearZ);
```

Let's begin with the fact that the vertex position is described using the Vector3 type (where 3 represents the number of dimensions, i.e., in our case, X, Y, Z).

To specify positions, we need some theoretical support:

Any VisualElement has a rendering area called the [contentRect](https://docs.unity3d.com/ScriptReference/Rect.html).
It has properties for height and width.

I want to draw my triangle always in the center of this area, so that, regardless of the size of the area, my triangle is positioned in the middle.

That's why I'm binding it to the size of the rendering area. If it changes, for example, due to a screen resize, it will trigger the regeneration of the entire UI element. Consequently, this method will be called again, but this time with updated contentRect dimensions.

![alt](4.png)

Here, the origin, as you may have noticed, is located in the top-left corner – position { x = 0, y = 0 }.

The region of our contentRect ends at the values { x = width, y = height }, which is logical overall. The farthest point on the X-axis is equal to the width of the element, and on the Y-axis, it's equal to the height.

So, what did we end up with in terms of vertices?

- Vertex with index 0 is located at coordinates { x = 0, y = height } (bottom-left corner).
- Vertex with index 1 is located at coordinates { x = width / 2, y = 0 } (top center).
- Vertex with index 2 is located at coordinates { x = width, y = height } (bottom-right corner).

After preparing the position and color values, it's finally time to render all of this!

```csharp
MeshWriteData mwd = mgc.Allocate(vertices.Length, indices.Length);
mwd.SetAllVertices(vertices);
mwd.SetAllIndices(indices);
```

Here, in short, we are saying that we want to create a mesh with a certain number of vertices and a specific number of indices.

Then, through the SetAllVertices() method, we define our vertices, and through SetAllIndices(), we assign our indices.

And voilà! We've got ourselves a triangle:


{{< video src="5.webm" type="video/webm" preload="auto" >}}


## Chapter 2: Diamond and Gradient!

Now, after the simple triangle, let's move on to something more interesting – a diamond.

We'll proceed as follows. Below is the code, and we'll provide an overview of only the important parts that have changed.

```csharp
using UnityEngine;
using UnityEngine.UIElements;

namespace CustomUI
{
    public class RombElement : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<RombElement> { }
        
        public RombElement()
        {
            generateVisualContent += GenerateVisualContent;
        }

        Vertex[] vertices = new Vertex[4];
        ushort[] indices = { 0, 1, 2, 2, 3, 0};

        void GenerateVisualContent(MeshGenerationContext mgc)
        {
            vertices[0].tint = new Color32(255, 0, 0, 255);
            vertices[1].tint = new Color32(0, 255, 0, 255);
            vertices[2].tint = new Color32(0, 0, 255, 255);
            vertices[3].tint = new Color32(17, 55, 55, 255);

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

As you can see, our vertices and indices have changed.

Now, we have 4 vertices, which is precisely what we need to create a rectangle, as we recall from geometry lessons.

```csharp
Vertex[] vertices = new Vertex[4];
ushort[] indices = { 0, 1, 2, 2, 3, 0};
```

Now, let's delve into the indices!

Let's break down the indices into triplets, resulting in {0, 1, 2} and {2, 3, 0}.

Now, let's illustrate our vertices and indices.

![For the first triplet, we use a dashed red line, and for the second one – blue.](6.png)

When it comes to the vertex positions, there's nothing new or complex there; it's as straightforward as it gets.

But what's interesting is the gradient and vertex colors!

For the triangle, all vertices had the value of Color.red. We can express this in the RGBA representation, and it would look like this:

```csharp
var redColor = new Color32(255, 0, 0, 255);
```

Yes, we can pass any color values and shades that can be described using RGBA.

But how do we create a gradient then? To answer that, let's address the question – what is this gradient of yours?

A gradient in computer graphics is a type of fill that, based on specified color parameters at key points, calculates intermediate colors for other points.

There are various types of gradients: radial, angular, reflected.

We'll be creating a linear gradient, and it looks like this

![](6.png)

Now, let's get back to our business and break down our vertices and colors:

```csharp
vertices[0].tint = new Color32(255, 0, 0, 255); // Red
vertices[1].tint = new Color32(0, 255, 0, 255); // Green 
vertices[2].tint = new Color32(0, 0, 255, 255); // Blue 
vertices[3].tint = new Color32(17, 55, 55, 255); // Dark-green 
```
Here's how the UI element will look like:
![](7.png)

Each vertex is colored in its own shade, and as we transition to another vertex, color blending occurs, resulting in the desired gradient.

However, there's a small nuance – a difference in colors between UI Builder and Runtime. I'll demonstrate that now:

![alt](8.png)

This issue arises from the fact that different color spaces are used in Runtime and UI Builder.

This becomes especially noticeable during color blending, as in our gradient.

I found confirmations of this on Unity forums:

- [Colors do not match ui builder](https://forum.unity.com/threads/colors-do-not-match-ui-builder-uss-variables.1420835/)
- [UI Builder does not support linear color space](https://forum.unity.com/threads/ui-builder-doesnt-support-linear-color-space-and-ui-toolkit-too.1235737/)
- [UIElements Runtime with different color spaces](https://forum.unity.com/threads/uielements-runtime-with-different-color-spaces.823395/)

What can be done, or will our gradient have this nuance?

By default, after creating a project, the [Linear color space](https://docs.unity3d.com/Manual/LinearRendering-LinearOrGammaWorkflow.html) is used.

![alt](9.png)

If you switch to Gamma, everything will fall into place, and it will come to life with new colors:

![alt](10.png)

## Chapter 3: Conclusion

And he lived happily ever after until the end of his days.