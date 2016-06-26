using UnityEngine;
using System.Collections.Generic;
using MovementEffects;

public class EnemyControl : MonoBehaviour, Entity {
    public Vision vision;
    public float moveSpeed = 2.0f; 
    public float gravity = 50.0f;  //uneccessary?
    public float maxDist, minDist;
    public float curHealth;
    public GameObject weapon;
    public HealthSystem healthsys;
    public GameObject shield;

    private NavMeshAgent nav;
    private GameObject player;
    private Vector3 moveDir;
    private CharacterController control;
    private Vector3 horizontalLook;

    private enum states
    {
        Idle, //Doing nothing
        Init, //Starting up

    }
    // Use this for initialization
    void Start () {
        control = GetComponent<CharacterController>();
        //playerHealth = player.GetComponent<PlayerHealth>();
        player = GameObject.FindGameObjectWithTag("Player");
        nav = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update () {
        //Alive
        /*
        if (healthsys.curHP > 0)
        {
            if (vision.alertness >= 1)
            {
                LookAt();
                if (vision.alertness == 2)
                {
                    //nav.SetDestination(vision.personalLastSighting);
                    //Movement(Vector3.forward);
                    Attack();
                }
            }
            else
            {
                moveDir = vision.personalLastSighting * moveSpeed;
            }
        }
        else
        {
            Debug.Log("DEAD");
        }*/
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }

    public void LookAt()
    {
        horizontalLook = new Vector3(player.transform.position.x, this.transform.position.y, player.transform.position.z);
        //This vision is snapping, look for a way to make this smooth
        transform.LookAt(horizontalLook);
    }

    public void Movement(Vector3 direction)
    {   
        moveDir = transform.TransformDirection(Vector3.forward) * moveSpeed;
        nav.SetDestination(direction);
        //transform.position += transform.forward * moveSpeed * Time.deltaTime;
        /*if (Vector3.Distance(transform.position, player.transform.position) >= minDist)
        {
            //moveDir.y = 0f;  //this disables freefly
            moveDir.y -= gravity * Time.deltaTime;
            //Debug.Log(moveDir);
            control.Move(moveDir * Time.deltaTime);
        }
        //player is very close
        if (Vector3.Distance(transform.position, player.transform.position) <= maxDist)
        {
            moveDir.y -= gravity * Time.deltaTime;
            moveDir.x = 0;
            moveDir.z = 0;
            control.Move(moveDir * Time.deltaTime);
        }*/
    }

    public void Attack()
    {
        if (Time.timeScale > 0)//Game is active Might have to be changed for main menu? most likely not
        {
            //Debug.Log("Cleared to shoot.");
            //state = State.Attacking;
            Timing.RunCoroutine(weapon.GetComponent<Weapon>().fire());
            /**else if (Input.GetMouseButton(1))
            {
                //state = State.Blocking;
            }
            else
            {
                //state = State.Idle;
            }**/
        }
    }

    public bool getBlock()
    {
        return true;
    }

    public GameObject getShield()
    {
        if (shield != null)
        {
            return shield;
        }
        else
        {
            return null;
        }
    }
}
