//using _Project.Scripts.InputActions;
using UnityEngine;

namespace _Project.Scripts
{
    public class CameraControl : MonoBehaviour
    {

        [SerializeField]
        float camerarotationspeed;
        [SerializeField]
        float cameradistance;
        [SerializeField]
        GameObject player;

        [SerializeField]
        float verticalDistance;
        PlayerControls playercontrols;

        void Awake()
        {
            playercontrols = new PlayerControls();
            playercontrols.Enable();

        }

        // Start is called before the first frame update
        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        // Update is called once per frame
        void Update()
        {

            PlayerControls.PlayerStandardActions Actions = playercontrols.PlayerStandard;

            //uses the mouse info from input controller system to change and rotate the camera
            Vector3 manualMove = new Vector3(Actions.LookX.ReadValue<float>(), Actions.LookY.ReadValue<float>(), 0);
            if (manualMove != Vector3.zero)
            {
                Vector3 horizEuler = transform.up * manualMove.x;
                Vector3 vertEuler = -transform.right * manualMove.y;
                Vector3 euler = horizEuler + vertEuler;


                transform.forward = Quaternion.Euler(euler * Time.deltaTime * camerarotationspeed) * transform.forward;
            }
            if (transform.eulerAngles.x > 85 && transform.eulerAngles.x > 15)
            {
                Vector3 temp = new Vector3(85, transform.localEulerAngles.y, transform.localEulerAngles.z);
                transform.localEulerAngles = temp;
            }
            else if (transform.eulerAngles.x < -85 && transform.eulerAngles.x < -15)
            {
                Vector3 temp = new Vector3(-85, transform.localEulerAngles.y, transform.localEulerAngles.z);
                transform.localEulerAngles = temp;
            }


            transform.position = (-transform.forward * cameradistance) + player.transform.position;

            transform.position = new Vector3(transform.position.x, transform.position.y + verticalDistance, transform.position.z);
        
        
        }
    }
}
