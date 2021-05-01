using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    UNARMED = 0,
    MACHINE_GUN = 1,
    MINIGUN = 2,
    PISTOL = 3,
    DUAL_PISTOL = 4,
    LASER1 = 5,
    LASER2 = 6,
    TOTAL_STATES = 7
}

public class WeaponInventory : MonoBehaviour
{
    Weapon currentWeapon;
    
    public Weapon[] weapons;
    
    

    public Weapon GetWeapon(WeaponType weaponType)
    {
        return currentWeapon = weaponType - 1 >= 0 ? weapons[(int)weaponType - 1] : null; 
    }


}
