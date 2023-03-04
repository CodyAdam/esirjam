using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    void Wipe()
    {
        float pas = 0.2f;
        float radius = pas;
        float max = Mathf.Max(GameManager.instance.GetCurrentWindow().GetSize().x, GameManager.instance.GetCurrentWindow().GetSize().y);

        while (radius < max/2)
        {
            DrawCircle(radius, 1);
            Collider[] enemies = Physics.OverlapSphere(transform.position, radius);
            foreach(Collider c in enemies)
            {
                if (c.GetComponent<EnemyEntity>())
                {
                    Destroy(c);
                }
            }
            radius += pas;
        }
    }

    void DrawCircle(float radius, float lineWidth)
    {
        var segments = 360;
        var line = GameManager.instance.player.AddComponent<LineRenderer>();
        line.useWorldSpace = false;
        line.startWidth = lineWidth;
        line.endWidth = lineWidth;
        line.positionCount = segments + 1;

        var pointCount = segments + 1;
        var points = new Vector3[pointCount];

        for (int i = 0; i < pointCount; i++)
        {
            var rad = Mathf.Deg2Rad * (i * 360f / segments);
            points[i] = new Vector3(Mathf.Sin(rad) * radius, 0, Mathf.Cos(rad) * radius);
        }

        line.SetPositions(points);
    }

    void BerserkMode()
    {
        GameManager.instance.player.GetComponent<PlayerController>().SetLevel(GameManager.instance.player.GetComponent<PlayerController>().GetLevel() * 2);
        float timer = 30f;
        while(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        GameManager.instance.player.GetComponent<PlayerController>().SetLevel(GameManager.instance.player.GetComponent<PlayerController>().GetLevel() / 2);
       ;
    }

    private void ReduceEnemies()
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
