using UnityEngine;
using System.Collections;

public interface Entity
{
    //Defining Things that can move, attack, die.
    void Attack();
    void Movement(Vector3 direction);
    bool getBlock();
    GameObject getShield();
}