using UnityEngine;
using UnityEngine.UI;

public class ForecastCreator : MonoBehaviour
{
    [SerializeField] ForecastTemplate[] levelUpcomingForecast = default;
    [SerializeField] GameObject forecastPrefab = default;
    ForecastLounge forecastLounge;
    [SerializeField] RefrenceMaster refMaster = default;
    MouseInputHandler mouseInput;
    float cardWidth;

    private void Start()
    {
        forecastLounge = refMaster.forecastLounge;
        mouseInput = refMaster.mouseInputHandler;
        CreateUpcomingForecast();
    }

    void CreateUpcomingForecast()
    {


        for (int i=0; i<levelUpcomingForecast.Length; i++) { 
            GameObject newForecast = Instantiate(forecastPrefab);
            #region TransformInfo
            if (i == 0) { cardWidth = newForecast.GetComponent<RectTransform>().rect.width; }
            float startXValue = -((levelUpcomingForecast.Length - 1) *.5f* cardWidth);

            newForecast.transform.SetParent(forecastLounge.transform);
            newForecast.transform.localScale = new Vector3(1, 1, 1);
            newForecast.GetComponent<RectTransform>().anchoredPosition = new Vector2(startXValue + (i * cardWidth), -50);
            #endregion

            newForecast.GetComponent<ForecastCard>().SetForecastType(levelUpcomingForecast[i], mouseInput);
        }
        forecastLounge.AddChildrenToList();
    }
}
