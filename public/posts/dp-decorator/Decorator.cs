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