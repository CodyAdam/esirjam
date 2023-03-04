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
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timer <= 0)
        {
            Destroy(this.gameObject);
            return;
        }

        else
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }
}
