using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour
{

    #region Singleton
    public static GameManager instance;
    private int level = 1;
    public Window currentWindow;
    private Window nextWindow;
    public GameObject windowPrefab;
    public Image fadeImage;
    public bool trigger = false;
    public List<GameObject> bullets = new List<GameObject>();

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

    void Update()
    {
        if (trigger)
        {
            NextLevel();
            trigger = false;
        }
    }

    public GameObject player;
    public GameObject music;
    public Transform targetPoint;

    public void SetTargetPoint(Vector3 newTargetPoint) { targetPoint.position = newTargetPoint; }

    // Start is called before the first frame update
    void Start()
    {
        currentWindow.Init(level);
        currentWindow.canSpawn = true;
        nextWindow = Instantiate(windowPrefab, Vector3.zero, Quaternion.identity).GetComponent<Window>();
        nextWindow.Init(level + 1);
        nextWindow.gameObject.SetActive(false);

        cam.orthographicSize = currentWindow.GetSize().y / 2;
        music.GetComponent<AudioController>().GetSource().Play();
    }

    public void NextLevel()
    {
        StartCoroutine(NextLevelCoroutine());
    }

    IEnumerator NextLevelCoroutine()
    {
        // stop time for animation
        Time.timeScale = 0;
        currentWindow.canSpawn = false;

        //TODO animation cracks bigger
        // wait for animation to finish
        yield return new WaitForSecondsRealtime(1f);
        GetComponent<AudioController>().GetSource().Play();
        currentWindow.ActiveBigCrack();
        yield return new WaitForSecondsRealtime(5f);

        // resume time
        Time.timeScale = 1;


        Color flash = fadeImage.color;
        flash.a = .4f;
        fadeImage.color = flash;
        fadeImage.DOFade(0, .2f).SetEase(Ease.InSine);
        // remove all bullets
        foreach (GameObject bullet in bullets)
        {
            Destroy(bullet);
        }
        level++;
        currentWindow.Break();
        currentWindow = nextWindow;
        currentWindow.gameObject.SetActive(true);
        currentWindow.canSpawn = true;

        yield return new WaitForSeconds(1f);

        cam.DOOrthoSize(currentWindow.GetSize().y / 2, 5f);
        nextWindow = Instantiate(windowPrefab, Vector3.zero, Quaternion.identity).GetComponent<Window>();
        nextWindow.Init(level + 1);
        nextWindow.gameObject.SetActive(false);
    }
}
