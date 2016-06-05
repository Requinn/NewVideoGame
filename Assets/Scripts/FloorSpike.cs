using UnityEngine;
using System.Collections;

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
            StartCoroutine(wait(interval));
        }
    }
    void OnTriggerExit(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            canFire = true;
        }
    }

    IEnumerator wait(float seconds)
    {
        canFire = false;
        yield return new WaitForSeconds(seconds);
        canFire = true;
    }
}
