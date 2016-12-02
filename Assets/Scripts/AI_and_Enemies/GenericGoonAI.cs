using UnityEngine;
using System.Collections;

public class GenericGoonAI : MonoBehaviour {
    public Vision vision;
    public HealthSystem healthsys;
    public GameObject enemy;
    public float curHealth;
    
    private Vector3 home;
    private Quaternion homerot;
    //patrolA and B will be invisivle objects in the world, and we will be grabbing the transform.position off of them
    //they will not be dynamic and will be assigned per level

    private Entity enemyscript;
    private NavMeshAgent nav;
    private NavMeshPath path;
    private GameObject player;
    public int prevAggro;


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
        //we don't have a patrol path, so our home is where we spawn
        home = GetComponent<Transform>().position;
        homerot = GetComponent<Transform>().rotation;
    }
	
	// Update is called once per frame
	void Update () {
        //we don't have sight anymore
        if (vision.alertness == 0)
        {
            //but we did see the player
            if(prevAggro == 1)
            {
                //but player is out of range now
                if (Vector3.Distance(nav.transform.position, player.transform.position) > vision.sightDistance)
                {
                    returnHome();
                }
            }
            //but we are locked onto the player
            if (prevAggro == 2)
            {
                nav.Resume();
                nav.destination = this.transform.position;
                float randWait = Random.Range(1.75f, 3.0f);
                wait(randWait);
                int nextMove = Random.Range(0, 10);
                // 1/11 chance to do wait
                // 8/11 chance to advance on the player
                if (nextMove > 0 && nextMove <= 8)
                {
                    Debug.Log("Advancing on player");
                    enemyscript.Movement(vision.personalLastSighting);
                }
                // 2/11 chance to return home
                else if (nextMove > 8 || nextMove <= 10)
                {
                    Debug.Log("retreating");
                    returnHome();
                }
                prevAggro = -1;
            }
        }
        //we have sight
        if(vision.alertness >= 1)//within chase distance
        {
            prevAggro = 0;
            //dropping our aggro back to alerted
            if (vision.alertness == 1)
            {
                nav.Resume();
            }
            Debug.Log("I should chase!");
            enemyscript.Movement(vision.personalLastSighting);
            prevAggro = vision.alertness;
            if (vision.alertness >= 2)//within attack distance
            {
                nav.Stop();
                enemyscript.LookAt();
                enemyscript.Attack();
                prevAggro = 2;
            }
        }
	}

    void returnHome()
    {
        Debug.Log("RETURNING");
        nav.destination = home;
    }

    IEnumerator wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
