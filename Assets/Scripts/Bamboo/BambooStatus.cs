using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BambooStatus
{
    public List<int> heightHistory = new List<int>(); //{ get; private set; }
    public List<WorldDictionary.BooType> typeHistory = new List<WorldDictionary.BooType>(); //= new List<WorldDictionary.BooType>();// { get; private set; }
    public List<bool> freezeHistory = new List<bool>();

    /*public BambooStatus()
    {
        heightHistory = new List<int>();
        typeHistory = new List<WorldDictionary.BooType>();
    }*/

    public void ArchiveStatus(int _height, WorldDictionary.BooType _type, bool _isFrozen)
    {
        heightHistory.Add(_height);
        typeHistory.Add(_type);
        freezeHistory.Add(_isFrozen);
    }

    public void RemoveLastStatus()
    {
        heightHistory.RemoveAt(heightHistory.Count - 1);
        typeHistory.RemoveAt(typeHistory.Count - 1);
        freezeHistory.RemoveAt(freezeHistory.Count - 1);
    }

}
