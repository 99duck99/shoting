using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public bool isTouchTop;
    public bool isTouchBottom;
    public bool isTouchRight;
    public bool isTouchLeft;

    public GameObject bulletObjA;
    public GameObject bulletObjB;

    public float life;
    public int score;
    SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    Color originalColor;
    
   
    

    public GameManager gm;
    

    // Start is called before the first frame update
    void Start()
    {
        originalColor = spriteRenderer.color;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }
    void Awake() 
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if((isTouchRight && h == 1) || (isTouchLeft && h == -1))
        {
            h = 0;
        }
        float v = Input.GetAxisRaw("Vertical");
        if((isTouchTop && v == 1) || (isTouchBottom && v == -1))
        {
            v = 0;
        }
        Vector3 curPos = transform.position;
        Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime;

        transform.position = nextPos + curPos;
    }

    void Fire()
    {
        if(!Input.GetButtonUp("Jump"))
        {
            return;
        }
        GameObject bullet = Instantiate(bulletObjA, transform.position, transform.rotation);
        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
    }

    void Hit(int dmg)
    {
        life -= dmg;
        spriteRenderer.color = new Color(1, 1, 1, 0.5f); //spriteRenderer.sprite = sprites[1];
        Invoke("ReturnSprite", 2f);
        gameObject.tag = "Godmode";
        Invoke("tc", 2f);
        
        if(life == 0)
        {
            gm.gameover();
        }
        else if(life > 0)
        {
            gm.RespawnPlayer();
        }

    }
    public void ReturnSprite()
    {
        spriteRenderer.sprite = sprites[0];
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                case "Top":
                isTouchTop = true;
                break;
                case "Bottom":
                isTouchBottom = true;
                break;
                case "Right":
                isTouchRight = true;
                break;
                case "Left":
                isTouchLeft = true;
                break;
            }
        }
        if(gameObject.tag == "Player")
        {
            if(collision.gameObject.tag == "EnemyBullet")
        {
            enemyshot es = collision.gameObject.GetComponent<enemyshot>();
            if (es != null)
            {
                Hit(es.dmg);
                Destroy(collision.gameObject);
            }
            


        }
        if(collision.gameObject.tag == "Enemy")
        {
            Hit(1);
        }
        }
        if(life > 5)
        {
            lifeitem item = collision.gameObject.GetComponent<lifeitem>();
            if(collision.gameObject.tag == "lifeitem")
            {
                life++;
                Destroy(collision.gameObject);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision) {
        if(collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                case "Top":
                isTouchTop = false;
                break;
                case "Bottom":
                isTouchBottom = false;
                break;
                case "Right":
                isTouchRight = false;
                break;
                case "Left":
                isTouchLeft = false;
                break;
            }
        }
    }
    
    void tc()
    {
        gameObject.tag = "Player";
        spriteRenderer.color = originalColor;
    }

    
        
}

