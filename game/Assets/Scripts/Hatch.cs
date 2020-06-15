using UnityEngine;

public class Hatch : Door
{
    private Animator animator;
    private Collider2D trigger;
    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        trigger = gameObject.GetComponent<BoxCollider2D>();
    }

    public override void Open()
    {
        animator.SetTrigger("Open");
        trigger.enabled = true;
    }

    public override void Close()
    {
        animator.SetTrigger("Close");
        trigger.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            NextFloor();
        }
    }

    private void NextFloor()
    {
        GameManager gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        animator.SetTrigger("Close");
        Destroy(player);

        gameManager.NextFloor();
    }
}
