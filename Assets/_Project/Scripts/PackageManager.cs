using UnityEngine;

namespace _Project.Scripts
{
    public class PackageManager : MonoBehaviour
    {
        bool hasPackage;
        public GameObject package;
        // Start is called before the first frame update
        void Awake()
        {
            hasPackage = true;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void drop()
        {
            hasPackage = false;
            package.transform.parent = null;
        }

        public void pickUp()
        {
            package.transform.position = this.transform.GetChild(this.transform.childCount - 1).position;
            package.transform.rotation = this.transform.GetChild(this.transform.childCount - 1).rotation;
            package.transform.parent = this.transform;
            hasPackage = true;
        }

        void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.tag == "Debris" && hasPackage)
            {
                drop();
            }
        }
    }
}
