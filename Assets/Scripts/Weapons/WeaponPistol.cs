using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPistol : Weapon
{

    bool shootLocker;

    protected new void Update()
    {
        if (shooting)
        {
            if (shootingCounter > shootingCooldown)
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
