using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{

    public float speed;

    private Transform target;
    private Vector2 targetPosition;
    private float healthPoints = 30f;
    private bool stopMoving = false;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    // Update is called once per frame
    private void Update()
    {
        if (!stopMoving)
        {
            targetPosition = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            this.gameObject.GetComponent<Rigidbody2D>().MovePosition(targetPosition);
        }

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
        healthPoints -= damage;

        if(healthPoints <= 0)
        {
            Die();
        }

        Debug.Log("Hit for" + damage);
    }

    public void Die()
    {
        stopMoving = true;
        this.gameObject.GetComponent<Animator>().SetBool("IsDead", true);
        GetComponent<Collider2D>().enabled = false;
    }

    private void DeleteGameObject()
    {
        Destroy(this.gameObject);
        Debug.Log("ObjectDestroyed");
    }
}
