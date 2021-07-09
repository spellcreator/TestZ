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
        //��������� ������, ��� ������� ����� ���� ����� ��� 
        inventory.Load();
        CreateFactory();
    }

    #region Fabrics
    // �������� ������ � ���������� � ��������
    void CreateFactory()
    {
        factoryA = new FactoryA(inventory, factoryAView);
        factoryB = new FactoryB(inventory, factoryBView);
    }
    #endregion Fabrics
}
