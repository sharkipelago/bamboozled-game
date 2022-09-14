using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] Toggle fullScreenToggle;
    [SerializeField] Toggle SFXToggle;
    [SerializeField] Slider volumeSlider;
    [SerializeField] LevelCompletionIndicator[] indicators;

    AudioGoverner audioGoverner;
    //0 = false, 1 = true
    int BoolToInt(bool boolInput)
    {
        int returnValue = (boolInput) ? 1 : 0;
        return returnValue;
    }

    bool IntToBool(int intInput)
    {
        bool returnValue = (intInput % 2 != 0) ? true : false;
        return returnValue;
    }

    private void Start()
    {
        audioGoverner = FindObjectOfType<AudioGoverner>();

        fullScreenToggle.isOn = IntToBool(PlayerPrefs.GetInt("FullScreen", BoolToInt(Screen.fullScreen)));
        SFXToggle.isOn = IntToBool(PlayerPrefs.GetInt("SFX", 1));
        volumeSlider.value = (PlayerPrefs.GetFloat("Volume", 1f));

        fullScreenToggle.onValueChanged.AddListener(delegate {
            PlayerPrefs.SetInt("FullScreen", BoolToInt(fullScreenToggle.isOn));
        });

        SFXToggle.onValueChanged.AddListener(delegate {
            PlayerPrefs.SetInt("SFX", BoolToInt(SFXToggle.isOn));
        });

        volumeSlider.onValueChanged.AddListener(delegate
        {
            PlayerPrefs.SetFloat("Volume", volumeSlider.value);
        });
    }


    public void UpdatePrefs()
    {
        Debug.Log("updated prefs");
        Screen.fullScreen = IntToBool(PlayerPrefs.GetInt("FullScreen", BoolToInt(false)));
        audioGoverner.sounds[0].source.volume = .25f * PlayerPrefs.GetFloat("Volume", 1f); 
        //Update All Player prefs that could've changed in the options here.
    }

    public void ResetProgress()
    {
        for (int i=0; i<9; i++)
        {
            int index = i + 1;
            PlayerPrefs.DeleteKey("Level" + index.ToString()+"Progress");
            indicators[i].UpdateIndicators();

        }
        Debug.Log("Progress Reset!");
    }

}
