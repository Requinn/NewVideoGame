using UnityEngine;
using System.Collections;

public class Vision : MonoBehaviour {
    public float FOVangle = 110.0f;
    public float alertAngle = 90.0f;
    public float sightDistance = 20.0f;
    public float alertDistance = 5.0f;
    //public bool playerSight;
    public int alertness;
    
    public Vector3 personalLastSighting;

    private GameObject player;
    private RaycastHit hit;
    
    // Use this for initialization
    void Awake() {
        player = GameObject.FindGameObjectWithTag("Player");
        alertness = 0;
        
    }

    // Update is called once per frame
    void FixedUpdate () {
        alertness = CanSeeMonster(player);
	}

    int CanSeeMonster(GameObject target)
    {
        float heightOfPlayer = 1.5f;

        Vector3 startVec = transform.position;
        startVec.y += heightOfPlayer;
        Vector3 startVecFwd = transform.forward;
        startVecFwd.y += heightOfPlayer;
        

        Vector3 rayDirection = target.transform.position - startVec;
        // Detect entities in view
        if ((Vector3.Angle(rayDirection, startVecFwd)) < FOVangle && Physics.Raycast(startVec, rayDirection, out hit, sightDistance))
        { 
            //Make sure it was the player
            if (hit.collider.gameObject == target)
            {
                personalLastSighting = target.transform.position;
                //They are very close
                if ((Vector3.Angle(rayDirection, startVecFwd)) < alertAngle && (Vector3.Distance(startVec, target.transform.position) <= alertDistance))
                {
                    //Debug.Log("close");
                    return 2;
                }
                //Debug.Log("Can see player");
                return 1;
            }
            
            else
            {
                //Debug.Log("Can not see player");
                return 0;
            }
        }
        return 0;
    }
}
