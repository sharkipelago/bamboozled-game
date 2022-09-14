using UnityEngine.UI;
using UnityEngine;

public class LevelCompletionIndicator : MonoBehaviour
{
    [SerializeField] Image completionImage;
    [SerializeField] Sprite goldCompletionSprite;
    int buttonIndex;

    private void Start()
    {
        buttonIndex = transform.parent.GetSiblingIndex() + 1;
        UpdateIndicators();
    }

    public void UpdateIndicators()
    {

        //For the pref, 0=Not completed, 1= Completed, 2=Completed w/ Gold Win

        int completionStatus = PlayerPrefs.GetInt("Level" + buttonIndex.ToString() + "Progress", 0);
        if (completionStatus == 2)
        {
            completionImage.sprite = goldCompletionSprite;
        }

        completionImage.enabled = (completionStatus == 0) ? false : true;
    }
}
