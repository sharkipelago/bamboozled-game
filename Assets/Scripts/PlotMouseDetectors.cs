using UnityEngine;
using UnityEngine.UI;

public class PlotMouseDetectors : MonoBehaviour
{
    [SerializeField] DetectorButton[] detectors = default;
    [SerializeField] Garden garden = default;
    [SerializeField] MouseInputHandler mouseInput = default;

    private void Start()
    {
        SetUpDetectorButtons();
    }

    void SetUpDetectorButtons()
    {
        for (int i = 0; i < garden.booPlots.Length; i++)
        {
            if (!garden.booPlots[i].gameObject.activeSelf)
            {
                detectors[i].gameObject.SetActive(false);
                continue;
            }

            detectors[i].SetUpDetector(i, mouseInput);
        }
    }
}
