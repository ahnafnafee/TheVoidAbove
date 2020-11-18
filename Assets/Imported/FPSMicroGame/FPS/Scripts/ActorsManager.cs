using System.Collections.Generic;
using UnityEngine;

namespace Imported.FPSMicroGame.FPS.Scripts
{
    public class ActorsManager : MonoBehaviour
    {
        public List<Actor> actors { get; private set; }

        private void Awake()
        {
            actors = new List<Actor>();
        }
    }
}
