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

    private void Update()
    {
        if (target != null)
        {
            //Si il est sur la cible, il la d�truit
            if (Vector2.Distance(target.position, transform.position) <= 0.1)
            {
                Destroy(gameObject);
            }

            //Sinon, il avance vers elle
            else
            {
                Vector2 direction = ((Vector2)target.position - (Vector2)transform.position).normalized;
                transform.Translate(direction * speed * Time.deltaTime);

                if(countdown >= fireRate)
                {
                    Shoot();
                    countdown = 0;
                }

                countdown += Time.deltaTime;
            }

        }
    }

    private void Shoot()
    {
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z - 2);
        GameObject newBullet = Instantiate(this.bullet.gameObject, newPosition, transform.rotation);
        newBullet.GetComponent<Bullet>().SetDirection((target.position - transform.position).normalized);
    }
}
