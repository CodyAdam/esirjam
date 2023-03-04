using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;

    Vector3 pos;
    Transform target;



    public Transform GetTarget()
    {
        return target;
    } 

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }



    private void Start()
    {
        target = null;
    }

    void Update()
    {
        //Si le joueur a selectionn� une cible, il fonce dessus
        if(target != null)
        {
            //Si il est sur la cible, il la d�truit
            if(Vector3.Distance(target.position, transform.position) <= 0.1)
            {
                Destroy(target.gameObject);
            }

            //Sinon, il avance vers elle
            else
            {
                Vector3 direction = (target.position - transform.position);
                direction.y = 0;
                pos = transform.position + direction.normalized * 4 * speed * Time.deltaTime;
                transform.position = pos;
            }
            
        }

        //Sinon, il va vers le curseur
        else
        {
            Vector3 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
            direction.y = 0;
            pos = transform.position + direction.normalized * speed * Time.deltaTime;
            transform.position = pos;
        }
    }
}