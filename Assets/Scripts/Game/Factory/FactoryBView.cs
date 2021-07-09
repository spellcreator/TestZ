using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FactoryBView : MonoBehaviour
{
    public event Action ButtonClick;

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
    public InventoryView view;
    public Inventory inventory;


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
            if(view.inventory.Resources[ResourceType.ResourceA] >= 10)
            {
                slider.value = 0;
                isWork = true;
                button.onClick.RemoveListener(PrepareFactory);
                ButtonClick?.Invoke();
                view.SetTextA();
                StartCoroutine(SpawnRedBall());
            }
        }
    }
    // Фактора работает и возвращает продукт после РАБоты
    private IEnumerator SpawnRedBall()
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
    }

    private IEnumerator FactoryWork()
    {
        slider.gameObject.SetActive(true);
        slider.DOValue(1, time).OnComplete(() => { slider.value = 0;});
        yield return new WaitForSeconds(time);

        slider.gameObject.SetActive(false);
        isWork = false;
        button.onClick.AddListener(PrepareFactory);

        CreateResource();

    }
    private void CreateResource()
    { 
         var sprite = Instantiate(Bball, transform);
         sprite.transform.DOMove(endFlyResourcePoint.position, 1f).OnComplete(() => { Destroy(sprite); view.SetTextB(); });
    }

    private void SetText()
    {
        if(isWork == false && view.inventory.Resources[ResourceType.ResourceA] >= 10)
        {
            buttonText.text = ("Ready");
        }
        else
        {
            buttonText.text = ("Disable");
        }
    }
}