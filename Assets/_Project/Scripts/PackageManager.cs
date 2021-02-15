using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts
{
    public class PackageManager : MonoBehaviour
    {
        public bool hasPackage;
        
        public GameObject package;
        [SerializeField]
        private GameObject grabRange;
        private bool startTimer;
        private float pgTimer = 1;
        // Start is called before the first frame update
        void Awake()
        {
            if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                hasPackage = false;
            }
            else
            {
                hasPackage = true;
                PickUp();
            }

            grabRange.SetActive(true);
            package.GetComponent<Rigidbody>().detectCollisions = true;
            package.transform.parent = null;
            startTimer = false;
        }

        // Update is called once per frame
        void Update()
        {
            if(startTimer)
            {
                pgTimer -= Time.deltaTime;
                if(pgTimer <= 0)
                {
                    package.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                    pgTimer = 1;
                    startTimer = false;
                }
            }
        }

        public void Drop(Vector3 vel)
        {
            hasPackage = false;
            package.transform.parent = null;
            package.GetComponent<Rigidbody>().detectCollisions = true;
            package.GetComponent<Rigidbody>().isKinematic = false;
            package.GetComponent<Rigidbody>().velocity += Vector3.Reflect(vel * 0.5f, vel.normalized);
            startTimer = true;
            grabRange.SetActive(true);
        }

        public void PickUp()
        {
            //TODO: Reference child object directly as hierarchical changes will create bugs
            
            package.transform.position = this.transform.Find("PackagePosition").position;
            package.transform.rotation = this.transform.Find("PackagePosition").rotation;
            package.transform.parent = this.transform;
            hasPackage = true;
            AkSoundEngine.PostEvent("success_pickup_event", gameObject);
            package.GetComponent<Rigidbody>().detectCollisions = false;
            package.GetComponent<Rigidbody>().isKinematic = true;
            grabRange.SetActive(false);
        }
    }
}
