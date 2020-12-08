using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTutorial : MonoBehaviour
{
    [SerializeField]
    private GameObject background;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            background.SetActive(true);
        }
    }
}
