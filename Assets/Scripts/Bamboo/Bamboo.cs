using UnityEngine;

[CreateAssetMenu(fileName = "New Bamboo", menuName = "Shoots/Bamboo")]
public class Bamboo : ScriptableObject
{
    [SerializeField] GameObject _bambooStalk = default;
    [SerializeField] Sprite _culmSprite = default;

    public GameObject bambooStalk { get { return _bambooStalk; } }
    public Sprite culmSprite { get { return _culmSprite; } }
}
