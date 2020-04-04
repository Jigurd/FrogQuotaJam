using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Movement movement;
    private SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<Movement>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }


    void HandleMovement()
    {
        //reset horizontal speed
        movement.velocity.x = 0;


        //if the player is jumping, add jump velocity to jump
        if (Input.GetButton("Jump"))
        {
            movement.velocity.y += movement.jumpVelocity;
        }
        //move left
        if (Input.GetButton("Left"))
        {
            movement.velocity.x -= movement.moveSpeed;
            sprite.flipX = false;
        }
        //move right
        if (Input.GetButton("Right"))
        {
            movement.velocity.x += movement.moveSpeed;
            sprite.flipX = true;
        }


    }
}
