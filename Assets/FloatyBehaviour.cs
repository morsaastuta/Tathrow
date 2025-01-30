using DG.Tweening;
using UnityEngine;

public class FloatyBehaviour : MonoBehaviour
{
    [Header("X")]
    [SerializeField] bool x;
    [SerializeField] float xFactor;
    [SerializeField] float xTime;

    [Header("Y")]
    [SerializeField] bool y;
    [SerializeField] float yFactor;
    [SerializeField] float yTime;

    void Start()
    {
        transform.position = new(transform.position.x - xFactor/2, transform.position.y - yFactor/2, transform.position.z);

        if (x) ((RectTransform)transform).DOAnchorPosX(((RectTransform)transform).anchoredPosition.x + xFactor, xTime).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);

        if (y) ((RectTransform)transform).DOAnchorPosY(((RectTransform)transform).anchoredPosition.y + yFactor, yTime).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }
}
