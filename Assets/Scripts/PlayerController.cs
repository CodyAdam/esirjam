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
    float level = 1;



    public Transform GetTarget()
    {
        return target;
    } 

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        transform.DOMove(target.position, .2f).SetEase(Ease.InCirc).OnComplete(() => {
            Destroy(target.gameObject);
            level += .1f;
        });
    }
    public float getLevel(){
        return level;
    }
    public void hit(){
        level -= .1f;
    }

    public float getSize(){
        return level;
    }


    private void Start()
    {
        target = null;
    }

    public void OnMove(InputValue value){
        direction = value.Get<Vector2>();
    }

    void FixedUpdate()
    {
        //Si le joueur a selectionn� une cible, il fonce dessus
        if(target == null){
            transform.Translate(direction.normalized*speed/60.0f);
        }
        transform.localScale = new Vector3(getSize(), getSize(), getSize());
    }

    void goTo(Vector2 pos, float speedFactor = 1){
        Vector2 direction = (pos - (Vector2)transform.position);
        pos = (Vector2)transform.position + direction.normalized * speedFactor * speed * Time.deltaTime;
        transform.position = pos;
    }
}