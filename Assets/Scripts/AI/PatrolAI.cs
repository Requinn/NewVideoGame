using UnityEngine;
using System.Collections;

public class PatrolAI : MonoBehaviour
{
    public Vision vision;
    public HealthSystem healthsys;
    public GameObject enemy;
    public float curHealth;
    public Transform[] patrolpoints;

    //patrolA and B will be invisivle objects in the world, and we will be grabbing the transform.position off of them
    //they will not be dynamic and will be assigned per level

    private Entity enemyscript;
    private NavMeshAgent nav;
    private NavMeshPath path;
    private GameObject player;
    private int destPoint = 0;


    private enum State
    {
        idle, patrol, attack, chase, returning,
    }
    // Use this for initialization
    void Start()
    {
        enemyscript = enemy.GetComponent(typeof(Entity)) as Entity;
        player = GameObject.FindGameObjectWithTag("Player");
        nav = GetComponent<NavMeshAgent>();
        //patrol from the get - go because we got places to visit 
        nav.autoBraking = false;
        patrol();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //we don't have a home position and we don't have any aggro, so we patrol
        if (vision.alertness == 0)
        {
            if (nav.remainingDistance < 0.5f)
            {
                patrol();
                
            }

        }
        if (vision.alertness <= 1)
        {
            nav.Resume();
        }
        if (vision.alertness >= 1)//within chase distance
        {
            
            Debug.Log("I should chase!");
            enemyscript.Movement(vision.personalLastSighting);
            if (vision.alertness >= 2)//within attack distance
            {
                nav.Stop();
                enemyscript.LookAt();
                enemyscript.Attack();
            }
        }
    }

    void patrol()
    {
        Debug.Log("PATROL");
        nav.destination = patrolpoints[destPoint].position;
        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % patrolpoints.Length;
    }
}