using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Mob_Slime : MonoBehaviour
{
    //public variables
    public Rigidbody2D mob;
    public Animator animator;
    public GameObject healthBarObject;
    public Collider2D mobCollider;

    //Mob stats
    public float speed;
    public int maxHealth;
    public int currentHealth;
    public int attack;

    //private variables
    private StatBar healthBar; //import script "StatBar"
    private bool playerCollision;
    private Player player; //import script "Player"
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        //get instance of mobs health bar
        healthBar = healthBarObject.GetComponent<StatBar>();

        //access players script
        player = GameObject.Find("Player").GetComponent<Player>();

    }

    // Update is called once per frame
    void Update()
    {
        //incriment timer
        timer = timer + Time.deltaTime;

        //Trigger mob AI
        MobMovement();

        //Update mobs health
        MobHealth();

        //Damage player
        MobAttack();

    }

    //Mob Attack ------------------------------------------------------------------------------------
    private void MobAttack()
    {
        //Player is touching mob (only tick once per second)
        if (timer >= 1 && playerCollision == true)
        {
            //damage player damage per second
            player.currentHealth = player.currentHealth - attack;

            //reset timer
            timer = 0;

        }
    }

    //Mob Movement ------------------------------------------------------------------------------------
    private void MobMovement()
    {

    }

    //Mob taking damage ------------------------------------------------------------------------------------
    public void TakeDamage(int damage)
    {
        //apply damage to mob
        currentHealth = currentHealth - damage;

        //play hit animation
        animator.SetTrigger("Hit");

    }
    //Mob Health ------------------------------------------------------------------------------------
    private void MobHealth()
    {
        //check to make sure health stays within range
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if (currentHealth <= 0)
        {
            //play death animation
            animator.SetTrigger("Death");

            //disable health bar
            healthBarObject.SetActive(false);

            //disable physics (prevents sprite from falling through floor on death)
            mob.simulated = false;

            //disable hitbox
            mobCollider.enabled = false;

            //disable script
            this.enabled = false;
            
        }

        //Update health bar
        healthBar.UpdateBar(currentHealth, maxHealth);
    }

    //Mob has collided with Player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //player is inside heal zone
        if (collision.gameObject.tag == "Player")
        {
            playerCollision = true;
        }
    }

    //player no longer touching mob
    private void OnCollisionExit2D(Collision2D collision)
    {
        //player left the heal zone
        if (collision.gameObject.tag == "Player")
        {
            playerCollision = false;
        }
    }
}
