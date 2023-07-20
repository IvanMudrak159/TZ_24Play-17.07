using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShakeController : MonoBehaviour
{
    private CinemachineVirtualCamera _camera;
    private CinemachineBasicMultiChannelPerlin _noise;

    public static CameraShakeController Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        _camera = GetComponent<CinemachineVirtualCamera>();
        _noise = _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        ResetIntensity();
    }

    public void ShakeCamera(float intensity, float shakeTime)
    {
        _noise.m_AmplitudeGain = intensity;
        StartCoroutine(WaitTime(shakeTime));
    }

    private IEnumerator WaitTime(float shakeTime)
    {
        yield return new WaitForSeconds(shakeTime);
        ResetIntensity();
    }

    private void ResetIntensity()
    {
        _noise.m_AmplitudeGain = 0f;
    }
}
