using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D player;
    public float moveSpeed;
    public float jumpSpeed;
    private bool isJumping;
    private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        //import components from Player game object
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    { 
        //Move left
        if (Input.GetKey(KeyCode.A))
        {
            //move player
            transform.position = transform.position + ((Vector3.left * moveSpeed) * Time.deltaTime);

            //flip player sprite left
            transform.localScale = new Vector3(-1, 1, 1);

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

            //change animation to running
            animator.SetBool("Running", true);
        }
        else
        {
            animator.SetBool("Running", false);
        }

        //Jump
        if (Input.GetKey(KeyCode.Space) && isJumping == false)
        {
            //jump
            player.velocity = Vector2.up * jumpSpeed;

            //lock jump until player is on the ground
            isJumping = true;

            //change animation to jumping
            animator.SetBool("Jumping", true);
        }  
    }

    //Collision Detection
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //check if player is grounded
        if (collision.gameObject.tag == "Ground")
        {
            //Reset jump
            isJumping = false;
            animator.SetBool("Jumping", false);
        }
    }
}
