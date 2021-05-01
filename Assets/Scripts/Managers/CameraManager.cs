using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public CinemachineVirtualCamera playerCamera;
    public CinemachineVirtualCamera partnerCamera;
    public CinemachineVirtualCamera partnerFreeControlCamera;

    List<CinemachineVirtualCamera> virtualCameras;

    public void Start()
    {
        virtualCameras = new List<CinemachineVirtualCamera>();
        virtualCameras.Add(playerCamera);
        virtualCameras.Add(partnerCamera);
        virtualCameras.Add(partnerFreeControlCamera);
        SetPlayerCamera();
    }

    public void SetPlayerCamera()
    {
        DeactivateAllVirtualCameras();
        playerCamera.gameObject.SetActive(true);
    }
    public void SetPartnerCamera()
    {
        DeactivateAllVirtualCameras();
        partnerCamera.gameObject.SetActive(true);

        StartCoroutine(DelayedFreePartnerCamera());
    }


    IEnumerator DelayedFreePartnerCamera()
    {
        yield return new WaitForSecondsRealtime (0.3f);
        partnerCamera.gameObject.SetActive(false);
        partnerFreeControlCamera.transform.position = Vector3.zero;
        partnerFreeControlCamera.transform.position = partnerCamera.transform.position;
        Debug.Log("Free control camera activated");
        partnerFreeControlCamera.gameObject.SetActive(true);
    }

    public CinemachineVirtualCamera GetPartnerCamera()
    {
        return partnerFreeControlCamera;
    }

    void DeactivateAllVirtualCameras()
    {
        for(int i = 0; i < virtualCameras.Count;i++)
        {
            virtualCameras[i].gameObject.SetActive(false);
        }
    }
}
