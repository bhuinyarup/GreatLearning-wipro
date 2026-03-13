namespace DesignPatternsDemo.Observer;

public sealed class WeatherStation
{
    private readonly List<IWeatherObserver> _observers = new();

    public void Register(IWeatherObserver observer)
    {
        if (!_observers.Contains(observer))
        {
            _observers.Add(observer);
        }
    }

    public void Unregister(IWeatherObserver observer)
    {
        _observers.Remove(observer);
    }

    public void SetTemperature(float temperature)
    {
        foreach (var observer in _observers)
        {
            observer.Update(temperature);
        }
    }
}
