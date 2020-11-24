using UnityEngine;

namespace _Project.Scripts
{
    public class PackageManager : MonoBehaviour
    {
        public bool hasPackage { get; set; }
        
        public GameObject package;
        [SerializeField]
        private GameObject grabRange;
        private bool startTimer;
        private float pgTimer = 1;
        // Start is called before the first frame update
        void Awake()
        {
            hasPackage = true;
            grabRange.SetActive(false);
            package.GetComponent<Rigidbody>().detectCollisions = false;
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
            package.GetComponent<Rigidbody>().detectCollisions = false;
            package.GetComponent<Rigidbody>().isKinematic = true;
            grabRange.SetActive(false);
        }
    }
}
