using DG.Tweening;
using UnityEngine;

public class HelpToggle : MonoBehaviour
{
    [SerializeField] RectTransform helpRootObject = default;
    [SerializeField] float tweenDuration;
    [SerializeField] GameObject leftButton;
    [SerializeField] GameObject rightButton;

    Vector3 currentPos;

    RectTransform levelRectTransform;
    RectTransform titleRectTransform;

    AudioGoverner audioGoverner;

    private void Awake()
    {
        DOTween.Init();
        audioGoverner = FindObjectOfType<AudioGoverner>();
        leftButton.SetActive(false);
    }

    bool _onExplanation = true;
    public bool onExplanation
    {
        get { return _onExplanation; }
        private set
        {
            SwitchScreenFocus();
            _onExplanation = value;
        }
    }

    public void ToggleScreen()
    {
        onExplanation = !onExplanation;

    }

    void SwitchScreenFocus()
    {
        /*if(audioGoverner != null)
            audioGoverner.PlaySound("SelectSFX");
            */
        int target = (_onExplanation) ? -1920 : 0;
        leftButton.SetActive(false);
        rightButton.SetActive(false);
        helpRootObject.DOAnchorPosX(target, tweenDuration).OnComplete(EnableButtons);

    }

    void EnableButtons()
    {
        leftButton.SetActive(!_onExplanation);
        rightButton.SetActive(_onExplanation);
    }
}
