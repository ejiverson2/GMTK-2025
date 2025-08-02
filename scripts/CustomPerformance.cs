using Godot;
using System.Collections.Generic;

public partial class CustomPerformance : Node
{
    private Dictionary<string, double> _metrics = new();

    private static CustomPerformance _instance = new CustomPerformance();

    private CustomPerformance()
    {
    }

    public static CustomPerformance getInstance()
    {
        return _instance;
    }

    public void SetMetric(string name, double value)
    {
        _metrics[name] = value;
    }

    public double GetMetric(string name)
    {
        return _metrics.TryGetValue(name, out var value) ? value : 0;
    }

    public Dictionary<string, double> GetAllMetrics()
    {
        return _metrics;
    }
}
