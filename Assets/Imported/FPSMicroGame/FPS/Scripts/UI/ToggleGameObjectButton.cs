using UnityEngine;
using UnityEngine.EventSystems;

namespace Imported.FPSMicroGame.FPS.Scripts.UI
{
    public class ToggleGameObjectButton : MonoBehaviour
    {
        public GameObject objectToToggle;
        public bool resetSelectionAfterClick;

        void Update()
        {
            if (objectToToggle.activeSelf && Input.GetButtonDown(GameConstants.k_ButtonNameCancel))
            {
                SetGameObjectActive(false);
            }
        }

        public void SetGameObjectActive(bool active)
        {
            objectToToggle.SetActive(active);

            if (resetSelectionAfterClick)
                EventSystem.current.SetSelectedGameObject(null);
        }
    }
}
