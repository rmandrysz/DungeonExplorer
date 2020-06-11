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

    public float maxHealth = 30f;
    private float currentHealth;

    public float invincibilityDuration = 1f;
    private float invincibilityCounter = 0f;
    private bool isInvincible = false;


    private Vector2 movementDirection;
    private Vector2 attackAxis;
    private float attack;

    private Vector2Int currentRoom;

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

        if (isInvincible)
        {
            invincibilityCounter -= Time.deltaTime;
            
            if (invincibilityCounter <= 0)
            {
                isInvincible = false;
            } 
        }

        GameObject.FindGameObjectWithTag("Dungeon").GetComponent<RoomGeneration>().GetRoom(currentRoom).ManageDoors(); //Close or open the doors;
            
    }

    private void Start()
    {
        currentHealth = maxHealth;
        baseMoveSpeed = moveSpeed;

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
        doorMovementDirections.Add("L", new Vector2(-12f, 0f));
        doorMovementDirections.Add("R", new Vector2(12f, 0f));
        doorMovementDirections.Add("T", new Vector2(0f, 11.2f));
        doorMovementDirections.Add("B", new Vector2(0f, -11.2f));
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
        }
    } //Check enemies in range and deal damage. Used by animation event;

    public void GetDamage(float damage)
    {
        if (!isInvincible)
        {
            animator.SetTrigger("Hit");

            invincibilityCounter = invincibilityDuration;
            isInvincible = true;

            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                Invoke("Die", 0.02f);
            }
        }
    } //Deal damage to the player object;

    private void Die()
    {
        Destroy(this.gameObject);
        FindObjectOfType<GameManager>().EndGame();
    } //Die;

    private void ResumeMovement()
    {
        moveSpeed = baseMoveSpeed;
    } //Return to movement after attacking. Used by animation event;

    private void OnDrawGizmosSelected()
    {
        foreach (Transform attackPoint in attackPoints)
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    } //Draw attack point spheres to show range;

    public Vector2Int GetRoomCoords()
    {
        return currentRoom;
    } //Get indexes of the room the player currently is in the room array from RoomGeneration script;

    public void ChangeRoom(string direction)
    {
        Dictionary<string, Vector2Int> gridMovement = new Dictionary<string, Vector2Int>
        {
            ["L"] = new Vector2Int(-1, 0),
            ["T"] = new Vector2Int(0, 1),
            ["R"] = new Vector2Int(1, 0),
            ["B"] = new Vector2Int(0, -1)
        };

        currentRoom += gridMovement[direction];
    } //Change the currentRoom value when changing the room;

    public void SetRoom(Vector2Int coords)
    {
        currentRoom = coords;
    } //Set the value of currentRoom variable;

}
