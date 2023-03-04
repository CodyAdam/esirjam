using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : EnemyEntity
{

    Vector2 direction;
    float timer = 3f; 

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.bullets.Add(gameObject);
    }

    void OnBecameInvisible(){
        Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D col) {
        if(col.gameObject.tag == "Player"){
            var player = col.GetComponent<PlayerController>();
            player.hit();
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(timer <= 0)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            transform.Translate(new Vector2(0, 1f) * speed / 60.0f);
        }
    }
}
