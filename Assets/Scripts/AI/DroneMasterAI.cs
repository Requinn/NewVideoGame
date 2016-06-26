using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DroneMasterAI : MonoBehaviour {
    public Vision vision;
    public GameObject enemy;
    public GameObject drone;
    public float fleedist;
    public float rotationSpeed;

    public List<GameObject> droneGroup;
    public int dronect = 0;

    private bool dronesActive = false;
    private Vector3 home;
    private Quaternion homerot;
    //patrolA and B will be invisivle objects in the world, and we will be grabbing the transform.position off of them
    //they will not be dynamic and will be assigned per level

    private Entity enemyscript;
    private NavMeshAgent nav;
    private NavMeshPath path;
    private GameObject player;
    public int prevAggro;
    // Use this for initialization
    void Start () {
        enemyscript = enemy.GetComponent(typeof(Entity)) as Entity;
        player = GameObject.FindGameObjectWithTag("Player");
        nav = GetComponent<NavMeshAgent>();
        //we don't have a patrol path, so our home is where we spawn
        home = GetComponent<Transform>().position;
        homerot = GetComponent<Transform>().rotation;
    }
	
	// Update is called once per frame
	void Update () {
        if (dronesActive)
        {
            checkDrones();
        }
        //We can see the player
        if(vision.alertness == 0)
        {
            if(prevAggro == 1)
            {
                //Lookat here
            }
            //prevAggro = 0;
        }
	    if(vision.alertness >= 1)
        {
            //all the drones are dead
            if (droneGroup.Count == 0)
            {
                droneGroup.Add(Instantiate(drone, transform.position + new Vector3(2f, 0f, 0f), GetComponentInParent<Transform>().rotation) as GameObject);
                droneGroup.Add(Instantiate(drone, transform.position + new Vector3(0f, 0, 2f), GetComponentInParent<Transform>().rotation) as GameObject);
                droneGroup.Add(Instantiate(drone, transform.position + new Vector3(0f, 0, -2f), GetComponentInParent<Transform>().rotation) as GameObject);
                droneGroup.Add(Instantiate(drone, transform.position + new Vector3(-2f, 0, 0f), GetComponentInParent<Transform>().rotation) as GameObject);
                dronesActive = true;
            }
            //if we have drones ane the player is still seen
            else if(vision.alertness == 1) {
                //since we aren't moving, we can look at and shoot the player
                if (nav.velocity == new Vector3(0,0,0))
                {
                    enemyscript.LookAt();
                    enemyscript.Attack();
                }
                prevAggro = 1;
            }
            //player is too close, so we run away
            if (vision.alertness == 2)
            {
                Vector3 rayFromPlayer = transform.position - player.transform.position;
                float distance = rayFromPlayer.magnitude;
                Vector3 direction = rayFromPlayer / distance;
                //Debug.Log(rayFromPlayer);
                nav.SetDestination(transform.position + (direction * fleedist));
                prevAggro = 2;
            }
        }
        
	}
    void checkDrones()
    {
        Debug.Log(dronect);
        if(dronect > droneGroup.Count - 1)
        {
            dronect = 0;
        }
        if(droneGroup[dronect] == null)
        {
            droneGroup.RemoveAt(dronect);
        }
        if(droneGroup.Count == 0)
        {
            dronesActive = false;
        }
        dronect++;

    }
}
