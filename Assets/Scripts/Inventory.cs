using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Inventory_Item itemPrefab;
    public RectTransform itemPanel;
    public bool isOpen = false;
    public GameObject menuTitle;
    public GameLogic gameLogic;
    public Inventory_Description itemDescription;
    public Sprite image;
    public int quantity;
    public string title;
    public string description;
    public MouseFollower mouseFollower;

    List<Inventory_Item> items = new List<Inventory_Item>();



    // Start is called before the first frame update
    void Start()
    {
        itemDescription.ResetDescription();
        mouseFollower.Toggle(false);

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

            item.OnItemClicked += HandleItemSelection;
            item.OnItemBeginDrag += HandleBeginDrag;
            item.OnItemDroppedOn += HandleSwap;
            item.OnItemEndDrag += HandleEndDrag;
            item.OnRightMouseBtnClick += HandleShowItemActions;
        }

        ////////////////////////////////////////////
        //debug purposes only - starting inventory//
        ////////////////////////////////////////////
        items[0].SetData(image, quantity);
    }

    private void HandleShowItemActions(Inventory_Item item)
    {
        
    }

    private void HandleEndDrag(Inventory_Item item)
    {
        mouseFollower.Toggle(false);
    }

    private void HandleSwap(Inventory_Item item)
    {
        
    }

    private void HandleBeginDrag(Inventory_Item item)
    {
        mouseFollower.Toggle(true);
        mouseFollower.SetData(image, quantity);
    }

    private void HandleItemSelection(Inventory_Item item)
    {
        itemDescription.SetDescription(image, title, description);

        ///////////////////////////
        //for debug purposes only//
        ///////////////////////////
        items[0].Select();

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

        //pause game
        gameLogic.PauseGame(true);

        itemDescription.ResetDescription();

        ///////////////////////////
        //for debug purposes only//
        ///////////////////////////
        items[0].Deselect();
        

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

        //unpause game
        gameLogic.PauseGame(false);
    }
}
