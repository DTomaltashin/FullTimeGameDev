using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] string RequiredInvetoryStringName;
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == Player.Instance.gameObject)
        {
            if(Inventory.Instance.inventory.ContainsKey(RequiredInvetoryStringName))
            {
                Destroy(gameObject);
                Inventory.Instance.RemoveInventoryItem(RequiredInvetoryStringName);
            }
        }
    }
}
