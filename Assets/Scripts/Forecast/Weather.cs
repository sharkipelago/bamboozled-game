using System;
using System.Collections.Generic;
using UnityEngine;

public class Weather : MonoBehaviour
{
    //Player Takes Action --> All Bamboo React To Weather --> Next Day Comes (Weather May/May Not Channge)
    //Triggered when player takes action
    public Action FinishDayAction;
    public Action ForecastAction;
    private SpriteRenderer weatherSpriteRenderer;
    //Has to be Added in Same Order as Enums
    [SerializeField] Sprite[] weatherSprites = default;
    //Should be odd so there is a center
    [SerializeField] Garden garden = default;
    enum ForecastType {
        Sun = 0,
        Rain = 1
    };


    AudioGoverner audioGoverner;

    public ForecastTemplate currentForecast;

    //[SerializeField] int forecastRange;
    int forecastRange { get => currentForecast.templateRange; }
    BambooPlot targetPlot {get => garden.currentPlot; }
    int targetPlotIndex { get => Array.IndexOf(plotArray, targetPlot); }

    List<Action> forecastList = new List<Action>();

    BambooPlot[] plotArray;

    //Variants are like which way the wind is facing or just variations of what area a forecast impacts not changes to the actual ability of the forecast
    /*int forecastVariations = 2;
    int currentForecastVariant = 0;

    public void ShiftForecastVariant()
    {
        if (currentForecastVariant < forecastVariations)
            currentForecastVariant++;
        else
            currentForecastVariant = 0;

        int currentFacingDirection = Math.Sign(garden.incomingRenderer.transform.localScale.x);
        int newDirection = ()?
    }*/

    public bool forecastFacingRight { get; private set; } = true;


    public void ShiftForecastVariant(bool shiftRight)
    {
        if(forecastFacingRight == shiftRight) { return; }
        Debug.Log("SHIFT");
        forecastFacingRight = shiftRight;
        Debug.Log(forecastFacingRight);
        //int newDirection = (forecastFacingRight)? 1 : -1;
        Vector3 scale = garden.incomingRenderer.transform.localScale;
        garden.incomingRenderer.transform.localScale = (forecastFacingRight) ?  new Vector3(Mathf.Abs(scale.x), scale.y , scale.z) : new Vector3(-Mathf.Abs(scale.x), scale.y, scale.z);
        garden.HighlightImpactedPlots();
    }


    private void Start()
    {
        //EDIT THIS currentForecast to be a ForecastTemplate instead
        currentForecast = new ForecastTemplate(WorldDictionary.ForecastType.Sun, 3);
        CreateForecastList();
     
        weatherSpriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        plotArray = garden.booPlots;

        audioGoverner = FindObjectOfType<AudioGoverner>();
    }

    /*public void SwapForecast(WorldDictionary.ForecastType newCast)
    {
        currentForecast = newCast;
        weatherSpriteRenderer.sprite = weatherSprites[(int)currentForecast];
    }*/

    public void Forecast()
    {
        ForecastAction.Invoke();
        forecastList[(int)currentForecast.templateType]();
        //True resets it to face right
        ShiftForecastVariant(true);
    }

    void CreateForecastList()
    {
        forecastList.Add(Sunshine);
        forecastList.Add(Rainfall);
        forecastList.Add(Windgust);
        forecastList.Add(Lightningstrike);
        forecastList.Add(Snowstorm);
    }

    public void FinishDay()
    {
        FinishDayAction.Invoke();
    }

    void Sunshine()
    {
        Debug.Log("Sunshine");
        if (PlayerPrefs.GetInt("SFX", 1) == 1)
            audioGoverner.PlaySound("SunSFX");
        if (targetPlot.booType == WorldDictionary.BooType.None) { return; }
        if (targetPlot.isFrozen)
        {
            targetPlot.isFrozen = false;
            return;
        }
        int distanceFromTarget;
        int startingDistance = (forecastRange - 1) / 2;
        if (!forecastFacingRight  && forecastRange % 2 == 0) { startingDistance += 1; }
        Debug.Log("Facing Right?" + forecastFacingRight + "Distance?" + startingDistance + "Range" + forecastRange);
        for (int i = 0; i < forecastRange; i++)
        {
            distanceFromTarget = i - startingDistance;

            int currentPlotIndex = targetPlotIndex + distanceFromTarget;
            distanceFromTarget++;

            //Out of Bounds?
            if (currentPlotIndex < 0 || currentPlotIndex > plotArray.Length - 1) { continue; }
            //Already a stalk?  Edit: I want it to uproot other stalks
            //if (plotArray[currentPlot].booType != WorldDictionary.BooType.None) { continue; }
            if (plotArray[currentPlotIndex] == targetPlot) { continue; }

            if (plotArray[currentPlotIndex].isFrozen)
            {
                plotArray[currentPlotIndex].isFrozen = false;
                continue;
            }

            plotArray[currentPlotIndex].booType = targetPlot.booType;

        }
        Debug.Log("Sunshine Finish");

    }

