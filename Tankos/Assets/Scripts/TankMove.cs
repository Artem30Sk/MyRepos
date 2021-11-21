using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Работа со сценами
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class TankMove : MonoBehaviour
{
	[Header("Скорость движения и вращения:")]
	[SerializeField] private float tankSpeed;
	[SerializeField] private float tankRotationSpeed;
	[SerializeField] public float health;
	[Header("Оружие:")]
	[SerializeField] private float reloadTime;
	[SerializeField] private BulletScript tankShell;
	[SerializeField] private Transform tankShellPoint; // откуда вылетают снаряды
	[SerializeField] private GameObject shootka;
	[SerializeField] private AudioClip shootsound;
	[Header("Башня танка:")]
	[SerializeField] private Transform turret;
	[SerializeField] private float turretSpeed;
	private Rigidbody2D body;
	private float shotTime = Mathf.Infinity;
	private bool canShot;
	public Image bar;
	private float fill;
	void Awake()
	{
		
		shootka.SetActive(false);
		body = GetComponent<Rigidbody2D>();
		body.gravityScale = 0;
		fill = 1f;
	}

	void FixedUpdate()
	{
		float v = Input.GetAxis("Vertical");
		float h = Input.GetAxis("Horizontal");
		body.AddForce(transform.right * tankSpeed * v, ForceMode2D.Impulse);
		body.AddTorque(tankRotationSpeed * h * -Mathf.Sign(v), ForceMode2D.Impulse);
	}

	Quaternion TurretRotation()
	{
		Vector3 mouse = Input.mousePosition;
		mouse.z = Camera.main.transform.position.z;
		Vector3 direction = Camera.main.ScreenToWorldPoint(mouse) - transform.position;
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		return Quaternion.AngleAxis(angle, Vector3.forward);
	}

	void CanShot()
	{
		if (canShot) return;
		shotTime += Time.deltaTime;

		if (shotTime > reloadTime)
		{
			shotTime = 0;
			canShot = true;
		}
	}
	void Vis()
    {
		
		shootka.SetActive(false);
	}
	void TankShot()
	{	
		if (Input.GetMouseButtonDown(0) && canShot)
		{
			GetComponent<AudioSource>().PlayOneShot(shootsound);
			shootka.SetActive(true);
			Invoke("Vis", 0.2f);
			canShot = false;
			float angle = Mathf.Atan2(tankShellPoint.right.y, tankShellPoint.right.x) * Mathf.Rad2Deg;
			BulletScript shell = Instantiate(tankShell, tankShellPoint.position, Quaternion.AngleAxis(angle, Vector3.forward)) as BulletScript;
			shell.SetDirection(tankShellPoint.right);
			
		}
	}

	void Update()
	{
		
		bar.fillAmount = fill;
			CanShot();
			TankShot();

		turret.rotation = Quaternion.Lerp(turret.rotation, TurretRotation(), turretSpeed * Time.deltaTime);
		
		
	}
	void OnCollisionEnter2D(Collision2D theCollision)
	{
		if (theCollision.gameObject.tag == "EnemyBullets")
		{
			BulletScript shell = theCollision.gameObject.GetComponent("BulletScript") as BulletScript;
			health -= shell.damage;
			fill -= (shell.damage/200);
			Destroy(theCollision.gameObject);
			if (health <= 0)
			{
				bar.fillAmount = 0;
				Destroy(gameObject);
				
			}
		}
	}
}
