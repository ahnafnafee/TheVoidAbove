using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveScript : MonoBehaviour
{
    private static readonly int CutoffHeight = Shader.PropertyToID("_CutoffHeight");
    [SerializeField] private float dissolveAmount;
    [SerializeField] private float dissolveSpeed;
    private Material material;
    private float mHeight;
    private bool isDissolving;
    double TOLERANCE = 0.01f;

    private void Start()
    {
        material = GetComponent<MeshRenderer>().material;
        mHeight = material.GetFloat(CutoffHeight);
    }

    void Update()
    {
        if (!isDissolving) return;
        if (!(Math.Abs(mHeight - dissolveAmount) > TOLERANCE)) return;
        mHeight -= 50f * Time.deltaTime;
        material.SetFloat(CutoffHeight, mHeight);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.tag.Equals("Player")) return;
        if (transform.GetChild(0).gameObject == null) return;

        Destroy(transform.GetChild(0).gameObject);
        StartDissolve();
    }

    private void StartDissolve()
    {
        isDissolving = true;
    }

    public void StopDissolve()
    {
        isDissolving = false;
    }
}
