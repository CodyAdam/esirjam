using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : MonoBehaviour
{

    
    protected float speed = 2.5f;

    private void OnMouseDown()
    {
        GameManager.instance.player.GetComponent<PlayerController>().SetTarget(transform);
    }
}
