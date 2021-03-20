using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace _Project.Scripts
{
    public class SpawningArea : MonoBehaviour {
 
        public GameObject objectToSpawn;   
        public float rateOfSpawn = 1;

        public float minScale = 0.6f, maxScale = 1.4f;
   
        private float nextSpawn = 0;
        public int astCount;
        float offsetX, offsetY, offsetZ;
        Bounds bounds;


        private void Start()
        {
            bounds = GetComponent<Collider>().bounds;
           
            for (int i = 0; i < astCount; i++)
            {
                Spawn();
            }
        }

        void Spawn()
        {
            offsetX = Random.Range(-bounds.extents.x, bounds.extents.x);
            offsetY = Random.Range(-bounds.extents.y, bounds.extents.y);
            offsetZ = Random.Range(-bounds.extents.z, bounds.extents.z);

            GameObject asteroid = Instantiate(objectToSpawn, gameObject.transform, true);
            asteroid.transform.position = bounds.center + new Vector3(offsetX, offsetY, offsetZ);
            asteroid.transform.rotation = Random.rotation;
            asteroid.transform.localScale = Vector3.one * Random.Range(minScale, maxScale);

        }
    }
}