using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts
{
    public class EnemyObjective : MonoBehaviour
    {

        [SerializeField]
        private List<GameObject> enemies;
        [SerializeField]
        private ObjectiveSystem objective;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

            foreach (GameObject enemy in enemies.ToArray())
            {
                if (enemy == null)
                {
                    enemies.Remove(enemy);
                }
            }
            if (enemies.Count <= 0)
            {
                objective.Complete();
            }
        }
    }
}
