using UnityEngine;
using System.Collections;

public class SwingingDoor : MonoBehaviour {
    public Transform hinge;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void activate(float signal)
    {
        hinge.Rotate(0, 90, 0);
    }
}
