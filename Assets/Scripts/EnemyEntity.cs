using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            var player = col.GetComponent<PlayerController>();
            player.hit();
            Destroy(gameObject);
        }
    }

    void Start()
    {
        GameManager.instance.enemies.Add(gameObject);
    }

}
