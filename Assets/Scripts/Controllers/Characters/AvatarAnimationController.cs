using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public  class IAvatarState
{
    public string stateTriggerName;
    public string animationBlendTreeName;

    bool stateActive = false;
    public IAvatarState ActivateState(Animator animatorReference, float blendTreeParameter)
    {
        if (!stateActive)
        {
            stateActive = true;
            animatorReference.SetTrigger(stateTriggerName);
            animatorReference.SetFloat(animationBlendTreeName, blendTreeParameter);
        }
        return this;
    }

    public void DeactivateState()
    {
        stateActive = false;
    }


};

public class Idle : IAvatarState { };
public class Shooting : IAvatarState { };
public class Walking : IAvatarState { };

public class AvatarAnimationController : MonoBehaviour
{
    Animator animator;

    public WeaponConfigurationSO currentWeaponType;
    public List<WeaponAnimation> weaponAnimationsList;

    public Idle idleState;
    public Shooting shootState;
    public Walking walkState;

    //public string walkTrigger = "Walk";
    //public string idleTrigger = "Idle";
    //public string shootTrigger = "Shoot";
    //public string idleType = "IdleType";
    //public string walkType = "WalkType";
    //public string shootType = "ShootType";

    IAvatarState currentState;


    //Support variables
    bool walking = false;
    bool idle = true;
    bool shooting = false;
    Dictionary<WeaponConfigurationSO, float> weaponAnimationsDict;

    float cachedBlendTreeParameter;

    private void Start()
    {
        animator = GetComponent<Animator>();
        InitializeWeaponAnimationsDict();
    }

    private void InitializeWeaponAnimationsDict()
    {
        weaponAnimationsList.ForEach(weaponAnimation =>
        {
            weaponAnimationsDict[weaponAnimation.weaponConfiguration] = weaponAnimation.BlendTreeParameter;
        });
    }

    public void SetState<T>() where T : IAvatarState
    {
        currentState.DeactivateState();
        if(typeof(T) == typeof(Idle))
        {
            currentState = idleState.ActivateState(animator, cachedBlendTreeParameter);
        }
        if (typeof(T) == typeof(Walking))
        {
            currentState = walkState.ActivateState(animator, cachedBlendTreeParameter);
        }
        if (typeof(T) == typeof(Shooting))
        {
            currentState = shootState.ActivateState(animator, cachedBlendTreeParameter);
        }
    }

    public void SetWeaponType(WeaponConfigurationSO newWeapon)
    {
        currentWeaponType = newWeapon;
        cachedBlendTreeParameter = weaponAnimationsDict[newWeapon];
        animator.SetFloat("WalkType", cachedBlendTreeParameter);
        //animator.SetFloat(idleType, cachedBlendTreeParameter);
    }
}

[Serializable]
public class WeaponAnimation
{
    public WeaponConfigurationSO weaponConfiguration;
    private float blendTreeParameter;
    private static float blendTreeStates = 7;

    public float BlendTreeParameter
    {
        get
        {
            return blendTreeParameter / blendTreeStates;
        }
        set => blendTreeParameter = value;
    }
}


