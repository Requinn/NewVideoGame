using UnityEngine;
using System.Collections.Generic;
using MovementEffects;

public class Drone_SummonedAI : MonoBehaviour {
    public Vision vision;
    public GameObject enemy;
    private Entity enemyscript;
    private NavMeshAgent nav;
    private NavMeshPath path;
    private GameObject player;
    private Vector3 storedDestination;
    private State state;
    private float sleeptime;
    public bool attackstate = false;
    private bool waiting = true;
    private bool canAttack = true;
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
        vision.personalLastSighting = transform.position;
        state = State.idling;
        //Timing.RunCoroutine(wait(3f));
    }
	
	// Update is called once per frame
	void Update () {
        if (waiting)
        {
            Timing.RunCoroutine(wait(0f, 5f));
        }
        else {
            if (vision.alertness == 1 || vision.alertness == 0)
            {
                state = State.moving;
                nav.SetDestination(vision.personalLastSighting);
            }
            if (vision.alertness == 2)
            {
                state = State.attacking;
                if (attackstate == false)
                {
                    enemyscript.LookAt();
                }
                if (canAttack)
                {
                    canAttack = false;
                    Timing.RunCoroutine(wait(5f, 8f));
                    enemyscript.Attack();
                    canAttack = true;
                }

            }
        }
	}

    IEnumerator<float> wait(float start, float end)
    {
        sleeptime = Random.Range(start, end);
        yield return Timing.WaitForSeconds(sleeptime);
        waiting = false;
    }
}
