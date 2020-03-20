using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 4f;
    public Rigidbody2D rigidbody;
    private Vector2 movementDirection;

    // Update is called once per frame
    void Update()
    {
        //Input
        movementDirection.x = Input.GetAxisRaw("Horizontal");
        movementDirection.y = Input.GetAxisRaw("Vertical");
        movementDirection.Normalize();
    }

    private void FixedUpdate()
    {
        //Movement
        rigidbody.MovePosition(rigidbody.position + movementDirection * moveSpeed * Time.fixedDeltaTime);
    }
}
