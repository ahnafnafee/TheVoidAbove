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
 
        // Update is called once per frame
        // void Update () {           
        //
        //     if(Time.time > nextSpawn)
        //     {
        //         nextSpawn = Time.time + rateOfSpawn;
        //    
        //         // Random position within this transform
        //         Vector3 rndPosWithin;
        //         rndPosWithin = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        //         rndPosWithin = transform.TransformPoint(rndPosWithin * .5f);
        //         Instantiate(objectToSpawn, rndPosWithin, transform.rotation);      
        //     }
        // }

        private void Start()
        {
            for (int i = 0; i < astCount; i++)
            {
                Spawn();
            }
        }

        void Spawn()
        {
            Bounds bounds = GetComponent<Collider>().bounds;
            float offsetX = Random.Range(-bounds.extents.x, bounds.extents.x);
            float offsetY = Random.Range(-bounds.extents.y, bounds.extents.y);
            float offsetZ = Random.Range(-bounds.extents.z, bounds.extents.z);
     
            GameObject asteroid = Instantiate(objectToSpawn, gameObject.transform, true);
            asteroid.transform.position = bounds.center + new Vector3(offsetX, offsetY, offsetZ);
            asteroid.transform.rotation = Random.rotation;
            asteroid.transform.localScale = Vector3.one * Random.Range(minScale, maxScale);

        }
    }
}