using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FollowCameraController : MonoBehaviour
{
    private PlayerInputControl playerInput;
    public Vector2 CameraInputVector { get; set; }
    private CinemachineFreeLook cinemachine;
    private float lookSpeed = 1f;
    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }
    private void Awake()
    {
        playerInput = new PlayerInputControl();
        cinemachine = GetComponent<CinemachineFreeLook>();
    }
    void Update()
    {
        cinemachine.m_XAxis.Value += playerInput.PlayerMain.Look.ReadValue<Vector2>().x * lookSpeed * 50 * Time.deltaTime;
        cinemachine.m_YAxis.Value += playerInput.PlayerMain.Look.ReadValue<Vector2>().y * lookSpeed * Time.deltaTime;

    }
}

