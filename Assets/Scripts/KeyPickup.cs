using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public string keyColor; // The color of the key
    public AudioClip pickupSound; // The sound that plays when the key is picked up
    public GameObject ObjctiveComplete;
    private AudioSource audioSource; // The AudioSource component


    void Start()
    {
        // Add an AudioSource component to the key object if one doesn't already exist
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = pickupSound;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            KeyCollection keyCollection = other.GetComponent<KeyCollection>();
            if (keyCollection != null)
            {
                keyCollection.CollectKey(keyColor);

                // Play the pickup sound
                audioSource.Play();
                ObjctiveComplete.SetActive(true);

                // Destroy the key object after the sound has finished playing
                Destroy(gameObject, pickupSound.length);
            }
        }
    }
}
