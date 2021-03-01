using System;
using _Project.Scripts.Utils;
using Cinemachine;
using TMPro;
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
        private const double k_Tolerance = 0.001f;

        [Header("GUI Texts")]
        [SerializeField] private TextMeshProUGUI xVal;
        [SerializeField] private TextMeshProUGUI yVal;

        UnityEvent mEvent = new UnityEvent();

        public void Start()
        {
            _freeLookComponent = GetComponent<CinemachineFreeLook>();
            lookSpeedX = _freeLookComponent.m_XAxis.m_MaxSpeed;
            lookSpeedY = _freeLookComponent.m_YAxis.m_MaxSpeed;

            GlobalVar.mSensitivityX = lookSpeedX;
            GlobalVar.mSensitivityY = lookSpeedY;

            xVal.text = Convert.ToString(lookSpeedX);
            yVal.text = Convert.ToString(lookSpeedY);

            mEvent.AddListener(UpdateAxis);

        }

        private void UpdateAxis()
        {

            _freeLookComponent.m_XAxis.m_MaxSpeed = lookSpeedX;
            _freeLookComponent.m_XAxis.m_MaxSpeed = GlobalVar.mSensitivityX;

            _freeLookComponent.m_YAxis.m_MaxSpeed = lookSpeedY;
            _freeLookComponent.m_YAxis.m_MaxSpeed = GlobalVar.mSensitivityY;
        }

        private void Update()
        {
            // Debug.Log($"X: {lookSpeedX}, Y: {lookSpeedY}");

            
            if (((Math.Abs(lookSpeedX - _freeLookComponent.m_XAxis.m_MaxSpeed) > k_Tolerance) ||
                 (Math.Abs(lookSpeedY - _freeLookComponent.m_YAxis.m_MaxSpeed) > k_Tolerance)))
            {
                mEvent?.Invoke();
            }
        }

        public void MouseSensitivityX (float speed)
        {
            xVal.text = Convert.ToString(Math.Round(speed, 3, MidpointRounding.ToEven));
            GlobalVar.mSensitivityX = speed;
            lookSpeedX = speed;
        }
        
        public void MouseSensitivityY (float speed)
        {
            yVal.text = Convert.ToString(Math.Round(speed, 3, MidpointRounding.ToEven));
            GlobalVar.mSensitivityY = speed;
            lookSpeedY = speed;
        }
    }
}