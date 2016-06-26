using UnityEngine;
using System.Collections;

public class Drone_SummonedAI : MonoBehaviour {
    public Vision vision;
    public GameObject enemy;
    private Entity enemyscript;
    private NavMeshAgent nav;
    private NavMeshPath path;
    private GameObject player;
    private Vector3 storedDestination;
    private State state;
    public bool attackstate = false; 

    public enum State
    {
        attacking,
        idling,
        moving,
    }

    // Use this for initialization
    void Awake () {
        enemyscript = enemy.GetComponent(typeof(Entity)) as Entity;
        player = GameObject.FindGameObjectWithTag("Player");
        nav = GetComponent<NavMeshAgent>();
        nav.autoBraking = true;
        state = State.idling;
    }
	
	// Update is called once per frame
	void Update () {
        if(vision.alertness == 1 || vision.alertness == 0)
        {
            state = State.moving;
            nav.SetDestination(vision.personalLastSighting);
        }
        if(vision.alertness == 2)
        {
            state = State.attacking;
            if (attackstate == false)
            {
                enemyscript.LookAt();
            }
            enemyscript.Attack();
            
        }
	}
}
