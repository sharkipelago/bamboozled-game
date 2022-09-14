using UnityEngine;

public class BambooStalk : MonoBehaviour
{
    //public Weather weather;
    //[SerializeField] GameObject bambooCulm;
    [SerializeField] GameObject bambooShoot = default;
    //public int booHeight { get; private set; } = 0;
    bool shootDestroyed = false;

    public SpriteRenderer freezeIndicator = default;

    [SerializeField] GameObject[] culmList = default;
    public int maxHeight { get { return culmList.Length; } } 

    private void Start()
    {
        int count = transform.childCount;
        GameObject[] culmList = new GameObject[count];

        for(int i = 0; i < count; i++)
        {
            culmList[i] = transform.GetChild(i).gameObject;
        }
    }

    private void DestroyShoot() {
        bambooShoot.SetActive(false);
        shootDestroyed = true;
    } 

    public void ChangeHeight(int newHeight)
    {
        if(newHeight == 0) { return; }
        if (!shootDestroyed) { DestroyShoot(); }
        foreach (var culm in culmList) { culm.SetActive(false); }
        for(int i = 0; i < newHeight; i++)
        {
            culmList[i].SetActive(true);
        }
    }

    /*public void Grow(int growAmount)
    {
        if (!shootDestroyed) { DestroyShoot(); }
        for(int i = 0; i<growAmount;i++)
        {
            GameObject newCulm = Instantiate(bambooCulm);
            Transform newCulmTrans = newCulm.transform;
            newCulmTrans.parent = transform;
            newCulmTrans.localPosition = new Vector3(0, 1.55f * booHeight, 0);
            if (booHeight % 2 > 0)
                newCulmTrans.localScale = new Vector3(-1, 1, 1); 
            booHeight++;
        }
    }*/
}
