using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : EnemyEntity
{
    Vector2 direction;

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection;
    }

    void OnBecameInvisible(){
        Destroy(gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(new Vector2(0, 1f) * GameManager.instance.bulletSpeed / 60.0f);
    }
}
