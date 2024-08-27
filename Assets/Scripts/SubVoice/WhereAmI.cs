using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhereAmI : MonoBehaviour
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
        TheSubs.GetComponent<Text>().text = "Where am I?";
        yield return new WaitForSeconds(2);
        TheSubs.GetComponent<Text>().text = "";

    }
}
