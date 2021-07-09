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


    public InventoryView view;


    private void Start()
    {
        FactoryIdle();
    }

    void FactoryIdle()
    {
        isWork = false; // фактори не работает
        slider.gameObject.SetActive(false); // выключаем слайдер 
        button.onClick.AddListener(PrepareFactory); // Вызываем онклик, по сути начинаем подготовку фабрики
    }

    // Проверяем, если фактори НЕ запущена то запускаем 
    void PrepareFactory()
    {
        if (!isWork)// Проверяем что фабрика не работает
        {
            slider.value = 0;// Сбрасываем слайдер в 0
            isWork = true; // Включаем фабрику
            button.onClick.RemoveListener(PrepareFactory); // Выключаем кнопку, чтоб еще раз не запустилась фабрика
            ButtonClick?.Invoke(); // если поставить время в 1 и затапывать кнопку, то можно словить ситуацию когда добавляется сразу 2ед ресурса.
            StartCoroutine(FactoryWork());// Запускаем фабрику
        }
    }
    // Фактора работает и возвращает продукт после РАБоты
    public IEnumerator FactoryWork()
    {
        slider.gameObject.SetActive(true);//Включаем прогресс бар для визуала производства
        slider.DOValue(1, time).OnComplete(() => { slider.value = 0; });//Двигаем слайдер до 1, тайм = время производства. збрасываем слайдер в 0, для ЧСВ 
        yield return new WaitForSeconds(time);// Ждем нужное нам время. Это и считается работой фабрики

        //buttonText.text = "Ready";

        EndWork();
    }
    void EndWork()
    {
        slider.gameObject.SetActive(false);// Выключаем прогресс бар
        isWork = false;//грим что фабрика не работает (закончила работу)
        button.onClick.AddListener(PrepareFactory);//Включаем кнопку, что бы можно было производить следующий ресурс

        var sprite = Instantiate(ball, transform);// Создаем высер нашего производства
        sprite.transform.DOMove(endFlyResourcePoint.position, 1f).OnComplete(() => { Destroy(sprite); view.SetTextA(); }); // Хуярим его на склад
    }

   


}
