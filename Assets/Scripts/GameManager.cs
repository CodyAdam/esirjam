using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Threading;

public class GameManager : MonoBehaviour
{

    #region Singleton
    public static GameManager instance;
    public GameObject menuButtons;
    private int level = 1;
    public Window currentWindow;
    private Window nextWindow;
    public AudioSource music;
    public GameObject windowPrefab;
    public Image fadeImage;
    public bool trigger = false;
    public List<GameObject> enemies = new List<GameObject>();

    public Camera cam;

    // Debug only button to test next level

    public Window GetCurrentWindow() { return currentWindow; }

    public void SetCurrentWindow(Window newCurrentWindow) { currentWindow = newCurrentWindow; }

    public int getLevel()
    {
        return level;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        cam = Camera.main;
    }
    #endregion

    public AudioController kaboom;
    public float bulletSpeed;
    float reduceTimer = 30f;
    bool usedWipe = false;
    bool usedReduce = false;
    void Update()
    {
        if (trigger)
        {
            NextLevel();
            trigger = false;
        }

        if (Input.GetKeyDown(KeyCode.P) && !usedWipe)
        {
            foreach (GameObject enemie in GameManager.instance.enemies)
            {
                Destroy(enemie);
            }
            kaboom.GetSource().Play();
            usedWipe = true;
        }



        if (Input.GetKeyDown(KeyCode.I) && !usedReduce && reduceTimer > 0)
        {
            bulletSpeed /= 2;
            foreach (GameObject enemie in enemies)
            {
                if (enemie != null && enemie.GetComponent<Minion>()) { enemie.GetComponent<Minion>().speed /= 2; }
            }
            usedReduce = true;
        }

        if (usedReduce && reduceTimer > 0) {
            reduceTimer -= Time.deltaTime; 
        }

        if (usedReduce && reduceTimer < 0)
        {
            foreach (GameObject enemie in enemies)
            {
                if (enemie != null) { enemie.GetComponent<Minion>().speed *= 2; }
            }
            bulletSpeed *= 2;
            usedReduce = false;
        }
    }

    public GameObject player;
    public Transform targetPoint;
    public PowerUpManager powerUpManager;

    public void SetTargetPoint(Vector3 newTargetPoint) { targetPoint.position = newTargetPoint; }

    // Start is called before the first frame update
    void Start()
    {
        menuButtons.SetActive(false);
        currentWindow.Init(level);
        currentWindow.canSpawn = true;
        nextWindow = Instantiate(windowPrefab, Vector3.zero, Quaternion.identity).GetComponent<Window>();
        nextWindow.Init(level + 1);
        nextWindow.gameObject.SetActive(false);

        cam.orthographicSize = currentWindow.GetSize().y / 2;
    }

    public void NextLevel()
    {
        StartCoroutine(NextLevelCoroutine());
    }

    IEnumerator NextLevelCoroutine()
    {
        // stop time for animation
        Time.timeScale = 0;
        var precedentVolume = music.volume;
        music.volume = 0.01f;
        currentWindow.canSpawn = false;

        //TODO animation cracks bigger
        // wait for animation to finish
        yield return new WaitForSecondsRealtime(1f);
        currentWindow.ActiveBigCrack();
        yield return new WaitForSecondsRealtime(1f);

        // resume time
        Time.timeScale = 1;

        music.volume = precedentVolume;
        GetComponent<AudioController>().GetSource().Play();
        Color flash = fadeImage.color;
        flash.a = .4f;
        fadeImage.color = flash;
        fadeImage.DOFade(0, .2f).SetEase(Ease.InSine);
        // remove all bullets
        foreach (GameObject enemie in enemies)
        {
            Destroy(enemie);
        }
        enemies.Clear();
        currentWindow.Break();
        if (level >= 5)
        {
            // disable the player and all enemies
            player.SetActive(false);
            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                enemy.SetActive(false);
            }
            // fing gameobject "Music" and stop the music
            music.Stop();
            menuButtons.SetActive(true);
            yield break;
        }
        currentWindow = nextWindow;
        currentWindow.gameObject.SetActive(true);
        currentWindow.canSpawn = true;
        level++;
        cam.DOOrthoSize(currentWindow.GetSize().y / 2, 5f);
        nextWindow = Instantiate(windowPrefab, Vector3.zero, Quaternion.identity).GetComponent<Window>();
        nextWindow.Init(level + 1);
        nextWindow.gameObject.SetActive(false);
    }


    public void retry(){
        level = 1;
        music.Play();
        player.SetActive(true);
        currentWindow = Instantiate(windowPrefab, Vector3.zero, Quaternion.identity).GetComponent<Window>();
        currentWindow.Init(level);
        currentWindow.canSpawn = true;
        nextWindow = Instantiate(windowPrefab, Vector3.zero, Quaternion.identity).GetComponent<Window>();
        nextWindow.Init(level + 1);
        nextWindow.gameObject.SetActive(false);

        cam.DOOrthoSize(currentWindow.GetSize().y / 2, 1f);
        cam.orthographicSize = 1;

        player.GetComponent<PlayerController>().level = 1;
        player.GetComponent<PlayerController>().setPosition(Vector2.zero);
        usedWipe = false;
        usedReduce = false;

        bulletSpeed = 7;

        menuButtons.SetActive(false);
    }
    public void quit(){
        #if UNITY_STANDALONE
            Application.Quit();
        #endif
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
