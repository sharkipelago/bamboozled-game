using UnityEngine;

[System.Serializable]
public class ForecastTemplate 
{
    [SerializeField] WorldDictionary.ForecastType _templateType = default;
    [SerializeField] int _templateRange = 1;
    public bool isEven { get => templateRange % 2 == 0; }

    public WorldDictionary.ForecastType templateType { get { return _templateType; } }
    public int templateRange { get { return _templateRange; } }

    public ForecastTemplate(WorldDictionary.ForecastType type, int range)
    {
        _templateRange = range;
        _templateType = type;
    }
}
