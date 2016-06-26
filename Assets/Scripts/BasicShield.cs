using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using MovementEffects;
/** Do not ask why I can't use MEC Coroutines, for whatever reason the magical solution doesn't apply here
    and it won't force stop coroutines like the way it does now. Luckily, 
    we shouldn't have an inordinate amount of shield regenerating on the field so hopefully it isn't a huge performance hit
    **/

public class BasicShield : MonoBehaviour, Shield {
    public float shieldhealth; //how much health the shield has
    public float shieldHPMAX;
    public float shieldchargeval; //how fast it recovers per second
    public float chargeinterval;
    public float shieldchargedelay; //delay on an unbroken shield
    public float shatterchargedelay; //delay on a broken shield
    private float timestamp;

    void Start()
    {
        shieldHPMAX = shieldhealth;
    }
    void Update()
    {
        if(shieldhealth < 0)
        {
            shieldhealth = 0;
        }
    }
    public void block(float damage)
    {
        StopCoroutine("recharge");
        StopCoroutine("regCharge");
        StopCoroutine("shatterCharge");
        if (shieldhealth  - damage > 0)
        {
            shieldhealth -= damage;
            //play a sound?
            StartCoroutine("regCharge");
        }
        else if (shieldhealth - damage == 0 || shieldhealth - damage < 0) //don't ask me why i can't just use <= 
        {
            shieldhealth -= damage;
            Debug.Log("BROKEN");
            //play a sound(s)?
            StartCoroutine("shatterCharge");
            shieldbreak();
           
        }
    }

    public bool shieldbreak()
    {
        while (shieldhealth <= 0)
        {
            return true;
        }
        return false;
    }

    public IEnumerator regCharge()
    {
        yield return new WaitForSeconds(shieldchargedelay); //wait a bit
        StartCoroutine("recharge");
        
    }

    public IEnumerator shatterCharge()
    {
        yield return new WaitForSeconds(shatterchargedelay); //wait a bit
        StartCoroutine("recharge");
    }

    public IEnumerator recharge()
    {
        while (shieldhealth < shieldHPMAX)
        {
            shieldhealth += shieldchargeval;
            yield return new WaitForSeconds(chargeinterval);
        }
    }

    public float getshieldHP()
    {
        return shieldhealth;
    }

    public float getshieldCharge()
    {
        return shieldchargedelay;
    }

    public float getshatterCharge()
    {
        return shatterchargedelay;
    }

    public float getshieldMAX()
    {
        return shieldHPMAX;
    }
}
