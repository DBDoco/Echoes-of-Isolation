using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public GameObject ThePlayer;
    public float EnemySpeed = 0.02f;
    public float HeightOffset = 1.0f;
    public float DetectionRadius = 5.0f;
    public float AttackRange = 1.5f;
    public float AttackCooldown = 1.0f; // Cooldown time between attacks
    public float DamageDelay = 0.5f; // Delay before the player takes damage

    private Animator animator;
    private Rigidbody rb;
    private bool isDead = false;
    private bool canAttack = true; // Track if the enemy can attack
    private bool isAttacking = false; // Track if the enemy is currently attacking

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        // Freeze rotation on X and Z axes to prevent tipping
        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }
    }

    void Update()
    {
        if (isDead) return;

        FollowPlayer();
    }

    void FollowPlayer()
    {
        if (ThePlayer != null && !isAttacking) // Enemy only follows the player if it's not attacking
        {
            float distanceToPlayer = Vector3.Distance(transform.position, ThePlayer.transform.position);

            if (distanceToPlayer <= AttackRange && canAttack)
            {
                StartCoroutine(PunchPlayerWithDelay());
            }
            else if (distanceToPlayer <= DetectionRadius)
            {
                Vector3 targetPosition = ThePlayer.transform.position;
                targetPosition.y += HeightOffset;

                transform.position = Vector3.MoveTowards(transform.position, targetPosition, EnemySpeed * Time.deltaTime);

                float movementSpeed = Vector3.Distance(transform.position, targetPosition);

                animator.SetFloat("Speed", movementSpeed);
            }
            else
            {
                animator.SetFloat("Speed", 0);
            }
        }
        else
        {
            animator.SetFloat("Speed", 0); // Stop movement animation if attacking
        }
    }

    IEnumerator PunchPlayerWithDelay()
    {
        canAttack = false;
        isAttacking = true; // Mark as attacking
        animator.SetTrigger("Punch");

        // Wait for the delay before applying damage
        yield return new WaitForSeconds(DamageDelay);

        // Check if the player is still within attack range before applying damage
        float distanceToPlayer = Vector3.Distance(transform.position, ThePlayer.transform.position);
        if (distanceToPlayer <= AttackRange)
        {
            GlobalHealth.ApplyDamage(1); // Apply 1 point of damage to the player
            // GlobalHealth.ApplyDamage now handles the screen flash and hurt sound
        }

        // Start the cooldown
        StartCoroutine(AttackCooldownCoroutine());
    }

    IEnumerator AttackCooldownCoroutine()
    {
        yield return new WaitForSeconds(AttackCooldown); // Wait for the cooldown time
        canAttack = true; // Allow attacking again
        isAttacking = false; // End attack state
    }

    public void Die()
    {
        isDead = true;
        animator.SetTrigger("Die");
        rb.constraints = RigidbodyConstraints.None; // Optionally, remove constraints on death
    }
}
