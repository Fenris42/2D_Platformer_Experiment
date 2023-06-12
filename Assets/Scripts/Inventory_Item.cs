using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventory_Item : MonoBehaviour
{
    //public variables
    public Image itemImage;
    public Text itemQuantity;
    public Image itemSelected;

    //private varialbes
    private bool empty = true;

    //events
    public event Action<Inventory_Item> OnItemClicked, OnItemDroppedOn, OnItemBeginDrag, OnItemEndDrag, OnRightMouseBtnClick;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Awake()
    {
        ResetData();
        Deselect();
    }

    public void ResetData()
    {
        itemImage.gameObject.SetActive(false);
        empty = true;

    }

    public void Deselect()
    {
        itemSelected.enabled = false;

    }

    public void SetData(Sprite sprite, int quantity)
    {
        itemImage.gameObject.SetActive(true);
        itemImage.sprite = sprite;
        itemQuantity.text = quantity.ToString();
        empty = false;

    }

    public void Select()
    {
        itemSelected.enabled = true;

    }

    public void OnBeginDrag()
    {
        if (empty)
        {
            return;
        }

        OnItemBeginDrag?.Invoke(this);

    }

    public void OnDrop()
    {
        OnItemDroppedOn?.Invoke(this);

    }
    
    public void OnEndDrag()
    {
        OnItemEndDrag?.Invoke(this);

    }

    public void OnPointerClick(BaseEventData data)
    {
        if (empty)
        {
            return;
        }

        PointerEventData pointerData = (PointerEventData)data;

        if (pointerData.button == PointerEventData.InputButton.Right)
        {
            OnRightMouseBtnClick?.Invoke(this);
        }
        else
        {
            OnItemClicked?.Invoke(this);
        }

    }
}
