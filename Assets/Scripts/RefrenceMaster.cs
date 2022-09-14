using UnityEngine;

public class RefrenceMaster : MonoBehaviour
{
    [SerializeField] Garden _garden = default;
    [SerializeField] Weather _weather = default;
    [SerializeField] ForecastLounge _forecastLounge = default;
    [SerializeField] LevelRequirements _levelRequirements = default;
    [SerializeField] MouseInputHandler _mouseInputHandler = default;

    private GameObject _governerObject;
    public GameObject GovernerObject {
        private get { return _governerObject; }
        set {
            Debug.Log("Queen" + value);

            _governerObject = value;
            SetGovernerReferences();
        }
    }



    public Garden garden { get { return _garden; } }
    public Weather weather { get { return _weather; } }
    public ForecastLounge forecastLounge { get { return _forecastLounge; } }
    public SceneGoverner sceneGoverner { get; private set; }
    public LevelRequirements levelRequirements { get { return _levelRequirements; } }
    public MouseInputHandler mouseInputHandler { get { return _mouseInputHandler; } }

   

    private void SetGovernerReferences()
    {
        Debug.Log("SCENE");
        sceneGoverner = GovernerObject.GetComponent<SceneGoverner>();
    }
}
