using UnityEngine;
using System.Collections.Generic;
using MovementEffects;
public class ChargeWeapon : MonoBehaviour, Weapon
{
    public float reloadTime = 2;
    public float chargeforce;
    public float chargeduration;
    public float damage;
    private bool canFire = true;
    public Collider col;

    private State state;
    private NavMeshAgent navagent;
    private Rigidbody body;
    private bool charging = false;
    private Vector3 moveDir;
    private GameObject attachedentity;

    public enum State
    {
        seeking,
        charging,
        stopping,
    }

    void Start()
    {
        navagent = GetComponentInParent<NavMeshAgent>();
        body = GetComponentInParent<Rigidbody>();
        attachedentity = this.transform.parent.gameObject;
        col.enabled = false;
    }

    public IEnumerator<float> fire()
    {
        if (canFire)
        {
            state = State.charging;
            canFire = false;
            Debug.Log("Charging");
            //enable the hitbox
            col.enabled = true;

            //stop all navmesh related movement + rotations, disable kinematics
            navagent.Stop();
            body.isKinematic = false;
            GetComponentInParent<Drone_SummonedAI>().attackstate = true;

            //instead of forward, personal last sighting?
            body.AddRelativeForce(Vector3.forward * chargeforce, ForceMode.Impulse);
            yield return Timing.WaitForSeconds(chargeduration);

            //done chargin, reenable everything
            state = State.stopping;
            if (attachedentity != null) //incase we die mid charge
            {
                GetComponentInParent<Drone_SummonedAI>().attackstate = false;
                body.isKinematic = true;
                col.enabled = false;
                navagent.transform.position = this.transform.position;
                navagent.ResetPath();
                navagent.Resume();
                
            }

            //Not a navmesh? Use character controller so it doesn't clip int shit with rigidbodies 
            Timing.RunCoroutine(reload());
        }
        yield return 0f;
    }

    public IEnumerator<float> reload()
    {
        //canFire = false;       
        yield return Timing.WaitForSeconds(reloadTime);
        canFire = true;
    }

    public void OnTriggerEnter(Collider c)
    {
        if(c.gameObject.tag == "Player")
        {
            c.gameObject.GetComponentInChildren<HealthSystem>().takeDamage(damage);
        }
    }
}
