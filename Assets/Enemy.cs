using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
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
    
    public Prime31.CharacterController2D controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<Prime31.CharacterController2D>();

        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.isGrounded)
        {
            velocity.y = 0;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.move(velocity * Time.deltaTime);
    }
}
