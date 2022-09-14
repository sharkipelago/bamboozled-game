using System;
using UnityEngine;

public class MouseInputHandler : MonoBehaviour
{
    [SerializeField] InputHandler inputHandler = default;
    [SerializeField] ForecastLounge forecastLounge = default;
    [SerializeField] Garden garden = default;

    public Action InteractableToggleAction;

    public Action PlotToggleAction;

    /*bool _forecastCardsInteractable = true;
    public bool forecastCardsInteractable {
        get { return _forecastCardsInteractable; }
        set {
            _forecastCardsInteractable = value;
            InteractableToggleAction.Invoke();
        }
    }*/

        //!!!IMPORTANT eventually change this so instead of usuing a bool it actually makes all the buttons not interactable (Make: button.interactable = false)
    public bool forecastCardsInteractable { get { return inputHandler.forecastLoungeActive; } }

    public bool plotInteractable { get{ return !inputHandler.forecastLoungeActive; } }


    public void NavigateForecastCards(GameObject card)
    {
        forecastLounge.currentForecastCard = card;
    }

    public void SelectForecastCard()
    {

        inputHandler.SelectionLogic();
    }

    public void NavigatePlots(int index)
    {
        garden.AssignCurrentPlot(index);
    }
}
