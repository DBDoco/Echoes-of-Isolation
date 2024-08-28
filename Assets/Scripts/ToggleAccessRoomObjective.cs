using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleAccessRoomObjective : MonoBehaviour
{
    public GameObject ObjctiveComplete;  

    void Start()
    {
        if (ObjctiveComplete != null)
        {
            ObjctiveComplete.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && ObjctiveComplete != null)
        {
            ObjctiveComplete.SetActive(true);  
        }
    }
}
