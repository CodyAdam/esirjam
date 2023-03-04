using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Transform targeted;

    public float viewRadius;

    Vector2 pos;
    Transform target;
    Transform focusTo;
    Vector2 direction = new Vector2();
    float firstRadius;
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
        GetComponent<CircleCollider2D>().radius = firstRadius * newLevel;
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
        firstRadius = GetComponent<CircleCollider2D>().radius;
        viewRadius = GetComponent<CircleCollider2D>().radius * 20;
    }

    public void OnMove(InputValue value)
    {
        direction = value.Get<Vector2>();
    }

    public void OnFire() 
    {
        if(focusTo == null)
            return;
        SetTarget(focusTo.transform);
        targeted.position = target.position;
    }


    void FixedUpdate()
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
        if(target == null){
            transform.Translate(direction.normalized*speed/60.0f);
        }
        transform.localScale = new Vector3(getSize(), getSize(), getSize());
        
    }

    void Focus()
    {
        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        position.z = -2;
        Collider2D[] enemies = Physics2D.OverlapCircleAll(position, viewRadius);

        float distance = 0;
        float minDistance = 99999999;

        focusTo = null;
        foreach (Collider2D enemy in enemies)
        {
            if(Vector3.Distance(enemy.transform.position, transform.position) > viewRadius)
                continue;
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
    void death(){

    }
}