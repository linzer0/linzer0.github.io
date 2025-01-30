+++
date = '2025-01-21T20:24:56+07:00'
draft = false
title = 'Design Patterns: Decorator'
hideSummary = true
+++

# General

Decorator. Hoorrayyy, it's first article in Design Patterns.

I want to describe patterns, use cases, and examples, and nuances.

# Decorator Pattern

## Introduction

The **Decorator** pattern is one of the structural design patterns. It allows us to dynamically extend the behavior of objects without modifying their structure. This pattern is widely used when we need to add responsibilities to objects in a flexible and reusable way.

## Key Concepts

1. **Abstract component** – Defines the base interface.
2. **Concrete component** – A basic implementation of the component.
3. **Decorator (Wrapper)** – Inherits from the base component and adds new behavior.

## UML Diagram

![](decorator.drawio2.svg)

## Code Example

Let's implement a simple **WebPage** system where different decorators extend functionality, such as playing sounds when opening and closing a page.

```csharp
namespace DecoratorExample;

public abstract class WebPage
{
    public abstract void Open();
    public abstract void Hide();
}

public class BasicWebPage : WebPage
{
    public override void Open()
    {
        Console.WriteLine("Opening basic web page");
    }
    
    public override void Hide()
    {
        Console.WriteLine("Hiding basic web page");
    }
}

public abstract class WebPageDecorator : WebPage
{
    protected WebPage _webPage;
    
    protected WebPageDecorator(WebPage webPage)
    {
        _webPage = webPage;
    }
    
    public override void Open()
    {
        _webPage.Open();
    }
    
    public override void Hide()
    {
        _webPage.Hide();
    }
}

public class SoundWebPageDecorator : WebPageDecorator
{
    public SoundWebPageDecorator(WebPage webPage) : base(webPage) { }
    
    public override void Open()
    {
        base.Open();
        PlayOpenSound();
    }
    
    public override void Hide()
    {
        base.Hide();
        PlayHideSound();
    }
    
    private void PlayOpenSound()
    {
        Console.WriteLine("Playing open sound");
    }
    
    private void PlayHideSound()
    {
        Console.WriteLine("Playing hide sound");
    }
}

class Program
{
    static void Main()
    {
        WebPage basicPage = new BasicWebPage();
        WebPage soundPage = new SoundWebPageDecorator(basicPage);
        
        soundPage.Open();  // Opening basic web page + Playing open sound
        soundPage.Hide();  // Hiding basic web page + Playing hide sound
    }
}
```

## When to Use

- When you need to **extend an object’s behavior dynamically** without modifying its code.
- When subclassing leads to **class explosion** due to multiple feature combinations.
- When you want to follow the **Open/Closed Principle** (open for extension, closed for modification).

## Pros & Cons

### Pros:

✅ More flexible than subclassing. 

✅ Supports the **Single Responsibility Principle** by separating concerns. 

✅ Can be combined to create complex behaviors.

### Cons:

❌ Can make debugging harder due to multiple layers of wrappers. 

❌ Increased complexity compared to direct inheritance.

## Conclusion

The **Decorator** pattern is a powerful tool for extending functionality without modifying existing code. 

It is widely used in GUI systems, logging, security layers, and even Unity components. 

Understanding this pattern helps create scalable and maintainable software.

