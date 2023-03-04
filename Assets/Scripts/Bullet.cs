using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : EnemyEntity
{
    public float speed;
    Vector2 direction;

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

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(new Vector2(0, 1f) * speed / 60.0f);
    }
}
