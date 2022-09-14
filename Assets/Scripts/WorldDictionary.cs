using System;
using System.Collections.Generic;
using UnityEngine;

public class WorldDictionary : MonoBehaviour
{
    //Add to governer

    #region Bamboo
    public static Dictionary<BooType, Bamboo> BooDictionary;
    [SerializeField] Bamboo[] Boos = default;
    public enum BooType
    {
        None = 0,
        Green = 1,
        Red = 2,
        Blue = 3,
        Orange = 4,
        Purple = 5,
        Yellow = 6
    };
    #endregion

    #region Weather
    public static Dictionary<ForecastType, Forecast> ForecastDictionary;
    [SerializeField] Forecast[] Forecasts = default;
    public enum ForecastType
    {
        Sun = 0,
        Rain = 1,
        Wind = 2,
        Lightning = 3,
        Snow = 4
    };
    #endregion

    void Awake()
    {
        BooDictionary = new Dictionary<BooType, Bamboo>();
        ForecastDictionary = new Dictionary<ForecastType, Forecast>();

        int i = 0;
        foreach (BooType key in Enum.GetValues(typeof(BooType)))
        {
            if(key == BooType.None) { continue; }
            BooDictionary.Add(key, Boos[i]);
            i++;
        }
        i = 0;
        foreach (ForecastType key in Enum.GetValues(typeof(ForecastType)))
        {
            ForecastDictionary.Add(key, Forecasts[i]);
            i++;
        }
    }
}
