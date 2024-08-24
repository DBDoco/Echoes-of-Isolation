using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    public float health = 20f;
    public AudioSource hitAudioSource; // AudioSource for the hit sound
    public AudioSource explosionAudioSource; // AudioSource for the explosion sound
    public GameObject explosionEffect;
    public float explosionRadius = 5f;
    public float explosionForce = 700f;
    public int explosionDamage = 5; // Damage dealt to enemies and player within the explosion radius
    private bool hasExploded = false; // Track if the barrel has already exploded

    void Start()
    {
        // Ensure that both audio sources are assigned
        if (hitAudioSource == null || explosionAudioSource == null)
        {
            Debug.LogError("AudioSources for hit and explosion sounds are not assigned.");
        }
    }

    public void TakeDamage(float amount)
    {
        if (hasExploded) return; // Do nothing if the barrel has already exploded

        Debug.Log("Barrel hit!");
        health -= amount;

        if (hitAudioSource != null)
        {
            hitAudioSource.Play(); // Play the hit sound
        }

        if (health <= 0f)
        {
            Explode();
        }
    }

    void Explode()
    {
        hasExploded = true; // Mark the barrel as exploded

        // Play explosion sound
        if (explosionAudioSource != null)
        {
            explosionAudioSource.Play();
        }

        // Instantiate and activate explosion effect
        if (explosionEffect != null)
        {
            GameObject explosion = Instantiate(explosionEffect, transform.position, transform.rotation);
            explosion.SetActive(true); // Ensure the explosion effect is active

            // Destroy the explosion effect after it has finished playing
            ParticleSystem ps = explosion.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                Destroy(explosion, ps.main.duration);
            }
            else
            {
                Destroy(explosion, 2f); // Fallback in case the particle system doesn't have a duration
            }
        }

        // Apply explosion force and damage to nearby objects
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Generate a random direction for the force
                Vector3 randomDirection = (nearbyObject.transform.position - transform.position).normalized;
                randomDirection += new Vector3(
                    Random.Range(-0.5f, 0.5f),
                    Random.Range(-0.5f, 0.5f),
                    Random.Range(-0.5f, 0.5f)
                ).normalized;

                // Apply the explosion force with random direction
                rb.AddExplosionForce(explosionForce, transform.position + randomDirection, explosionRadius);
            }

            // Check if the object is an enemy and apply damage
            Enemy enemy = nearbyObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(explosionDamage); // Apply damage to the enemy
            }

            // Check if the object is the player and apply damage
            if (nearbyObject.CompareTag("Player"))
            {
                GlobalHealth.ApplyDamage(explosionDamage); // Apply damage to the player's health and trigger effects
            }
        }
    }
}
