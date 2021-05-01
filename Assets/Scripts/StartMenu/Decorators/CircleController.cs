using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleController : MonoBehaviour
{
    public Transform targetTransform;
    public Transform originalTransform;
    public Transform creditsPosition;

    Vector3 targetPosition;
    Vector3 originalPosition;

    public void Start()
    {
        //originalTransform = transform;
        originalPosition = transform.position;
        targetPosition = targetTransform.position;
    }

    public void SetStartScreenCircle()
    {
        transform.DOMove(originalPosition, 0.3f);
        
    }

    public void SetMainMenuCircle()
    {
        transform.DOMove(targetPosition, 00.3f);
    }

    public void SetCreditsCircle()
    {
        transform.DOMove(creditsPosition.transform.position, 0.3f);
    }


}
