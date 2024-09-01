using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunshot : MonoBehaviour
{
    public AudioSource gunfireAudioSource;
    public AudioSource emptyClipAudioSource;
    public Animation gunAnimation;
    public string shootAnimationName = "Gunshot";

    public GameObject Flash;

    public GameObject UpCurs;
    public GameObject DownCurs;
    public GameObject LeftCurs;
    public GameObject RightCurs;

    public GameObject? ObjctiveComplete;  

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
                if (ObjctiveComplete != null)
                {
                    ObjctiveComplete.SetActive(true);
                }
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

        Flash.SetActive(true);

        UpCurs.GetComponent<Animator>().enabled = true;
        DownCurs.GetComponent<Animator>().enabled = true;
        LeftCurs.GetComponent<Animator>().enabled = true;
        RightCurs.GetComponent<Animator>().enabled = true;

        StartCoroutine(MuzzleOff());
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

    private IEnumerator MuzzleOff()
    {
        yield return new WaitForSeconds(0.1f);
        Flash.SetActive(false);
    }
}
