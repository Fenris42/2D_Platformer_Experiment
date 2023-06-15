using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //public variables
    public Inventory_Item itemPrefab;
    public RectTransform itemPanel;
    public bool isOpen = false;
    public GameObject menuTitle;
    public GameLogic gameLogic;
    public Inventory_Description itemDescription;
    public MouseFollower mouseFollower;
    

    //private variables
    private int currentlyDraggedItemIndex = -1;
    [SerializeField] private ItemActionPanel actionPanel;

    //inventory array
    List<Inventory_Item> items = new List<Inventory_Item>();

    //events
    public event Action<int> OnDescriptionRequested, OnItemActionRequested, OnStartDragging;
    public event Action<int, int> OnSwapItems;

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

    }

    public void UpdateData(int itemIndex, Sprite itemImage, int itemQuantity)
    {
        if (items.Count > itemIndex)
        {
            items[itemIndex].SetData(itemImage, itemQuantity);
        }
    }

    private void HandleShowItemActions(Inventory_Item item)
    {
        int index = items.IndexOf(item);
        if (index == -1)
        {
            return;
        }
        OnItemActionRequested?.Invoke(index);
    }

    private void HandleEndDrag(Inventory_Item item)
    {
        ResetDraggedItem();
    }

    private void HandleSwap(Inventory_Item item)
    {
        int index = items.IndexOf(item);
        if (index == -1)
        {
            return;
        }
        OnSwapItems?.Invoke(currentlyDraggedItemIndex, index);
        HandleItemSelection(item);

    }

    private void HandleBeginDrag(Inventory_Item item)
    {
        
        int index = items.IndexOf(item);
        if (index == -1)
        {
            return;
        }
        currentlyDraggedItemIndex = index;
        HandleItemSelection(item);
        OnStartDragging?.Invoke(index);
    }

    public void CreateDraggedItem(Sprite sprite, int quantity)
    {
        mouseFollower.Toggle(true);
        mouseFollower.SetData(sprite, quantity);
    }

    private void HandleItemSelection(Inventory_Item item)
    {
        int index = items.IndexOf(item);
        if (index == -1)
        {
            return;
        }

        OnDescriptionRequested?.Invoke(index);

    }

    private void ResetDraggedItem()
    {
        mouseFollower.Toggle(false);
        currentlyDraggedItemIndex = -1;
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

        ResetSelection();

    }

    public void ResetSelection()
    {
        itemDescription.ResetDescription();
        DeselectAllItems();

    }

    private void DeselectAllItems()
    {
        foreach (Inventory_Item item in items)
        {
            item.Deselect();
        }

        actionPanel.Toggle(false);

    }

    public void ShowItemAction(int itemIndex)
    {
        actionPanel.Toggle(true);
        actionPanel.transform.position = items[itemIndex].transform.position;

    }

    public void AddAction(string actionname, Action performAction)
    {
        actionPanel.AddButton(actionname, performAction);

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

        ResetDraggedItem();
        actionPanel.Toggle(false);
    }

    internal void UpdateDescription(int itemIndex, Sprite itemImage, string name, string description)
    {
        itemDescription.SetDescription(itemImage, name, description);
        DeselectAllItems();
        items[itemIndex].Select();
    }

    internal void ResetAllItems()
    {
        foreach (var item in items)
        {
            item.ResetData();
            item.Deselect();
        }
    }
}
