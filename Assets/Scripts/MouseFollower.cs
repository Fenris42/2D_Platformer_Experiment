using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollower : MonoBehaviour
{
    //public variables
    public Canvas canvas;
    public Inventory_Item item;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)canvas.transform, Input.mousePosition, canvas.worldCamera, out position);
        transform.position = canvas.transform.TransformPoint(position);
    }

    public void SetData(Sprite sprite, int quantity)
    {
        item.SetData(sprite, quantity);

    }

    public void Toggle(bool val)
    {
        gameObject.SetActive(val);

    }
}
