using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    enum ItemType { Coin, Health, Ammo, InventoryItem } //create dropdown menu for itemtypes
    [SerializeField] ItemType itemType; // variable declaration for itemtype

    Player playerScript;
    Inventory inventoryScript;

    //if InventoryItem then required fields;
    [SerializeField] Sprite inventorySprite;
    [SerializeField] string inventoryItemName;
    
    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<Player>();
        inventoryScript = GameObject.Find("Player").GetComponent<Inventory>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            if (itemType == ItemType.Coin)
            {
                playerScript.coinsCollected++;
            }
            else if (itemType == ItemType.Health)
            {
                //add health... not more than 100
                if (playerScript.health < 100)
                {
                    playerScript.health += 10;
                }
            }
            else if (itemType == ItemType.Ammo)
            {

            }
            else if (itemType == ItemType.InventoryItem)
            {
                inventoryScript.AddInventoryItem(inventoryItemName, inventorySprite);
            }
            else
            {

            }

            playerScript.UpdateUI();
            Destroy(gameObject);
        }
    }
}
