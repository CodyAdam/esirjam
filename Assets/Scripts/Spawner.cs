using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform enemy;
    public float spawnTimer;
    float timer = 0;
    int difficulty = 1;
    float timeDifficulty = 15;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Difficulty" + difficulty);
        if (timer >= spawnTimer)
        {
            for(int i = 0;i < difficulty; i++) { Spawn(); }
            timer = 0;
        }

        timer += Time.deltaTime;
        timeDifficulty -= Time.deltaTime;

        if(timeDifficulty < 0) { 
            difficulty++;
            Debug.Log("Difficulty" + difficulty);
            timeDifficulty = 15;
        }
    }

    void Spawn()
    {
        GameObject newEnemy = Instantiate(this.enemy.gameObject, transform.position, transform.rotation);
        newEnemy.GetComponent<Minion>().SetTarget(GameManager.instance.player.transform);
    }
}
