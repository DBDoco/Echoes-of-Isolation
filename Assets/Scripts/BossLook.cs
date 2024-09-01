using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLook : MonoBehaviour
{
    public GameObject ThePlayer;
    public float DetectionRadius = 15f; 
    public float RotationSpeed = 3f; 
    public bool isAlive = true;

    void Update()
    {
        if (isAlive && ThePlayer != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, ThePlayer.transform.position);

            if (distanceToPlayer <= DetectionRadius)
            {
                Vector3 directionToPlayer = (ThePlayer.transform.position - transform.position).normalized;

                Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);

                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, RotationSpeed * Time.deltaTime);
            }
        }
    }

    public void Die()
    {
        isAlive = false;
    }
}
