using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTerminator : EnemyController
{
    [HideInInspector]
    public Transform target;
    public float detectionRadius;
    public LayerMask targetsMask;
    public WeaponContainer terminatorWeapon;
    public GameObject deathParticles;
    RaycastHit[] raycastInfos;
    bool targetDetected;
    bool shooting;
    Collider2D[] detectedColliders;

    // Update is called once per frame
    void Update()
    {


        if (!targetDetected)
        {
            TryDetectPlayer();
        }

        if (targetDetected)
        {
            Vector3 targetPosition = target.position;
            targetPosition.z = transform.position.z;
            transform.up = -(targetPosition - transform.position).normalized;
            //transform.rotation = Quaternion.Euler(0, transform.rotation.y, 0);

            // transform.rotation = Quaternion.Euler(0, 0, transform.rotation.z);
            terminatorWeapon.SetWeaponShooting(true);
        }
        else
            terminatorWeapon.SetWeaponShooting(false);

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

    private void TryDetectPlayer()
    {
        //raycastInfos = Physics.SphereCastAll(transform.position, detectionRadius, Vector3.up, 0.01f, targetsMask);

        detectedColliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius, targetsMask);
        if (detectedColliders != null)
        {

            for (int i = 0; i < detectedColliders.Length; i++)
            {

                targetDetected = true;
                this.target = detectedColliders[i].transform;

                break;
            }
        }
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

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Debug.Log("So far so good");
            ChangeHealth(-10);
        }
    }


    public void DeathSequence()
    {
        Instantiate(deathParticles, transform.position, deathParticles.transform.rotation, null);
        Destroy(gameObject);
    }
}
