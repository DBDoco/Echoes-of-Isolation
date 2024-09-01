using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public string keyColor; 
    public AudioClip pickupSound; 
    public GameObject ObjectiveComplete;
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
            KeyCollection keyCollection = other.GetComponent<KeyCollection>();
            if (keyCollection != null)
            {
                keyCollection.CollectKey(keyColor);

                audioSource.Play();
                if (ObjectiveComplete != null)
                    ObjectiveComplete.SetActive(true);

                Destroy(gameObject, pickupSound.length);
            }
        }
    }
}
