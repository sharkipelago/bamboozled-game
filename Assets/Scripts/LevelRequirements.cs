using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelRequirements : MonoBehaviour//,UncommentForRecDisappear IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Requirement[] requirements = default;
    [SerializeField] GameState gameState;
    RequirementDisplay[] displays;
    float initialX;
    float initialY;
    float offScreenY = 450;
    RectTransform groupRect;

    Vector2 intialPos { get { return new Vector2(initialX, initialY); } }
    Vector2 offScreenPos { get { return new Vector2(initialX, offScreenY); } }


    public Action UpdateDisplay;
    //index 0 = none and will never be used, index 1 = green, index 2 = red ...

    /*bool _reqsOnScreen; 
    private bool reqsOnScreen
    {
        get { return _reqsOnScreen; }
        set {
            _reqsOnScreen = value;
            groupRect.anchoredPosition = (_reqsOnScreen) ? intialPos : offScreenPos;
        }
    }UncommentForRecDisappear*/


    public bool updateInitialized = false;

    public int[] bambooLevelProgress { get; private set; } = new int[Enum.GetValues(typeof(WorldDictionary.BooType)).Length];

    void Awake()
    {
        Transform grouperChild = transform.GetChild(0);
        int count = grouperChild.childCount;
        displays = new RequirementDisplay[count];
        displays = grouperChild.GetComponentsInChildren<RequirementDisplay> (true);
        groupRect = grouperChild.GetComponent<RectTransform>();
        initialX = groupRect.anchoredPosition.x;
        initialY = groupRect.anchoredPosition.y;
        SetUpRequirements();
    }

    void SetUpRequirements()
    {
        bool lastReq = false;
        for(int i=0; i < requirements.Length; i++)
        {
            if (i == requirements.Length - 1) { lastReq = true; }
            Requirement thisReq = requirements[i];
            displays[i].SetDisplay(thisReq.reqAmount, thisReq.reqType, lastReq);
        }
    }

    public void UpdateLevelProgress(WorldDictionary.BooType type, int changeAmount, bool isUndoing)
    {
        if (type == WorldDictionary.BooType.Yellow) { Debug.Log("Update" + type.ToString() + changeAmount.ToString()); }
        bambooLevelProgress[(int)type] += changeAmount;
        if (!updateInitialized) { return; }
        UpdateDisplay.Invoke();
        if (!isUndoing) { CheckRequirementProgress(); }
    }

    /*public*/ void CheckRequirementProgress()
    {
        foreach(Requirement req in requirements)
        {
            

            if (bambooLevelProgress[(int)req.reqType] != req.reqAmount) { return; }
            Debug.Log("Progress" + req.reqType.ToString() + bambooLevelProgress[(int)req.reqType].ToString() + "/" + req.reqAmount);

        }
        gameState.OnWinLevel();
        Debug.Log("Won!");
    }

    /*void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        reqsOnScreen = true;
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        reqsOnScreen = false;
    }UncommentForRecDisappear*/
}
