using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingSprite : MonoBehaviour
{
    public float rotationSpeed = 1f;

    private void FixedUpdate()
    {
        transform.Rotate(0, 0, rotationSpeed);
    }
}
