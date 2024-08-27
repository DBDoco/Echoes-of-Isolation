using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhatIsThisPlace : MonoBehaviour
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
        TheSubs.GetComponent<Text>().text = "What is this place?";
        yield return new WaitForSeconds(2);
        TheSubs.GetComponent<Text>().text = "";

    }
}
