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
        speed = 7f;
    }

    void OnBecameInvisible(){
        Destroy(gameObject);
    }
    public void OnTriggerEnter(Collider col){
        if(col.gameObject.tag == "Player"){
            var player = col.GetComponent<PlayerController>();
            player.hit();
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(new Vector2(0, 1f) * speed / 60.0f);
    }
}
