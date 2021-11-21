using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(HPObject))]
public class TurretSqript : MonoBehaviour
{
    [Header("Оружие:")]
    [SerializeField] private float reloadTime;
    [SerializeField] private BulletScript tankShell;
    [SerializeField] private Transform tankShellPoint; // откуда вылетают снаряды
    [SerializeField] private GameObject shootka;
    [SerializeField] private AudioClip shootsound;
    [SerializeField] private Transform target;
    [SerializeField] private Transform turretHead;
    [SerializeField] private float health;
    public Transform explosion;
    private HPObject HP;
    private bool distanceshot;
    void Awake()
    {
        shootka.SetActive(false);
        HP = GetComponent<HPObject>();
        
        distanceshot = false;
    }

    private void Update()
    {
       
        if (HP.currentHP <= 0)
        {
            Destroy(gameObject);
        }
        TankShot();
    }
    // скорость вращения
    public float rotationSpeed = 1F;
    //мертвая зона вращения (чтобы турель не дергалась при x=0)
    public float deadZone = 0.1F;
    //направление вращения ( "0" - не вращать, "1" - вправо и "-1" - влево)
    private float rotateDirection = 0;
    private bool canshoot = false;
    public float attackMaximumDistance = 50.0f; //дистанция атаки
    public float attackMinimumDistance = 5.0f;      //мертвая зона
    private float nextTime = 0.0F;
    private float timeRate = 0.5F;

    void LateUpdate()
    {
        float squaredDistance = (turretHead.position - target.transform.position).sqrMagnitude;
        if (Mathf.Pow(attackMinimumDistance, 2) < squaredDistance && squaredDistance < Mathf.Pow(attackMaximumDistance, 2)) //если дистанция больше мертвой зоны и меньше дистанции поражения пушки
        {
            distanceshot = true;
            if (reloadTime > 0) reloadTime -= Time.deltaTime; //если таймер перезарядки больше нуля - отнимаем его
            if (reloadTime <= 0) TankShot();
            if (transform.InverseTransformPoint(target.position).x > deadZone / 2) rotateDirection = -1F;
            else if (transform.InverseTransformPoint(target.position).x < -deadZone / 2) rotateDirection = 1F;
            else
            {
                if (transform.InverseTransformPoint(target.position).y < 0) rotateDirection = 1F;
                else rotateDirection = 0;
            }

            transform.rotation *= Quaternion.Euler(0, 0, rotationSpeed * rotateDirection);
        }
        
        else
        {
           
            distanceshot = false;
        }
    }
    void Vis()
    {

        shootka.SetActive(false);
    }
    
    void TankShot()
    {

        if ((distanceshot==true) && (Time.time > nextTime))
        {
            nextTime = Time.time + timeRate;

            GetComponent<AudioSource>().PlayOneShot(shootsound);
            shootka.SetActive(true);
            Invoke("Vis", 0.2f);
            float angle = Mathf.Atan2(tankShellPoint.right.y, tankShellPoint.right.x) * Mathf.Rad2Deg;
            BulletScript shell = Instantiate(tankShell, tankShellPoint.position, Quaternion.AngleAxis(angle, Vector3.forward)) as BulletScript;
            shell.SetDirection(tankShellPoint.right);
        }
    }
    void OnCollisionEnter2D(Collision2D theCollision)
    {
        if (theCollision.gameObject.tag == "MyBullets")
        {
            BulletScript shell = theCollision.gameObject.GetComponent("BulletScript") as BulletScript;
            health -= shell.damage;
            Destroy(theCollision.gameObject);
            if(health<=0)
            {
                if (explosion) ;
                {
                    GameObject exploder = ((Transform)Instantiate(explosion, this.transform.position, this.transform.rotation)).gameObject;
                    Destroy(exploder, 2.0f);
                }
                Destroy(gameObject);
                GameController controller = GameObject.FindGameObjectWithTag("GameController").GetComponent("GameController") as GameController;
                controller.IncreaseScore(10);
               
            }
        }
    }
}
