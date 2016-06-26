using UnityEngine;
using System.Collections;

public interface Projectile
{
    void OnTriggerEnter(Collider c); //hit detection
    void kill(); //controls bullet lifetime
    void setTarget(string target);
    void setDamage(float damage);
    float getVelocity();
}
