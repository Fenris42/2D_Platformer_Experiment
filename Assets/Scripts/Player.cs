using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    //public variables
    public Rigidbody2D player;
    public Animator animator;
    public GameObject healthBarObject; //import child game object health bar
    
    //Player Stats
    public float moveSpeed;
    public float jumpSpeed;
    public int maxHealth;
    public int currentHealth;

    //private variables
    private bool isJumping;
    private StatBar healthBar;//import script "StatBar"



    // Start is called before the first frame update
    void Start()
    {
        //get instance of players health bar
        healthBar = healthBarObject.GetComponent<StatBar>();
    }

    // Update is called once per frame
    void Update()
    {
        //Update players movement and inputs
        PlayerInput();

        //Update players health
        PlayerHealth();
          
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

            //prevent healthbar from flipping with player sprite
            healthBarObject.transform.localScale = new Vector3(-1, 1, 1);
            
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
            //change animation to sword attack
            animator.SetBool("Sword_Attack", true);

        }
        else
        {
            animator.SetBool("Sword_Attack", false);   
        }
    }

    //Player Health -----------------------------------------------------------------------------------------------------------------
    private void PlayerHealth()
    {
        //check to make sure players health stays within range
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        //Update health bar
        healthBar.UpdateBar(currentHealth, maxHealth);
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
}