using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CinemachineShake : MonoBehaviour
{
    private CinemachineVirtualCamera _cinemachineVirtualCamera;
    public static CinemachineShake Instance { get;private set; }
    // Start is called before the first frame update
    
    private float actualShakeTimer;
    private void Awake()
    {
        Instance = this;
        _cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCamera(float intensity,float shakeTimer)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        actualShakeTimer = shakeTimer;

    }

    // Update is called once per frame
    void Update()
    {
        if (actualShakeTimer > 0)
        {
            actualShakeTimer -= Time.deltaTime;
            if (actualShakeTimer <= 0f)
            {
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
                    _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;
            }

        }
        
    }
}
