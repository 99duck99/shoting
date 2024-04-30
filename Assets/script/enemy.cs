using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public string enemyName;
    public float speed;
    public int health;
    public Sprite[] sprites;
    SpriteRenderer spriteRenderer;
    public GameObject player;
    
    public GameObject bulletObjB;
    public float MaxShotDelay;
    public float curShotDelay;

    
    void Update()
    {
        shot();
        reload();
    }

    void Awake() 
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    void OnHit(int dmg)
    {
        health -= dmg;
        spriteRenderer.sprite = sprites[1];
        Invoke("ReturnSprite", 0.1f);
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }


    void ReturnSprite()
    {
        spriteRenderer.sprite = sprites[0];
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "BorderBullet")
        {
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "Bullet")
        {
            bullet bullet = collision.gameObject.GetComponent<bullet>();
            OnHit(bullet.dmg);
        }
    }

    void shot()
    {
        if(MaxShotDelay > curShotDelay)
           return;

        if(enemyName == "A")
        {
            GameObject bullet = Instantiate(bulletObjB, transform.position, transform.rotation);
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();

            Vector3 dirVec = player.transform.position - transform.position;
            rigid.AddForce(dirVec.normalized * 4, ForceMode2D.Impulse);
        }
        if(enemyName == "b")
        {
            GameObject bulletR = Instantiate(bulletObjB, transform.position + Vector3.right*0.3f, transform.rotation);
            Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();

            Vector3 dirVecR = player.transform.position - transform.position + Vector3.right*0.3f;
            rigidR.AddForce(dirVecR.normalized * 1, ForceMode2D.Impulse);

            GameObject bulletL = Instantiate(bulletObjB, transform.position + Vector3.left*0.3f, transform.rotation);
            Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();

            Vector3 dirVecL = player.transform.position - transform.position + Vector3.left*0.3f;
            rigidL.AddForce(dirVecL.normalized * 1, ForceMode2D.Impulse);
        }

        curShotDelay = 0;
    }

    void reload()
    {
        curShotDelay += Time.deltaTime;
    }
    
}
