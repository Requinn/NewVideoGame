using UnityEngine;
using System.Collections;

public class EnemyControl : MonoBehaviour {
    public Vision vision;
    public float moveSpeed = 2.0f;
    public float gravity = 50.0f;
    public float maxDist, minDist;
    public float healthMax = 100.0f;
    public float curHealth;
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
        curHealth = healthMax; //fullhealth for now
        control = GetComponent<CharacterController>();
        //playerHealth = player.GetComponent<PlayerHealth>();
        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update () {
        //Alive
        if (curHealth > 0)
        {
            if (vision.alertness >= 1)
            {
                horizontalLook = new Vector3(player.transform.position.x, this.transform.position.y, player.transform.position.z);
                transform.LookAt(horizontalLook);
                if (vision.alertness == 2)
                {
                    moveDir = transform.TransformDirection(Vector3.forward) * moveSpeed;
                    //transform.position += transform.forward * moveSpeed * Time.deltaTime;
                    if (Vector3.Distance(transform.position, player.transform.position) >= minDist)
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
                    }

                }

            }
            else
            {
                moveDir = vision.personalLastSighting * moveSpeed;
            }
        }
        else
        {
            //Creature is dead.
        }
    }

    public void takeDamage(float damage)
    {
        curHealth -= damage;
    }

}
