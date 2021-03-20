using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace _Project.Scripts
{
    public class PanelOpen : MonoBehaviour
    {
        [SerializeField]
        private GameObject pannel1;
        [SerializeField]
        private GameObject pannel2;
        [SerializeField]
        private GameObject zapZone;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (pannel1.GetComponent<PanelCheck>().checkDone() && pannel2.GetComponent<PanelCheck>().checkDone())
            {
                zapZone.SetActive(false);
                Destroy(this.gameObject);
            }
        }
    }
}
