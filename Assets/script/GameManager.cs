using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float diffiycultytime;
    public float diffiyculty;
    public GameObject[] enemyObjs;
    public Transform[] spawnPoints;
    public float MaxSpawnDelay;
    public float curSpawnDelay;

    public GameObject player;

        void Start()
    {
        
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

    public void RespawnPlayer()
    {
        Invoke("RespawnPlayerexe", 2f);
        player.transform.position = Vector3.down * 3;
        player.SetActive(true);
    }
    public void RespawnPlayerexe()
    {
        player.transform.position = Vector3.down * 3;
        player.SetActive(true);
    }
}
