using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour, Weapon {
    public Rigidbody projectile;
    public float fireRate;
    public int fullMag = 15;
    public int magazine = 15;
    public float reloadTime = 2;

    private bool canFire = true;

    public IEnumerator fire()
    {
        if (canFire)
        {
            if (magazine > 0)
            {
                magazine--;
                canFire = false;
                Rigidbody instantProjectile = Instantiate(projectile, transform.position, GetComponentInParent<Transform>().rotation) as Rigidbody;
                instantProjectile.velocity = transform.TransformDirection(new Vector3(0, projectile.GetComponent<Projectile>().velocity, 0));
                //Extra shit like sounds and muzzle flash or whatever goes here
                yield return new WaitForSeconds(1 / fireRate); //reciprocal to get rounds per second.
                canFire = true;
            }
            else
            {
                StartCoroutine(reload());
            }
        } 
    }

    public IEnumerator reload()
    {
        canFire = false;
        //play reload animation
        yield return new WaitForSeconds(reloadTime);
        magazine = fullMag;
        canFire = true;
    }
}
