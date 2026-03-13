using DesignPatternsDemo.Factory;
using DesignPatternsDemo.Observer;
using DesignPatternsDemo.Singleton;

namespace DesignPatternsDemo;

public static class PatternDemoRunner
{
    public static string RunSingletonDemo()
    {
        Logger.Instance.Clear();
        Logger.Instance.Log("Application started.");
        return Logger.Instance.Messages.Single();
    }

    public static string RunFactoryDemo(string type)
    {
        var factory = new DocumentFactory();
        var document = factory.Create(type);
        return $"{document.Type}: {document.Render()}";
    }

    public static float RunObserverDemo(float temperature)
    {
        var station = new WeatherStation();
        var display = new WeatherDisplay();

        station.Register(display);
        station.SetTemperature(temperature);

        return display.LastTemperature;
    }
}
