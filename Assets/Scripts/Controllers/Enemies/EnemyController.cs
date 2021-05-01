using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
[Serializable]
public class EnemyEvent : UnityEvent { }


public class EnemyController : MonoBehaviour
{
    public float health;
    public EnemyEvent OnEnemyHurted;
    public EnemyEvent OnEnemyDeath;

    public void ChangeHealth(float change)
    {
        health += change;
        if (change < 0)
            OnEnemyHurted.Invoke();
        if (health <= 0)
            OnEnemyDeath.Invoke();
    }
}
