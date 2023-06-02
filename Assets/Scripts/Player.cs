using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    //public variables
    public Rigidbody2D player;
    public Animator animator;
    public GameObject healthBarObject; //import child game object health bar
    public SpriteRenderer playerSprite;
    public GameObject manaBarObject;
    public LayerMask mobLayer;
    public GameObject fireballPrefab;

    //Player Stats
    public float moveSpeed;
    public float jumpSpeed;
    public int maxHealth;
    public int currentHealth;
    public float swordRange;
    public Transform swordAttackRange;
    public int swordAttackPower;
    public int fireballSpeed;
    public int fireballCost;
    public int fireballDamage;
    public int currentMana;
    public int maxMana;
    public int manaRegenRate;
    


    //private variables
    private bool isJumping;
    private StatBar healthBar;//import script "StatBar"
    private StatBar manaBar; //import script "StatBar"
    private GameLogic GameLogic; //import script "GameOver"
    private bool GameOver = false;
    private float timer;



    // Start is called before the first frame update
    void Start()
    {
        //get instance of players health bar
        healthBar = healthBarObject.GetComponent<StatBar>();

        //get instance of players mana bar
        manaBar = manaBarObject.GetComponent<StatBar>();

        //get instance of game logic script
        GameLogic = GameObject.Find("Game Logic").GetComponent<GameLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        //Update players movement and inputs
        if (GameOver == false)
        {
            //check for and apply players keyboard/mouse inputs
            PlayerInput();

            //increment timer
            timer = timer + Time.deltaTime;

            //convert frames to seconds
            if (timer >= 1)
            {
                RegenMana();

                //reset timer
                timer = 0;
            }
        }
    }

    //Player Input -----------------------------------------------------------------------------------------------------------------
    private void PlayerInput()
    {
        //force character to return to idle if both left and right activated
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            animator.SetBool("Running", false);
        }
        //Move left
        else if (Input.GetKey(KeyCode.A))
        {
            //move player
            transform.position = transform.position + ((Vector3.left * moveSpeed) * Time.deltaTime);

            //flip player sprite left
            transform.localScale = new Vector3(-1, 1, 1);

            //prevent health bar from flipping with player sprite
            healthBarObject.transform.localScale = new Vector3(-1, 1, 1);

            //prevent mana bar from flipping with player sprite
            manaBarObject.transform.localScale = new Vector3(-1, 1, 1);

            //change animation to running
            animator.SetBool("Running", true);
        }
        //Move right
        else if (Input.GetKey(KeyCode.D))
        {
            //move player
            transform.position = transform.position + ((Vector3.right * moveSpeed) * Time.deltaTime);

            //flip player sprite right
            transform.localScale = new Vector3(1, 1, 1);

            //prevent healthbar from flipping with player sprite
            healthBarObject.transform.localScale = new Vector3(1, 1, 1);

            //prevent mana bar from flipping with player sprite
            manaBarObject.transform.localScale = new Vector3(1, 1, 1);

            //change animation to running
            animator.SetBool("Running", true);
        }
        //return sprite to idle animation
        else
        {
            animator.SetBool("Running", false);
        }

        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && isJumping == false)
        {
            //lock jump until player is on the ground
            isJumping = true;

            //jump
            player.velocity = (Vector2.up * jumpSpeed);

            //change animation to jumping
            animator.SetBool("Jumping", true);
        }

        //Sword attack (left mouse button)
        if (Input.GetMouseButtonDown(0))
        {
            PlayerSwordAttack();
        }

        //fireball attack (right mouse button)
        if (Input.GetMouseButtonDown(1))
        {
            PlayerFireballAttack();
        }
    }

    //heal player
    public void HealPlayer(int Health)
    {
        //apply healing to player
        currentHealth = currentHealth + Health;

        //play healing animation
        animator.SetTrigger("Heal");

        //update healthbar
        PlayerHealth();

    }

    //damage palyer
    public void DamagePlayer(int Damage)
    {
        //apply damage to player
        currentHealth = currentHealth - Damage;

        //play player damaged animation
        animator.SetTrigger("Damaged");

        //update healthbar
        PlayerHealth();

    }

    //Update Player Health -----------------------------------------------------------------------------------------------------------------
    private void PlayerHealth()
    {
        //check to make sure players health stays within range
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;

        }
        else if (currentHealth <= 0)
        {
            currentHealth = 0;

            //play die animation
            animator.SetBool("Die", true);

            //disable health bar
            healthBarObject.SetActive(false);

            //disable mana bar
            manaBarObject.SetActive(false);

            //set game over
            GameLogic.GameOver = true;

            this.enabled = false;

        }

        //Update health bar
        healthBar.UpdateBar(currentHealth, maxHealth);
    }

    //gain mana
    private void GainMana(int Mana)
    {
        //keep mana in range
        if (currentMana > maxMana)
        {
            currentMana = maxMana;
        }
        else if ((currentMana + Mana) > maxMana)
        {
            currentMana = maxMana;
        }
        //apply mana
        else
        {
            currentMana = currentMana + Mana;
        }

        //Update mana bar
        manaBar.UpdateBar(currentMana, maxMana);
    }

    //use mana
    private void UseMana(int Mana)
    {
        //keep mana in range
        if (currentMana < 0)
        {
            currentMana = 0;
        }
        else if((currentMana - Mana) < 0)
        {
            currentMana = 0;
        }
        //consume mana
        else
        {
            currentMana = currentMana - Mana;
        }

        //Update mana bar
        manaBar.UpdateBar(currentMana, maxMana);
    }

    //regen mana
    private void RegenMana()
    {
        if (currentMana < maxMana)
        {
            GainMana(manaRegenRate);
        }
        
    }
    
    //Player sword Attack -----------------------------------------------------------------------------------------------------------------
    private void PlayerSwordAttack()
    {
        //change animation to sword attack
        animator.SetTrigger("Sword_Attack");

        //detect enemies in sword range
        Collider2D[] enemies = Physics2D.OverlapCircleAll(swordAttackRange.position, swordRange, mobLayer);

        //loop through each enemy in range
        foreach (Collider2D enemy in enemies)
        {
            //damage enemy
            //TO DO - Make dynamic and not just to slimes
            enemy.GetComponent<Mob_Slime>().TakeDamage(swordAttackPower);
        }

    }

    //player fireball attack -----------------------------------------------------------------------------------------------------------------
    private void PlayerFireballAttack()
    {
        //check if enough mana
        if (currentMana >= fireballCost)
        {
            //change animation to casting
            animator.SetTrigger("Cast");

            //use mana
            UseMana(fireballCost);

            //spawn position
            float x = (float)(transform.position.x + 0.5); //offset fireball to be not centered on player
            float y = (float)(transform.position.y - 0.7); //offset fireball to be towards hands

            //spawn fireball
            Instantiate(fireballPrefab, new Vector3(x, y), transform.rotation);



        }
    }

    //Collision Detection -----------------------------------------------------------------------------------------------------------------
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //check if player is grounded
        if (collision.gameObject.tag == "Ground")
        {
            //Reset jump
            animator.SetBool("Jumping", false);
            isJumping = false;
        }
    }

    //For Debug - Draw sword attack range in editor
    private void OnDrawGizmosSelected()
    {
        //check for null value
        if (swordAttackRange == null)
        {
            return;
        }

        //draw circle in editor
        Gizmos.DrawWireSphere(swordAttackRange.position, swordRange);
    }
}