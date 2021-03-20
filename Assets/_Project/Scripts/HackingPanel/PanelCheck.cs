using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace _Project.Scripts
{
    public class PanelCheck : MonoBehaviour
    {
        [SerializeField]
        private float stayTimer;
        [SerializeField]
        private ObjectiveSystem objective;
        [SerializeField]
        private GameObject progessUI;
        [SerializeField]
        private Image progressBar;

        [SerializeField] private TextMeshProUGUI progressStatus;

        [SerializeField]
        private HudManager hudManager;
        [SerializeField]
        private string objName;

        [SerializeField]
        private List<GameObject> enemies;

        private bool startTimer;
        private float timer;
        private bool isDone;
        private bool spawned;
        // Start is called before the first frame update
        void Start()
        {
            spawned = false;
            timer = stayTimer;
            isDone = false;
            progressBar.fillAmount = 0;
            startTimer = false;
        }

        // Update is called once per frame
        void Update()
        {
            progressStatus.text = isDone ? "Hacking Complete" : "Hacking in Progress";

            if(startTimer && !isDone)
            {
                if(stayTimer <= 0)
                {
                    isDone = true;
                    Destroy(gameObject.GetComponent<Hud>());
                    hudManager.UpdateHudList(objName);
                    objective.Complete();
                }
                if(stayTimer <= timer / 2 && ! spawned)
                {
                    spawnEnemies(enemies);
                    spawned = true;
                }
                stayTimer -= Time.deltaTime;
                progressBar.fillAmount = stayTimer/timer;
            }
            else
            {
                stayTimer = timer;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.tag.Equals("Player"))
            {
                progessUI.SetActive(true);
                startTimer = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag.Equals("Player"))
            {
                progessUI.SetActive(false);
                startTimer = false;
                stayTimer = timer;
                progressBar.fillAmount = 0;
            }
        }

        public bool checkDone()
        {
            return isDone;
        }
        public void spawnEnemies(List<GameObject> enemyList)
        {
            foreach(GameObject enemy in enemyList)
            {
                GameObject newObject = enemy;
                Instantiate(newObject);
                newObject.SetActive(true);
            }
        }
    }
}
