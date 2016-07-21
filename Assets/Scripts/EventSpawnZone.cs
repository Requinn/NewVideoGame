using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventSpawnZone : MonoBehaviour {
    public List<GameObject> spawnlist;
    private bool cantrigger = true;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player" && cantrigger)
        {
            for(int i = 0; i < spawnlist.Count; i++)
            {
                spawnlist[i].SetActive(true);
                spawnlist[i].GetComponent<QuickSpawner>().Spawn(); 
            }
            cantrigger = false;
            Destroy(this.gameObject);
        }
    }
}
