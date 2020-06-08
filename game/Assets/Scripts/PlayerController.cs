using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Space]
    [Header("Values:")]
    private float baseMoveSpeed;
    public float moveSpeed = 4f;

    public float attackRange;
    public float attackDamage = 5f;
    float timeSinceLastAttack = 0f;
    private float attackRate = 1.5f;

    public float invincibilityDuration = 2.5f;
    private float invincibilityCountdown = 0f;
    private bool isInvincible = false;

    //public t

    private Vector2 movementDirection;
    private Vector2 attackAxis;
    private float attack;

    [Space]
    [Header("References:")]
    public Rigidbody2D rb;
    public Animator animator;
    public LayerMask enemyLayers;

    [Header("Attack Points:")]
    public List<Transform> attackPoints;

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
        baseMoveSpeed = moveSpeed;
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

        if (attack != 0 && timeSinceLastAttack >= 1 / attackRate)
            Attack(attack);

        if (attackAxis != Vector2.zero && timeSinceLastAttack >= 1 / attackRate)
        {
            Attack(attackAxis);
        }

    }

    private void GetInput()
    {
        movementDirection.x = Input.GetAxisRaw("Horizontal");
        movementDirection.y = Input.GetAxisRaw("Vertical");
        movementDirection.Normalize();

        attackAxis.x = Input.GetAxisRaw("AttackHorizontal");
        attackAxis.y = Input.GetAxisRaw("AttackVertical");
        attack = Input.GetAxis("Attack");
    } //Check for input;

    private void InitDirections()
    {
        doorMovementDirections.Add("L", new Vector2(-7f, 0f));
        doorMovementDirections.Add("R", new Vector2(7f, 0f));
        doorMovementDirections.Add("T", new Vector2(0f, 6.2f));
        doorMovementDirections.Add("B", new Vector2(0f, -6.2f));
    } //Initialize how much to transport the player while moving through door;

    public void MoveThroughDoor(string direction)
    {
        this.gameObject.transform.Translate(doorMovementDirections[direction]);
    } //Get to the other side of the door;

    private void Attack(Vector2 attackInput)
    {
        moveSpeed = 0f;

        timeSinceLastAttack = 0f;

        animator.SetFloat("AttackHorizontal", attackInput.x);
        animator.SetFloat("AttackVertical", attackInput.y);
        animator.SetTrigger("Attack");
    } //Start appropriate attack animation if activated by the arrow keys;

    private void Attack(float attackInput)
    {
        moveSpeed = 0f;

        timeSinceLastAttack = 0f;

        animator.SetFloat("AttackHorizontal", movementDirection.x);
        animator.SetFloat("AttackVertical", movementDirection.y);
        animator.SetTrigger("Attack");

    } //Start appropriate attack animation if activated by space bar;

    private void DealDamage(int attackPointIndex)
    {

        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(attackPoints[attackPointIndex].position, attackRange, enemyLayers);

        foreach (Collider2D enemy in enemiesInRange)
        {
            enemy.gameObject.GetComponent<Enemy>().GetDamage(attackDamage);
            Debug.Log("Hit " + enemy.name + " for " + attackDamage);
        }
    } //Check enemies in range and deal damage. Used by animation event;

    private void ResumeMovement()
    {
        moveSpeed = baseMoveSpeed;
    } //Return to movement after attacking. Used by animation event;

    private void OnDrawGizmosSelected()
    {
        foreach (Transform attackPoint in attackPoints)
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    } //Draw attack point spheres to show range;
}
