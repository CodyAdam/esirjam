using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy : MonoBehaviour
{

    Color startColor;

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
}
