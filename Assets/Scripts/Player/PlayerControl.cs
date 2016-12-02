using UnityEngine;
using System.Collections.Generic;
using MovementEffects;

public class PlayerControl : MonoBehaviour, Entity {
    public float walkspeed = 10.0f;
    public float sprintspeed = 13.33f;
    public float jump = 5.0f;
    public float gravity = 25.0f;
    public float interactdist = 5.0f;

    public HealthSystem health;
    public List<GameObject> weapons;
    public GameObject shield;

    public Animator animator;
    public int walkHash = Animator.StringToHash("Run");

    private float speed;
    private Vector3 moveDir = Vector3.zero; //not moving anywhere
    private CharacterController control;
    public Camera camera;
    private GameObject currentWeapon;
    private RaycastHit interactloc;
    private Ray playersight;

    public float vertMovement = -1f;
    public enum State
    {
        Idle, //Doing nothing
        Moving, //Moving around
        Attacking, //attacking
        Blocking, //shield is active
        Dead, //no health
        Conversing, //Talking with an NPC
    }
    public State state;

    // Use this for initialization
    void Start () {
        /**
        if (weapons[1] != null)
        {
            weapons[1].SetActive(false);
        }**/
        //currentWeapon = weapons[0];
        //For now, we start at max hp
        //animator = GetComponent<Animator>();
        control = GetComponent<CharacterController>();
        camera = GetComponentInChildren<Camera>();
        
    }
	
	// Update is called once per frame
	void Update () {
        if (state != State.Dead)
        {
            moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            //animator.SetFloat("Input X", moveDir.x);
            //animator.SetFloat("Input Z", moveDir.z);
            Movement(moveDir);
            LookAt();
            if (weapons.Count >= 1)
            {
                if (weapons[0] != null)
                {
                    Attack();
                    if (weapons.Count >= 2)
                    {
                        if (weapons[1] != null)
                        {
                            WeaponSwap();
                        }
                    }
                }
            }
        }
	}

    public void Movement(Vector3 direction)
    {
        direction = camera.transform.TransformDirection(direction);
        direction *= speed;
        direction.y = 0f;  //this disables freefly
        if (direction != new Vector3(0,0,0))
        {
            //animator.SetBool("Moving", true);
            //animator.SetBool("Running", true);
        }
        else
        {
            //animator.SetBool("Moving", false);
            //animator.SetBool("Running", false);
        }
        /**if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = sprintspeed;
        }
        else
        {
            speed = walkspeed;
        }**/
        if (state == State.Blocking)
        {
            speed = walkspeed / 3;
        }
        else {
            speed = walkspeed;
        }
        if (Input.GetButton("Jump") && control.isGrounded) 
        {
            vertMovement = jump;
            //direction.y = jump;
            //control.Move(direction * Time.deltaTime);
        }
        //this prevents a gravity build up
        if (vertMovement > -gravity)
        {
            vertMovement -= gravity * Time.deltaTime;
        }
        //moving up with a force of X constantly acted on by a force of Y over time, Y force is greater so it will eventually force X to 0
        direction.y = vertMovement;
        control.Move(direction * Time.deltaTime);
    }

    public void LookAt()
    {
        playersight = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(playersight, out interactloc, interactdist))
        {
            //interactloc.collider.gameObject.SendMessage("activate", 1.0f, SendMessageOptions.DontRequireReceiver);
            //Debug.Log(interactloc.distance);
            if (Input.GetKeyDown(KeyCode.E))
            {
                interactloc.collider.gameObject.SendMessage("activate", 1.0f, SendMessageOptions.DontRequireReceiver);

            }
        }
    }

    public void Attack()
    {
        //Mouse reference Left - 0 : Middle - 2 : Right - 1
        if (Time.timeScale > 0)//Game is active Might have to be changed for main menu? most likely not
        {
            if (Input.GetMouseButton(0))
            {
                state = State.Attacking;
                //animator.SetTrigger("Attack1Trigger");
                Timing.RunCoroutine(currentWeapon.GetComponent<Weapon>().fire());
            }
            else if (Input.GetMouseButton(1))
            {
                state = State.Blocking;
            }
            else
            {
                state = State.Idle;
            }
        }
        
    }

    public void Die()
    {
        state = State.Dead;
    }

    public bool getBlock()
    {
        if(state == State.Blocking && health.shieldHP > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void pickup(int signal)
    {
        //This section will definitely need some kind of expanding later
        //I can't predict the future, and this does the job for now
        if(weapons.Count == 1)
        {
            currentWeapon = weapons[0];
        }
        if(weapons.Count == 2)
        {
            weapons[1].SetActive(false);
        }
    }

    public void WeaponSwap()
    {
        if(Time.timeScale > 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if (currentWeapon == weapons[0])
                {
                    weapons[0].SetActive(false);
                    currentWeapon = weapons[1];
                    weapons[1].SetActive(true);
                }
                else
                {
                    weapons[1].SetActive(false);
                    currentWeapon = weapons[0];
                    weapons[0].SetActive(true);
                }
            }
        }
    }

    public GameObject getShield()
    {
        return shield;
    }

    public float getHealth()
    {
        return health.curHP;
    }

}
