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
        shield = GetComponentInParent<PlayerControl>().shield;
        shieldMAX = shield.GetComponent<Shield>().getshieldMAX();
        //shieldHP = shield.GetComponent<Shield>().getshieldHP();
        //shieldRecharge = shield.GetComponent<Shield>().getshieldCharge();
        //shieldBrokecharge = shield.GetComponent<Shield>().getshatterCharge();
    }
    
    void Update()
    {
        shieldHP = shield.GetComponent<Shield>().getshieldHP();
        UIhealth.SetShieldBar(shieldHP / shieldMAX);
    }

    public void takeDamage(float damage)
    {
        if (!shield.GetComponent<Shield>().shieldbreak() && GetComponentInParent<PlayerControl>().getBlock()) //shield isn't broken and player is blocking
        {
            shield.GetComponent<Shield>().block(damage);
            
        }
        else { 
            if (curHP > 0) //just to be sure
            {
                curHP -= damage;
                if (curHP < 0)
                {
                    curHP = 0;
                }
            }
            UIhealth.SetHealthBar(curHP/maxHP);

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
