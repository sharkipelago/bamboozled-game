using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class RequirementDisplay : MonoBehaviour
{
    [SerializeField] TMP_Text amountText = default;
    [SerializeField] Image booTypeImage = default;
    LevelRequirements levelReqs = default;

    Color completedColor = Color.green;
    Color uncompletedColor = Color.white;
    Color overflowColor = Color.red;

    public bool isCompleted { get { return reqProgress == reqGoal; } }
    int reqGoal;
    bool isLastReq;
    private int reqProgress { get {
            return levelReqs.bambooLevelProgress[(int)reqType];
        } }
    WorldDictionary.BooType reqType;

    public void SetDisplay(int amount, WorldDictionary.BooType type, bool _lastReq)
    {
        levelReqs = gameObject.GetComponentInParent<LevelRequirements>();
        levelReqs.UpdateDisplay += RefreshDisplay;
        isLastReq = _lastReq;

        gameObject.SetActive(true);
        reqGoal = amount;
        RefreshDisplay();
        reqType = type;

        booTypeImage.sprite = WorldDictionary.BooDictionary[reqType].culmSprite;
        if (isLastReq)
        {
            levelReqs.updateInitialized = true;
            levelReqs.UpdateDisplay.Invoke();
        }
    }

    private void RefreshDisplay()
    {
        amountText.color = (isCompleted) ? completedColor : uncompletedColor;
        amountText.text = reqProgress.ToString() + "/" + reqGoal.ToString();
    }
}
