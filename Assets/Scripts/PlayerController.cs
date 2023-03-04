using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public double dashSpeedFactor;
    public float maxRange;
    public Transform targeted;

    Vector2 pos;
    Transform target;
    Transform focusTo;
    Vector2 direction = new Vector2();



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

    public void OnMove(InputValue value)
    {
        direction = value.Get<Vector2>();
    }

    public void OnFire() 
    {

        if (Vector2.Distance(focusTo.position, transform.position) < GetComponent<SphereCollider>().radius * 6)
        {
            SetTarget(focusTo.transform);
            targeted.position = target.position;

        }
    }


    void Update()
    {
        Focus();

        if (focusTo != null)
        {
            Vector3 newPosition = new Vector3(focusTo.position.x, focusTo.position.y, focusTo.position.z - 5);
            targeted.position = newPosition;
        }

        else
        {
            Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
            targeted.position = newPosition;
        }

        //Si le joueur a selectionn� une cible, il fonce dessus
        if (target != null)
        {
            //Si il est sur la cible, il la d�truit
            if (Vector2.Distance(target.position, (Vector2)transform.position) <= 0.1)
            {
                Destroy(target.gameObject);
                Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
                targeted.position = newPosition;
            }

            //Sinon, il avance vers elle
            else
            {
                targeted.position = target.transform.position;
                goTo(target.position, 4);
            }

        }

        //Sinon, il va vers le curseur
        else
        {
            transform.Translate(direction.normalized * speed * Time.deltaTime);
        }
    }

    void goTo(Vector2 pos, float speedFactor = 1){
        Vector2 direction = (pos - (Vector2)transform.position);
        pos = (Vector2)transform.position + direction.normalized * speedFactor * speed * Time.deltaTime;
        transform.position = pos;
    }

    void Focus()
    {
        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        position.z = -2;
        Collider[] enemies = Physics.OverlapSphere(position, 50f);
        float distance = 0;
        float minDistance = 99999999;

        foreach (Collider enemy in enemies)
        {
            if (enemy.GetComponent<EnemyEntity>())
            {
                distance = Vector3.Distance(position, enemy.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    focusTo = enemy.transform;
                }
            }
        }
    }
}