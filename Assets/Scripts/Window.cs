using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Window : MonoBehaviour
{
    private int level;
    private BoxCollider2D bounds;

    public List<Sprite> backgrounds = new List<Sprite>();

    public SpriteRenderer bg = null;

    //list of gameobjects that are in the window that need to be destroyed on break
    public List<GameObject> cracks = new List<GameObject>();
    public GameObject bigCrack;
    private int HP = 1;

    public GameObject frame;

    public bool canSpawn = false;
    private Tween shakeTween;

    public GameObject enemyPrefab;
    private Tween spawnTween;
    private float spawnCooldown;
    int difficulty;
    float timeDifficulty = 5f;
    public Transform spawnMin;
    public Transform spawnMax;


    public void Init(int level)
    {
        ResetPrefabs();
        if (level >= 6)
            return;
        this.level = level;
        this.HP = level * 4;
        this.difficulty = 1;
        var scale = Mathf.Exp(level / 1.3f);
        GameManager.instance.player.GetComponent<PlayerController>().viewRadius *= scale * 0.3f;
        GameManager.instance.player.GetComponent<PlayerController>().speed *= scale * 0.3f;
        enemyPrefab.transform.localScale *= scale* 0.3f;
        enemyPrefab.GetComponent<Minion>().speed *= scale * 0.3f;
        enemyPrefab.GetComponent<Minion>().bullet.transform.localScale *= scale*0.3f;
        GameManager.instance.bulletSpeed *= scale * 0.3f;

        bg.sprite = backgrounds[level - 1];
        spawnCooldown = 1 / (level * 0.5f);
        transform.localScale = new Vector3(scale, scale, scale);
        bounds = GetComponent<BoxCollider2D>();
        spawnTween = DOVirtual.DelayedCall(spawnCooldown, Spawn, true).SetLoops(-1);
    }

    private void ResetPrefabs()
    {
        GameManager.instance.player.GetComponent<PlayerController>().viewRadius = GameManager.instance.player.GetComponent<CircleCollider2D>().radius * 20;
        GameManager.instance.player.GetComponent<PlayerController>().speed = 15;
        GameManager.instance.bulletSpeed = 7;
        enemyPrefab.GetComponent<Minion>().speed = 2.5f;
        enemyPrefab.transform.localScale = new Vector3(1,1,1);
        enemyPrefab.GetComponent<Minion>().bullet.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }

    private void Spawn()
    {
        if (canSpawn)
        {
            for (int i = 0; i <= difficulty; i++)
            {
                // get a random position on the spawnMin and spawnMax transform (in world space)
                // only along the sides of the box
                int side = Random.Range(0, 4);
                Vector3 randomPos = Vector3.zero;

                if (side == 0)
                {
                    randomPos = new Vector3(spawnMin.position.x+1, Random.Range(spawnMin.position.y+1, spawnMax.position.y-1), -1);
                }
                else if (side == 1)
                {
                    randomPos = new Vector3(spawnMax.position.x-1, Random.Range(spawnMin.position.y+1, spawnMax.position.y-1), -1);
                }
                else if (side == 2)
                {
                    randomPos = new Vector3(Random.Range(spawnMin.position.x+1, spawnMax.position.x-1), spawnMin.position.y+1, -1);
                }
                else if (side == 3)
                {
                    randomPos = new Vector3(Random.Range(spawnMin.position.x + 1, spawnMax.position.x - 1), spawnMax.position.y-1, -1);
                }

            
                GameObject newEnemy = Instantiate(enemyPrefab, randomPos, Quaternion.identity);
                newEnemy.GetComponent<Minion>().SetTarget(GameManager.instance.player.transform);
            }

            timeDifficulty --;

            if (timeDifficulty < 0)
            {
                difficulty++;
                timeDifficulty = 5f;
            }
        }
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
        canSpawn = false;
        GetComponent<AudioSource>().Play();
        bigCrack.SetActive(true);
    }

    public Vector2 GetSize()
    {
        return new Vector2(transform.localScale.x * bounds.size.x, transform.localScale.y * bounds.size.y);
    }

    public void Hit()
    {
        HP--;
        SpriteRenderer frameRenderer = frame.GetComponent<SpriteRenderer>();
        frameRenderer.color = new Color(.8f, 0, 0, 1);
        frameRenderer.DOColor(Color.white, 0.5f).SetEase(Ease.InSine);
        // shake the frame
        if (shakeTween == null || !shakeTween.IsPlaying())
        {
            shakeTween = frame.transform.DOShakePosition(0.1f, 0.3f, 10, 90, false, true);
        }
        else
        {
            shakeTween.Restart();
        }
        if (HP <= 0)
        {
            GameManager.instance.NextLevel();
        }
    }

}
