using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {
    public float velocity;

    private Vector3 difference, destination;
	// Use this for initialization
	void Start () {
        destination = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(destination);
        if (transform.position != destination)
        {
            difference = destination - transform.position;
            transform.position = Vector3.Lerp(transform.position, destination, Time.smoothDeltaTime);
        }
	}

     public void ChangeDirection(Vector3 dest)
    {
        Debug.Log("Destination changed.");
        destination = dest;
    }

    public Vector3 getPosition()
    {
        return transform.position;
    }

    public void OnTriggerEnter(Collider col)
    {
        //make player a child of platform so we don't slide off
        col.transform.SetParent(this.transform,true);
    }

    public void OnTriggerExit(Collider col)
    {
        col.transform.SetParent(null, true);
    }
}
