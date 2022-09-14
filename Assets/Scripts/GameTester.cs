using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTester : MonoBehaviour
{
    [SerializeField] GovernerBridge govBridge =default;
    SceneGoverner sceneGov;

    private void Start()
    {
        sceneGov = govBridge.sceneGoverner;
    }

    public void GoToTestScene()
    {
        govBridge.sceneGoverner.GoToTestLevel();
    }

    public void GoCredits()
    {
        govBridge.sceneGoverner.GoToCredits();
    }

    public void GoToHelp()
    {
        govBridge.sceneGoverner.GoToHelp();
    }
}
