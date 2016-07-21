using UnityEngine;
using System.Collections.Generic;
using MovementEffects;

public class PlayerControl : MonoBehaviour, Entity {
    public float walkspeed = 10.0f;
    public float sprintspeed = 13.33f;
    public float jump = 5.0f;
    public float gravity = 25.0f;

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
        weapons[1].SetActive(false);
        currentWeapon = weapons[0];
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
            Attack();
            WeaponSwap();
        }
	}

    public void Movement(Vector3 direction)
    {
        direction = camera.transform.TransformDirection(direction);
        direction *= speed;
        if(direction != new Vector3(0,0,0))
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
        /**if (Input.GetButton("Jump") && control.isGrounded) 
        {
            moveDir.y = jump;
            control.Move(moveDir * Time.deltaTime);
        }
        else
        {
            
        }**/
        direction.y = 0f;  //this disables freefly
        direction.y -= gravity * Time.deltaTime;
        control.Move(direction * Time.deltaTime);
    }

    public void LookAt()
    {
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


    public void WeaponSwap()
    {
        if(Time.timeScale > 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
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
