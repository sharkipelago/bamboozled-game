using UnityEngine;

[System.Serializable]
public class Requirement 
{
    [SerializeField] WorldDictionary.BooType _requirementType = default;
    [SerializeField] int _requirementAmount = default;

    public WorldDictionary.BooType reqType { get {
            if (_requirementType == WorldDictionary.BooType.None)
                Debug.LogWarning("RequirementType is None");
            return _requirementType;
        } }
    public int reqAmount { get { return _requirementAmount; } }
}
