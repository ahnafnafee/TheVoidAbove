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
        [SerializeField]
        private Transform packagePos;
        [SerializeField]
        private ObjectiveSystem objective;
        [SerializeField]
        private GameObject highlighter;
        [SerializeField]
        private MeshRenderer pkgBase;
        [SerializeField]
        private Material defaultMat;
        [SerializeField]
        private Material highlightMat;
        private bool startTimer;
        private float pgTimer = 1;
        private Rigidbody pkgRb;

        private bool isObjectiveNotNull;

        // Start is called before the first frame update
        void Awake()
        {
            grabRange.SetActive(true);
            pkgRb = package.GetComponent<Rigidbody>();
            pkgRb.detectCollisions = true;
            package.transform.parent = null;
            startTimer = false;
        }

        void Start()
        {
            isObjectiveNotNull = objective != null;

            // Needs to be modular
            if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                hasPackage = false;
                highlighter.SetActive(true);
                pkgBase.material = highlightMat;
            }
            else
            {
                hasPackage = true;
                PickUp();
            }
        }

        // Update is called once per frame
        void Update()
        {
            if(startTimer)
            {
                pgTimer -= Time.deltaTime;
                if (pgTimer <= 0)
                {
                    pkgRb.velocity = new Vector3(0, 0, 0);
                    pgTimer = 1;
                    startTimer = false;
                }
            }
        }

        public void Drop(Vector3 vel)
        {
            hasPackage = false;

            if (isObjectiveNotNull)
                objective.Pending();

            highlighter.SetActive(true);
            pkgBase.material = highlightMat;

            package.transform.parent = null;
            package.GetComponent<Collider>().enabled = true;

            pkgRb.detectCollisions = true;
            pkgRb.isKinematic = false;
            pkgRb.velocity += Vector3.Reflect(vel * 0.5f, vel.normalized);
            startTimer = true;
            grabRange.SetActive(true);
        }

        public void PickUp()
        {
            //TODO: Reference child object directly as hierarchical changes will create bugs

            hasPackage = true;

            highlighter.SetActive(false);
            pkgBase.material = defaultMat;

            package.transform.position = packagePos.position;
            package.transform.rotation = packagePos.rotation;
            package.transform.parent = this.transform;

            if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                Debug.Log("OBJ COMPLETE");
                objective.Complete();
            }


            AkSoundEngine.PostEvent("success_pickup_event", gameObject);
            Debug.Log(gameObject.name);
            package.GetComponent<Collider>().enabled = false;
            pkgRb.detectCollisions = false;
            pkgRb.isKinematic = true;
            grabRange.SetActive(false);
        }
    }
}
