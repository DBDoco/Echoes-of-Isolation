using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    public float health = 20f;
    public AudioSource hitAudioSource; 
    public AudioSource explosionAudioSource; 
    public GameObject explosionEffect;
    public float explosionRadius = 5f;
    public float explosionForce = 700f;
    public int explosionDamage = 5; 
    private bool hasExploded = false; 

    void Start()
    {
        if (hitAudioSource == null || explosionAudioSource == null)
        {
            Debug.LogError("AudioSources for hit and explosion sounds are not assigned.");
        }
    }

    public void TakeDamage(float amount)
    {
        if (hasExploded) return; 

        health -= amount;

        if (hitAudioSource != null)
        {
            hitAudioSource.Play(); 
        }

        if (health <= 0f)
        {
            Explode();
        }
    }

    void Explode()
    {
        hasExploded = true;

        if (explosionAudioSource != null)
        {
            explosionAudioSource.Play();
        }

        if (explosionEffect != null)
        {
            GameObject explosion = Instantiate(explosionEffect, transform.position, transform.rotation);
            explosion.SetActive(true);

            ParticleSystem ps = explosion.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                Destroy(explosion, ps.main.duration);
            }
            else
            {
                Destroy(explosion, 2f);
            }
        }

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 randomDirection = (nearbyObject.transform.position - transform.position).normalized;
                randomDirection += new Vector3(
                    Random.Range(-0.5f, 0.5f),
                    Random.Range(-0.5f, 0.5f),
                    Random.Range(-0.5f, 0.5f)
                ).normalized;

                rb.AddExplosionForce(explosionForce, transform.position + randomDirection, explosionRadius);
            }

            Enemy enemy = nearbyObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(explosionDamage);
            }

            if (nearbyObject.CompareTag("Player"))
            {
                GlobalHealth.ApplyDamage(explosionDamage);
            }
        }
    }
}
