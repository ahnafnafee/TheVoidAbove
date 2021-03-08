using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts
{
    public class ObjectiveSystem : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        //TODO: Needs to package pickup mission prefab in Level 2

        public void Complete()
        {
            foreach (Transform child in transform)
            {
                switch (child.gameObject.name)
                {
                    case "Border":
                        child.gameObject.GetComponent<Image>().color = new Color32(32, 173, 114, 255);
                        break;
                    case "Pending":
                        child.gameObject.SetActive(false);
                        break;
                    case "Complete":
                        child.gameObject.SetActive(true);
                        break;
                    case "Text":
                        child.gameObject.GetComponent<TextMeshProUGUI>().color = new Color32(32, 173, 114, 255);
                        break;
                    case "CrossOut":
                        child.gameObject.SetActive(true);
                        break;
                }
            }
        }

        public void Pending()
        {
            foreach (Transform child in transform)
            {
                switch (child.gameObject.name)
                {
                    case "Border":
                        child.gameObject.GetComponent<Image>().color = new Color32(255,201,48, 255);
                        break;
                    case "Pending":
                        child.gameObject.SetActive(true);
                        break;
                    case "Complete":
                        child.gameObject.SetActive(false);
                        break;
                    case "Text":
                        child.gameObject.GetComponent<TextMeshProUGUI>().color = new Color32(255,201,48, 255);
                        break;
                    case "CrossOut":
                        child.gameObject.SetActive(false);
                        break;
                }
            }
        }
    }
}
