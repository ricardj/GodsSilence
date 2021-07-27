using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New inventory", menuName = "WeaponSystem / New inventory")]
public class WeaponInventorySO : MonoBehaviour
{
    public List<WeaponConfigurationSO> weaponConfigurations;

    public WeaponConfigurationSO GetWeaponByIndex(int index)
    {
        return weaponConfigurations[index];
    }
}
