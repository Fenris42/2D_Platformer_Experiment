using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    //public variables
    public SpriteRenderer sprite;

    //private variables
    private GameObject player;
    private float direction;
    private int speed;
    private int damage;

    // Start is called before the first frame update
    void Start()
    {
        //get player object
        player = GameObject.Find("Player");

        //get firing direction
        direction = player.transform.localScale.x;

        //get fireball speed
        speed = player.GetComponent<Player>().fireballSpeed;

        //get fireball damage
        damage = player.GetComponent<Player>().fireballDamage;
    }

    // Update is called once per frame
    void Update()
    {
        //fireball travels
        Move();

    }

    //fireball movement
    private void Move()
    {
        //flip sprit to correct direction
        transform.localScale = new Vector2(direction, transform.localScale.y);

        //move left
        if (direction < 0)
        {
            transform.position = transform.position + ((Vector3.left * speed) * Time.deltaTime);
        }
        //move right
        if (direction > 0)
        {
            transform.position = transform.position + ((Vector3.right * speed) * Time.deltaTime);
        }
    }

    //collision detection
    private void OnTriggerExit2D(Collider2D collision)
    {
        //fireball left the scene
        if (collision.gameObject.tag == "Scene Edge")
        {
            Destroy(gameObject);
        }
        //fireball collided with mob
        if (collision.gameObject.tag == "Mob")
        {
            //apply damage to mob(s) - TO DO make dynamic
            collision.gameObject.GetComponent<Mob_Slime>().TakeDamage(damage);
        }
    }

}

