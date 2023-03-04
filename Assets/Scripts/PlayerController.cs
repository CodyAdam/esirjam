using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public double dashSpeedFactor;
    public float maxRange;

    Vector3 pos;
    Transform target;
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

    public void OnMove(InputValue value){
        direction = value.Get<Vector2>();
        Debug.Log(direction);
    }

    public void MouseDown(){
        Collider[] enemies = Physics.OverlapSphere(Input.mousePosition, 10f);
        float distance = 0;
        float minDistance = 0;
        foreach(Collider enemy in enemies){
            if(enemy.tag == "Ennemy"){
                distance = Vector3.Distance(transform.position, enemy.transform.position);
                if(distance < minDistance){
                    minDistance = distance;
                    transform.GetComponent<PlayerController>().SetTarget(enemy.transform);
                }
            }
        }
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
                goTo(target.position, 4);
            }
            
        }

        //Sinon, il va vers le curseur
        else
        {
            var delta3d = new Vector3(direction.x, 0, direction.y);
            transform.Translate(delta3d.normalized*speed*Time.deltaTime);
        }
    }

    void goTo(Vector3 pos, float speedFactor = 1){
        Vector3 direction = (pos - transform.position);
        direction.y = 0;
        pos = transform.position + direction.normalized * speedFactor * speed * Time.deltaTime;
        transform.position = pos;
    }
}