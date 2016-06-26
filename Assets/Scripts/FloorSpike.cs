using UnityEngine;
using System.Collections.Generic;
using MovementEffects;

public class FloorSpike : MonoBehaviour {
    public float damage;
    public float interval;

    private Collider col;
    private bool canFire = true;
    // Use this for initialization
    void Start ()
    {
        col = GetComponent<Collider>();
	}
	
	// Update is called once per frame
	void Update ()
    {

    }

    void OnTriggerStay(Collider c)
    {
        if (c.gameObject.tag == "Player" && canFire)
        {
            c.gameObject.GetComponent<PlayerControl>().health.takeDamage(damage);
            Timing.RunCoroutine(wait(interval));
        }
    }
    void OnTriggerExit(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            canFire = true;
        }
    }

    IEnumerator<float> wait(float seconds)
    {
        canFire = false;
        yield return Timing.WaitForSeconds(seconds);
        canFire = true;
    }
}
