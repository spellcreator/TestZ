using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FactoryAView : MonoBehaviour
{
    public event Action ButtonClickA;
    public event Action ResourceEndFly;

    private bool isWork;
    public float time = 5;

    public Button button;
    public TMPro.TextMeshProUGUI buttonText;
    public Transform endFlyResourcePoint;
    public GameObject ball;
    public Slider slider;


    private void Start()
    {
        //buttonText.text = "Ready";
        isWork = false;
        slider.gameObject.SetActive(false);
        button.onClick.AddListener(OnButtonClick);
    }

    // ���������, ���� ������� �� �������� �� ��������� 
    private void OnButtonClick()
    {
        if (!isWork)
        {
           //buttonText.text = "Working...";
            slider.value = 0;
            isWork = true;
            button.onClick.RemoveListener(OnButtonClick);
            StartCoroutine(FactoryWork());
        }
    }
    // ������� �������� � ���������� ������� ����� ������
    public IEnumerator FactoryWork()
    {
        slider.gameObject.SetActive(true);
        slider.DOValue(1, time).OnComplete(() => { slider.value = 0; });
        yield return new WaitForSeconds(time);

        slider.gameObject.SetActive(false);
        isWork = false;
        button.onClick.AddListener(OnButtonClick);
        //buttonText.text = "Ready";

        CreateResource();

        ButtonClickA?.Invoke();
    }
    void CreateResource()
    {
        var sprite = Instantiate(ball, transform);
        sprite.transform.DOMove(endFlyResourcePoint.position, 1f).OnComplete(() => { Destroy(sprite); ResourceEndFly?.Invoke(); });
    }
    //slider.value = 0;
    //slider.DOValue(1, time).OnComplete(() => { slider.value = 0; });
    // ������ �������� � ������� ����� �������� DelayedCall

    // �������� ������ �� ��������UI, �������� ������� �� �����. ������� �������� ����� ����� ����. �������� ���
    /*sprite.transform.DOMove(endFlyResourcePoint.position, 1f).OnComplete(() => { });
    Sequence sequence = DOTween.Sequence();
    sequence.Append(DOFade0);
    sequence.Append(DOVirtual.DelayedCall(0f,() => { SetText}));
    sequence.Append(DOFade1);
    sequence.Play();*/
}
