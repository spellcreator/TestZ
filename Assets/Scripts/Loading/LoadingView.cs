using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LoadingView : MonoBehaviour
{
    [SerializeField]
    private float showLogo = 3.5f;
    [SerializeField]
    private float fadeLogo = 2.5f;
    [SerializeField]
    private float fadeBack = 2.5f;

    [SerializeField]
    private Image logo;
    
    [SerializeField]
    private Image back;

    [SerializeField]
    private Loading loading;

    private void Start()
    {
        ShowLogo();
    }
    public void ShowLogo()
    {
        logo.color = new Color(1, 1, 1, 0);
        logo.DOFade(1, showLogo).SetEase(Ease.Linear).OnComplete(() => { FadeLoading(); });
    }
    public void FadeLoading()
    {
        logo.color = new Color(1, 1, 1, 1);
        back.color = new Color(0, 0, 0, 1);
        logo.DOFade(0, fadeLogo).SetEase(Ease.Linear).OnComplete(() => { back.DOFade(0, fadeBack).SetEase(Ease.Linear);});
        DOVirtual.DelayedCall(fadeLogo - loading.LoadingProgressTime, () => loading.WaitForProgram());  
    }
}
