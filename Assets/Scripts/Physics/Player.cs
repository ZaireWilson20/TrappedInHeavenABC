using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Prime31.CharacterController2D))]
public class Player : MonoBehaviour
{
    public float jumpHeight = 4;
    public float timeToJumpApex = .4f;
    float accelerationTimeAirborne = .2f;
    float accelerationTimeGrounded = .2f;
    public float friction = 2f; 
    public float maxVelocX = 6; 
    public float moveSpeed = 2;
    public float slideSlopeMaxSpeed = 100;
    public float slideSlopeAccel = 50; 
    public float baseMoveSpeed; 

    float gravity;
    [HideInInspector]
    public float jumpVelocity;
    public Vector3 velocity;
    float velocityXSmoothing;


    public Prime31.CharacterController2D controller;
    public bool proj;
    Animator anim;
    SpriteRenderer sprite; 
    private bool hurtTimeStarted = false;

    public InputInfo inputInfo; 
    public struct InputInfo
    {
        public bool jump, left, right, stationary;
        public bool SlidingDownSlope;

        public void resetRL()
        {
            right = false;  
            left = false; 
        }
    }

    void Start()
    {
        controller = GetComponent<Prime31.CharacterController2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        baseMoveSpeed = moveSpeed; 
        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
    }



    void Update()
    {
        if (controller.isGrounded){
            velocity.y = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded){
            velocity.y = jumpVelocity;
            inputInfo.jump = true; 
        }
        else
        {
            inputInfo.jump = false;
        }

        // TEMP ANIM STUFF
        if (inputInfo.left || inputInfo.right)
        {
            anim.SetBool("Running", true);
        }
        else
        {
            anim.SetBool("Running", false);
        }
        // -------------------------------

        //  HURT INFO
        if (controller.charState == Prime31.CharacterController2D.CharacterState.HURT && !hurtTimeStarted)
        {
            StartCoroutine(HurtTimer());
            hurtTimeStarted = true;
        }
        // ------------------------------

        else if (controller.charState == Prime31.CharacterController2D.CharacterState.SAFE)
        {

            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
            if (input.x != 0)
            {
                inputInfo.left = input.x < 0;
                inputInfo.right = input.x > 0;
                inputInfo.stationary = false;
                if (input.x < 0)
                {
                    sprite.flipX = true;
                }
                else
                {
                    sprite.flipX = false;
                }

                float targetVelocityX = input.x * moveSpeed;
                velocity.x += targetVelocityX;
                velocity.x = Mathf.Min(Mathf.Abs(velocity.x), maxVelocX) * Mathf.Sin(input.x);
                //velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, accelerationTimeGrounded);
            }
            else if (controller.isGrounded)
            {
                //  Apply Friciton if no horizontal input
                velocity.x -= Mathf.Min(Mathf.Abs(velocity.x), friction) * Mathf.Sign(velocity.x);
                
                inputInfo.stationary = true;
                inputInfo.resetRL();
            }
            velocity.y += gravity * Time.deltaTime;
        }



        if (controller.collisionState.movingDownSlope && controller.collisionState.slopeAngle > 20f && inputInfo.stationary)
        {
            //slideDownSlope(controller.collisionState.slopeDirection, controller.collisionState.slopeAngle);
        }
           controller.move(velocity * Time.deltaTime);
    }

    IEnumerator HurtTimer()
    {
        yield return new WaitForSeconds(.5f);
        Debug.Log("hurting");
        controller.charState = Prime31.CharacterController2D.CharacterState.SAFE;
        hurtTimeStarted = false;

        //DO SOMETHING


    }

    public void slideDownSlope(float slideDirection, float angle, ref Vector3 deltaMovement)
    {
        deltaMovement.x += slideSlopeAccel * slideDirection;
        Debug.Log(deltaMovement.x);

        if (deltaMovement.x > slideSlopeMaxSpeed)
        {
           velocity.x = slideSlopeMaxSpeed * slideDirection;
        }
        
    }

    
}

