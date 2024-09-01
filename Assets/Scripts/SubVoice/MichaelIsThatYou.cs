using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MichaelIsThatYou : MonoBehaviour
{
    public GameObject TheSubs;
    public AudioSource Voice;

    void OnTriggerEnter(Collider other)
    {
        this.GetComponent<BoxCollider>().enabled = false;
        StartCoroutine(Sub());
    }

    IEnumerator Sub()
    {
        Voice.Play();
        TheSubs.GetComponent<Text>().text = "Michael...? Is that... really you?";
        yield return new WaitForSeconds(4);
        TheSubs.GetComponent<Text>().text = "";

    }
}
