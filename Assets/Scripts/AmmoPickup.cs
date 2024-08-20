using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public AudioSource AmmoSound;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            AmmoSound.Play();
            if (GlobalAmmo.LoadedAmmo == 0)
                GlobalAmmo.LoadedAmmo += 10;
            else
                GlobalAmmo.CurrentAmmo += 10;
            Destroy(gameObject);
        }
    }
}
