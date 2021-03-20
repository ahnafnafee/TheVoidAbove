using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts
{
    public class Laser : MonoBehaviour
    {
        public GameObject gameObject1;
        public GameObject gameObject2;

        private LineRenderer line;
        Vector3 startPos;
        Vector3 endPos;
        CapsuleCollider capsule;

        [FormerlySerializedAs("LineWidth")] public float lineWidth;
        void Start()
        {
            line = this.gameObject.GetComponent<LineRenderer>();
            if (gameObject1 != null && gameObject2 != null)
            {
                // Update position of the two vertex of the Line Renderer
                line.SetPosition(0, gameObject1.transform.position);
                line.SetPosition(1, gameObject2.transform.position);

            }


            startPos = gameObject1.transform.position;
            endPos = gameObject2.transform.position;
            capsule = gameObject.AddComponent<CapsuleCollider>();
            capsule.isTrigger = true;
            capsule.radius = lineWidth / 2;
            capsule.center = Vector3.zero;
            capsule.direction = 2;

            capsule.transform.position = startPos + (endPos - startPos) / 2;
            capsule.transform.LookAt(startPos);
            capsule.height = (endPos - startPos).magnitude;

        }

        // Update is called once per frame
        void Update()
        {
            /*
        // Check if the GameObjects are not null
        if (gameObject1 != null && gameObject2 != null)
        {
            // Update position of the two vertex of the Line Renderer
            line.SetPosition(0, gameObject1.transform.position);
            line.SetPosition(1, gameObject2.transform.position);

        }
        */
        }

    }
}
