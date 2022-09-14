using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Back : MonoBehaviour
{
    SceneGoverner sceneGoverner;
    // Start is called before the first frame update
    void Start()
    {
        sceneGoverner = FindObjectOfType<SceneGoverner>();
    }

    public void BackFunc()
    {
        sceneGoverner.GoToLevelSelect();
    }
}
