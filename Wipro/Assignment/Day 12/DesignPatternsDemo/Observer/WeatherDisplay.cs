namespace DesignPatternsDemo.Observer;

public sealed class WeatherDisplay : IWeatherObserver
{
    public float LastTemperature { get; private set; }

    public void Update(float temperature)
    {
        LastTemperature = temperature;
    }
}
