using UnityEngine;
using DG.Tweening;

public class MainMenuGrowthTween : MonoBehaviour
{
    [SerializeField] float durationInMinutes = 10f;
    [SerializeField] SpriteRenderer message;

    void Start()
    {
        //Eat ma ass
        DOTween.Init();
        if(PlayerPrefs.GetInt("SFX",1) == 1)
        {
            Sequence mySequence = DOTween.Sequence();
            mySequence.Append(transform.DOMoveY(0, durationInMinutes * 60));
            mySequence.Append(message.DOFade(1, 5f));
            mySequence.Append(message.DOFade(0, 5f));
        }
    }


}
