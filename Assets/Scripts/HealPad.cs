using UnityEngine;
using System.Collections;

public class HealPad : MonoBehaviour {
    public float heal;
    public float interval;

    private Collider col;
    private bool canFire = true;
    // Use this for initialization
    void Start()
    {
        col = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay(Collider c)
    {
        //Debug.Log("Collided2");
        if (c.gameObject.tag == "Player" && canFire)
        {
            c.gameObject.GetComponent<PlayerControl>().health.addHP(heal, false);
            StartCoroutine(wait(interval));
        }
    }

    IEnumerator wait(float seconds)
    {
        canFire = false;
        yield return new WaitForSeconds(seconds);
        canFire = true;
    }
}
