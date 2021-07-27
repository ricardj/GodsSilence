using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PartnerController : MonoBehaviour
{
    public AvatarAnimationController characterAnimationController;
    public bool followTarget;
    public GameObject target;
    public float targetPositionOffset;

    NavMeshAgent navMeshAgent;

    Rigidbody2D rb;

    [Header("Weapons")]
    WeaponContainer currentWeapon;

    public WeaponInventory weaponInventory;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        navMeshAgent = GetComponent<NavMeshAgent>();
       
        if (!navMeshAgent.Warp(transform.position))
        {
            Debug.LogError("Nav mesh agent not warped");
        }
        
    }

    private void Update()
    {
        if(followTarget && target != null)
        {
            if ((target.transform.position - transform.position).magnitude > targetPositionOffset)
            {
                navMeshAgent.SetDestination(target.transform.position);
                navMeshAgent.isStopped = false;
            }           
            else
                navMeshAgent.isStopped = true;
        }
       
        if (navMeshAgent.velocity.magnitude > 0)
            characterAnimationController.SetState<Walking>();
        else
            characterAnimationController.SetState<Idle>();

    }

    public void SetWeaponByIndex(int weaponIndex)
    {
        if(currentWeapon != null)
            currentWeapon.SetWeaponShooting(false);
        currentWeapon = weaponInventory.GetWeapon(weaponIndex);
        characterAnimationController.SetWeaponType(currentWeapon.weaponConfiguration);
    }

    public void SetWeapon(WeaponConfigurationSO weaponConfiguration)
    {
        if (currentWeapon != null)
            currentWeapon.SetWeaponShooting(false);
        currentWeapon.weaponConfiguration = weaponConfiguration;
        characterAnimationController.SetWeaponType(currentWeapon.weaponConfiguration);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
        }

        if(collision.gameObject.CompareTag("Bullet"))
        {
            gameObject.SetActive(false);
        }
    }

    
}
