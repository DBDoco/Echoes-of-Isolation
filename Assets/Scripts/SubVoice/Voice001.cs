using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Voice001 : MonoBehaviour
{
    public GameObject TheSubs;
    public AudioSource Voice;

    void OnTriggerEnter(Collider other)
    {
        StartCoroutine(Sub());
        this.GetComponent<BoxCollider>().enabled = false;

    }

    IEnumerator Sub()
    {
        Voice.Play();
        TheSubs.GetComponent<Text>().text = "What is that?";
        yield return new WaitForSeconds(2);
        TheSubs.GetComponent<Text>().text = "";

    }
}
