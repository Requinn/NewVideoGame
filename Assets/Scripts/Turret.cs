using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {
    public Vision vision;
    public string target;
    public Rigidbody projectile;
    public float fireRate;

    private bool canFire = true;
    private GameObject player;
    private Vector3 horizontalLook;


    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        if (gameObject.tag.Equals("Enemy"))
        {
            target = "Player";
        }
    }

    // Update is called once per frame
    void Update() {
        //In range and aggro'd
        if (vision.alertness >= 1)
        {
            horizontalLook = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
            transform.LookAt(horizontalLook);
            if (vision.alertness == 2 && canFire)
            {
                StartCoroutine(fire());
            }
        }
    }
    IEnumerator fire()
    {
        canFire = false;
        projectile.GetComponent<Projectile>().setTarget("Player");
        Rigidbody instantProjectile = Instantiate(projectile, transform.position, GetComponentInParent<Transform>().rotation) as Rigidbody;
        instantProjectile.velocity = transform.TransformDirection(new Vector3(0, 0, projectile.GetComponent<Projectile>().getVelocity()));
        //Extra shit like sounds and muzzle flash or whatever goes here
        yield return new WaitForSeconds(1 / fireRate); //reciprocal to get rounds per second.
        canFire = true;
    }
}
