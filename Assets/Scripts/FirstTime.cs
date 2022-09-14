using UnityEngine;
using UnityEngine.UI;

public class FirstTime : MonoBehaviour
{
    [SerializeField] Image helpButton;

    void Start()
    {
       if(PlayerPrefs.GetInt("CheckedHelp",0) == 0)
       {
            helpButton.color = Color.red;
       } 
    }

    public void UpdateCheckHelpPref()
    {
        PlayerPrefs.SetInt("CheckedHelp", 1);
    }

}
