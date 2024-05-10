using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float diffiycultytime;
    public float diffiyculty;
    public GameObject[] enemyObjs;
    public Transform[] spawnPoints;
    public float MaxSpawnDelay;
    public float curSpawnDelay;

    public GameObject player;
    public GameObject itemObj;
    public Text scoreText;
    public GameObject gameOverSet;
    public GameObject boss;


    

        void Start()
    {
        Invoke("SpawnBoss", 60f);
    }

    // Update is called once per frame
    void Update()
    {
        curSpawnDelay += Time.deltaTime;
        diffiycultytime += Time.deltaTime;

        if(diffiycultytime >= diffiyculty)
        {
            diffiyculty -= 0.01f;
            diffiycultytime = 0;
            if(diffiyculty < 0.5)
            {
                diffiyculty = 2;
            }
        }

        if(curSpawnDelay > MaxSpawnDelay)
        {
            SpawnEnemy();
            MaxSpawnDelay = Random.Range(0.4f, diffiyculty);
            curSpawnDelay = 0;
            
        }
            
        

        //UI스코어
        Player playerLogic = player.GetComponent<Player>();
        scoreText.text = string.Format("{0:n0}", playerLogic.score);
    }

    void SpawnEnemy()
    {
        int randomEnemy = Random.Range(0, 4);
        int ranPoint = Random.Range(0, 16);
        GameObject enemy = Instantiate(enemyObjs[randomEnemy],
                           spawnPoints[ranPoint].position,
                           spawnPoints[ranPoint].rotation);

        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        enemy enemyLogic = enemy.GetComponent<enemy>();
        enemyLogic.player = player;

        if (ranPoint == 11 || ranPoint == 12 || ranPoint == 16)
        {
            if(randomEnemy == 0 || randomEnemy == 3)
            {
                enemy.transform.Rotate(Vector3.back * 90);
                rigid.velocity = new Vector2(enemyLogic.speed * (-1), Random.Range(-1, -5));
            }
            else
            {
                Destroy(enemy);
            }
        }
        else if (ranPoint == 13 || ranPoint == 14 || ranPoint == 15)
        {
            if(randomEnemy == 0 || randomEnemy == 3)
            {
                enemy.transform.Rotate(Vector3.forward * 90);
                rigid.velocity = new Vector2(enemyLogic.speed, Random.Range(-1, -5));
            }
            else
            {
                Destroy(enemy);
            }
        }
        else
        {
            rigid.velocity = new Vector2(0, enemyLogic.speed * (-1));
        }

    }

    public void SpawnBoss()
    {
        GameObject enemy = Instantiate(boss,
                           spawnPoints[17].position,
                           spawnPoints[17].rotation);

        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        enemy enemyLogic = enemy.GetComponent<enemy>();
        enemyLogic.player = player;
        rigid.velocity = new Vector2(0, enemyLogic.speed * (-1));

    }

    

    

    public void RespawnPlayer()
    {
        //player.SetActive(false);
        Invoke("RespawnPlayerexe", 2f);
        
    }
    public void RespawnPlayerexe()
    {
        //player.transform.position = Vector3.down * 3f;
        player.SetActive(true);
    }

    public void gameover()
    {
        gameOverSet.SetActive(true);
        player.SetActive(false);
    }

    public void Retry()
    {
        SceneManager.LoadScene(0);
    }
}
