using UnityEngine;
using System.Collections;

public class FacePlayer : MonoBehaviour
{
    private GameObject player;
    private Vector3 lookAtPos;

    //find the player
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    //find player's pos and face it
    void Update()
    {
        lookAtPos = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        transform.LookAt(lookAtPos);
    }
}
