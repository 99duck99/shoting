using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyshot : MonoBehaviour
{
    public int dmg;
   
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "BorderBullet" || collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
