using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunshot : MonoBehaviour
{
    public AudioSource gunAudioSource; 
    public Animation gunAnimation;     
    public string shootAnimationName = "Gunshot";  

    void Start()
    {
        gunAudioSource.playOnAwake = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        gunAudioSource.Play();
        gunAnimation.Play(shootAnimationName);
    }
}
