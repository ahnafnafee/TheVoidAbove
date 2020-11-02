using UnityEngine;

namespace _Project.Scripts
{
    public class EnemyMove : MonoBehaviour
    {
        [SerializeField]
        private float enemySpeed;
        private bool right;
        private bool isMoving = true;
        // Start is called before the first frame update
        void Start()
        {
            right = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (isMoving)
            {
                if (right)
                {
                    transform.position += new Vector3(0, 0, 2 * enemySpeed * Time.deltaTime);
                }
                else
                {
                    transform.position += new Vector3(0, 0, -2 * enemySpeed * Time.deltaTime);
                }
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.tag.Equals("Turn"))
            {
                if (other.transform.tag.Equals("Turn"))
                {
                    right = !right;
                }
            }
        }
        public void stopMoving()
        {
            isMoving = false;
        }
        public void startMoving()
        {
            isMoving = true;
        }
    }
}
