using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour
{
    public float health = 100f; // Increased health for the boss
    public string bossName = "Boss Name"; // Name of the boss to display

    private BossMove bossMove;
    private BossLook bossLook;
    private Animator animator;

    void Start()
    {
        bossMove = GetComponent<BossMove>();
        bossLook = GetComponent<BossLook>();
        animator = GetComponent<Animator>();
        BossUIManager.instance.SetBossName(bossName); // Set the boss name in the UI
        BossUIManager.instance.SetBossHealth(health); // Set the initial health in the UI
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        BossUIManager.instance.UpdateBossHealth(health); // Update the UI health bar

        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        if (bossMove != null) bossMove.Die();
        if (bossLook != null) bossLook.Die();
        StartCoroutine(DestroyAfterAnimation());
        BossUIManager.instance.HideBossUI(); // Hide the boss UI after the boss is defeated
    }

    IEnumerator DestroyAfterAnimation()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }
}
