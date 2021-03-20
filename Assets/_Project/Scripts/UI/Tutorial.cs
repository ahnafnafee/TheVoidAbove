using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts
{
    public class Tutorial : MonoBehaviour
    {
        private static readonly int CutoffHeight = Shader.PropertyToID("_CutoffHeight");
        [SerializeField] private float dissolveAmount;
        [SerializeField] private float dissolveSpeed;
        private float mHeight;
        private bool isDissolving;
        double TOLERANCE = 0.01f;

        private Material material;

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
            StartCoroutine(DestroyObject());
        }

        IEnumerator DestroyObject()
        {
            yield return new WaitForSeconds(1.5f);
            Destroy(gameObject);
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
}