using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    private static InventoryController Instance;

    [Header("For Instantiating Objects")]
    [SerializeField] private GameObject _3dParent;

    [Header("For UI")]
    [SerializeField] private Text totalValueText;
    [SerializeField] private Text totalItemsText;


    private int totalValue = 0;
    private int totalItems = 0;

    private void Awake()
    {
        //singleton class
        if(Instance!=null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public static InventoryController GetInstance()
    {
        return Instance;
    }

    //public function being called on whenever a click is recorded in ui
    public void AddToCart(string objectId, int itemValue)
    {
        totalItems++;
        totalValue += itemValue;
        UpdateCatalogueUI();
        InstantiateObject(objectId);

    }

    //update the value and number of items in ui
    private void UpdateCatalogueUI()
    {
        totalValueText.text = "Total Value in Cart = INR " + totalValue.ToString();
        totalItemsText.text = "Total Items in Cart = " + totalItems.ToString();
    }

    //instantiate the asset
    private void InstantiateObject(string objectId)
    {
        if (string.IsNullOrEmpty(objectId))
        {
            Debug.LogError("Prefab name is not specified!");
            return;
        }


        if (CloneObject(objectId))
            return;

        GameObject prefab = Resources.Load<GameObject>("Assets/" + objectId);

        if (prefab != null)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-0.2f, 0.2f), 4f, Random.Range(-0.1f, 0.2f));
            GameObject instantiatedPrefab = Instantiate(prefab, spawnPosition, Quaternion.identity);

            if (_3dParent != null)
            {
                instantiatedPrefab.transform.SetParent(_3dParent.transform);
                instantiatedPrefab.name = objectId;
            }
            else
            {
                Debug.LogError("GameObject named '_3dParent' not found in the scene!");
            }
        }
        else
        {
            Debug.LogError("Failed to load prefab with name: " + objectId);
        }

    }

    //clone the existing gameobject in hierarchy if it exists, instead of fetching from resources
    private bool CloneObject(string objectId)
    {
        foreach(Transform child in _3dParent.transform)
        {
            if(child.name==objectId)
            {
                GameObject clonedObject = Instantiate(child.gameObject, Vector3.one*100, Quaternion.identity);
                clonedObject.gameObject.SetActive(false);
                clonedObject.transform.SetParent(_3dParent.transform);
                clonedObject.transform.localPosition = new Vector3(Random.Range(-0.2f, 0.2f), 4f, Random.Range(-0.1f, 0.2f));
                clonedObject.transform.localRotation = Quaternion.identity;
                clonedObject.SetActive(true);
                return true;
            }
        }
        return false;
    }
}
