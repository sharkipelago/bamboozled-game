using System;
using UnityEngine;
using UnityEngine.UI;

public class ShootType : MonoBehaviour
{
    [SerializeField] GameObject[] stalkTypes = default;
    public GameObject currentStalkType { get; private set; }
    [SerializeField] Image currentBambooChoice = default;
    [SerializeField] Image backupBambooChoice = default;
    bool swapped;
    //RectTransform currentBooPosition;
    //RectTransform backupBooPosition;
    private void Start()
    {
        currentStalkType = stalkTypes[0];
        //currentBooPosition = currentBambooChoice.rectTransform;
        //backupBooPosition = backupBambooChoice.rectTransform;
    }

    public void Swap()
    {
        Vector3 oldPosition = currentBambooChoice.rectTransform.position;
        currentBambooChoice.rectTransform.position = backupBambooChoice.rectTransform.position;
        backupBambooChoice.rectTransform.position = oldPosition;
        if (swapped)
            currentBambooChoice.transform.SetAsFirstSibling();
        else
            currentBambooChoice.transform.SetAsLastSibling();

        int currentIndex = Array.IndexOf(stalkTypes, currentStalkType);
        currentIndex = (currentIndex + 1 == stalkTypes.Length) ? 0 : currentIndex + 1;
        currentStalkType = stalkTypes[currentIndex];
        swapped = !swapped;
        
    }
}
