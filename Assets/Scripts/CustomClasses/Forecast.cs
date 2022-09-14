using UnityEngine;

[CreateAssetMenu (fileName = "New Forecast", menuName = "Shoots/Forecast")]
public class Forecast : ScriptableObject
{
    [SerializeField] Sprite _cardSprite = default;
    [SerializeField] Sprite _highlightedCardSprite = default;
    [SerializeField] Sprite _objectSprite = default;

    public Sprite cardSprite { get { return _cardSprite; } }
    public Sprite highlightedCardSprite { get { return _cardSprite; } }
    public Sprite objectSprite { get { return _objectSprite; } }
    int range;
}
