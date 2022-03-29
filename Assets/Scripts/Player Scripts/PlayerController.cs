using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 move;
    public float speed, jumForce, gravity, verticalVelocity;

    private bool wallSlide, turn, superJump;

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


    if (GameManager.instance.finish)
    {
        move = Vector3.zero;
        if (!charController.isGrounded)
            verticalVelocity -= gravity * Time.deltaTime;
        else
            verticalVelocity = 0;
        
        move.y = verticalVelocity;

        charController.Move(new Vector3(0, move.y * Time.deltaTime, 0));
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Dance"))
        {
            anim.SetTrigger("Dance");
            transform.eulerAngles = Vector3.up * 180;
        }
        return;
    }

        if (!GameManager.instance.start)
            return;


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

        if(superJump)
        {
            superJump = false;
            verticalVelocity = jumForce * 1.75f;

            if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
                anim.SetTrigger("Jump");
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
            if(hit.collider.tag == "Wall" || hit.collider.tag == "Slide")
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

            if(hit.collider.tag == "Trampoline")
                superJump = true;


            if (transform.forward != hit.collider.transform.up && hit.collider.tag == "Ground" && !turn)
                turn = true;
        }
    }
}
