using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]

public class Movement : MonoBehaviour {
    public float walkspeed = 10.0f;
    public float sprintspeed = 13.33f;
    public float jump = 5.0f;
    public float gravity = 25.0f;
    private float speed;
    private Vector3 moveDir = Vector3.zero; //not moving anywhere
    private CharacterController control;
	
	// Update is called once per frame
	void Update () {
        control = GetComponent<CharacterController>();
        moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveDir = transform.TransformDirection(moveDir);
        moveDir *= speed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = sprintspeed;
        }
        else
        {
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
}
