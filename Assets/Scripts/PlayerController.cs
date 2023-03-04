using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public double dashSpeedFactor;

    Vector2 pos;
    Transform target;
    Vector2 direction = new Vector2();



    public Transform GetTarget()
    {
        return target;
    } 

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        transform.DOMove(target.position, .2f).SetEase(Ease.InCirc).OnComplete(() => {
            Destroy(target.gameObject);
        });
    }



    private void Start()
    {
        target = null;
    }

    public void OnMove(InputValue value){
        direction = value.Get<Vector2>();
    }

    void Update()
    {
        //Si le joueur a selectionn� une cible, il fonce dessus
        if(target == null){
            transform.Translate(direction.normalized*speed*Time.deltaTime);
        }
    }

    void goTo(Vector2 pos, float speedFactor = 1){
        Vector2 direction = (pos - (Vector2)transform.position);
        pos = (Vector2)transform.position + direction.normalized * speedFactor * speed * Time.deltaTime;
        transform.position = pos;
    }
}