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
            //Si il est sur la cible, il la détruit
            if (Vector3.Distance(target.position, transform.position) <= 0.1)
            {
                Destroy(gameObject);
            }

            //Sinon, il avance vers elle
            else
            {
                Vector3 direction = (target.position - transform.position).normalized;
                pos = transform.position + direction * speed * Time.deltaTime;
                pos.y = transform.position.y;
                transform.position = pos;

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
        GameObject newBullet = Instantiate(this.bullet.gameObject, transform.position, transform.rotation);
        newBullet.GetComponent<Bullet>().SetDirection((target.position - transform.position).normalized);
    }
}
