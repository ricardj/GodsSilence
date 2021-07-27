using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponContainerMinigun : WeaponContainer
{
    public Transform[] extraSpawners;

    int spawningIndex = 0;
    protected override void SpawnBullet()
    {
        Transform currentSpawningPosition = spawningPosition;
        if (spawningIndex == 0)
            currentSpawningPosition = spawningPosition;
        else
            currentSpawningPosition = extraSpawners[spawningIndex-1];


        GameObject newBullet = Instantiate(weaponConfiguration.bulletPrefab, currentSpawningPosition.position, currentSpawningPosition.rotation, null);
        Vector3 totalBulletForce = newBullet.transform.up * weaponConfiguration.bulletForce;
        newBullet.GetComponent<Rigidbody2D>().AddForce(totalBulletForce, ForceMode2D.Impulse);

        spawningIndex++;
        if (spawningIndex >= extraSpawners.Length + 1)
            spawningIndex = 0;
    }
}
