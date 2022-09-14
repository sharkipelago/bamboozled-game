using UnityEngine;
using DG.Tweening;
using System.Collections;

public class FloatTween : MonoBehaviour
{
    float newPosition;
    [SerializeField] float tweenDuration = 4f;
    [SerializeField] float initialDelay;

    private void Start()
    {
        StartCoroutine(Delay());
    }

    void StartTween()
    {
        newPosition = Random.Range(.2f, .5f);

        if (PlayerPrefs.GetInt("SFX", 1) != 0)
        {
            DOTween.Init();
            transform.DOLocalMoveY(transform.position.y - newPosition, tweenDuration).SetLoops(-1, LoopType.Yoyo);

        }
    }

    IEnumerator Delay() {

        yield return new WaitForSeconds(initialDelay);
        StartTween();
    }
}
