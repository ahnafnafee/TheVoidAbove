using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowControl : MonoBehaviour
{
    public GameObject book;
    public int nubPage;
    public int nubPic;
    private List<GameObject> allpage = new List<GameObject>();
    private List<GameObject> allpic = new List<GameObject>();
    public float time_fade;
    public float timer;
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
            if (timer <= 0.0f) {
                NextPage();
            }
        }
        
    }

    public void ChangePic()
    {
        nubPic ++;
        if (nubPic >= allpic.Count)
        {
            PicFadeOut();
        }
        else {
            allpic[nubPic].SetActive(true);
        }
    }

    public void PicFadeOut() {
        foreach (GameObject pic in allpic) {
            pic.GetComponent<Animator>().SetTrigger("Fade_out");
        }
        timer = time_fade;
    }

    public void NextPage() {
        allpic = new List<GameObject>();
        allpage[nubPage].SetActive(false);
        nubPage++;
        nubPic = 0;
        allpage[nubPage].SetActive(true);
        AddNewPics();
    }
    public void AddNewPics() {
        foreach (Transform child in allpage[nubPage].transform)
        {
            allpic.Add(child.gameObject);
        }
        allpic[nubPic].SetActive(true);
    }
}
