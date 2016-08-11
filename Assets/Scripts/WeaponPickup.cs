using UnityEngine;
using System.Collections;

public class WeaponPickup : MonoBehaviour {
    public Transform attachlocation;

    private GameObject player;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    //reciever
    private void activate(float signal)
    {

        //this.transform.SetParent(player.transform.GetChild(2).transform, false);
        //transform.position = player.transform.GetChild(2).transform.position;
        this.transform.SetParent(attachlocation, false);
        transform.position = attachlocation.position;
        //transform.rotation = new Quaternion(0,0,0,0);
        //transform.LookAt(Camera.main.transform);
        player.GetComponent<PlayerControl>().weapons.Add(this.gameObject);
        player.SendMessage("pickup", 1);
    }
}
