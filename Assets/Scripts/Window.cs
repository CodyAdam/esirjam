using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Window : MonoBehaviour
{
    private int level;
    private BoxCollider2D boxCollider;

    //list of gameobjects that are in the window that need to be destroyed on break
    public List<GameObject> cracks = new List<GameObject>();
    public GameObject bigCrack;
    private int HP = 1;

    public void Init(int level)
    {
        this.level = level;
        this.HP = level * 4;
        var scale = Mathf.Exp(level / 2f);
        transform.localScale = new Vector3(scale, scale, scale);
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public void Break()
    {
        // destroy all cracks
        foreach (GameObject crack in cracks)
        {
            Destroy(crack);
        }
        Destroy(gameObject);
    }

    public void ActiveBigCrack()
    {
        bigCrack.SetActive(true);
    }

    public Vector2 GetSize()
    {
        return new Vector2(transform.localScale.x * boxCollider.size.x, transform.localScale.y * boxCollider.size.y);
    }

    public void Hit()
    {
        HP--;
        if (HP <= 0)
        {
            GameManager.instance.NextLevel();
        }
    }

}
