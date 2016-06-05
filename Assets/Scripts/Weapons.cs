using UnityEngine;
using System.Collections;

public interface Weapon
{
    IEnumerator fire(); //how fast between attack intervals
    IEnumerator reload(); //on a gun, reloading ammo. On a sword, "rest" between combos/
}
