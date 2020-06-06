using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyFollow : Enemy
{

    public new float speed;

    private Transform target;
    private Vector2 targetPosition;
    private float maxHealth = 30f;
    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;

        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    // Update is called once per frame
    private void Update()
    {
        targetPosition = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        this.gameObject.GetComponent<Rigidbody2D>().MovePosition(targetPosition);

        if(target.position.x > this.gameObject.transform.position.x)
        {
            this.gameObject.GetComponent<Animator>().SetFloat("Horizontal", 1);
        }
        else
        {
            this.gameObject.GetComponent<Animator>().SetFloat("Horizontal", -1);
        }

        this.gameObject.GetComponent<Animator>().SetFloat("Speed", 1);
    }

    public void GetHit(float damage)
    {
        this.gameObject.GetComponent<Animator>().SetTrigger("Hit");
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            Die();
        }

        Debug.Log("Hit for" + damage);
    }

    public void Die()
    {
        speed = 0f;
        this.gameObject.GetComponent<Animator>().SetBool("IsDead", true);
        GetComponent<Collider2D>().enabled = false;
    }

    private void DeleteGameObject()
    {
        Destroy(this.gameObject);
    }
}
