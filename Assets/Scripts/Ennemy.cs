using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy : MonoBehaviour
{
    float speed = 5f;
    Color startColor;
    Transform target;
    Vector3 pos;

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
        startColor = GetComponent<Renderer>().material.color;
    }

    private void OnMouseOver()
    {
        GetComponent<Renderer>().material.color = Color.red;
    }

    private void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = startColor;
    }

    private void OnMouseDown()
    {
        GameManager.instance.player.GetComponent<PlayerController>().SetTarget(transform);
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
            }

        }
    }
}
