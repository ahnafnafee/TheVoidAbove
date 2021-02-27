using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestIndicatorRegister : MonoBehaviour
{
    [SerializeField] float destroyTimer = 20.0f;

    private void Start()
    {
        Invoke("Register", Random.Range(0, 8));
    }
    void Register()
    {
        DI_System.CreateIndicator(this.transform);
    }
}
