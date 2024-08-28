using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardFragmentPickup : MonoBehaviour
{
    public string cardColor;
    public AudioClip pickupSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = pickupSound;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CardFragmentCollection cardCollection = other.GetComponent<CardFragmentCollection>();
            if (cardCollection != null)
            {
                cardCollection.CollectCardFragment(cardColor);

                audioSource.Play();

                Destroy(gameObject, pickupSound.length);
            }
        }
    }
}
