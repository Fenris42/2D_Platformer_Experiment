using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealZone : MonoBehaviour
{
    //public variables
    public int healPerSec;
    
    //private variables
    private Player player; //import player script
    private float timer;
    private bool playerCollision = false;

    // Start is called before the first frame update
    void Start()
    {
        //import player script
        player = GameObject.Find("Player").GetComponent<Player>();

    }

    // Update is called once per frame
    void Update()
    {
        //increment timer
        timer = timer + Time.deltaTime;

        //only tick once per second
        if (timer >= 1 && playerCollision == true)
        {
            //heal player heal per sec
            player.currentHealth = player.currentHealth + healPerSec;

            //reset timer
            timer = 0;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //player is inside heal zone
        if (collision.gameObject.tag == "Player")
        {
            playerCollision = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //player left the heal zone
        if (collision.gameObject.tag == "Player")
        {
            playerCollision = false;
        }
    }
}

