using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    Dictionary<string, Sprite> inventory = new Dictionary<string, Sprite>();
    //refer the image ui gameobject you want to update based on InventoryItems
    [SerializeField] Image inventorySpriteImage;
    //the default inventoryImage when no item is collected or active
    [SerializeField] Sprite inventorBlankSprite;

    public void AddInventoryItem(string itemName, Sprite itemImage)
    {
        inventory.Add(itemName,itemImage);
        //swap inventoryBlank with added inventory Item
        inventorySpriteImage.sprite = itemImage;
    }

    public void RemoveInventoryItem(string itemName)
    {
        inventory.Remove(itemName);
        //swap inventoryItem with inventoryBlank
        inventorySpriteImage.sprite = inventorBlankSprite;
    }

}
