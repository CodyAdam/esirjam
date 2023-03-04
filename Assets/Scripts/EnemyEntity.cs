using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : MonoBehaviour
{
    protected float speed = 2.5f;

    public float GetSpeed() { return speed; }

    public void SetSpeed(float newSpeed) { speed = newSpeed; }
}
