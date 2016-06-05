using UnityEngine;
using System.Collections;

public interface Shield
{
    void block(float damage);
    IEnumerator regCharge();
    IEnumerator recharge();
    IEnumerator shatterCharge();

    bool shieldbreak();
    float getshieldHP();
    float getshieldCharge();
    float getshatterCharge();
    float getshieldMAX();
}
