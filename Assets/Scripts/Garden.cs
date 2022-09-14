using System;
using UnityEngine;

public class Garden : MonoBehaviour
{
    [SerializeField] Color highlightColor = default;
    [SerializeField] Color defaultColor = default;

    [SerializeField] Weather weather = default;

    SpriteRenderer _incomingRenderer;
    public SpriteRenderer incomingRenderer
    {
        get { return _incomingRenderer; }
        set
        {
            _incomingRenderer = value;
        }
    }
        

    [SerializeField] SpriteRenderer weatherIndicator = default;

    [SerializeField] SpriteRenderer[] impactedIndicators;


    ForecastTemplate _incomingForecast;
    public ForecastTemplate incomingForecast { get { return _incomingForecast; }
        set {
            _incomingForecast = value;
            incomingRenderer.sprite = WorldDictionary.ForecastDictionary[value.templateType].objectSprite;
            HighlightToggle(_currentPlot.spriteRenderer, true);
            weatherIndicator.enabled = true;
            weather.currentForecast = value;
        }
    }
    [SerializeField] GameObject incomingForecastObj = default;

    public BambooPlot[] booPlots;
    BambooPlot _currentPlot;
    public BambooPlot currentPlot {
        get { return _currentPlot;}
        private set {
            //Turn off old plot

            HighlightToggle(_currentPlot.spriteRenderer, false);
            _currentPlot = value;
            //Turn on New Plot
            HighlightToggle(_currentPlot.spriteRenderer, true);
        }
    }

    private void Start()
    {
        _currentPlot = booPlots[1];
        incomingRenderer = incomingForecastObj.GetComponent<SpriteRenderer>();
       // HighlightToggle(_currentPlot.spriteRenderer, true);
        weather.ForecastAction += ClearIncomingSprite;
        ClearIncomingSprite();
    }

    public void AssignCurrentPlot(int index)
    {
        currentPlot = booPlots[index];
    }

    public void ClearIncomingSprite()
    {

        incomingRenderer.sprite = null;
        weatherIndicator.enabled = false;
        for (int i = 0; i < impactedIndicators.Length; i++)
        {
            impactedIndicators[i].enabled = false;
        }
    }

    public void Navigate(int direction)
    {
        int currentIndex = Array.IndexOf(booPlots, currentPlot);
        if(currentIndex + direction > -1 && currentIndex + direction < booPlots.Length)
            AssignCurrentPlot(currentIndex + direction);
    }

    //Eventually Make Paramater Bamboo Type
    /*public void Plant()
    {
        if (_currentPlot.stalkObject == null)
        {
            _currentPlot.Sow(shootType.currentStalkType);
            weather.FinishDayAction.Invoke();
        }
    }*/

    /*public void Spread()
    {
        Debug.Log(_currentPlot.stalkObject);
        if (_currentPlot.stalkObject == null)
        {
            Debug.Log("Empty");
        }
        else
        {
            int currentIndex = Array.IndexOf(booPlots, currentPlot);
            int j = -3;
            for (int i = 0; i < 2; i++)
            {
                j += 2;
                if (currentIndex + j < 0 || currentIndex + j > booPlots.Length - 1) { break; }
                if (booPlots[currentIndex + j].stalkObject != null) { break; }
                booPlots[currentIndex + j].Sow(shootType.currentStalkType);
            }
        }
    }*/

    private void HighlightToggle(SpriteRenderer _spriteRenderer, bool highlight)
    {

        //_spriteRenderer.color = (highlight)? highlightColor : defaultColor;
        incomingForecastObj.transform.position = _spriteRenderer.gameObject.transform.position + new Vector3(0, 5, 0);
        weatherIndicator.transform.position = _spriteRenderer.gameObject.transform.position - new Vector3(0, .5f, 0);
        if(highlight) { HighlightImpactedPlots(); }
    }

    public void HighlightImpactedPlots()
    {

        int currentPlotIndex = Array.IndexOf(booPlots, currentPlot);
        int facingRightSwitch = (weather.forecastFacingRight) ? 1 : -1;
        //Debug.Log("Current" + currentPlotIndex);

        for (int i = 0; i < impactedIndicators.Length; i++)
        {
            impactedIndicators[i].enabled = false;
        }

        if (incomingForecast.templateType == WorldDictionary.ForecastType.Rain || incomingForecast.templateType == WorldDictionary.ForecastType.Sun || incomingForecast.templateType == WorldDictionary.ForecastType.Snow)
        {
            int facingRightShift = (weather.forecastFacingRight) ? 1 : 0;
            int startingPlotIndex = (incomingForecast.isEven) ? currentPlotIndex - (incomingForecast.templateRange / 2 - facingRightShift) : currentPlotIndex - (incomingForecast.templateRange / 2);
            //Debug.Log("Start" + startingPlotIndex);
            for (int i = startingPlotIndex; i <incomingForecast.templateRange + startingPlotIndex; i++)
            {
               // Debug.Log(i + "v.s." + currentPlotIndex);
                if (i < 0 || i > impactedIndicators.Length - 1) { continue; }


                if (i == currentPlotIndex) { continue; }

                impactedIndicators[i].enabled = true;
                //Debug.Log("i" + i);
            }
        }

        else if (incomingForecast.templateType == WorldDictionary.ForecastType.Wind )
        {
            int targetIndicatorIndex = currentPlotIndex + (facingRightSwitch * (incomingForecast.templateRange));
            if (targetIndicatorIndex < 0 || targetIndicatorIndex > impactedIndicators.Length - 1) { return; }
            impactedIndicators[targetIndicatorIndex].enabled = true;
        }

        else if (incomingForecast.templateType == WorldDictionary.ForecastType.Lightning)
        {
            int startingPlotIndex = (weather.forecastFacingRight) ? currentPlotIndex : currentPlotIndex - currentPlot.booHeight + 1;
            /*Debug.Log("Starter" + startingPlotIndex);
            int limit = currentPlot.booHeight - 1 + startingPlotIndex;
            Debug.Log("Limit" + limit);*/
            for (int i = startingPlotIndex; i < currentPlot.booHeight + startingPlotIndex; i ++)
            {
                //Debug.Log(i + "A");
                if (i < 0 || i > impactedIndicators.Length - 1) { continue; }
                //Debug.Log(i + "B");
                if (i == currentPlotIndex) { continue; }
                impactedIndicators[i].enabled = true;
            }
        }
    }
}
