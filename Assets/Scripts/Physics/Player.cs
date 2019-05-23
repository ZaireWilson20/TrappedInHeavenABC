using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Prime31.CharacterController2D))]
public class Player : MonoBehaviour
{
    public float jumpHeight = 4;
    public float timeToJumpApex = .4f;
    float accelerationTimeAirborne = .2f;
    float accelerationTimeGrounded = .05f;
    public float moveSpeed = 6;
    public float baseMoveSpeed; 

    float gravity;
    [HideInInspector]
    public float jumpVelocity;
    public Vector3 velocity;
    float velocityXSmoothing;

    public Prime31.CharacterController2D controller;
    public bool proj;

    private bool hurtTimeStarted = false;

    public InputInfo inputInfo; 
    public struct InputInfo
    {
        public bool jump, left, right, stationary;
        public bool SlidingDownSlope;
    }

    void Start()
    {
        controller = GetComponent<Prime31.CharacterController2D>();
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



        if (controller.charState == Prime31.CharacterController2D.CharacterState.HURT && !hurtTimeStarted)
        {
            StartCoroutine(HurtTimer());
            hurtTimeStarted = true;
        }
        else if (controller.charState == Prime31.CharacterController2D.CharacterState.SAFE)
        {

            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
            if (input.x != 0)
            {
                inputInfo.left = input.x < 0;
                inputInfo.right = input.x > 0;
                inputInfo.stationary = false; 
            }
            else if (inputInfo.jump == false)
            {
                inputInfo.stationary = true; 
            }
            if (controller.collisionState.movingDownSlope && Input.GetKey(KeyCode.W))
            {
                moveSpeed *= 2;
                input.x = 1;
                // If player holds down, call slide down slope method from controller
            }
            if (!inputInfo.SlidingDownSlope)
            {
                float targetVelocityX = input.x * moveSpeed;
                velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, accelerationTimeGrounded);
            }
            velocity.y += gravity * Time.deltaTime;
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
}

