﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Space]
    [Header("Values:")]
    public float moveSpeed = 4f;
    private float attackRate = 2f;
    float timeSinceLastAttack = 0f;

    public Vector2 movementDirection;

    [Space]
    [Header("References:")]
    public Rigidbody2D rb;
    public Animator animator;

    private Dictionary<string, Vector2> doorMovementDirections = new Dictionary<string, Vector2>();

    // Update is called once per frame
    void Update()
    {
        //Input
        GetInput();
    }

    private void Start()
    {
        InitDirections();

        timeSinceLastAttack = 1 / attackRate;
    }

    private void FixedUpdate()
    {
        //Movement
        rb.MovePosition(rb.position + movementDirection * moveSpeed * Time.fixedDeltaTime);

        //Animation
        animator.SetFloat("Horizontal", movementDirection.x);
        animator.SetFloat("Vertical", movementDirection.y);
        animator.SetFloat("Speed", movementDirection.sqrMagnitude);

        timeSinceLastAttack += Time.deltaTime;

    }

    private void GetInput()
    {
        movementDirection.x = Input.GetAxisRaw("Horizontal");
        movementDirection.y = Input.GetAxisRaw("Vertical");
        movementDirection.Normalize();
    }

    private void InitDirections()
    {
        doorMovementDirections.Add("L", new Vector2(-7f, 0f));
        doorMovementDirections.Add("R", new Vector2(7f, 0f));
        doorMovementDirections.Add("T", new Vector2(0f, 6.2f));
        doorMovementDirections.Add("B", new Vector2(0f, -6.2f));
    }

    public void MoveThroughDoor(string direction)
    {
        this.gameObject.transform.Translate(doorMovementDirections[direction]);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //Debug.Log (timeSinceLastAttack >= 1 / attackRate);

        if (collision.gameObject.CompareTag("Enemy") && timeSinceLastAttack >= (1 / attackRate))
        {
            collision.gameObject.GetComponent<Enemy>().GetDamage(5f);
            timeSinceLastAttack = 0f;
            //Debug.Log("Trigger Entered");
        }
        
    }

}
