using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerControl : MonoBehaviour {
    public float walkspeed = 10.0f;
    public float sprintspeed = 13.33f;
    public float jump = 5.0f;
    public float gravity = 25.0f;

    public HealthSystem health;
    public List<GameObject> weapons;
    public GameObject shield;

    private float speed;
    private Vector3 moveDir = Vector3.zero; //not moving anywhere
    private CharacterController control;
    private Camera camera;
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
        control = GetComponent<CharacterController>();
        camera = GetComponentInChildren<Camera>();
        
    }
	
	// Update is called once per frame
	void Update () {
        if (health.curHP > 0)
        {
            Movement();
            Attack();
            WeaponSwap();
        }
        else
        {
            state = State.Dead;
        }
	}

    public void Movement()
    {
        
        moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveDir = camera.transform.TransformDirection(moveDir);
        moveDir *= speed;
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
        moveDir.y = 0f;  //this disables freefly
        moveDir.y -= gravity * Time.deltaTime;
        control.Move(moveDir * Time.deltaTime);
    }

    public void Attack()
    {
        //Mouse reference Left - 0 : Middle - 2 : Right - 1
        if (Time.timeScale > 0)//Game is active Might have to be changed for main menu? most likely not
        {
            if (Input.GetMouseButton(0))
            {
                state = State.Attacking;
                StartCoroutine(currentWeapon.GetComponent<Weapon>().fire());
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
    public bool getBlock()
    {
        if(state == State.Blocking)
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
    
}
