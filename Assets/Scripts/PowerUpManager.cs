using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{

    public AudioController kaboom;

    bool usedWipe = false;
    public void Wipe()
    {
        if (!usedWipe)
        {
            foreach (GameObject enemie in GameManager.instance.enemies)
            {
                Destroy(enemie);
            }
            kaboom.GetSource().Play();
            usedWipe = true;
        }   
    }

    public void BerserkMode()
    {
        Debug.Log("Berserk Mode");
        float timer = 30f;
        while(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        GameManager.instance.player.GetComponent<PlayerController>().SetLevel(GameManager.instance.player.GetComponent<PlayerController>().GetLevel() / 2);
    }

    public void ReduceEnemies()
    {
        float timer = 30f;
        float max = Mathf.Max(GameManager.instance.GetCurrentWindow().GetSize().x, GameManager.instance.GetCurrentWindow().GetSize().y);

        Collider[] enemies = Physics.OverlapSphere(transform.position, max / 2);
        foreach (Collider c in enemies)
        {
            c.GetComponent<Minion>().speed /= 2;
        }

        while (timer > 0)
        {

            timer -= Time.deltaTime;
        }

        foreach (Collider c in enemies)
        {
            c.GetComponent<Minion>().speed *= 2;
        }

    }
}
