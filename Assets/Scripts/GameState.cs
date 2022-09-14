using UnityEngine;
using DG.Tweening;

public class GameState : MonoBehaviour
{
    [SerializeField] RefrenceMaster refMaster;
    [SerializeField] GameObject pauseScreenPanel;
    [SerializeField] SpriteRenderer victoryEmote;
    [SerializeField] Sprite goldVictorySprite;
    [SerializeField] float victoryEmoteDuration = 5f;

    AudioGoverner audioGoverner;
    SceneGoverner sceneGoverner;
    ForecastLounge forecastLounge;
    bool isGoldenWin = false;
    bool inputEnabled = true;

    private void Start()
    {
        forecastLounge = refMaster.forecastLounge;
        DOTween.Init();
        audioGoverner = FindObjectOfType<AudioGoverner>();
        sceneGoverner = audioGoverner.gameObject.GetComponent<SceneGoverner>();
    }

    bool _isPaused = false;
    public bool isPaused { get { return !inputEnabled; }
        set {
            inputEnabled = !value;
            TogglePauseScreen();
        } }

    public void TogglePause() => isPaused = !isPaused;
    public void QuitToHome() {
        Debug.Log(refMaster);
        Debug.Log(refMaster.sceneGoverner);
        sceneGoverner.backFromLevel = true;
        refMaster.sceneGoverner.GoToLevelSelect(); }
    public void RestartLevel() => refMaster.sceneGoverner.RefreshLevel();

    void TogglePauseScreen()
    {
        pauseScreenPanel.SetActive(isPaused);
    }

    public void OnWinLevel()
    {
        inputEnabled = false;
        isGoldenWin = !forecastLounge.outOfCards;
        if (isGoldenWin) { victoryEmote.sprite = goldVictorySprite; } 
        //victoryEmote.gameObject.SetActive(true);
        Sequence mySequence = DOTween.Sequence();
        audioGoverner.PlaySound("VictorySFX");
        int prefValue = (isGoldenWin) ? 2 : 1;
        PlayerPrefs.SetInt("Level" + sceneGoverner.levelIndex.ToString() + "Progress", prefValue);
        mySequence.Append(victoryEmote.DOFade(1, victoryEmoteDuration));
        mySequence.Append(victoryEmote.DOFade(0, victoryEmoteDuration+1)).OnComplete(() => QuitToHome());
        Debug.Log(forecastLounge.forecastCardArray.Length);
        Debug.Log("Winna!, Golden Win:" + isGoldenWin);
    }

}
