using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;


public class SimpleOnCollisionEnter : MonoBehaviour
{
    public bool useTarget;
    public GameObject target;
    public UnityEvent OnCollisionEnterEvent;

    public bool enabledCollision;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(enabledCollision)
        {
            if (useTarget)
            {
                if (collision.gameObject == target)
                    OnCollisionEnterEvent.Invoke();
            }
            else
            {
                Debug.Log(collision.gameObject.name);
                OnCollisionEnterEvent.Invoke();
            }
        }
    }

}
