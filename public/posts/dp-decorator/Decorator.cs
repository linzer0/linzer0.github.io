namespace DefaultNamespace;

public class Main
{
    public void Start()
    {
        var webPage = new WebPage();
        
        //base logic
        var baseWebPageDecorator = new BaseWebPageDecorator(webPage);
        
        // base logic + play audio
        var soundWebPage = new SoundWebPageDecorator(webPage);
    }
}

public abstract class WebPage
{
    public virtual void Open();
    public virtual void Hide();
}

public class BaseWebPageDecorator : WebPage
{
    private WebPage _currentPage;

    public override void Open()
    {
        _currentPage.Open();
    };

    public override void Hide()
    {
        _currentPage.Hide();
    };

    public BaseWebPageDecorator(WebPage page)
    {
        _currentPage = page;
    }
}

public class SoundWebPageDecorator : BaseWebPageDecorator
{
    private WebPage _currentWebPage;

    public void Open()
    {
        base.Open();
        PlayOpenSound();
    };

    public void Hide()
    {
        base.Hide();
        PlayHideSound();
    };
    
    public void PlayOpenSound() {};

    public void PlayHideSound() {};

    public SoundWebPageDecorator(IWebPage webPage)
    {
        _currentWebPage = webPage;
    }
}