using UnityEngine;

namespace _Project.Scripts.Misc
{
    public class DestroyTunnel : MonoBehaviour
    {
        [SerializeField] private GameObject tunnel;

        private void OnTriggerExit(Collider other)
        {
            if (other.tag.Equals("Player"))
            {
                Destroy(tunnel);
            }
        }
    }
}
