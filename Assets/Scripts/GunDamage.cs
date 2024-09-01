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
            Enemy enemyTarget = hit.transform.GetComponent<Enemy>();
            Boss bossTarget = hit.transform.GetComponent<Boss>();
            Barrel barrelTarget = hit.transform.GetComponent<Barrel>();

            if (enemyTarget != null)
            {
                GameObject bloodInstance = Instantiate(Blood, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));

                Destroy(bloodInstance, 5f);

                enemyTarget.TakeDamage(damage);
            }
            else if (bossTarget != null)
            {
                GameObject bloodInstance = Instantiate(Blood, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));

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
