using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public GameObject Door;
    public float Distance;

    void Update()
    {
        Distance = PlayerCasting.DistanceFromTarget;
        if (Input.GetButtonDown("Action"))
        {
            if (Distance <= 2)
            {
                Door.GetComponent<Animator>().enabled = true;
                StartCoroutine(OpenTheDoor());
            }
        }
    }

    private IEnumerator OpenTheDoor()
    {
        yield return new WaitForSeconds(4);
        Door.GetComponent<Animator>().enabled = false;
    }
}
