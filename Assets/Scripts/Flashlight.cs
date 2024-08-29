using UnityEngine;
using System.Collections;

public class FlashlightToggle : MonoBehaviour
{
    public KeyCode toggleKey = KeyCode.F;
    public AudioClip toggleSound;
    public AudioClip stunSound;
    public GameObject ToggleFlashlightObjective;
    public GameObject StunEnemyObjective;
    public float stunRange = 10f;
    public float stunCooldown = 5f;

    public GameObject flashlight;
    private AudioSource audioSource;
    private bool canStun = true;
    public GameObject StunEffect;
    public GameObject stunFlashLight;
    public Camera fpsCam;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            ToggleFlashlight();
        }

        if (Input.GetMouseButtonDown(1) && canStun)
        {
            StunFlash();
        }
    }

    void ToggleFlashlight()
    {
        if (flashlight != null)
        {
            flashlight.SetActive(!flashlight.activeSelf);

            if (ToggleFlashlightObjective != null)
            {
                ToggleFlashlightObjective.SetActive(true);
            }

            if (toggleSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(toggleSound);
            }
        }
    }

    void StunFlash()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, stunRange))
        {
            Enemy enemy = hit.collider.GetComponent<Enemy>();
            Collider enemyCollider = enemy.GetComponent<Collider>();
            if (enemy != null && enemyCollider != null)
            {
                if (StunEnemyObjective != null)
                {
                    StunEnemyObjective.SetActive(true);
                }
                enemy.Stun();
                Vector3 enemyTop = enemyCollider.bounds.center + Vector3.up * enemyCollider.bounds.extents.y;
                Vector3 stunEffectPosition = new Vector3(enemyTop.x, enemyTop.y + 0.3f, enemyTop.z);

                GameObject stunEffectInstance = Instantiate(StunEffect, stunEffectPosition, Quaternion.identity);
                Destroy(stunEffectInstance, 3f);
            }
        }

        if (stunSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(stunSound);
        }
        StartCoroutine(FlashStunLight());
        StartCoroutine(StunCooldown());
    }

    IEnumerator StunCooldown()
    {
        canStun = false;
        yield return new WaitForSeconds(stunCooldown);
        canStun = true;
    }

    IEnumerator FlashStunLight()
    {
        if (stunFlashLight != null)
        {
            stunFlashLight.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            stunFlashLight.SetActive(false);
        }
    }
}