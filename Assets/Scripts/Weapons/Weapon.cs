using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float shootingCooldown;
    public Transform spawningPosition;
    public float bulletForce;

    [SerializeField]
    protected bool shooting;

    protected float shootingCounter;

    protected void Update()
    {
        if(shooting)
        {
            if(shootingCounter > shootingCooldown)
            {
                shootingCounter = 0;
                SpawnBullet();
            }   
        }
        shootingCounter += Time.deltaTime;
    }

    protected virtual void SpawnBullet()
    {
        
        GameObject newBullet = Instantiate(bulletPrefab, spawningPosition.transform.position, spawningPosition.rotation, null);
        Vector3 totalBulletForce = newBullet.transform.up * bulletForce;
        
        newBullet.GetComponent<Rigidbody2D>().AddForce(totalBulletForce, ForceMode2D.Impulse);
    }

    public virtual void SetWeaponShooting(bool shooting)
    {
        this.shooting = shooting;
    }



}
