using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Controller : MonoBehaviour
{
    //public variables
    public Inventory inventory;
    public int inventorySize;

    

    // Start is called before the first frame update
    void Start()
    {
        inventory.InitializeInventory(inventorySize);

    }

    // Update is called once per frame
    void Update()
    {
        //open inventory
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventory.isOpen == false)
            {
                inventory.Show();
            }
            else
            {
                inventory.Hide();
            }
        }
    }
}
