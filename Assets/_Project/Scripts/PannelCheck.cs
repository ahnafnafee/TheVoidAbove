using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace _Project.Scripts
{
    public class PannelCheck : MonoBehaviour
    {
        [SerializeField]
        private float stayTimer;

        private bool startTimer;
        private float timer;
        private bool isDone;
        // Start is called before the first frame update
        void Start()
        {
            timer = stayTimer;
            isDone = false;
            startTimer = false;
        }

        // Update is called once per frame
        void Update()
        {
            if(startTimer)
            {
                if(stayTimer <= 0)
                {
                    isDone = true;
                }
                stayTimer -= Time.deltaTime;
            }
            else
            {
                stayTimer = timer;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.tag.Equals("Player"))
            {
                startTimer = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag.Equals("Player"))
            {
                startTimer = false;
            }
        }

        public bool checkDone()
        {
            return isDone;
        }
    }
}
