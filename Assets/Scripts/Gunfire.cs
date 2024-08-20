using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunshot : MonoBehaviour
{
    public AudioSource gunfireAudioSource;
    public AudioSource emptyClipAudioSource;
    public Animation gunAnimation;
    public string shootAnimationName = "Gunshot";

    public GameObject UpCurs;
    public GameObject DownCurs;
    public GameObject LeftCurs;
    public GameObject RightCurs;

    void Start()
    {
        gunfireAudioSource.playOnAwake = false;
        emptyClipAudioSource.playOnAwake = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (GlobalAmmo.LoadedAmmo > 0)
            {
                Shoot();
            }
            else
            {
                PlayEmptyClipSound();
            }
        }
    }

    void Shoot()
    {
        gunfireAudioSource.Play();
        gunAnimation.Play(shootAnimationName);

        UpCurs.GetComponent<Animator>().enabled = true;
        DownCurs.GetComponent<Animator>().enabled = true;
        LeftCurs.GetComponent<Animator>().enabled = true;
        RightCurs.GetComponent<Animator>().enabled = true;

        StartCoroutine(WaitingAnim());

        GlobalAmmo.LoadedAmmo -= 1;
    }

    void PlayEmptyClipSound()
    {
        emptyClipAudioSource.Play();
    }

    private IEnumerator WaitingAnim()
    {
        yield return new WaitForSeconds(0.1f);
        UpCurs.GetComponent<Animator>().enabled = false;
        DownCurs.GetComponent<Animator>().enabled = false;
        LeftCurs.GetComponent<Animator>().enabled = false;
        RightCurs.GetComponent<Animator>().enabled = false;
    }
}