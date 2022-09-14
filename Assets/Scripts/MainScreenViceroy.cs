using UnityEngine;
using DG.Tweening;

public class MainScreenViceroy : MonoBehaviour
{
    [SerializeField] RectTransform levelSelectRoot = default;
    [SerializeField] RectTransform titleRoot = default;
    [SerializeField] Camera mainCamera = default;
    [SerializeField] float tweenDuration;
    float maxDuration;
    Vector3 currentPos;

    RectTransform levelRectTransform;
    RectTransform titleRectTransform;

    AudioGoverner audioGoverner;

    private void Awake()
    {
        DOTween.Init();
        currentPos = mainCamera.transform.position;
        levelRectTransform = levelSelectRoot.GetComponent<RectTransform>();
        titleRectTransform = titleRoot.GetComponent<RectTransform>();
        audioGoverner = FindObjectOfType<AudioGoverner>();
        maxDuration = tweenDuration;
    }

    bool _onTitleScreen = true;
    public bool onTitleScreen { get { return _onTitleScreen; }
        private set {
            SwitchScreenFocus();
            _onTitleScreen = value;
        } }

    public void ToggleScreen(bool isInstant)
    {
        if(isInstant) { tweenDuration = 0; }
        onTitleScreen = !onTitleScreen;
        if (isInstant) { tweenDuration = maxDuration; }

    }

    void SwitchScreenFocus()
    {
        /*Vector3 currentPos = mainCamera.transform.position;
        Vector3 targetLocation = (_onTitleScreen) ? new Vector3(9.6f, currentPos.y, currentPos.z) : new Vector3(-9.6f, currentPos.y, currentPos.z);
        mainCamera.transform.position = targetLocation ;

        Vector2 newUIPos = (_onTitleScreen) ? new Vector2(-1920 , 0) : new Vector2(1920, 0);
        levelSelectRoot.anchoredPosition += newUIPos;
        titleRoot.anchoredPosition += newUIPos;*/

        int direction = (_onTitleScreen) ? 1 : -1;

        mainCamera.transform.DOLocalMoveX(9.6f * direction, tweenDuration);

        levelRectTransform.DOAnchorPosX(levelRectTransform.anchoredPosition.x + (1920 * -direction), tweenDuration);
        titleRectTransform.DOAnchorPosX(titleRectTransform.anchoredPosition.x + (1920 * -direction), tweenDuration);

    }

    public void SelectSFX()
    {
        if (PlayerPrefs.GetInt("SFX", 1) == 1)
            audioGoverner.PlaySound("SelectSFX");
    }

    public void CancelSFX()
    {
        if (PlayerPrefs.GetInt("SFX", 1) == 1)
            audioGoverner.PlaySound("BackSFX");
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    /* void SwitchUI()
     {
         titleRoot.SetActive(_onTitleScreen);
         levelSelectRoot.SetActive;
     }*/
}
