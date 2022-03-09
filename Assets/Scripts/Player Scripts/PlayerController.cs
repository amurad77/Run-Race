using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 move;
    public float speed, jumForce, gravity, verticalVelocity;

    private bool wallSlide, turn;

    private bool doubleJump;
    private CharacterController charController;

    private Animator anim;

    void Awake()
    {
        charController = GetComponent<CharacterController>();
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    void Update()
    {
        move = Vector3.zero;
        move = transform.forward;

        if (charController.isGrounded)
        {
            wallSlide = false;
            verticalVelocity = 0;
            if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                Jump();
                doubleJump = true;
            }

            if (turn)
            {
                turn = false;
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 180,
                    transform.eulerAngles.z);
            }
        }

        if (!wallSlide)
        {
            gravity = 30;
            verticalVelocity -= gravity * Time.deltaTime;

            anim.SetBool("WallSlide", true);
        }
        else
        {
            gravity = 15;
            verticalVelocity -= gravity * Time.deltaTime;

            anim.SetBool("WallSlide", false);
        }

        anim.SetBool("WallSlide", wallSlide);
        anim.SetBool("Grounded", charController.isGrounded);

        // else
        // {
        //     gravity = 30;
        //     verticalVelocity -= gravity * Time.deltaTime;
        //     if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) && doubleJump)
        //     {
        //         verticalVelocity += jumForce * .5f;
        //         doubleJump = false;
        //     }
        // }

        move.Normalize();
        move *= speed;
        move.y = verticalVelocity;
        charController.Move(move * Time.deltaTime);
     }

    void Jump()
    {
        verticalVelocity = jumForce;
        anim.SetTrigger("Jump");
    }

    
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(!charController.isGrounded)
        {
            if(hit.collider.tag == "Wall")
            {
                if (verticalVelocity < -.6f)
                    wallSlide = true;
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    Jump();
                    doubleJump = false;

                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 180,
                        transform.eulerAngles.z);

                    wallSlide = false;
                }
            }
        }
        else
        {
            if (transform.forward != hit.collider.transform.up && hit.collider.tag == "Ground" && !turn)
                turn = true;
            
        }
    }
}
