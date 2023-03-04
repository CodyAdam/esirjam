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
    bool isDashing = false;



    public Transform GetTarget()
    {
        return target;
    } 

    public void SetTarget(Transform newTarget)
    {
        if(isDashing)
            return;
        isDashing = true;
        target = newTarget;
        transform.DOMove(target.position, .2f).SetEase(Ease.InCirc).OnComplete(() => {
            level += .1f;
            isDashing = false;
            Destroy(target.gameObject);
        });
    }
    public bool IsDashing(){
        return isDashing;
    }
    public float GetLevel(){
        return level;
    }

    public void SetLevel(float newLevel)
    {
        level = newLevel;
    }
    public void hit(){
        if(!isDashing){
            if(level < .5f)
                death();
            else
                level -= .1f;
        }
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

    void death(){

    }
}