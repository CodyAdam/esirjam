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
                goTo(target.position);
            }
            
        }

        //Sinon, il va vers le curseur
        else
        {
            goTo(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }

    void goTo(Vector3 pos, float speedFactor = 1){
        Vector3 direction = (pos - transform.position);
        direction.y = 0;
        pos = transform.position + direction.normalized * speedFactor * speed * Time.deltaTime;
        transform.position = pos;
    }
}