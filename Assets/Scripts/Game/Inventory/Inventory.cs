using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System;

public class Inventory : MonoBehaviour
{
    public event Action AddResources;

    private Dictionary<ResourceType, int> resources;
    private const string fileName = "Save.json";
    
    public Dictionary<ResourceType, int> Resources
    {
        get { return resources; }
    }
    public string Path
    {
        get { return $"{Application.persistentDataPath}/{fileName}"; }
    }

    #region Save
    public void Save()
    {
       
        string json = JsonConvert.SerializeObject(resources);

        StreamWriter writer = new StreamWriter(Path);
        writer.WriteLine(json);
        writer.Close();
    }
    public void Load()
    {
        var exist = File.Exists(Path); // проверка наличия файла
        if (exist)
        {
            StreamReader sr = new StreamReader(Path); // открываем файл
            string json = sr.ReadLine(); // читаем строку
            sr.Close();
            resources = JsonConvert.DeserializeObject<Dictionary<ResourceType, int>>(json);
        }
        else
        {
            // Добавить проверку там где добавляю ресурс, чтоб по нормальному было
            resources = new Dictionary<ResourceType, int>() { { ResourceType.ResourceA, 0 }, { ResourceType.ResourceB, 0 } }; ;
            
            Save(); // Cоздаем файл, если его нет. Если есть используем Load
        }
    }
    #endregion Save

    public void AddResource(ResourceType type, int count = 1)
    {
        var factory = new CollectorFactory(resources);
        
        var collector = factory.GetCollector(type);

        if (collector != null)
        {
            collector.Collect(count);
            AddResources?.Invoke();
        }
        else
        {
            Debug.LogWarning("Collector is Null");
        }
        Save();
    }

    public bool ConsumeResource(ResourceType type, int count)
    {
        if(Resources[type] >= count)
        {
            Resources[type] -= count;
            return true;
        }
        else
        {
            return false;
        }
    }


    public void ResetCounter()
    {
        resources = new Dictionary<ResourceType, int>() { { ResourceType.ResourceA, 0 }, { ResourceType.ResourceB, 0 } }; ;
    }
}
