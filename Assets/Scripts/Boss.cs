using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public float health = 100f; 
    public string bossName = "Boss Name"; 
    public GameObject objectiveComplete;

    private BossMove bossMove;
    private BossLook bossLook;
    private Animator animator;

    public AudioSource Voice;
    public GameObject TheSubs;

    void Start()
    {
        bossMove = GetComponent<BossMove>();
        bossLook = GetComponent<BossLook>();
        animator = GetComponent<Animator>();
        BossUIManager.instance.SetBossName(bossName); 
        BossUIManager.instance.SetBossHealth(health); 
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        BossUIManager.instance.UpdateBossHealth(health);

        if (health <= 0f)
        {
            Die();
            objectiveComplete.SetActive(true);
        }
    }

    void Die()
    {
        if (bossMove != null) bossMove.Die();
        if (bossLook != null) bossLook.Die();
        StartCoroutine(DestroyAfterAnimation());
        BossUIManager.instance.HideBossUI(); 
        StartCoroutine(PlayVoice());
    }

    IEnumerator DestroyAfterAnimation()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }

    IEnumerator PlayVoice()
    {
        yield return new WaitForSeconds(4.5f);
        Voice.Play();
        TheSubs.GetComponent<Text>().text = "I am sorry... Michael... I am so sorry.";
        yield return new WaitForSeconds(4);
        TheSubs.GetComponent<Text>().text = "";
    }
}
