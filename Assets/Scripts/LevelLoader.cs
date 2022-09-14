using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    GovernerRefrence govRef;
    SceneGoverner sceneGoverner;

    private void Start()
    {
        govRef = GetComponent<GovernerRefrence>();
        sceneGoverner = GetComponent<SceneGoverner>();
    }


    public void LoadLevelPrefab(int level)
    {
        string path = "Levels" + "/Level" + level.ToString();
        GameObject levelPrefab = Resources.Load(path) as GameObject;
        Instantiate(levelPrefab);
        govRef.ConnectToRefMaster();
    }
}
