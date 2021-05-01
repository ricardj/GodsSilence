using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CharacterAnimationController : MonoBehaviour
{
    Animator animator;

    public WeaponType currentWeaponType;

    public string walkTrigger = "Walk";
    public string idleTrigger = "Idle";
    public string shootTrigger = "Shoot";
    public string idleType = "IdleType";
    public string walkType = "WalkType";
    public string shootType = "ShootType";


    //Support variables
    bool walking = false;
    bool idle = true;
    bool shooting = false;

    private void Start()
    {
        animator = GetComponent<Animator>();

    }

    

    public void Walk()
    {
        if(!walking)
        {
            animator.SetTrigger(walkTrigger);
            animator.SetFloat(walkType, (float)currentWeaponType / ((float)WeaponType.TOTAL_STATES - 1));
            walking = true;
            idle = false;
            shooting = false;
        }
        
    }
    public void Idle()
    {
        if(!idle)
        {
            animator.SetTrigger(idleTrigger);
            animator.SetFloat(idleType, (float)currentWeaponType / ((float)WeaponType.TOTAL_STATES - 1));
            idle = true;
            walking = false;
            shooting = false;
        }
        
    }

    public void Shoot()
    {
        if(!shooting)
        {
            animator.SetTrigger(shootTrigger);
            animator.SetFloat(shootType, (float)currentWeaponType / ((float)WeaponType.TOTAL_STATES - 1));
            shooting = true;
            idle = false;
            walking = false;
        }
        
    }

    public void SetWeaponType(WeaponType newWeaponType)
    {
        currentWeaponType = newWeaponType;
        animator.SetFloat(walkType, (float)currentWeaponType / ((float)WeaponType.TOTAL_STATES - 1));
        animator.SetFloat(idleType, (float)currentWeaponType / ((float)WeaponType.TOTAL_STATES - 1));
        //animator.SetFloat(shootType, (float)currentWeaponType / ((float)WeaponType.TOTAL_STATES - 1));
    }
}
