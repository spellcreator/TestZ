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

    private void Start()
    {
        button.onClick.AddListener(OnButtonClick);
        isWork = false;
        slider.gameObject.SetActive(false);
    }

    private void Update()
    {
        SetText();
    }

    // Проверяем, если фактори НЕ запущена то запускаем 
    private void OnButtonClick()
    {
        if (!isWork)
        {
            if(view.inventory.Resources[ResourceType.ResourceA] >= 10)
            {
                slider.value = 0;
                isWork = true;
                button.onClick.RemoveListener(OnButtonClick);
                ButtonClick?.Invoke();
                view.SetTextA();
                StartCoroutine(SpawnRedBall());
            }

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
    }

    public IEnumerator FactoryWork()
    {
        slider.gameObject.SetActive(true);
        slider.DOValue(1, time).OnComplete(() => { slider.value = 0;});
        yield return new WaitForSeconds(time);

        slider.gameObject.SetActive(false);
        isWork = false;
        button.onClick.AddListener(OnButtonClick);

        CreateResource();

    }
    void CreateResource()
    { 
         var sprite = Instantiate(Bball, transform);
         sprite.transform.DOMove(endFlyResourcePoint.position, 1f).OnComplete(() => { Destroy(sprite); view.SetTextB(); });

    }

    void SetText()
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




/*


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FactoryAView : MonoBehaviour
{

    public event Action ButtonClick;

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
        ButtonClick?.Invoke();
        slider.gameObject.SetActive(false);// Выключаем прогресс бар
        isWork = false;//грим что фабрика не работает (закончила работу)
        button.onClick.AddListener(PrepareFactory);//Включаем кнопку, что бы можно было производить следующий ресурс

        var sprite = Instantiate(ball, transform);// Создаем высер нашего производства
        sprite.transform.DOMove(endFlyResourcePoint.position, 1f).OnComplete(() => { Destroy(sprite); }); // Хуярим его на склад
    }

}
*/