using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Inventory_Item itemPrefab;
    public RectTransform itemPanel;
    public bool isOpen = false;
    public GameObject menuTitle;

    List<Inventory_Item> items = new List<Inventory_Item>();



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //initialize inventory to a size determined on player.cs
    public void InitializeInventory(int inventorySize)
    {
        //loop through creating each inventory slot
        for (int i = 0; i < inventorySize; i++) 
        {
            //spawn slot from a prefab
            Inventory_Item item = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);

            //set game object parent to inventory/inventory_grid/scroll view/viewport/content
            item.transform.SetParent(itemPanel);

            //force correct bug setting prefab instantiations to a massive scale
            item.transform.localScale = new Vector3(1, 1, 1);

            //add inventory slot to list
            items.Add(item);
        }
    }

    //display inventory window
    public void Show()
    {
        //set panel is open toggle
        isOpen = true;

        //display inventory UI
        gameObject.SetActive(true);

        //dispaly menu category
        menuTitle.SetActive(true);
    }

    //close inventory window
    public void Hide()
    {
        //disable panel is open toggle
        isOpen = false;

        //hide inventory UI
        gameObject.SetActive(false);

        //hide menu category
        menuTitle.SetActive(false);
    }
}
