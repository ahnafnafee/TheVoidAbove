using UnityEngine;

namespace _Project.Scripts.Utils
{
    public class MoveAlong : MonoBehaviour
    {
        private Transform pTransform;

        private void Start() {
            pTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void Update()
        {

            var position = transform.position;
            var playerPos = pTransform.position;
            position = new Vector3(playerPos.x, position.y, playerPos.z - 1560);
            transform.position = position;
        }
    }
}
