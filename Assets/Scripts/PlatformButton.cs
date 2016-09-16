using UnityEngine;
using System.Collections;

public class PlatformButton : MonoBehaviour {
    public Transform pointA, pointB;
    public MovingPlatform platform;
    public bool atHome;
	void Start () {
        atHome = true;
	}
	//Platform will only move if it as either of the two points, so we can't just jiggle the plat in place.
    void activate(float signal)
    {
        Debug.Log("ACTIVATED");
        if (V3Equal(platform.getPosition(), pointB.position))
        {
            platform.ChangeDirection(pointA.position);
        }
        if (V3Equal(platform.getPosition(), pointA.position))
        {
            platform.ChangeDirection(pointB.position);
        }
    }

    public bool V3Equal(Vector3 a, Vector3 b)
    {
        return Vector3.SqrMagnitude(a - b) < 0.001;
    }

}
