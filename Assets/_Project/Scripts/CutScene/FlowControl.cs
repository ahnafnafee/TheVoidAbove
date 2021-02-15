using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts
{
    public class FlowControl : MonoBehaviour
    {
        public GameObject book;
        private int nubPage;
        private int nubPic;
        private List<GameObject> allpage = new List<GameObject>();
        private List<GameObject> allpic = new List<GameObject>();
        private List<GameObject> allText = new List<GameObject>();
        private float time_fade;
        private float timer;
        private TypeWriter textScript;

        void Start()
        {
            time_fade = 0.40f;
            timer = 0.0f;
            nubPage = 0;
            nubPic = 0;
            foreach (Transform child in book.transform)
            {
                allpage.Add(child.gameObject);
            }
            allpage[nubPage].SetActive(true);
            AddNewPics();
        }

        // Update is called once per frame
        void Update()
        {
            if (timer > 0.0f)
            {
                timer -= Time.deltaTime;
                if (timer <= 0.0f)
                {
                    NextPage();
                }
            }

        }

        public void ChangePic()
        {
            textScript = allText[nubPic].GetComponent<TypeWriter>();
            if (textScript.currentText == textScript.fullText)
            {
                allText[nubPic].SetActive(false);
                nubPic++;
                if (nubPic >= allpic.Count)
                {
                    PicFadeOut();
                }
                else
                {
                    allpic[nubPic].SetActive(true);
                    allText[nubPic].SetActive(true);
                }
            }
            else
            {
                textScript.delay = 0f;
            }
        }

        public void PicFadeOut()
        {
            foreach (GameObject pic in allpic)
            {
                pic.GetComponent<Animator>().SetTrigger("Out");
            }
            timer = time_fade;
        }

        public void NextPage()
        {
            allpic = new List<GameObject>();
            allText = new List<GameObject>();
            allpage[nubPage].SetActive(false);
            nubPage++;
            nubPic = 0;
            if (nubPage == allpage.Count)
            {
                gameObject.SetActive(false);
                FindObjectOfType<ProgressLoadScene>().LoadScene(3);
                //SceneManager.LoadScene(3);
            }
            else
            {
                allpage[nubPage].SetActive(true);
                AddNewPics();
            }
        }
        public void AddNewPics()
        {
            foreach (Transform child in allpage[nubPage].transform.GetChild(1).transform)
            {
                allpic.Add(child.gameObject);
            }
            foreach (Transform child in allpage[nubPage].transform.GetChild(0).transform)
            {
                allText.Add(child.gameObject);
            }
            allpic[nubPic].SetActive(true);
            allText[nubPic].SetActive(true);
        }
    }
}
