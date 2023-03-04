using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowWall : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    public Vector2 direction;
    public GameObject crackPrefab;
    public Window window;
    public float minCrackScale = 0.5f;
    public float maxCrackScale = 1.5f;

    public float minCrackRotation = 0f;
    public float maxCrackRotation = 360f;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            Vector2 hitPosition = other.transform.position;
            // instantiate crack at hit position with random rotation and scale

            // random rotation
            float randomRotation = Random.Range(minCrackRotation, maxCrackRotation);
            // offset with direction (-90 degrees)
            randomRotation += Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            // to quaternion
            Quaternion rotation = Quaternion.Euler(0, 0, randomRotation);

            // random scale
            float randomScale = Random.Range(minCrackScale, maxCrackScale);

            // instantiate crack
            GameObject crack = Instantiate(crackPrefab, hitPosition, rotation);
            crack.transform.localScale = new Vector3(randomScale, randomScale, randomScale) * window.transform.localScale.x;
            window.GetComponent<Window>().cracks.Add(crack);
            window.Hit();
        }
    }


}
