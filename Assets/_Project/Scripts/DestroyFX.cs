using UnityEngine;

namespace _Project.Scripts
{
    public class DestroyFX : MonoBehaviour
    {
        void Start()
        {
            Destroy(gameObject, 5.0f);
        }
    }
}
