using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSystem : MonoBehaviour
{
    [SerializeField] private InventorySO inventoryData;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Item item = collision.GetComponent<Item>();
        if (item != null)
        {
            int remander = inventoryData.AddItem(item.InventoryItem, item.Quantity);
            if (remander == 0)
            {
                item.DestroyItem();
                
            }
            else
            {
                item.Quantity = remander;
            }
        }
    }
}
