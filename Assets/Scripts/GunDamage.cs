using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunDamage : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;

    public GameObject Bullet;
    public GameObject Blood;

    public Camera fpsCam;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Enemy target = hit.transform.GetComponent<Enemy>();
            if (target != null)
            {
                Instantiate(Blood, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                target.TakeDamage(damage);
            } else {
                Instantiate(Bullet, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
            }
        }
    }
}
