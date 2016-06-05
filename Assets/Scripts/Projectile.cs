using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
    public float velocity;
    public float lifetime;
    public float damage;
    public string target;
	// Use this for initialization
	void Start () {
        kill();
    }
	
	// Update is called once per frame
	void Update () {
	    
	}
    void OnTriggerEnter(Collider c)
    {
        //Debug.Log("Collided2");
        if (c.gameObject.tag == "Player")
        {
            c.gameObject.GetComponent<PlayerControl>().health.takeDamage(damage);
            Destroy(this.gameObject);
        }
        if (c.gameObject.tag == "Terrain")
        {
            Destroy(this.gameObject);
        }
    }
    void kill()
    { 
        Destroy(this.gameObject, lifetime);

    }
}
