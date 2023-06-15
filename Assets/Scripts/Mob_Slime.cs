using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

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
    public int aggroRange;
    public int attackRange;
    public Item lootPrefab;
    public ItemSO loot;

    //private variables
    private StatBar healthBar; //import script "StatBar"
    private bool playerCollision;
    private Player player; //import script "Player"
    private float timer;
    private float distanceToPlayer;
    private GameLogic GameLogic;

    // Start is called before the first frame update
    void Start()
    {
        //get instance of mobs health bar
        healthBar = healthBarObject.GetComponent<StatBar>();

        //access players script
        player = GameObject.Find("Player").GetComponent<Player>();

        //get instance of game logic script
        GameLogic = GameObject.Find("Game Logic").GetComponent<GameLogic>();

    }

    // Update is called once per frame
    void Update()
    {
        if (GameLogic.GameOver == false)
        {
            //incriment timer
            timer = timer + Time.deltaTime;

            //check distance to player
            distanceToPlayer = Vector2.Distance(player.transform.position, transform.position);

            //Trigger mob AI
            MobMovement();

            //Update mobs health
            MobHealth();

            //Damage player
            MobAttack();
        }
        

    }

    //Mob Attack ------------------------------------------------------------------------------------
    private void MobAttack()
    {
        //Player is touching mob (only tick once per second)
        if (timer >= 1 && playerCollision == true)
        {
            //damage player damage per second
            //player.currentHealth = player.currentHealth - attack;
            player.DamagePlayer(attack);

            //reset timer
            timer = 0;

        }
        
        //mob attack
        if (timer >= 1 && distanceToPlayer <= attackRange)
        {
            //play attack animation
            animator.SetTrigger("Attack");

            //damage player
            //player.currentHealth = player.currentHealth - attack;
            player.DamagePlayer(attack);

            //reset timer
            timer = 0;
        }
    }

    //Mob Movement ------------------------------------------------------------------------------------
    private void MobMovement()
    {
        //check if player is in aggro range but dont overlapt with player
        if (distanceToPlayer < aggroRange && distanceToPlayer >= attackRange)
        {
            //player is to the left of mob
            if (player.transform.position.x < transform.position.x)
            {
                //move left towards player
                transform.position = transform.position + ((Vector3.left * speed) * Time.deltaTime);

                //flip sprite left
                transform.localScale = new Vector3(-1, 1, 1);

                //prevent healthbar from flipping with player sprite
                healthBarObject.transform.localScale = new Vector3(-1, 1, 1);

                //set animation to moving
                animator.SetBool("Run", true);

            }
            //player is to the right of mob
            else
            {
                //move right towards player
                transform.position = transform.position + ((Vector3.right * speed) * Time.deltaTime);

                //flip sprite left
                transform.localScale = new Vector3(1, 1, 1);

                //prevent healthbar from flipping with player sprite
                healthBarObject.transform.localScale = new Vector3(1, 1, 1);

                //set animation to moving
                animator.SetBool("Run", true);
            }
        }
        //reset to idle animation
        else
        {
            animator.SetBool("Run", false);
        }

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

            //drop loot ----------
            //spawn position
            float xCoord = (float)(transform.position.x + 0);
            float yCoord = (float)(transform.position.y - 0);

            //set loot
            lootPrefab.InventoryItem = loot;
            lootPrefab.Quantity = 1;

            //spawn loot
            Instantiate(lootPrefab, new Vector3(xCoord, yCoord), transform.rotation);

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
