using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New weapon configuration", menuName = "WeaponSystem / New weapon configuration")]
public class WeaponConfigurationSO : ScriptableObject
{
    public string weaponName;
    public GameObject bulletPrefab;
    public float shootingCooldown;
    public float bulletForce;
}