    void Rainfall()
    {
        Debug.Log("Rainfall");
        if (PlayerPrefs.GetInt("SFX", 1) == 1)
            audioGoverner.PlaySound("RainSFX");

        int distanceFromTarget;
        int startingDistance = (forecastRange - 1)/ 2;
        for (int i = 0; i < forecastRange; i++)
        {
            distanceFromTarget = i - startingDistance;

            int currentPlot = targetPlotIndex + distanceFromTarget;
            distanceFromTarget++;
            //Out of Bounds?
            if (currentPlot < 0 || currentPlot > plotArray.Length - 1) { continue; }

            //Doesn't Have a Stalk?
            
            //Same as writing == WorldDictionary.BooDictionary.None
            if (plotArray[currentPlot].booType == 0) { continue; }

            if(plotArray[currentPlot].isFrozen)
            {
                plotArray[currentPlot].isFrozen = false;
                continue;
            }

            plotArray[currentPlot].booHeight++;

        }


    }

    void Windgust()
    {
        Debug.Log("Windgust");
        if (PlayerPrefs.GetInt("SFX",1) == 1)
            audioGoverner.PlaySound("WindSFX");
        if (targetPlot.booType == WorldDictionary.BooType.None) { return; }



        int newPlotIndex = (forecastFacingRight) ? targetPlotIndex + forecastRange : targetPlotIndex - forecastRange;

  

        //Out of Bounds?
        //Already a stalk?
       // Debug.Log("A");


        //Debug.Log("B");

        if (plotArray[targetPlotIndex].isFrozen)
        {
            plotArray[targetPlotIndex].isFrozen = false;
            return;
        }

        if (newPlotIndex < 0 || newPlotIndex > plotArray.Length - 1 /*|| newPlot.booType != WorldDictionary.BooType.None*/) {
            Debug.Log("Flopped Out of Range");
        }
        else
        {
            BambooPlot newPlot = plotArray[newPlotIndex];
            if (newPlot.isFrozen)
            {
                newPlot.isFrozen = false;
            }
            else
            {
                newPlot.booType = targetPlot.booType;
                newPlot.booHeight = targetPlot.booHeight;
            }
        }

        plotArray[targetPlotIndex].booType = WorldDictionary.BooType.None;
        Debug.Log("Windgust Finish");
    }

    void Lightningstrike()
    {
        if (PlayerPrefs.GetInt("SFX", 1) == 1)
            audioGoverner.PlaySound("LightningSFX");

        WorldDictionary.BooType targetType = targetPlot.booType;
        if (targetType == 0) { return; }
        if (targetPlot.booHeight < 1) { return; }
        if (targetPlot.isFrozen)
        {
            targetPlot.isFrozen = false;
            return;
        }
        int distance = targetPlot.booHeight - 1;
        for (int i = 0; i < distance; i++)
        {
            //So we don't make another one on the target plot
            //if statement is to switch direction
            int currentPlot = (forecastFacingRight) ? targetPlotIndex + i + 1 : targetPlotIndex - i - 1;

            //Out of Bounds?
            if (currentPlot < 0 || currentPlot > plotArray.Length - 1) { break; }
            if (plotArray[currentPlot].isFrozen)
            {
                plotArray[currentPlot].isFrozen = false;
                continue;
            }
            plotArray[currentPlot].booType = targetType;
            plotArray[currentPlot].booHeight = 1;
        }

        targetPlot.booHeight = 1;

    }

    void Snowstorm()
    {
        Debug.Log("Snowstorm");
        if (PlayerPrefs.GetInt("SFX", 1) == 1)
            audioGoverner.PlaySound("SnowSFX");
        int distanceFromTarget;
        int startingDistance = (forecastRange - 1) / 2;
        for (int i = 0; i < forecastRange; i++)
        {
            distanceFromTarget = i - startingDistance;

            int currentPlot = targetPlotIndex + distanceFromTarget;
            distanceFromTarget++;
            //Out of Bounds?
            if (currentPlot < 0 || currentPlot > plotArray.Length - 1) { continue; }

            //Doesn't Have a Stalk?

            //Same as writing == WorldDictionary.BooDictionary.None
            if (plotArray[currentPlot].booType == 0) { continue; }

            plotArray[currentPlot].isFrozen = true;

        }
    }
}
