using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyFollow : Enemy
{

    public float speed;

    private Transform target;
    private Vector3 direction;
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
        direction = target.position - transform.position;
        direction.Normalize();
        MovePosition(direction);

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

    public override void GetDamage(float damage)
    {
        this.gameObject.GetComponent<Animator>().SetTrigger("Hit");
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            Die();
        }

        Debug.Log("Hit for" + damage);
    }

    public override void Die()
    {
        speed = 0f;
        this.gameObject.GetComponent<Animator>().SetBool("IsDead", true);
        GetComponent<Collider2D>().enabled = false;
    }

    private void DeleteGameObject()
    {
        Destroy(this.gameObject);
    }

    private void MovePosition(Vector3 direction)
    {
        gameObject.GetComponent<Rigidbody2D>().MovePosition(transform.position + (direction * speed * Time.deltaTime));
    }
}
