using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LoadingView : MonoBehaviour
{
    [SerializeField]
    private float loadingTime = 3.5f;

    [SerializeField]
    private Image logo;
    
    [SerializeField]
    private Image back;

    public void ShowLogo()
    {
        logo.color = new Color(1, 1, 1, 0);
        logo.DOFade(1, loadingTime).SetEase(Ease.Linear);
    }public void FadeLoading()
    {
        logo.color = new Color(1, 1, 1, 1);
        back.color = new Color(0, 0, 0, 1);
        logo.DOFade(0, loadingTime).SetEase(Ease.Linear);
        back.DOFade(0, loadingTime).SetEase(Ease.Linear);
    }
}
