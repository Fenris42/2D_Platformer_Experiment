using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_Description : MonoBehaviour
{
    //public variables
    public Image itemImage;
    public Text title;
    public Text description;

    // Start is called before the first frame update
    void Start()
    {
        ResetDescription();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetDescription()
    {
        itemImage.gameObject.SetActive(false);
        title.text = "";
        description.text = "";

    }

    public void SetDescription(Sprite sprite, string itemName, string itemDescription)
    {
        itemImage.gameObject.SetActive(true);
        itemImage.sprite = sprite;
        title.text = itemName;
        description.text = itemDescription;
    }
}
