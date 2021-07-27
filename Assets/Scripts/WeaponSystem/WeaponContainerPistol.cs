using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponContainerPistol : WeaponContainer
{

    bool shootLocker;

    protected new void Update()
    {
        if (shooting)
        {
            if (shootingCounter > weaponConfiguration.shootingCooldown)
            {
                shootingCounter = 0;
                SpawnBullet();

                //We just shoot one bullet
                shooting = false;
            }
        }
        shootingCounter += Time.deltaTime;
    }
    public override void SetWeaponShooting(bool shooting)
    {
        if(shooting && !this.shooting && !shootLocker )
        {
            shootLocker = true;
            this.shooting = true;
        }


        if (!shooting && shootLocker)
        {
            shootLocker = false;
            this.shooting = false;
        }

    }
}
