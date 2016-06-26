using UnityEngine;
using System.Collections;

public class InventoryStation : MonoBehaviour {
    private GameObject player;
	// Use this for initialization
	void Start () {
	player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame

    public void OnCollisionEnter(Collision c)
    {
        Debug.Log("touching station");
        if (c.gameObject.tag == "InventoryStation" && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Opening inventory");
            //inven.InvActive = true;
        }
        else
        {
            //inven.InvActive = false;
        }
    }
}
