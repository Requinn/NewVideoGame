using UnityEngine;
using System.Collections;

public class BasicBullet : MonoBehaviour, Projectile {
    public float velocity;
    public float lifetime;
    public float damage;
    public string target;
    // Use this for initialization

    void Start()
    {
        kill();
    }

    public void OnTriggerEnter(Collider c)
    {
        //Debug.Log("Collided2");
        if (c.gameObject.tag == target)
        {
            c.gameObject.GetComponentInChildren<HealthSystem>().takeDamage(damage);
            Destroy(this.gameObject);
        }
        else if (c.gameObject.tag == "Terrain" || c.gameObject.tag == "Untagged")
        {
            Destroy(this.gameObject);
        }
    }
    public void kill()
    {
        Destroy(this.gameObject, lifetime);

    }

    public void setTarget(string tgt)
    {
        target = tgt;
    }

    public float getVelocity()
    {
        return velocity;
    }
}
