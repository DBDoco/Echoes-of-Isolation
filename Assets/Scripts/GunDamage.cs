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
            if (GlobalAmmo.LoadedAmmo > 0)
            {
                Shoot();
            }
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            // Check if the hit object has an Enemy component
            Enemy enemyTarget = hit.transform.GetComponent<Enemy>();
            // Check if the hit object has a Boss component
            Boss bossTarget = hit.transform.GetComponent<Boss>();
            Barrel barrelTarget = hit.transform.GetComponent<Barrel>();

            if (enemyTarget != null)
            {
                // Create blood effect
                GameObject bloodInstance = Instantiate(Blood, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));

                // Destroy blood effect after a delay (e.g., 5 seconds)
                Destroy(bloodInstance, 5f);

                enemyTarget.TakeDamage(damage);
            }
            else if (bossTarget != null)
            {
                // Create blood effect
                GameObject bloodInstance = Instantiate(Blood, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));

                // Destroy blood effect after a delay (e.g., 5 seconds)
                Destroy(bloodInstance, 5f);

                bossTarget.TakeDamage(damage);
            }
            else if (barrelTarget != null)
            {
                barrelTarget.TakeDamage(damage);
            }
            else
            {
                Vector3 bulletHolePosition = hit.point + hit.normal * 0.01f;
                GameObject bulletHoleInstance = Instantiate(Bullet, bulletHolePosition, Quaternion.FromToRotation(Vector3.up, hit.normal));
                Destroy(bulletHoleInstance, 10f);
            }
        }
    }
}
