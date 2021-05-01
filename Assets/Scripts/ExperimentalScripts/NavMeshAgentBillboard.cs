using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavMeshAgentBillboard : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(DelayedPlayerUp());
    }
    IEnumerator DelayedPlayerUp()
    {
        for(int i = 0; i < 100; i++)
        {
            transform.forward = Vector3.forward;
            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z + 180f); 
            yield return null;
            //yield return new WaitForSeconds(0.01f);
        }
    }
    private void Update()
    {
        //transform.rotation = Quaternion.Euler(0,0, transform.rotation.z);
        //transform.forward = Vector3.forward;
    }
}
