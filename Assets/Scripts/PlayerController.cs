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
        //Si le joueur a selectionné une cible, il fonce dessus
        if(target != null)
        {
            //Si il est sur la cible, il la détruit
            if(Vector3.Distance(target.position, transform.position) >= 0.1)
            {
                //Destruction()
            }

            //Sinon, il avance vers elle
            else
            {
                Vector3 direction = (target.position - transform.position).normalized;
                pos = transform.position + direction * 2 * speed * Time.deltaTime;
                pos.y = transform.position.y;
                transform.position = pos;
            }
            
        }

        //Sinon, il va vers le curseur
        else
        {
            Vector3 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
            pos = transform.position + direction * speed * Time.deltaTime;
            pos.y = transform.position.y;
            transform.position = pos;

            
        }
    }
}