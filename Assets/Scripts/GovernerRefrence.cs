using UnityEngine;

public class GovernerRefrence : MonoBehaviour
{
    SceneGoverner sceneGoverner;

    private void Start()
    {
        sceneGoverner = gameObject.GetComponent<SceneGoverner>();
    }

    public void ConnectToRefMaster()
    {
        RefrenceMaster refMaster = FindObjectOfType<RefrenceMaster>();
        refMaster.GovernerObject = gameObject;
    }

    public void ConnectToGovernerBridge(bool isSecondScreen)
    {
        Debug.Log("Connecting with" + isSecondScreen);
        GovernerBridge govBridge = FindObjectOfType<GovernerBridge>();
        govBridge.sceneGoverner = sceneGoverner;
        if (isSecondScreen) { govBridge.mainScreenViceroy.ToggleScreen(true); }
        //govBridge.optionsMenu.UpdatePrefs();
    }
}
