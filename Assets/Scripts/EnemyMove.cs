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
    public float AttackCooldown = 1.0f; 
    public float DamageDelay = 0.5f; 
    public AudioClip enemySound; 
    private AudioSource audioSource;

    private Animator animator;
    private Rigidbody rb;
    private bool isDead = false;
    private bool canAttack = true; 
    private bool isAttacking = false; 
    private bool playerDetected = false; 

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            Debug.LogError("Rigidbody component missing. Please attach a Rigidbody component to the enemy.");
        }
        else
        {
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        }

        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("AudioSource component missing. Please attach an AudioSource component to the enemy.");
        }
        else if (enemySound != null)
        {
            audioSource.clip = enemySound;
            audioSource.loop = true; 
        }
    }

    void Update()
    {
        if (isDead) return;

        FollowPlayer();
    }

    void FollowPlayer()
    {
        if (ThePlayer != null && !isAttacking) 
        {
            float distanceToPlayer = Vector3.Distance(transform.position, ThePlayer.transform.position);

            if (distanceToPlayer <= AttackRange && canAttack)
            {
                StartCoroutine(PunchPlayerWithDelay());
            }
            else if (distanceToPlayer <= DetectionRadius || playerDetected)
            {
                if (!playerDetected)
                {
                    playerDetected = true; 
                }

                if (!audioSource.isPlaying && enemySound != null)
                {
                    audioSource.Play(); 
                }

                Vector3 targetPosition = ThePlayer.transform.position;
                targetPosition.y += HeightOffset;

                transform.position = Vector3.MoveTowards(transform.position, targetPosition, EnemySpeed * Time.deltaTime);

                float movementSpeed = Vector3.Distance(transform.position, targetPosition);

                animator.SetFloat("Speed", movementSpeed);
            }
            else
            {
                animator.SetFloat("Speed", 0);
                if (audioSource.isPlaying)
                {
                    audioSource.Pause();
                }
            }
        }
        else
        {
            animator.SetFloat("Speed", 0); 
        }
    }

    IEnumerator PunchPlayerWithDelay()
    {
        canAttack = false;
        isAttacking = true; 
        animator.SetTrigger("Punch");

        yield return new WaitForSeconds(DamageDelay);

        float distanceToPlayer = Vector3.Distance(transform.position, ThePlayer.transform.position);
        if (distanceToPlayer <= AttackRange)
        {
            GlobalHealth.ApplyDamage(1);
        }

        StartCoroutine(AttackCooldownCoroutine());
    }

    IEnumerator AttackCooldownCoroutine()
    {
        yield return new WaitForSeconds(AttackCooldown);
        canAttack = true; 
        isAttacking = false; 
    }

    public void Die()
    {
        isDead = true;
        animator.SetTrigger("Die");

        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
