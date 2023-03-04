using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : MonoBehaviour
{

    
    protected Vector3 pos;
    protected float speed = 2.5f;
    Color startColor;


    private void Start()
    {
        startColor = GetComponent<Renderer>().material.color;
    }

    #region ColorGestion

    private void OnMouseOver()
    {
        GetComponent<Renderer>().material.color = Color.red;
    }

    private void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = startColor;
    }

    #endregion

    private void OnMouseDown()
    {
        GameManager.instance.player.GetComponent<PlayerController>().SetTarget(transform);
    }
}
