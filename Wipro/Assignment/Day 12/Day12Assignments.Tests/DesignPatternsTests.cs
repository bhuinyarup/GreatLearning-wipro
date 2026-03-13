using DesignPatternsDemo;
using DesignPatternsDemo.Factory;
using DesignPatternsDemo.Observer;
using DesignPatternsDemo.Singleton;

namespace Day12Assignments.Tests;

public class DesignPatternsTests
{
    [Fact]
    public void Singleton_AlwaysReturnsSameInstance()
    {
        var first = Logger.Instance;
        var second = Logger.Instance;

        Assert.Same(first, second);
    }

    [Fact]
    public void Factory_CreatesExpectedDocumentType()
    {
        var factory = new DocumentFactory();

        var pdf = factory.Create("pdf");
        var word = factory.Create("word");

        Assert.Equal("PDF", pdf.Type);
        Assert.Equal("WORD", word.Type);
    }

    [Fact]
    public void Observer_RegistersAndNotifiesObservers()
    {
        var station = new WeatherStation();
        var display = new WeatherDisplay();

        station.Register(display);
        station.SetTemperature(29.5f);

        Assert.Equal(29.5f, display.LastTemperature);
    }

    [Fact]
    public void Observer_DoesNotNotifyAfterUnregister()
    {
        var station = new WeatherStation();
        var display = new WeatherDisplay();

        station.Register(display);
        station.Unregister(display);
        station.SetTemperature(31f);

        Assert.Equal(0f, display.LastTemperature);
    }

    [Fact]
    public void DemoRunner_ProvidesSimpleUsageExamples()
    {
        var singletonMessage = PatternDemoRunner.RunSingletonDemo();
        var factoryMessage = PatternDemoRunner.RunFactoryDemo("pdf");
        var observedTemperature = PatternDemoRunner.RunObserverDemo(26f);

        Assert.Equal("Application started.", singletonMessage);
        Assert.Contains("PDF", factoryMessage);
        Assert.Equal(26f, observedTemperature);
    }
}
