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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
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
        
}
