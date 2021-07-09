using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{

    private FactoryA factoryA;
    private FactoryB factoryB;

    public InventoryView inventoryView;

    public FactoryAView factoryAView;
    public FactoryBView factoryBView;

    private void Awake()
    {
        //��������� ������, ��� ������� ����� ���� ����� ��� 
        inventoryView.inventory.Load();
        Logic();
    }

    #region Fabrics
    // �������� ������ � ���������� � ��������
    void Logic()
    {
        factoryA = new FactoryA(inventoryView.inventory, factoryAView);
        factoryB = new FactoryB(inventoryView.inventory, factoryBView);
    }
    #endregion Fabrics
}
