using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialBillboard : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(InitialBillboardFrames());
    }

    IEnumerator InitialBillboardFrames()
    {
        float counterPeriod = 1f;
        float counter = 0;

        while (counter < counterPeriod)
        {
            counter += 0.1f;
            transform.forward = Vector3.forward;
            yield return null;
        }
        
        
    }
}