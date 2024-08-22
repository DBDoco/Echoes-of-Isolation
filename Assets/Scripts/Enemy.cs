using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 50f;
    private EnemyMove enemyMove;

    void Start()
    {
        enemyMove = GetComponent<EnemyMove>();
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        enemyMove.Die(); 
        StartCoroutine(DestroyAfterAnimation()); 
    }

    IEnumerator DestroyAfterAnimation()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject); 
    }
}
