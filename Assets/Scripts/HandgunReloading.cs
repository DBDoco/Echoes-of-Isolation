using System.Collections;
using UnityEngine;

public class HandgunReloading : MonoBehaviour
{
    public AudioSource reloadAudioSource;
    public Animation gunAnimation;
    public string reloadAnimationName = "HandgunReload";

    public GameObject UpCurs;
    public GameObject DownCurs;
    public GameObject LeftCurs;
    public GameObject RightCurs;

    private const int MaxClipSize = 10;
    private bool isReloading = false;
    private MonoBehaviour shootingScript;

    void Start()
    {
        reloadAudioSource.playOnAwake = false;

        if (!gunAnimation.GetClip(reloadAnimationName))
        {
            Debug.LogError($"Animation clip '{reloadAnimationName}' not found. Please check the animation name and ensure it's added to the Animation component.");
        }

        shootingScript = GetComponent<MonoBehaviour>();
        if (shootingScript == null || shootingScript.GetType().Name != "Gunfire")
        {
            MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour script in scripts)
            {
                if (script.GetType().Name == "Gunfire")
                {
                    shootingScript = script;
                    break;
                }
            }

            if (shootingScript == null)
            {
                Debug.LogError("Shooting script (Gunfire) not found on this GameObject. Please ensure it's attached to the same GameObject as this script.");
            }
        }

        // Ensure crosshair is initially visible
        SetCrosshairVisibility(true);
    }

    void Update()
    {
        if (isReloading) return;

        int clipCount = GlobalAmmo.LoadedAmmo;
        int reserveCount = GlobalAmmo.CurrentAmmo;
        int reloadAvailable = reserveCount > 0 ? Mathf.Min(MaxClipSize - clipCount, reserveCount) : 0;

        if (Input.GetButtonDown("Reload") && reloadAvailable > 0)
        {
            StartCoroutine(ReloadSequence(reloadAvailable));
        }
    }

    IEnumerator ReloadSequence(int ammoToReload)
    {
        isReloading = true;
        DisableShooting();

        // Show crosshair at the start of reload
        SetCrosshairVisibility(false);

        reloadAudioSource.Play();
        gunAnimation.Play(reloadAnimationName);

        // Wait for the animation to complete
        yield return new WaitForSeconds(gunAnimation[reloadAnimationName].length);

        GlobalAmmo.LoadedAmmo += ammoToReload;
        GlobalAmmo.CurrentAmmo -= ammoToReload;

        // Hide crosshair at the end of reload
        SetCrosshairVisibility(true);

        EnableShooting();
        isReloading = false;

        yield return null;
    }

    private void SetCrosshairVisibility(bool visible)
    {
        UpCurs.SetActive(visible);
        DownCurs.SetActive(visible);
        LeftCurs.SetActive(visible);
        RightCurs.SetActive(visible);
    }

    private void DisableShooting()
    {
        if (shootingScript != null)
        {
            shootingScript.enabled = false;
        }
    }

    private void EnableShooting()
    {
        if (shootingScript != null)
        {
            shootingScript.enabled = true;
        }
    }
}