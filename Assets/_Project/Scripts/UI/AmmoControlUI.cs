using UnityEngine;

namespace _Project.Scripts
{
    public class AmmoControlUI : MonoBehaviour
    {

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
        }

        public void Display(int nubAmmo) {
            for (int i = 0; i < nubAmmo; i++)
                transform.GetChild(i).gameObject.SetActive(true);
            for (int i = nubAmmo; i < 5; i++)
                transform.GetChild(i).gameObject.SetActive(false);
        }
    
    
    }
}
