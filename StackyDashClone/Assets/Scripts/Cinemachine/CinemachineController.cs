using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CinemachineController : MonoBehaviour
{
    #region Singleton Pattern
    public static CinemachineController instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    [System.NonSerialized] public float vCamFov;
    CinemachineVirtualCamera vCam;
    CinemachineTransposer transposer;
    void Start()
    {
        vCam = GetComponent<CinemachineVirtualCamera>();
        vCamFov = vCam.m_Lens.FieldOfView;
        transposer = vCam.GetCinemachineComponent<CinemachineTransposer>();
        transposer.m_FollowOffset = new Vector3(-.5f, 7, -11);
    }

    public void ChangeCamPosInTime(Vector3 target, float duration, bool isAddition = true)
    {
        Vector3 pos = isAddition ? transposer.m_FollowOffset + target : target;
        DOTween.To(() => transposer.m_FollowOffset, x => transposer.m_FollowOffset = x, pos, duration);
    }

    void Update()
    {
        
    }
}
