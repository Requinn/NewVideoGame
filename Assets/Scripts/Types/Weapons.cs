
using System.Collections.Generic;
using MovementEffects;

public interface Weapon
{
    IEnumerator<float> fire(); //how fast between attack intervals
    IEnumerator<float> reload(); //on a gun, reloading ammo. On a sword, "rest" between combos/
}
