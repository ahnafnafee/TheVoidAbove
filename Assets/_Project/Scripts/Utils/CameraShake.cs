using System.Collections;
using Cinemachine;
using UnityEngine;

namespace _Project.Scripts.Utils
{
    public class CameraShake : MonoBehaviour
    {
        [Header("Camera Shake")]
        [SerializeField] private CinemachineFreeLook freeCam;

        private CinemachineVirtualCamera midCam;
        private CinemachineBasicMultiChannelPerlin virtualCameraNoise;

        // Start is called before the first frame update
        void Start()
        {
            midCam = freeCam.GetRig(1);
            virtualCameraNoise = midCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }

        public void Shake(float amplitude, float time)
        {
            StartCoroutine(ShakeIE(amplitude, time));
        }

        private IEnumerator ShakeIE(float amplitude, float time)
        {
            virtualCameraNoise.m_AmplitudeGain = amplitude;
            yield return new WaitForSeconds(time);
            virtualCameraNoise.m_AmplitudeGain = 0f;
        }
    }
}
