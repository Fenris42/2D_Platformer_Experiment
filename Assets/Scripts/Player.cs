using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D player;
    public float moveSpeed;
    public float jumpSpeed;
    private bool isJumping = false;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    { 
        //Move left
        if (Input.GetKey(KeyCode.A))
        {
            //flip player sprite left
            transform.localScale = new Vector3(-1, 1, 1);

            //move player
            transform.position = transform.position + ((Vector3.left * moveSpeed) * Time.deltaTime);
        }

        //Move right
        if (Input.GetKey(KeyCode.D))
        {
            //flip player sprite right
            transform.localScale = new Vector3(1, 1, 1);

            //move player
            transform.position = transform.position + ((Vector3.right * moveSpeed) * Time.deltaTime);
        }

        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && isJumping == false)
        {
            //jump
            player.velocity = Vector2.up * jumpSpeed;

            //lock jump until player is on the ground
            isJumping = true;
        }
        //Jump reset
        if (player.velocity.y == 0)
        {
            isJumping = false;
        }
        
    }
}
