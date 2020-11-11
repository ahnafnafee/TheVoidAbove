using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace _Project.Scripts
{
    [RequireComponent(typeof(CinemachineFreeLook))]
    public class MouseSensitivity : MonoBehaviour
    {
        [FormerlySerializedAs("LookSpeed")]
        [SerializeField]
        private float lookSpeedX = 1f;
        [SerializeField]
        private float lookSpeedY = 1f;
        [FormerlySerializedAs("InvertY")] public bool invertY = false;
        private CinemachineFreeLook _freeLookComponent;
        [SerializeField] private float lSpeed = default;
        private const double Tolerance = 0.001f;

        UnityEvent mEvent = new UnityEvent();

        public void Start()
        {
            _freeLookComponent = GetComponent<CinemachineFreeLook>();
            lookSpeedX = _freeLookComponent.m_XAxis.m_MaxSpeed;
            lookSpeedY = _freeLookComponent.m_YAxis.m_MaxSpeed;
            mEvent.AddListener(UpdateAxis);

        }

        private void UpdateAxis()
        {
            _freeLookComponent.m_XAxis.m_MaxSpeed = lookSpeedX;
            _freeLookComponent.m_YAxis.m_MaxSpeed = lookSpeedY;
        }

        private void Update()
        {
            // Debug.Log($"X: {lookSpeedX}, Y: {lookSpeedY}");

            
            if (((Math.Abs(lookSpeedX - _freeLookComponent.m_XAxis.m_MaxSpeed) > Tolerance) ||
                 (Math.Abs(lookSpeedY - _freeLookComponent.m_YAxis.m_MaxSpeed) > Tolerance)))
            {
                mEvent?.Invoke();
            }
        }

        public void MouseSensitivityX (float speed)
        {
            lookSpeedX = speed;
        }
        
        public void MouseSensitivityY (float speed)
        {
            lookSpeedY = speed;
        }
    }
}