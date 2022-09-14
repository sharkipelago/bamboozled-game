using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ForecastCard : MonoBehaviour, IPointerEnterHandler, ISelectHandler
{
    public ForecastTemplate cardTemplate { get; private set; }
    public int initalIndex { get; private set; }
    [SerializeField] TMP_Text rangeText;
    MouseInputHandler mouseInput;
    Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.interactable = true;
    }

    public void SetForecastType(ForecastTemplate template, MouseInputHandler _mouseInput)
    {
        if (template.templateRange % 2 != 1) { Debug.LogWarning(name + "Forecast Range is Even"); }
        if (template.templateRange == 0)
            cardTemplate = new ForecastTemplate(template.templateType, 1);
        else
            cardTemplate = template;
        GetComponent<Image>().sprite = WorldDictionary.ForecastDictionary[template.templateType].cardSprite;
        initalIndex = transform.GetSiblingIndex();
        name = cardTemplate.templateType.ToString();
        mouseInput = _mouseInput;

        rangeText.text = cardTemplate.templateRange.ToString();
        //mouseInput.InteractableToggleAction += ChangeInteractability;
    }

    /*void ChangeInteractability()
    {
        button.interactable = !button.interactable;
    }*/

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (mouseInput.forecastCardsInteractable)
        {
            mouseInput.NavigateForecastCards(gameObject);
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        if (mouseInput.forecastCardsInteractable)
        {
            mouseInput.SelectForecastCard();
        }
    }
}
