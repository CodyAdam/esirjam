using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    #region Singleton
    public static GameManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    #endregion

    public Transform player;

    public void MouseDown(){
        Collider[] enemies = Physics.OverlapSphere(Input.mousePosition, 10f);
        float distance = 0;
        float minDistance = 0;
        foreach(Collider enemy in enemies){
            if(enemy.tag == "Ennemy"){
                distance = Vector3.Distance(player.position, enemy.transform.position);
                if(distance < minDistance){
                    minDistance = distance;
                    player.GetComponent<PlayerController>().SetTarget(enemy.transform);
                }
            }
        }
    }
}
