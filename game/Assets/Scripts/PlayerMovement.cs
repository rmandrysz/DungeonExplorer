using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Space]
    [Header("Values:")]
    public float moveSpeed = 4f;
    
    private Vector2 movementDirection;

    [Space]
    [Header("References:")]
    public Rigidbody2D rb;
    public Animator animator;

    // Update is called once per frame
    void Update()
    {
        //Input
        GetInput();
    }

    private void FixedUpdate()
    {
        //Movement
        rb.MovePosition(rb.position + movementDirection * moveSpeed * Time.fixedDeltaTime);

        //Animation
        animator.SetFloat("Horizontal", movementDirection.x);
        animator.SetFloat("Vertical", movementDirection.y);
        animator.SetFloat("Speed", movementDirection.sqrMagnitude);

    }

    private void GetInput()
    {
        movementDirection.x = Input.GetAxisRaw("Horizontal");
        movementDirection.y = Input.GetAxisRaw("Vertical");
        movementDirection.Normalize();
    }

}
