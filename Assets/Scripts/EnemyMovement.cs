using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform player; 
    public float speed = 3f;  
    public float rotationSpeed = 5f; 
    public float heightOffset = 1f;  

    void Update()
    {
        Vector3 direction = player.position - transform.position;
        direction.y = 0;  

        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
        }

        Vector3 moveDirection = direction.normalized * speed * Time.deltaTime;
        transform.position += moveDirection;

        Vector3 newPosition = transform.position;
        newPosition.y = player.position.y + heightOffset;
        transform.position = newPosition;
    }
}
