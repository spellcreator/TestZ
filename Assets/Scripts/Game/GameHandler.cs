using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{

    private FactoryA factoryA;
    private FactoryB factoryB;

    public Inventory inventory;

    public FactoryAView factoryAView;
    public FactoryBView factoryBView;

    private void Awake()
    {
        //загружаем баланс, или создаем новый если файла нет 
        inventory.Load();
        CreateFactory();
    }

    #region Fabrics
    // Загрузка логики и связывание с визуалом
    void CreateFactory()
    {
        factoryA = new FactoryA(inventory, factoryAView);
        factoryB = new FactoryB(inventory, factoryBView);
    }
    #endregion Fabrics
}
