using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeemsLikeTheGenerators : MonoBehaviour
{
    public GameObject TheSubs;
    public AudioSource Voice;
    public AudioSource GeneratorSound; 

    void OnTriggerEnter(Collider other)
    {
        StartCoroutine(Sub());
        this.GetComponent<BoxCollider>().enabled = false;
    }

    IEnumerator Sub()
    {
        Voice.Play();
        GeneratorSound.Play(); 
        TheSubs.GetComponent<Text>().text = "Seems like the generators turned off... I have to turn them on again";
        yield return new WaitForSeconds(4);
        TheSubs.GetComponent<Text>().text = "";
    }
}
