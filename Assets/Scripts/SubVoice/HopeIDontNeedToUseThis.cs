using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HopeIDontHaveToUseThis : MonoBehaviour
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
        TheSubs.GetComponent<Text>().text = "Hope I don't need to use this, but better safe than sorry.";
        yield return new WaitForSeconds(3);
        TheSubs.GetComponent<Text>().text = "";

    }
}
