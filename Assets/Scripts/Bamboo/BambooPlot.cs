using System.Collections.Generic;
using UnityEngine;

public class BambooPlot : MonoBehaviour
{
    //!!! Rewrite so calling grow increasees booheight not othter way round because if we want to grow more than 1 grow will only trigger once
    [SerializeField] Weather weather;
    [SerializeField] LevelRequirements levelReqs = default;
    [SerializeField] LevelHistory levelHistory;
    //[SerializeField] ForecastLounge forecastLounge = default;
    //Changing the Type to something other than none will create a shoot
    [SerializeField] WorldDictionary.BooType _booType;
    bool currentlyUndoing { get { return levelHistory.CurrentlyUndoing; } }

    bool _isFrozen = false;
    public bool isFrozen { get{ return _isFrozen; }
        set {
            if(thisStalk == null)
            {
                Debug.LogWarning(name + "Nothing to Freeze");
            }
            else
            {
                _isFrozen = value;
                FreezeToggle();
            }
            //Debug.Log(name + "B-Freeze" + _isFrozen);

            //if (_isFrozen == value) { return; }

            //Debug.Log(name + "Post-Freeze" + _isFrozen);

        }
    }

    public WorldDictionary.BooType booType
    {
        get { return _booType; }
        set {

            if (thisStalkObject != null)
                ClearCurrentStalk();

            _booType = value;

            if (value != WorldDictionary.BooType.None) {

                stalkObjectPrefab = WorldDictionary.BooDictionary[value].bambooStalk;
                Bud();
                booHeight = 0;
            }
            else
            {
                booHeight = -1;
            }
        }
    }

    private BambooStalk thisStalk = null;
    private GameObject stalkObjectPrefab = null;
    private GameObject thisStalkObject = null;
    /*public GameObject stalkObject {
        get { return _stalkObject;}
        set {
            if (value != null)
                thisStalk = stalkObject.GetComponent<BambooStalk>();
        }
    }*/

    //-1 Is no Bamboo, 0 is Shoot, >1 is how many culms
    [Range (-1, 8)][SerializeField] int initialBooHeight = -1;
    public SpriteRenderer spriteRenderer { get; private set; }



    private int _booHeight = -1;
    public int booHeight {
        get { return _booHeight;}
        //Not private set so weather can change it
        set{
            if(booType != WorldDictionary.BooType.None)
            {
                int newValue;
                if (value > thisStalk.maxHeight)
                {
                    Debug.LogWarning(name + "Has Grown to it's max");
                    newValue = thisStalk.maxHeight;
                }
                else
                    newValue = value;

                if (_booHeight >= 0)
                {
                    int change = newValue - _booHeight;
                    levelReqs.UpdateLevelProgress(_booType, change, currentlyUndoing);
                    if(_booType == WorldDictionary.BooType.Yellow) { Debug.Log("Change " + change + "At" + transform.GetSiblingIndex().ToString()); }
                }

                _booHeight = newValue;
                thisStalk.ChangeHeight(newValue);
            }
        }
    }

    private void Awake()
    {
        booType = _booType;
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        //stalkObject = (booType != WorldDictionary.BooDictKeys.None) ? WorldDictionary.BooDictionary[booType] : null;
        if (stalkObjectPrefab != null)
        {
            //Allows for lazy setting of heights (setting a type but nnot changing the number)
            if(initialBooHeight < 0) { return; }
            booHeight = initialBooHeight;
        }
    }



    //Instantiating Stalk Prefab
    void Bud()
    {
 
        GameObject newStalkObj = Instantiate(stalkObjectPrefab);
        newStalkObj.transform.parent = transform;
        newStalkObj.transform.localPosition = Vector2.zero;
        thisStalk = newStalkObj.GetComponent<BambooStalk>();
        thisStalkObject = newStalkObj;
    }

    private void ClearCurrentStalk()
    {
        booHeight = 0;
        Destroy(thisStalkObject);
    }

    void FreezeToggle()
    {
        //Debug.Log(name + thisStalk.freezeIndicator.enabled);
        thisStalk.freezeIndicator.enabled = _isFrozen;
    }

    private void OnMouseEnter()
    {

        Debug.Log("Hovern' Over Plot:" + name);
    }
     
    private void OnMouseDown()
    {
        Debug.Log("poop");
    }
}
