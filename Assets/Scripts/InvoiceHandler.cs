using UnityEngine.UI;
using UnityEngine;

public class InvoiceHandler : MonoBehaviour
{
    private static InvoiceHandler Instance;

    [SerializeField] private GameObject invoiceObjectTemplate;
    [SerializeField] private GameObject invoiceObjectParent;

    private void Awake()
    {
        if(Instance!=null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public static InvoiceHandler GetInstance()
    {
        return Instance;
    }

    public void UpdateInvoice(string sku)
    {
        if (UpdateQuantity(sku))
            return;
        else
        {
            GameObject instantiatedObject = Instantiate(invoiceObjectTemplate, invoiceObjectParent.transform);
            instantiatedObject.name = sku;
            instantiatedObject.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = sku;
            instantiatedObject.transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text = 1.ToString();
            instantiatedObject.SetActive(true);
        }
    }


    //if item is already listed then increase the quantity
    bool UpdateQuantity(string sku)
    {
        foreach(Transform child in invoiceObjectParent.transform)
        {
            if(child.name==sku)
            {
                int currentNumber = int.Parse(child.transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text);
                currentNumber++;
                child.transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text = currentNumber.ToString();
                return true;
            }
        }
        return false;
    }
}
