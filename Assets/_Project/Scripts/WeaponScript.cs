﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts
{
    public class WeaponScript : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        
        public void Shoot(Vector3 pos,Quaternion rot, bool isEnemy)
        {
            GameObject bullet = Instantiate(bulletPrefab, pos, rot);
        }
    }
}
