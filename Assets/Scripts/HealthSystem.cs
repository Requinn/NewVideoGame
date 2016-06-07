using UnityEngine;
using System.Collections;

public class HealthSystem : MonoBehaviour {
    public float maxHP = 100.0f;
    public float curHP;
    public float shieldHP;
    public float shieldMAX;
    private GameObject shield;
    public UIHealth UIhealth;
    // Use this for initialization
    void Start () {
        curHP = maxHP;
        shield = GetComponentInParent<Entity>().getShield();
        if (shield != null)
        {
            shieldMAX = shield.GetComponent<Shield>().getshieldMAX();
        }
        //shieldHP = shield.GetComponent<Shield>().getshieldHP();
        //shieldRecharge = shield.GetComponent<Shield>().getshieldCharge();
        //shieldBrokecharge = shield.GetComponent<Shield>().getshatterCharge();
    }
    
    public void resetShield()
    {
        shield = GetComponentInParent<Entity>().getShield();
        shieldMAX = shield.GetComponent<Shield>().getshieldMAX();
    }

    void Update()
    {
        if (shield != null)
        {
            shieldHP = shield.GetComponent<Shield>().getshieldHP();
            UIhealth.SetShieldBar(shieldHP / shieldMAX);
        }
    }

    public void takeDamage(float damage)
    {
        if (shieldHP > 0 && GetComponentInParent<Entity>().getBlock()) //shield isn't broken and player is blocking
        {
            takeShieldDmg(damage);
        }
        else if (curHP > 0) //just to be sure
        {
            takeHealthDmg(damage);
        }
        UIhealth.SetHealthBar(curHP / maxHP);
    }
    // I separated these two functions just in case I want to call them directly. I.e. damage only shields or health, and not be conditional on block.
    public void takeShieldDmg(float damage)
    {
        shield.GetComponent<Shield>().block(damage);
    }

    public void takeHealthDmg(float damage)
    {
        curHP -= damage;
        if (curHP < 0)
        {
            curHP = 0;
        }
    }

    public void addHP(float heal, bool overheal)
    {
        if (curHP < maxHP)
        {
            if (curHP + heal > maxHP && !overheal)
            {
                curHP = curHP + (heal - ((curHP + heal) - maxHP));
            }
            else
            {
                curHP += heal;
            }
        }
        UIhealth.SetHealthBar(curHP / maxHP);
    }


}
