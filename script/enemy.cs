using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public string enemyName;
    public int enemyScore;
    public float speed;
    public int health;
    public Sprite[] sprites;
    SpriteRenderer spriteRenderer;
    public GameObject player;
    
    public GameObject bulletObjB;
    public int bullet3 = 0;
    public float MaxShotDelay;
    public float curShotDelay;

    public GameObject lifeitem;
     GameManager gm;
    public int patternIndex;
    public int sum;
    
    void Start()
    {
        if(enemyName == "c")
        {
            Invoke("Think", 5);
        }
        
        
    }
    void Update()
    {
        if(enemyName == "c")
        {
            return;   
        }
        shot();
        reload();
    }

    void Awake() 
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        if(enemyName == "c")
        {
            Invoke("stop", 0.6f);
        }
    }

    void stop()
    {
        if(!gameObject.activeSelf)
        {
            return;
        }
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.zero;
    }
    
    void OnHit(int dmg)
    {
        health -= dmg;
        spriteRenderer.sprite = sprites[1];
        Invoke("ReturnSprite", 0.1f);
        if(health <= 0)
        {
            Player playerLogic = player.GetComponent<Player>();
            playerLogic.score += enemyScore;
            Destroy(gameObject);
            if(enemyName == "c")
            {
                Invoke("Bossspawn", 60f);
            }
            //아이템 생성 로직
            int ranPoint = Random.Range(0, 16);
            if(ranPoint > 14)
            {
                Instantiate(lifeitem, transform.position, Quaternion.identity);
            }
        }
    }
    void Bossspawn()
    {
        gm.SpawnBoss();
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

    void Think()
    {
        patternIndex = Random.Range(0, 3);
        switch(patternIndex)
        {
            case 0:
            FireFoward();
            break;
            case 1:
            FireShot();
             break;
            case 2:
            FireArc();
            break;
            case 3:
            FireAround();
             break;
        }
    }

    void FireFoward()
    {
         Debug.Log("실행");
        
        //총알 4개
        GameObject bulletRR = Instantiate(bulletObjB, transform.position + Vector3.right*0.3f, transform.rotation);
        GameObject bulletR = Instantiate(bulletObjB, transform.position + Vector3.right*0.6f, transform.rotation);
        GameObject bulletLL = Instantiate(bulletObjB, transform.position + Vector3.left*0.3f, transform.rotation);
        GameObject bulletL = Instantiate(bulletObjB, transform.position + Vector3.left*0.6f, transform.rotation);
            Rigidbody2D rigidRR = bulletRR.GetComponent<Rigidbody2D>();
            Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
            Rigidbody2D rigidLL = bulletLL.GetComponent<Rigidbody2D>();
            Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
            rigidRR.AddForce(Vector2.down * 8, ForceMode2D.Impulse);
            rigidL.AddForce(Vector2.down * 8, ForceMode2D.Impulse);
            rigidR.AddForce(Vector2.down * 8, ForceMode2D.Impulse);
            rigidLL.AddForce(Vector2.down * 8, ForceMode2D.Impulse);
        Invoke("Think", 2);
    }

    void FireShot()
    {
         Debug.Log("3실행");
        for(int i = 0; i < 23; i++)
        {
            Invoke("ing", 0.15f);
        }
        sum = 0;
        Invoke("Think", 4);
        
    }

    void FireArc()
    { 
        bullet3 = 0;
        Invoke("ingg", 0.15f);
        
    }

    void FireAround()
    {
        Debug.Log("4실행");
        Invoke("Think", 4);
    }

    void reload()
    {
        curShotDelay += Time.deltaTime;
    }

    void ing()
    {
        
        GameObject bullet = Instantiate(bulletObjB, transform.position, Quaternion.identity);
        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        Vector2 dirVec = new Vector2(Mathf.Sin(sum) ,-1);
        rigid.AddForce(dirVec.normalized * 4, ForceMode2D.Impulse);
        sum++;
    }

    void ingg()
    {
            if(bullet3 < 25)
            {
            int index = Random.Range(1, 20);
            GameObject bullet = Instantiate(bulletObjB, transform.position, Quaternion.identity);
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector2 dirVec = new Vector2(Mathf.Sin(index), -1);
            rigid.AddForce(dirVec.normalized * 4, ForceMode2D.Impulse);
            Invoke("ingg", 0.15f);
            bullet3++;
            }
    }
    
}
