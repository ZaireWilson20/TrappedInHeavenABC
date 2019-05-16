using UnityEngine;
using System.Collections;

namespace Prime31
{
    [RequireComponent(typeof(CharacterController2D))]
    public class Player : MonoBehaviour
    {

        public float jumpHeight = 4;
        public float timeToJumpApex = .4f;
        float accelerationTimeAirborne = .2f;
        float accelerationTimeGrounded = .1f;
        public float moveSpeed = 6;

        float gravity;
        [HideInInspector]
        public float jumpVelocity;
        public Vector3 velocity;
        float velocityXSmoothing;

        public CharacterController2D controller;
        public bool proj; 
        void Start()
        {
            controller = GetComponent<CharacterController2D>();

            gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
            jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
            print("Gravity: " + gravity + "  Jump Velocity: " + jumpVelocity);
        }

        void Update()
        {
            if (controller.isGrounded)
            {
                velocity.y = 0;
            }


            
            if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
            {
                velocity.y = jumpVelocity;
            }

            if (!proj)
            {
                Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
                float targetVelocityX = input.x * moveSpeed;
                velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, accelerationTimeGrounded);
            }
            velocity.y += gravity * Time.deltaTime;
            controller.move(velocity * Time.deltaTime);
        }

    }
}