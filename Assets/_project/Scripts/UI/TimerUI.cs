using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    public Image fillBar;

    private CanvasGroup _canvasGroup;

    public TextMeshProUGUI timerTMP;
    public TextMeshProUGUI urgentTMP;

    private int _previousSec;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void SetFillBar(float amount, float remainingTime)
    {
        urgentTMP.gameObject.SetActive(false);
        fillBar.fillAmount = amount;
        var remainingTimeInSec = Mathf.RoundToInt(remainingTime);
        timerTMP.text = remainingTimeInSec.ToString();
        if (remainingTimeInSec < 6)
        {
            urgentTMP.gameObject.SetActive(true);
            urgentTMP.text = remainingTimeInSec.ToString();
            timerTMP.gameObject.SetActive(false);
            if (remainingTimeInSec != _previousSec)
            {
                urgentTMP.transform.localScale = Vector3.zero;
                urgentTMP.transform.DOScale(1.5f, .2f).SetEase(Ease.OutBack);
                _previousSec = remainingTimeInSec;
            }
        }
        else
        {
            timerTMP.gameObject.SetActive(true);
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
        _canvasGroup.DOFade(1, .2f);
    }

    public void Hide()
    {
        _canvasGroup.DOFade(0, .2f).OnComplete(() => gameObject.SetActive(false));
    }
}
