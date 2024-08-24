using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject ThePlayer;
    public GameObject Camera1;
    public GameObject UI;

    void Start()
    {
        StartCoroutine(CutsceneBegin());
    }

    IEnumerator CutsceneBegin()
    {
        yield return new WaitForSeconds(4.5f); 
        ThePlayer.SetActive(true); 
        UI.SetActive(true); 
    }
}
