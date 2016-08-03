using UnityEngine;
using System.Collections.Generic;
using MovementEffects;

public class PlayerGun : MonoBehaviour, Weapon
{
    public Rigidbody projectile;
    public float fireRate;
    //public int fullMag = 15;
    //public int magazine = 15;
    public float reloadTime = 2;

    public int energycost = 5;

    public string target;
    public float damage;
    private bool canFire = true;
    public HealthSystem shieldenrgy;

    public IEnumerator<float> fire()
    {
        if (canFire)
        {
            if (shieldenrgy.shieldHP > energycost)
            {
                //magazine--;
                shieldenrgy.takeShieldDmg(energycost);
                canFire = false;
                projectile.GetComponent<Projectile>().setDamage(damage);
                projectile.GetComponent<Projectile>().setTarget(target);
                Rigidbody instantProjectile = Instantiate(projectile, transform.position, GetComponentInParent<Transform>().rotation) as Rigidbody;
                instantProjectile.velocity = transform.TransformDirection(new Vector3(0, projectile.GetComponent<Projectile>().getVelocity(), 0));
                //Extra shit like sounds and muzzle flash or whatever goes here
                yield return Timing.WaitForSeconds(1 / fireRate); //reciprocal to get rounds per second.
                canFire = true;
            }
            else
            {
                //maybe some kind of noise like bweeeorr to tel you're out of energy
                Timing.RunCoroutine(reload());
            }
        }
    }

    public IEnumerator<float> reload()
    {
        canFire = false;
        //play reload animation
        yield return Timing.WaitForSeconds(reloadTime);
        //magazine = fullMag;
        canFire = true;
    }
}
