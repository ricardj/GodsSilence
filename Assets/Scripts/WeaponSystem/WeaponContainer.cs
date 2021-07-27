using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponContainer : MonoBehaviour
{

    public Transform spawningPosition;
    public WeaponConfigurationSO weaponConfiguration;

    [SerializeField]
    protected bool shooting;

    protected float shootingCounter;

    protected void Update()
    {
        if(shooting)
        {
            if(shootingCounter > weaponConfiguration.shootingCooldown)
            {
                shootingCounter = 0;
                SpawnBullet();
            }   
        }
        shootingCounter += Time.deltaTime;
    }

    protected virtual void SpawnBullet()
    {
        
        GameObject newBullet = Instantiate(weaponConfiguration.bulletPrefab, spawningPosition.transform.position, spawningPosition.rotation, null);
        Vector3 totalBulletForce = newBullet.transform.up * weaponConfiguration.bulletForce;
        
        newBullet.GetComponent<Rigidbody2D>().AddForce(totalBulletForce, ForceMode2D.Impulse);
    }

    public virtual void SetWeaponShooting(bool shooting)
    {
        this.shooting = shooting;
    }



}
