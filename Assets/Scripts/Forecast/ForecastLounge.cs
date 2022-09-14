using System;
using System.Collections.Generic;
using UnityEngine;

public class ForecastLounge : MonoBehaviour
{
    int activeCards{get { return forecastCardArray.Length - hiddenCardIndexes.Count; } }
    public bool outOfCards { get { return activeCards == 0; } }
    int recursiveOverflowLock = 0;
    public Action BackTrackAction;
    public GameObject[] forecastCardArray /*{ get; private set; }*/ ;


    float currentY;

    [SerializeField] RefrenceMaster refMaster;
    Garden garden;
    Weather weather;
    LevelRequirements levelReqs;
    GameObject _currentForecastCard = null;
    public GameObject currentForecastCard {
        get { return _currentForecastCard; }
        set
        {
            if (_currentForecastCard != null)
                HighlightCardToggle(_currentForecastCard.GetComponent<RectTransform>(), true);
            _currentForecastCard = value;
            HighlightCardToggle(_currentForecastCard.GetComponent<RectTransform>(), false);
        }
    }

    [SerializeField] List<int> hiddenCardIndexes = new List<int>();

    private void Start()
    {
        garden = refMaster.garden;
        weather = refMaster.weather;
        levelReqs = refMaster.levelRequirements;

        weather.ForecastAction += OnForecast;


    }

    public void AddChildrenToList()
    {
        Debug.Log(transform.childCount);
        forecastCardArray = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; ++i)
        {
            forecastCardArray[i] = transform.GetChild(i).gameObject;
        }
        currentY = forecastCardArray[0].GetComponent<RectTransform>().anchoredPosition.y;
        currentForecastCard = forecastCardArray[0];
    }

    public void NavigateForecastLounge(int direction)
    {

        int index = Array.IndexOf(forecastCardArray, currentForecastCard);
        int target = index + direction;
        int nextIndex = NextAvailable(index, direction);
        recursiveOverflowLock = 0;
        if (nextIndex < 0)
            return;
        else
            currentForecastCard = forecastCardArray[nextIndex];

        //Debug.Log(currentForecastCard.name);
    }

    private int NextAvailable(int _index, int _direction)
    {

        recursiveOverflowLock++;
        if(recursiveOverflowLock > forecastCardArray.Length)
        {
            Debug.LogWarning("Recursive Stack Overflowed");
            return - 1;
        }


        int target = _index + _direction;

        if (_index + _direction > forecastCardArray.Length - 1)
            target = 0;
        else if (_index + _direction < 0)
            target = forecastCardArray.Length - 1;

        if (!forecastCardArray[target].gameObject.activeSelf)
        {
            return NextAvailable(_index + _direction, _direction);
        }
        else
        {
            return target;
        }
    }

    private void OnForecast()
    {
        HiddenToggle(false, Array.IndexOf(forecastCardArray, currentForecastCard));
        //Destroy(currentForecastCard);
        if (forecastCardArray.Length == 0)
        {
            Debug.Log("No More Cards");

        }
        else
            NavigateForecastLounge(1);


        //levelReqs.CheckRequirementProgress();

    }

    public void ForecastOnDeckToggle(bool isToggled)
    {
        if (isToggled)
            garden.incomingForecast = currentForecastCard.GetComponent<ForecastCard>().cardTemplate;
        else
            garden.ClearIncomingSprite();
    }

    void HighlightCardToggle(RectTransform _rectTransform, bool isHighlighted)
    {
        float currentX = _rectTransform.anchoredPosition.x;
        _rectTransform.anchoredPosition = (isHighlighted) ? new Vector2(currentX, currentY) : new Vector2(currentX, 30f);
    }

    public void BackTrack()
    {
        if (hiddenCardIndexes.Count > 0)
        {
            HiddenToggle(true, hiddenCardIndexes[hiddenCardIndexes.Count - 1]);
            BackTrackAction.Invoke();
        }
        else
            Debug.LogWarning("No Hidden Cards");

    }

    void HiddenToggle(bool isHidden, int cardIndex)
    {

        if (!isHidden)
        {
            hiddenCardIndexes.Add(cardIndex);
        }
        else
        {
            hiddenCardIndexes.Remove(cardIndex);
        }
        /*else if (isHidden)
        {
            hiddenCardIndexes.Remove(cardIndex);
            int index = card.GetComponent<ForecastCard>().initalIndex;
            if (forecastCardList.Count < index)
                forecastCardList.Add(card);
            else
                forecastCardList.Insert(index, card);
        }*/
        forecastCardArray[cardIndex].SetActive(isHidden);

    }
}
