using DG.Tweening;
using UnityEngine;

public class Potion : MonoBehaviour
{
    private void Start()
    {
        transform.DOMoveY(transform.position.y + 1, 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }
}
