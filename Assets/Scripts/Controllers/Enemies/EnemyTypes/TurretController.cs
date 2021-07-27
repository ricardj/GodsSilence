using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : EnemyController
{
    [HideInInspector]
    public Transform target;
    public float detectionRadius;
    public LayerMask targetsMask;
    public WeaponContainer turretWeapon;
    public GameObject deathParticles;
    RaycastHit[] raycastInfos;
    bool targetDetected;
    bool shooting;
    Collider2D[] detectedColliders;

    // Update is called once per frame
    void Update()
    {

        
        if(!targetDetected)
        {
            //raycastInfos = Physics.SphereCastAll(transform.position, detectionRadius, Vector3.up, 0.01f, targetsMask);
            detectedColliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius, targetsMask);
            if (detectedColliders != null)
            {
               
                for (int i = 0;i < detectedColliders.Length; i++)
                {
                   
                    targetDetected = true;
                    this.target = detectedColliders[i].transform;
                    
                    break;
                }
            }
        }

        if (targetDetected)
        {
            transform.up = target.position - transform.position;

           // transform.rotation = Quaternion.Euler(0, 0, transform.rotation.z);
            turretWeapon.SetWeaponShooting(true);
        }
        else
            turretWeapon.SetWeaponShooting(false);

        //if(targetDetected && target != null)
        //{
        //    transform.LookAt(target);
        //    Vector3 rayDirection = (target.position - transform.position).normalized;
        //    RaycastHit hit;
        //    if(Physics.Raycast(transform.position, rayDirection, out hit, detectionRadius))
        //    {
        //        if(hit.transform.gameObject != target)
        //        {
        //            TargetLost();
        //        }
        //    }else
        //    {
        //        TargetLost();
        //    }
        //}
    }

    public void TargetLost()
    {
        targetDetected = false;
        //target = null;
        shooting = false;
    }


    void OnDrawGizmosSelected()
    {
        //GameObject[] objs = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        GameObject g = gameObject;
        UnityEditor.Handles.color = Color.green;
        UnityEditor.Handles.DrawWireDisc(g.transform.position, -g.transform.forward, detectionRadius);
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            ChangeHealth(-10);
        }
    }


    public void DeathSequence()
    {
        Instantiate(deathParticles, transform.position, Quaternion.identity, null);
        Destroy(gameObject);
    }

}
