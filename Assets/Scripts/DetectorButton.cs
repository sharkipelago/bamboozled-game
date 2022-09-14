using UnityEngine;
using UnityEngine.EventSystems;

public class DetectorButton : MonoBehaviour, IPointerEnterHandler, ISelectHandler
{
    public int detectorIndex = default;
    MouseInputHandler mouseInput;


    public void SetUpDetector(int index, MouseInputHandler _mouseInput)
    {
        detectorIndex = index;
        mouseInput = _mouseInput;

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (mouseInput.plotInteractable)
        {
            mouseInput.NavigatePlots(detectorIndex);
        }
    }


    public void OnSelect(BaseEventData eventData)
    {
        if (mouseInput.plotInteractable)
        {
            mouseInput.SelectForecastCard();
        }
    }
}
