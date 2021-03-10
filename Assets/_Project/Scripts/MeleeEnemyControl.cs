using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace _Project.Scripts
{
	public class MeleeEnemyControl : MonoBehaviour
	{
		public int status; // 0 means finding the player, 1 means moving to player , 2 means attack player.
		public int damage;
		public float timeToChangeDirection;
		public float speed;
		public float speed_rotate;
		public float speed_rush;
		public float angle_new;
		public float angle_current;
		public float range_melee;
		public float range_attack;
		public float time_attack = 0.5f;
		public float time_attack_current = 0.8f;

		public float time_dash;
		public float time_dash_current;
		public float range_area;


		private Vector3 pos_player;
		public Image healthFillImage;
		[SerializeField] private GameObject hitParticlePrefab1;
		[SerializeField] private GameObject hitParticlePrefab2;
		public GameObject mark_aggro;
		public Vector3 pos_ori;
		private Health enemyHealth;
		private int hit_count;
		// Start is called before the first frame update
		void Start()
		{
			time_dash = 3.0f;
			time_dash_current = -0.1f;
			pos_ori = transform.position;
			hit_count = 0;
			pos_player = GameObject.Find("Player").GetComponent<Player>().transform.position;
			speed = 20;
			speed_rush = 180;
			status = 0;
			damage = 5;
			range_melee = 30f;
			range_attack = 45f;
			time_attack = 1.2f;
			time_attack_current = time_attack;
			angle_new = transform.rotation.y;
			ChangeDirection();
			enemyHealth = GetComponent<Health>();
		}

		// Update is called once per frame
		void Update()
		{
			if (Vector3.Distance(pos_ori, transform.position) >= range_area)
			{
				status = 3;
			}
			switch (status)
			{
				case 0:
					OnFinding();
					break;
				case 1:
					OnMoving();
					break;
				case 2:
					OnAttack();
					break;
				case 3:
					Back();
					break;
				default:
					break;
			}

			healthFillImage.fillAmount = enemyHealth.objectHealth / enemyHealth.health;
		}
		private void ChangeDirection()
		{
			angle_new = Random.Range(-180f, 180f);
			//transform.Rotate(0f, newY, 0f, 0f);
			//transform.rotation = Quaternion.Slerp(transform.rotation, new Quaternion(0f, newY, 0f, 0f), speed_rotate * Time.deltaTime);
			timeToChangeDirection = 10f;
		}
		private void OnTriggerEnter(Collider collision)
		{
			if (collision.gameObject.tag == "Player")
			{
				if (status == 0)
				{
					status = 1;
					pos_player = GameObject.Find("Player").GetComponent<Player>().transform.position;
					CreateMark();
				}
				Debug.Log("I see player");
			}
		}

		private void OnTriggerExit(Collider collision)
		{
			if (collision.gameObject.tag == "Player")
			{
				if (status == 1)
					status = 0;
				Debug.Log("I lost player");
			}
		}

		private void OnCollisionEnter(Collision collision)
		{
			if (collision.transform.CompareTag("Bullet"))
			{
				ContactPoint contact = collision.contacts[0];
				Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
				Vector3 position = contact.point;


				if (collision.gameObject.name.Contains("Bullet 3"))
				{
					if (GameObject.Find("Player").GetComponent<Player>().isPowered())
					{
						Instantiate(hitParticlePrefab2, position, rotation);
						enemyHealth.TakeDamage(4);
					}
					else
					{
						Instantiate(hitParticlePrefab1, position, rotation);
						enemyHealth.TakeDamage(1);
					}
					if (status == 0)
					{
						status = 1;
						pos_player = GameObject.Find("Player").GetComponent<Player>().transform.position;
						CreateMark();
					}
				}
			}
		}
		private void OnFinding()
		{
			if ((transform.eulerAngles.y - 360f) < -180f)
			{
				angle_current = transform.eulerAngles.y;
			}
			else
			{
				angle_current = transform.eulerAngles.y - 360f;
			}
			timeToChangeDirection -= Time.deltaTime;

			if (timeToChangeDirection <= 0.0f)
			{

				ChangeDirection();
			}
			if (Mathf.Abs(angle_new - angle_current) >= 1.0f * speed_rotate)
			{
				if ((angle_new - transform.rotation.y) > 0.0f)
				{
					transform.Rotate(0f, 1f * speed_rotate, 0f, 0f);
				}
				else
				{
					transform.Rotate(0f, -1f * speed_rotate, 0f, 0f);
				}
			}

			//GetComponent<Rigidbody>().velocity = transform.forward * 10f;
			transform.Translate(Vector3.forward * Time.deltaTime * speed);
		}
		private void OnMoving()
		{


			if (Vector3.Distance(pos_player, transform.position) >= range_melee)
			{
				if (time_dash_current > 0.0f)
				{
					pos_player = GameObject.Find("Player").GetComponent<Player>().transform.position;
					time_dash_current -= Time.deltaTime;
					transform.position = Vector3.MoveTowards(transform.position, pos_player, 0.22f * speed_rush * Time.deltaTime);
					//transform.position = Vector3.MoveTowards(transform.position, origianlPosition, 2*  enemySpeed * Time.deltaTime);
					Vector3 direction = GameObject.Find("Player").transform.position - transform.position;
					if (direction != Vector3.zero)
						transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 2 * Time.deltaTime * speed_rush);
				}
				else
				{
					transform.position = Vector3.MoveTowards(transform.position, pos_player, speed_rush * Time.deltaTime);
					//transform.position = Vector3.MoveTowards(transform.position, origianlPosition, 2*  enemySpeed * Time.deltaTime);
					Vector3 direction = GameObject.Find("Player").transform.position - transform.position;
					if (direction != Vector3.zero)
						transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 2 * Time.deltaTime * speed_rush);
				}
			}
			else
			{
				status = 2;
				time_dash_current = time_dash;
				time_attack_current = time_attack;
			}
		}

		private void OnAttack()
		{

			if (time_attack_current > 0.0f)
			{
				time_attack_current -= Time.deltaTime;
			}
			else
			{
				if (Vector3.Distance(GameObject.Find("Player").transform.position, transform.position) <= range_attack)
				{
					AkSoundEngine.PostEvent("blade_slice_event", this.gameObject);
					GameObject.Find("Player").gameObject.GetComponent<Health>().TakeDamage(damage);
				}
				status = 1;
				pos_player = GameObject.Find("Player").GetComponent<Player>().transform.position;
			}
		}

		private void CreateMark() {
			Vector3 pos_new = transform.position + new Vector3(0f,50f,0f);
			GameObject mark = Instantiate(mark_aggro, pos_new, transform.rotation);
			mark.transform.SetParent(transform);
			pos_player = GameObject.Find("Player").GetComponent<Player>().transform.position;

		}
		private void Back()
		{
			if (Vector3.Distance(pos_ori, transform.position) >= 1.0f)
			{
				transform.position = Vector3.MoveTowards(transform.position, pos_ori, 0.7f*speed_rush * Time.deltaTime);
				Vector3 direction = pos_ori - transform.position;
				if (direction != Vector3.zero)
					transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 2 * Time.deltaTime * speed_rush);
				//transform.position = Vector3.MoveTowards(transform.position, origianlPosition, 2*  enemySpeed * Time.deltaTime);
			}
			else {
				status = 0;
			}

		}
	}
}

