using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroyer : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D theCollision)
    {
        if (theCollision.gameObject.tag == "MyBullets" || theCollision.gameObject.tag == "EnemyBullets")
        {
            BulletScript shell = theCollision.gameObject.GetComponent("BulletScript") as BulletScript;
            Destroy(theCollision.gameObject);
            
        }
    }
}
