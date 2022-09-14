using UnityEngine.SceneManagement;
using UnityEngine;

public class TestAssistant : MonoBehaviour
{
    //static bool setToDefault = false;
    // Start is called before the first frame update
    void Start()
    {
        if (!SceneGoverner.offOfDefault)
        {
            if (SceneManager.GetActiveScene().buildIndex != (int)SceneGoverner.SceneList.Default)
            {
                SceneManager.LoadScene((int)SceneGoverner.SceneList.Default);
                //setToDefault = true;
            }
        }
  
    }
}
