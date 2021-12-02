using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake cameraShakeInstance;
    CinemachineVirtualCamera VCam;
    float shakeTimer, shakeTimerTotal, startingIntensity;
    void Awake()
    {
        cameraShakeInstance = this;
        VCam = GetComponent<CinemachineVirtualCamera>();
    }
    void Update()
    {
        if (shakeTimer >= 0)
            shakeTimer -= Time.deltaTime;
        else
        {
            CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = VCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(startingIntensity, 0f, 1 - (shakeTimer / shakeTimerTotal));
        }
    }
    public void ShakeCam(float intensity, float duration)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = VCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        shakeTimer = duration;
        shakeTimerTotal = duration;
        startingIntensity = intensity;
    }
}
