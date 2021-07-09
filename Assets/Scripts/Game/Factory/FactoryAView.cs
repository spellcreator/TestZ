using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FactoryAView : MonoBehaviour
{

    public event Action ButtonClick;

    private bool isWork;
    public float time = 5;

    public Button button;
    public TMPro.TextMeshProUGUI buttonText;
    public Transform endFlyResourcePoint;
    public GameObject ball;
    public Slider slider;

    public Inventory inventory;
    public InventoryView view;


    private void Start()
    {
        FactoryIdle();
    }

    private void FactoryIdle()
    {
        SetText();
        inventory.AddResources += SetText;
        isWork = false; 
        slider.gameObject.SetActive(false); 
        button.onClick.AddListener(PrepareFactory); 
    }

    // Проверяем, если фактори НЕ запущена то запускаем 
    private void PrepareFactory()
    {
        if (!isWork)
        {
            slider.value = 0;
            isWork = true; 
            button.onClick.RemoveListener(PrepareFactory);
            ButtonClick?.Invoke();
            StartCoroutine(FactoryWork());
        }
    }
    // Фактора работает и возвращает продукт после РАБоты
    private IEnumerator FactoryWork()
    {
        slider.gameObject.SetActive(true);
        slider.DOValue(1, time).OnComplete(() => { slider.value = 0; }); 
        yield return new WaitForSeconds(time);

        EndWork();
    }
    private void EndWork()
    {
        slider.gameObject.SetActive(false);
        isWork = false;
        button.onClick.AddListener(PrepareFactory);
        SetText();
        var sprite = Instantiate(ball, transform);
        sprite.transform.DOMove(endFlyResourcePoint.position, 1f).OnComplete(() => { Destroy(sprite); view.SetTextA(); });
    }

    private void SetText()
    {
        if(!isWork)
        {
            buttonText.text = ("Ready");
        }
        else
        {
            buttonText.text = ("Working");
        }
    }
}
