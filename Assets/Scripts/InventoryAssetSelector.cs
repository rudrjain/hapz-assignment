using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryAssetSelector : MonoBehaviour
{
    private InventoryClass data;
    [SerializeField] private Image catalogueImage;
    [SerializeField] private Text productNameText, quantityText, addToCartText;
    [SerializeField] private Button addToCartButton;
    [SerializeField] private int quantity;

    private void Start()
    {
        addToCartButton.onClick.AddListener(AddObjectToCart);
    }

    //get data from ui manager and set on button
    public void SetData(InventoryClass invntry)
    {
        data = invntry;
        quantity = data.quantity;
        SetDataOnTemplate();
        SetImage();
        gameObject.SetActive(true);
    }

    //set the text values
    private void SetDataOnTemplate()
    {
        productNameText.text = data.displayName;
        quantityText.text = "Quantity - " + data.quantity.ToString();
        addToCartText.text = "Buy Now For INR " + data.price.ToString();
        HandleActiveInactiveButton();
    }

    //set the thumbnail of tile
    private void SetImage()
    {
        Sprite loadedSprite = Resources.Load<Sprite>("Thumbnails/" + data.id);

        if (loadedSprite != null)
        {
            catalogueImage.sprite = loadedSprite;
        }
        else
        {
            Debug.LogError("Failed to load sprite with name: " + data.id);
        }
    }

    //disable button whenever quanity reaches 0
    private void HandleActiveInactiveButton()
    {
        if(quantity<1)
        {
            addToCartButton.interactable = false;
            quantityText.text = "Quantity - 0";
        }
        else
        {
            addToCartButton.interactable = true;
            quantityText.text = "Quantity - " +quantity.ToString();
        }
    }

    //instantiate 3d object
    private void AddObjectToCart()
    {
        quantity--;
        HandleActiveInactiveButton();
        InventoryController.GetInstance().AddToCart(data.id, data.price);
        InvoiceHandler.GetInstance().UpdateInvoice(data.id);
    }
}
