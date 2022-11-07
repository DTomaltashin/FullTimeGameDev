using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    enum ItemType { Coin, Health, Ammo, InventoryItem } //create dropdown menu for itemtypes
    [SerializeField] ItemType itemType; // variable declaration for itemtype

    //if InventoryItem then required fields;
    [SerializeField] Sprite inventorySprite;
    [SerializeField] string inventoryItemName;
    
    void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == Player.Instance.gameObject)//as the player object is a singleton we can access it directly like this
        {
            if (itemType == ItemType.Coin)
            {
               Player.Instance.coinsCollected++;
            }
            else if (itemType == ItemType.Health)
            {
                //add health... not more than 100
                if (Player.Instance.health < 100)
                {
                    Player.Instance.health += 10;
                }
            }
            else if (itemType == ItemType.Ammo)
            {

            }
            else if (itemType == ItemType.InventoryItem)
            {
                Inventory.Instance.AddInventoryItem(inventoryItemName, inventorySprite);
            }
            else
            {

            }

            Player.Instance.UpdateUI();
            Destroy(gameObject);
        }
    }
}
