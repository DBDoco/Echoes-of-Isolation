using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLook : MonoBehaviour
{

    public GameObject ThePlayer;

    void Start()
    {

    }

    void Update()
    {

        transform.LookAt(ThePlayer.transform);

    }
}