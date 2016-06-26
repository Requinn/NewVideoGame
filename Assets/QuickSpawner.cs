using UnityEngine;
using System.Collections;

public class QuickSpawner : MonoBehaviour {
    public GameObject Enemytospawn;
	// Use this for initialization

	void Start () {
        this.gameObject.SetActive(false);
	}

    public void Spawn()
    {
        //maybe do some fancy effects
        //maybe have a timer
        Instantiate(Enemytospawn, this.transform.position,this.transform.rotation);
        Destroy(this.gameObject);
    }
	
}
