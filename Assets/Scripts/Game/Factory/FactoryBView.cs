using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FactoryBView : MonoBehaviour
{
    public event Action ButtonClick;
    [SerializeField]
    private bool isWork;
    [SerializeField]
    private float time = 5;
    [SerializeField]
    private float timeResourceFly = 1f;
    [SerializeField]
    private int ballInAnim;

    [SerializeField]
    private Button button;
    [SerializeField]
    private TMPro.TextMeshProUGUI buttonText;
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private Transform endFlyResourcePoint;
    [SerializeField]
    private Transform midFlyResourcePoint;
    [SerializeField]
    private Transform redFabricFlyResourcePoint;
    [SerializeField]
    private GameObject Bball;
    [SerializeField]
    private GameObject Rball;
    [SerializeField]
    private InventoryView view;
    [SerializeField]
    private Inventory inventory;


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
            if(inventory.ConsumeResource(ResourceType.ResourceA, 10))
            {
                slider.value = 0;
                isWork = true;
                button.onClick.RemoveListener(PrepareFactory);
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
            sprite.transform.DOMove(transform.position, timeMove).OnComplete(() => { Destroy(sprite);});
            Vector3[] pass = new Vector3[2];
            pass[0] = midFlyResourcePoint.position;
            pass[1] = transform.position;
            sprite.transform.DOPath(pass, timeMove, PathType.CatmullRom, PathMode.Sidescroller2D);
        }
        yield return new WaitForSeconds(spawnTime * ballInAnim * timeMove);
        StartCoroutine(FactoryWork());
    }

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
        var sprite = Instantiate(Bball, transform);
        sprite.transform.DOMove(endFlyResourcePoint.position, timeResourceFly).OnComplete(() => { Destroy(sprite); ButtonClick?.Invoke(); view.SetTextB();});
        DOVirtual.DelayedCall(time + timeResourceFly, () => SetText());
    }

    private void SetText()
    {
        if(isWork == false && inventory.Resources[ResourceType.ResourceA] >= 10)
        {
            buttonText.text = ("Ready");
        }
        else
        {
            buttonText.text = ("Disable");

        }
    }

}