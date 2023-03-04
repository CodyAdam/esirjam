using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector3 pos;
    public float speed = 10f;


    void Update()
    {
        Vector3 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        pos = transform.position + direction * speed * Time.deltaTime;
        pos.y = transform.position.y;
        transform.position = pos;

    }
}