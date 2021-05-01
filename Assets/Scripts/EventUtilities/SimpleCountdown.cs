using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

[Serializable]
public class SimpleCountdownEvent : UnityEvent { };
public class SimpleCountdown : MonoBehaviour
{
    public float countdownTime = 3f;
    public SimpleCountdownEvent OnCountdownFinished;
    private void Start()
    {
        StartCoroutine(StartCountdown());
    }


    IEnumerator StartCountdown()
    {
        yield return new WaitForSeconds(countdownTime);
        OnCountdownFinished.Invoke();

    }
}
