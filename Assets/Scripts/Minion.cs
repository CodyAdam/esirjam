using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : EnemyEntity
{
    public Transform bullet;

    Transform target;
    float fireRate = 1f;
    float countdown = 0f;

    public Transform GetTarget()
    {
        return target;
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    private void Update(){
        if(countdown >= fireRate)
        {
            Shoot();
            countdown = 0;
        }

        countdown += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            //Si il est sur la cible, il la d�truit

            if (Vector2.Distance(target.position, transform.position) <= 0.1)
            {
                // Destroy(gameObject);
            }

            //Sinon, il se tourne vers elle et avance
            else
            {
                Vector2 direction = ((Vector2)target.position - (Vector2)transform.position).normalized;
                transform.Translate(new Vector2(0, speed /60.0f));
                float rot_z = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
            }

        }
    }


    private void Shoot()
    {
        GameObject newBullet = Instantiate(this.bullet.gameObject, transform.position, transform.rotation);
        newBullet.GetComponent<Bullet>();
    }
}
