using System.Collections.Generic;
using UnityEngine;

public class LevelHistory : MonoBehaviour
{
    [SerializeField] RefrenceMaster refMaster = default;


    Garden garden;
    Weather weather;
    ForecastLounge forecastLounge;
    //Height is for each plot and width is for each move
    [SerializeField] List<BambooStatus> levelHistoryList = default;
    BambooPlot[] gardenPlots;
    public bool CurrentlyUndoing = false;

    private void Start()
    {
        garden = refMaster.garden;
        weather = refMaster.weather;
        forecastLounge = refMaster.forecastLounge;

        gardenPlots = garden.booPlots;
        //BambooStatus[] placeHolder = new BambooStatus[gardenPlots.Length];
        //levelHistoryList = new List<BambooStatus>(placeHolder);
        //Debug.Log(levelHistoryList[0]);
        for(int i=0;i < gardenPlots.Length;i++)
        {
            levelHistoryList.Add(new BambooStatus());
        }

        weather.ForecastAction += ArchiveGardenState;
        forecastLounge.BackTrackAction += RewindState;


    }

    void ArchiveGardenState()
    {

        for (int i=0; i< gardenPlots.Length; i++)
        {

            levelHistoryList[i].ArchiveStatus(gardenPlots[i].booHeight, gardenPlots[i].booType, gardenPlots[i].isFrozen);
            if (i == 4)
            { //Debug.Log("Archived" + gardenPlots[i].isFrozen);
                Debug.Log("Current History");
                foreach (bool p in levelHistoryList[i].freezeHistory)
                {
                    //Debug.Log(p);
                }
            }
        }
    }

    void RewindState()
    {
        int intendedStateIndex = levelHistoryList[0].heightHistory.Count - 1;
        CurrentlyUndoing = true;
        for (int i = 0; i < gardenPlots.Length; i++)
        {
            if(i == 6) { Debug.Log("Current" + gardenPlots[i].booHeight + "New" + levelHistoryList[i].heightHistory[intendedStateIndex]); }
            gardenPlots[i].booType = levelHistoryList[i].typeHistory[intendedStateIndex];
            gardenPlots[i].booHeight = levelHistoryList[i].heightHistory[intendedStateIndex];
            gardenPlots[i].isFrozen = levelHistoryList[i].freezeHistory[intendedStateIndex];
            levelHistoryList[i].RemoveLastStatus();
        }
        CurrentlyUndoing = false;
    }



}
