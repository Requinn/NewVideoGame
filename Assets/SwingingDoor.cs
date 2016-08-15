using UnityEngine;
using System.Collections;

public class SwingingDoor : MonoBehaviour {
    public Transform hinge;
    public float swingspeed;
    private float start = 0.0f;
    private float finish = 90.0f;
    private bool open = false;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (open)
        {
            hinge.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, finish, 0), Time.deltaTime * swingspeed);
        }
        if(!open)
        {
           hinge.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, start, 0), Time.deltaTime * swingspeed);
        }
        
    }

    private void activate(float signal)
    {
        open = !open;
    }
}
