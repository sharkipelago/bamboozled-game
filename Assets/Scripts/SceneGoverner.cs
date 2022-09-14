using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class SceneGoverner : MonoBehaviour
{
    LevelLoader levelLoader;
    [SerializeField] GovernerRefrence govRef;
    [SerializeField] GameObject crossFadeImageObject;
    [SerializeField] float crossFadeDuration;
    public int levelIndex { get; private set; }
    public static bool offOfDefault = false;
    Sequence endCrossFadeSeq;
    [SerializeField] Color goldenFadeColor;
    Image fadeImage;
    bool levelGolden { get { return PlayerPrefs.GetInt("Level" + levelIndex.ToString() + "Progress") == 2; } }
    public bool backFromLevel = false;
    bool fadeGolden;

    private void Start()
    {
        levelLoader = GetComponent<LevelLoader>();
        govRef = GetComponent<GovernerRefrence>();
        if (CurrentScene() == SceneList.Default)
            if (!offOfDefault)
            {
                offOfDefault = true;
                ChangeScene(SceneList.LevelSelect);
            }
        DOTween.Init();
        endCrossFadeSeq = DOTween.Sequence();

        fadeImage = crossFadeImageObject.GetComponent<Image>();

    }

    public enum SceneList {
        Default = 0,
        LevelSelect = 1,
        GameLevel = 2,
        TestLevel = 3,
        Credits = 4,
        Help = 5
    }

    public void GoToGame() => ChangeScene(SceneList.GameLevel);
    public void GoToLevelSelect() => ChangeScene(SceneList.LevelSelect);
    public void GoToTestLevel() => ChangeScene(SceneList.TestLevel);
    public void GoToCredits() => ChangeScene(SceneList.Credits);
    public void GoToHelp() => ChangeScene(SceneList.Help);

    void ChangeScene(SceneList targetScene)
    {
        if(CurrentScene() != SceneList.Default)
        {
            StartCrossfade((int)targetScene);
            //crossFade((int)targetScene, true);
        }
        else {
            SceneManager.LoadScene((int)targetScene);
        }

    }

    SceneList CurrentScene()
    {
        return (SceneList)SceneManager.GetActiveScene().buildIndex;
    }

    public void EnterLevel(int levelNumber)
    {
        levelIndex = levelNumber;
        ChangeScene(SceneList.GameLevel);
    }

    public void RefreshLevel()
    {
        if (CurrentScene() != SceneList.TestLevel) { EnterLevel(levelIndex); }
        else { GoToTestLevel(); }

    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //crossFade((int)CurrentScene(), false);
        SceneList loadedScene = (SceneList)scene.buildIndex;
        EndCrossfade();

        if (loadedScene == SceneList.LevelSelect)
        {

            Debug.Log(govRef);
            govRef.ConnectToGovernerBridge(backFromLevel);
            backFromLevel = false;
        }
        else if (loadedScene == SceneList.GameLevel)
        {
            Debug.Log("Loader");
            levelLoader.LoadLevelPrefab(levelIndex);
        }

        else if (loadedScene == SceneList.TestLevel)
        {
            govRef.ConnectToRefMaster();
        }
    }

    private void StartCrossfade(int targetSceneIndex)
    {
        bool correctScene = (targetSceneIndex == (int)SceneList.GameLevel || CurrentScene() == SceneList.GameLevel);
        fadeGolden = (levelGolden && correctScene) ? true : false;
        fadeImage.color = (fadeGolden) ? Color.yellow /*If possibleReplace with goldenfadecolor*/ : Color.white;
        /*Sequence startCrossFadeSeq = DOTween.Sequence();
        startCrossFadeSeq.Append(crossFadeImage.transform.DOMoveY(0, crossFadeDuration));
        startCrossFadeSeq.AppendCallback(ActualLoadScene(targetSceneIndex));*/
        Debug.Log("CrossFadeBegun" + "Golden" + levelGolden);
        crossFadeImageObject.transform.DOLocalMoveY(100, crossFadeDuration).OnComplete(() => SceneManager.LoadScene(targetSceneIndex));
        //SceneManager.LoadScene(targetSceneIndex);
    }



    private void EndCrossfade()
    {
        if (fadeImage != null) { fadeImage.color = (fadeGolden) ? Color.yellow : Color.white; }
   
        Sequence endCrossFadeSeq = DOTween.Sequence();
        endCrossFadeSeq.Append(crossFadeImageObject.transform.DOLocalMoveY(100, .5f));
        endCrossFadeSeq.Append(crossFadeImageObject.transform.DOLocalMoveY(-3000, crossFadeDuration));
        //endCrossFadeSeq.Append(crossFadeImage.transform.DOLocalMoveY(3000, 0));
    }

    /*private void crossFade(int targetSceneIndex, bool isStartFade)
    {

        if (!isStartFade)
        {
            endCrossFadeSeq.Append(crossFadeImage.transform.DOLocalMoveY(100, .5f));
            endCrossFadeSeq.Append(crossFadeImage.transform.DOLocalMoveY(-3000, crossFadeDuration));
        }
        else
        {
            endCrossFadeSeq.OnRewind(()=> SceneManager.LoadScene(targetSceneIndex));
            endCrossFadeSeq.SmoothRewind();
            //SceneManager.LoadScene(targetSceneIndex);
        }
        //crossFadeImage.transform.DOLocalMoveY(100, crossFadeDuration).OnComplete(() => SceneManager.LoadScene(targetSceneIndex));

    }*/
}
