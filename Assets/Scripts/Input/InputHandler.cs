using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class InputHandler : MonoBehaviour
{
    [SerializeField]  Garden garden = default;
    [SerializeField] ForecastLounge forecastLounge = default;
    [SerializeField] Weather weather = default;
    [SerializeField] GameState gameState = default;
    // [SerializeField] MouseInputHandler mouseInputHandler = default;

    private bool _forecastLoungeActive = true;
    public bool forecastLoungeActive { get { return _forecastLoungeActive; }
        private set {

            _forecastLoungeActive = value;
            forecastLounge.ForecastOnDeckToggle(!value);
            //mouseInputHandler.forecastCardsInteractable = value;
        } } 


    bool isOutOfCards () { return forecastLounge.outOfCards; }

    public void OnNavigate(CallbackContext context)
    {
        if(gameState.isPaused) { return; }
        Vector2 navDirection = context.ReadValue<Vector2>();
        if (navDirection != Vector2.zero)
        {
            int direction = Mathf.RoundToInt(navDirection.x);
            if (forecastLoungeActive)
                forecastLounge.NavigateForecastLounge(direction);
            else
                garden.Navigate(direction);
        }

    }

    public void OnUndo(CallbackContext context)
    {
        if (gameState.isPaused) { return; }
        if (context.performed)
            if (!forecastLoungeActive)
                forecastLoungeActive = true;
            else
                forecastLounge.BackTrack();

    }

    public void OnSelect(CallbackContext context)
    {
        if (gameState.isPaused) { return; }
        if (context.performed)
        {
            SelectionLogic();
        }
    }

    public void SelectionLogic()
    {

        if (!isOutOfCards())
        {
            if (!forecastLoungeActive)
                weather.Forecast();
            forecastLoungeActive = !forecastLoungeActive;


        }
        else
        {
            Debug.Log("Can't Select You're Out of Cards!");
        }
    }



    public void OnSwitch(CallbackContext context)
    {
        if (context.performed)
        {
            weather.ShiftForecastVariant(!weather.forecastFacingRight);
        }
    }

    public void OnPause(CallbackContext context)
    {
        if (context.performed)
            gameState.TogglePause();
    }
}
