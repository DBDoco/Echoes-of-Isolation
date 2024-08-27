using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject ThePlayer;
    public GameObject Camera1;
    public GameObject UI;
    public AudioSource cutsceneAudio; // Reference to the Audio Source for cutscene music

    void Start()
    {
        StartCoroutine(CutsceneBegin());
    }

    IEnumerator CutsceneBegin()
    {
        // Start playing the cutscene music
        cutsceneAudio.Play();

        // Cutscene duration
        yield return new WaitForSeconds(4.5f);

        // Activate the player and UI
        ThePlayer.SetActive(true);
        UI.SetActive(true);

        // Stop playing the cutscene music
        cutsceneAudio.Stop();

        // Disable this cutscene object to ensure it doesn't replay
        this.gameObject.SetActive(false);
    }
}
