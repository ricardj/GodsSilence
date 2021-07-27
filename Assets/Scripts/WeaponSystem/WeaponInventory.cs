using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class WeaponInventory : MonoBehaviour
{
    WeaponContainer currentWeapon;
    
    public WeaponContainer[] weapons;
    
    

    public WeaponContainer GetWeapon(int weaponIndex)
    {
        return currentWeapon = weaponIndex - 1 >= 0 ? weapons[(int)weaponIndex - 1] : null; 
    }


}
