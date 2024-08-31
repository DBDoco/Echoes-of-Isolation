using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour
{
    public AudioSource introAudio;

    void Start()
    {
        StartCoroutine(PlayAudio());
    }

    private IEnumerator PlayAudio()
    {
        introAudio.Play();
        yield return new WaitForSeconds(64f);
        SceneManager.LoadScene("Level001");
    }
}
