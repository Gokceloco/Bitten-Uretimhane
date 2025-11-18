using UnityEngine;
using DG.Tweening;

public class HealthBar : MonoBehaviour
{
    public Transform fillBarPivot;
    private void Start()
    {
        gameObject.SetActive(false);
    }
    public void SetFillAmount(float ratio)
    {
        if (ratio > 0)
        {
            fillBarPivot.DOKill();
            fillBarPivot.DOScaleX(ratio, .3f);
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        fillBarPivot.DOKill();
    }
}
