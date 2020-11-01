using UnityEngine;

namespace _Project.Scripts
{
    public class StarField : MonoBehaviour
    {
        Transform thisTransform;
        ParticleSystem.Particle[] points;
        float starDistanceSqr;
        float starClipDistanceSqr;

        [SerializeField]
        Color starColor;

        [SerializeField]
        int starsMax = 600;

        [SerializeField]
        float starSize = .35f;

        [SerializeField]
        float starDistance = 60f;

        [SerializeField]
        float starClipDistance = 15f;

        void Awake()
        {
            thisTransform = GetComponent<Transform>();
   
            starDistanceSqr = starDistance * starDistance;
            starClipDistanceSqr = starClipDistance * starClipDistance;
        }

        void CreateStars()
        {
            points = new ParticleSystem.Particle[starsMax];

            for (int i = 0; i < starsMax; i++)
            {

                points[i].position = Random.insideUnitSphere * starDistance + thisTransform.position;
                points[i].startColor = new Color(255,255,255,255);
                points[i].startSize = starSize;
            }
        }

        void Update()
        {
            if (points == null) CreateStars();

            for (int i = 0; i < starsMax; i++)
            {
            
                if ((points[i].position - thisTransform.position).sqrMagnitude > starDistanceSqr)
                {
                    points[i].position = Random.insideUnitSphere.normalized * starDistance + thisTransform.position;
                }
                if ((points[i].position - thisTransform.position).sqrMagnitude <= starClipDistanceSqr)
                {
                    float percentage = (points[i].position - thisTransform.position).sqrMagnitude / starClipDistanceSqr;
                    points[i].startColor = new Color(1, 1, 1, percentage);
                    points[i].startSize = percentage * starSize;
                }
            }

            GetComponent<ParticleSystem>().SetParticles(points, points.Length);

        }
    }
}
