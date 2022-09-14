using UnityEngine;

public class LevelSelectionRequest : MonoBehaviour
{
    [SerializeField] GovernerBridge governerBridge = default;

    public void RequestLevel(int level)
    {
        governerBridge.sceneGoverner.EnterLevel(level);
    }
}
