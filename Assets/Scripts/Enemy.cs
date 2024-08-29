using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float health = 50f;
    public float stunDuration = 2f;

    private EnemyMove enemyMove;
    private EnemyLook enemyLook;
    private Animator animator;
    private bool isStunned = false;

    void Start()
    {
        enemyMove = GetComponent<EnemyMove>();
        enemyLook = GetComponent<EnemyLook>();
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0f)
        {
            Die();
        }
    }

    public void Stun()
    {
        if (!isStunned)
        {
            StartCoroutine(ApplyStun());
        }
    }

    IEnumerator ApplyStun()
    {
        isStunned = true;

        // Disable movement and looking
        if (enemyMove != null) enemyMove.enabled = false;
        if (enemyLook != null) enemyLook.enabled = false;

        // Pause the animation
        if (animator != null)
        {
            animator.speed = 0;
        }

        yield return new WaitForSeconds(stunDuration);

        // Re-enable movement and looking
        if (enemyMove != null) enemyMove.enabled = true;
        if (enemyLook != null) enemyLook.enabled = true;

        // Resume the animation
        if (animator != null)
        {
            animator.speed = 1;
        }

        isStunned = false;
    }

    void Die()
    {
        if (enemyMove != null) enemyMove.Die();
        if (enemyLook != null) enemyLook.Die();
        StartCoroutine(DestroyAfterAnimation());
    }

    IEnumerator DestroyAfterAnimation()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}