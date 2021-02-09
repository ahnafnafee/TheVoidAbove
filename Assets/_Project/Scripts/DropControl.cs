using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace _Project.Scripts
{
    public class DropControl : MonoBehaviour
    {
        [SerializeField]
        private float gainedHealth;
        [SerializeField]
        private float rotSpeed;

        private float enemyHealth;

        // Update is called once per frame
        void Update()
        {
            //var rotx = transform.rotation.x;
            //var roty = transform.rotation.y;
            //var rotz = transform.rotation.z;
            //transform.Rotate(rotx, roty, rotz);

            transform.Rotate(0, rotSpeed * Time.deltaTime, 0, Space.World);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag.Equals("Player"))
            {
                other.gameObject.GetComponent<Player>().powerUp();
                AkSoundEngine.PostEvent("ammo_event", this.gameObject);
                Destroy(this.gameObject);
            }
        }
    }
}

