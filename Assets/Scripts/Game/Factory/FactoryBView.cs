using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FactoryBView : MonoBehaviour
{
    public event Action ButtonClickB;
    public event Action ResourceEndFly;

    private bool isWork;
    public float time = 5;
    public int ballInAnim;

    public Button button;
    public TMPro.TextMeshProUGUI buttonText;
    public Slider slider;
    public Transform endFlyResourcePoint;
    public Transform redFabricFlyResourcePoint;
    public GameObject Bball;
    public GameObject Rball;


    private void Start()
    {
        buttonText.text = "Ready";
        button.onClick.AddListener(OnButtonClick);
        isWork = false;
        slider.gameObject.SetActive(false);
    }
    // Проверяем, если фактори НЕ запущена то запускаем 
    private void OnButtonClick()
    {
        if (!isWork)
        {
            buttonText.text = "Waiting...";
            slider.value = 0;
            isWork = true;
            button.onClick.RemoveListener(OnButtonClick);
            StartCoroutine(SpawnRedBall());
            //StartCoroutine(FactoryWork());

        }
    }
    // Фактора работает и возвращает продукт после РАБоты
    public IEnumerator SpawnRedBall()
    {
        float spawnTime = 0.1f;
        ballInAnim = 10;
        float timeMove = 2.5f;
        for(int i = 0; i <= ballInAnim; i++)
        {
            yield return new WaitForSeconds(spawnTime);
            var sprite = Instantiate(Rball, redFabricFlyResourcePoint);
            sprite.transform.DOMove(transform.position, timeMove).OnComplete(() => { Destroy(sprite);  });
        }
        yield return new WaitForSeconds(spawnTime * ballInAnim * timeMove);
        StartCoroutine(FactoryWork());
        buttonText.text = "Working...";
    }

    public IEnumerator FactoryWork()
    {
        slider.gameObject.SetActive(true);
        slider.DOValue(1, time).OnComplete(() => { slider.value = 0; });
        yield return new WaitForSeconds(time);

        slider.gameObject.SetActive(false);
        isWork = false;
        button.onClick.AddListener(OnButtonClick);
        buttonText.text = "Ready";


        CreateResource();

        ButtonClickB?.Invoke();
        Debug.Log("WorkEnd");
    }
    void CreateResource()
    {
        var sprite = Instantiate(Bball, transform);
        sprite.transform.DOMove(endFlyResourcePoint.position, 1f).OnComplete(() => { Destroy(sprite); ResourceEndFly?.Invoke(); });

    }
}








    //slider.value = 0;
    //slider.DOValue(1, time).OnComplete(() => { slider.value = 0; });
    // убрать карутину и сделать через онкомлит DelayedCall

    // Поменять текста на текстпроUI, закинуть фабрики на сцену. Сделать анимации через хуйню ниже. Прогресс бар
    /*sprite.transform.DOMove(endFlyResourcePoint.position, 1f).OnComplete(() => { });
    Sequence sequence = DOTween.Sequence();
    sequence.Append(DOFade0);
    sequence.Append(DOVirtual.DelayedCall(0f,() => { SetText}));
    sequence.Append(DOFade1);
    sequence.Play();*/