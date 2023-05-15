using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class StatBar : MonoBehaviour
{
    //public variables
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateBar(int current, int max)
    {
        //get bar fill ratio
        float percentage = (float)current / (float)max;

        //scale health bar fill
        slider.value = percentage;
    }
}
