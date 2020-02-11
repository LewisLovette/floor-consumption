using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController characterController;

    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 0.0f;

    private float disableTime = 5;
    private float collisionDisableTimer = 0;
    private Vector3 moveDirection = Vector3.zero;

    private int layerIgnorTo = 9;
    private int layerIgnorFrom = 10;

    private GameObject ghost;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        //so doesn't move when colliding. Box collider acts as trigger
        characterController.detectCollisions = false;

        ghost = GameObject.FindGameObjectWithTag("Ghost");

    }

    void Update()
    {
        //ghost player to follow player
        ghost.transform.position = this.transform.position;

        if (characterController.isGrounded)
        {
            // We are grounded, so recalculate
            // move direction directly from axes

            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            moveDirection *= speed;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);


        //Acts as timer to check collision so it turns it on every x seconds to check for a new collision
        collisionDisableTimer -= 1f * Time.deltaTime;
        print(collisionDisableTimer);

        //Check collision once timer has run out
        if(collisionDisableTimer <= 0)
        {  
            Physics.IgnoreLayerCollision(layerIgnorTo, layerIgnorFrom, false);
            print("Are collisions between " + layerIgnorTo + " and " + layerIgnorFrom + " being ignored?   " + Physics.GetIgnoreLayerCollision(layerIgnorTo, layerIgnorFrom));

        }
    }

    //Detect collisions between the GameObjects with Colliders attached
    void OnControllerColliderHit(ControllerColliderHit collision)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collision.gameObject.layer == 9)
        {
            //Turn off collisions between object and player layer.
            Physics.IgnoreLayerCollision(layerIgnorTo, layerIgnorFrom, true);
            print("Are collisions between " + layerIgnorTo + " and " + layerIgnorFrom + " being ignored?   " + Physics.GetIgnoreLayerCollision(layerIgnorTo, layerIgnorFrom));
            

            collisionDisableTimer = disableTime;
        }
    }
}
