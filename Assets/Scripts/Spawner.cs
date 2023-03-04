using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform enemy;
    public float spawnTimer;
    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timer >= spawnTimer)
        {
            Spawn();
            timer = 0;
        }
        timer += Time.deltaTime;
    }

    void Spawn()
    {
        GameObject newEnemy = Instantiate(this.enemy.gameObject, transform.position, transform.rotation);
        newEnemy.GetComponent<Minion>().SetTarget(GameManager.instance.player.transform);
    }
}
