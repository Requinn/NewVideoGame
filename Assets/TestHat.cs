using UnityEngine;
using System.Collections;

public class TestHat : MonoBehaviour {
    /**This is test code.**/

    public Transform attachlocation;

    private GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void activate(float signal)
    {
        //this.transform.SetParent(player.transform.GetChild(2).transform, false);
        //transform.position = player.transform.GetChild(2).transform.position;
        this.transform.SetParent(attachlocation, false);
        transform.position = attachlocation.position;
    }
}
