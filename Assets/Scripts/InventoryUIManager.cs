using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    [SerializeField] private TextAsset catalogueJson;
    //private InventoryObject inventoryObject = new InventoryObject();

    [SerializeField] private GameObject templateObject;
    [SerializeField] private GameObject templateParent;

    private GameObject instantiatedGameObject;

    //read json on start and start populating UI
    private void Start()
    {
        string jsonString = catalogueJson.text;
        InventoryObject data = JsonUtility.FromJson<InventoryObject>(jsonString);
        PopulateData(data);
    }


    //populate UI elements
    void PopulateData(InventoryObject data)
    {
        foreach (InventoryClass inventory in data.Inventory)
        {
            instantiatedGameObject = Instantiate(templateObject, templateParent.transform);
            instantiatedGameObject.name = inventory.id;
            instantiatedGameObject.GetComponent<InventoryAssetSelector>().SetData(inventory);
        }
    }
}

[Serializable]
public class InventoryClass
{
    public string id;
    public int price;
    public int quantity;
    public string displayName;
}
[Serializable]
public class InventoryObject
{
    public List<InventoryClass> Inventory;
}