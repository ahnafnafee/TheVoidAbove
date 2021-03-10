using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_volume : MonoBehaviour
{
    [SerializeField]
    int level = 100;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AkSoundEngine.SetRTPCValue("masterVolume", level);
    }
}
