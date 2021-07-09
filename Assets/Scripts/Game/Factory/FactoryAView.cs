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
    [SerializeField]
    private float time = 5;
    [SerializeField]
    private float timeResourceFly = 1f;
    [SerializeField]
    private Button button;
    [SerializeField]
    private TMPro.TextMeshProUGUI buttonText;
    [SerializeField]
    private Transform endFlyResourcePoint;
    [SerializeField]
    private GameObject ball;
    [SerializeField]
    private Slider slider;

    [SerializeField]
    private Inventory inventory;
    [SerializeField]
    private InventoryView view;


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
            SetText();
            DOVirtual.DelayedCall(time + timeResourceFly, () => ButtonClick?.Invoke());
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
        sprite.transform.DOMove(endFlyResourcePoint.position, timeResourceFly).OnComplete(() => { Destroy(sprite); view.SetTextA(); });
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
