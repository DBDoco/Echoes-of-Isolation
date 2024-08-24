using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunDamage : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;

    public GameObject Bullet;
    public GameObject Blood;
    public GameObject BulletHole;  // Bullet hole prefab

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
            Enemy enemyTarget = hit.transform.GetComponent<Enemy>();
            Barrel barrelTarget = hit.transform.GetComponent<Barrel>();

            if (enemyTarget != null)
            {
                // Create blood effect
                Instantiate(Blood, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                enemyTarget.TakeDamage(damage);
            }
            else if (barrelTarget != null)
            {
                barrelTarget.TakeDamage(damage);
            }
            else
            {
                // Instantiate a bullet hole at the hit point
                CreateBulletHole(hit);

                // Instantiate bullet impact effect at the hit point
                Instantiate(Bullet, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
            }
        }
    }

    void CreateBulletHole(RaycastHit hit)
    {
        // Slightly increase the offset to prevent z-fighting
        Vector3 bulletHolePosition = hit.point + hit.normal * 0.05f;

        // Instantiate the bullet hole
        GameObject bulletHole = Instantiate(BulletHole, bulletHolePosition, Quaternion.LookRotation(hit.normal));

        // Optionally do not parent it, or set a different layer or scale
        // bulletHole.transform.SetParent(hit.transform);
    }

}
