using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector3 pos;
    public float speed;


    void Update()
    {
        Vector3 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        pos = transform.position + direction.normalized * speed * Time.deltaTime ;
        pos.y = transform.position.y;
        transform.position = pos;

    }
}