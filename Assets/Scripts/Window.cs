using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Window : MonoBehaviour
{
    private int level;
    private BoxCollider2D boxCollider;
    public void Init(int level) {
        this.level = level;
        var scale = Mathf.Exp(level/2f);
        transform.localScale = new Vector3(scale, scale, scale);
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public void Break() {
        Destroy(gameObject);
    }

    public Vector2 GetSize() {
        return  new Vector2(transform.localScale.x * boxCollider.size.x, transform.localScale.y * boxCollider.size.y);
    }
}
