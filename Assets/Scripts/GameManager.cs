using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{

    #region Singleton
    public static GameManager instance;
    private int level = 1;
    private Window currentWindow;
    private Window nextWindow;
    public GameObject windowPrefab;
    public bool trigger = false;

    public Camera cam;

    // Debug only button to test next level


    int level = 1;

    public int getLevel(){
        return level;
    }
    /**
     *
     * To call when the window is broken
     *
     */
    public void increaseLevel(){
        level++;
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

    void Update(){
        if(trigger){
            NextLevel();
            trigger = false;
        }
    }

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        currentWindow = Instantiate(windowPrefab, Vector3.zero, Quaternion.identity).GetComponent<Window>();
        currentWindow.Init(level);
        nextWindow = Instantiate(windowPrefab, Vector3.zero, Quaternion.identity).GetComponent<Window>();
        nextWindow.Init(level + 1);
        nextWindow.gameObject.SetActive(false);

        cam.orthographicSize = currentWindow.GetSize().y/2;
    }

    public void NextLevel()
    {
        level++;
        currentWindow.Break();
        currentWindow = nextWindow;
        currentWindow.gameObject.SetActive(true);
        cam.DOOrthoSize(currentWindow.GetSize().y/2, 1f);
        nextWindow = Instantiate(windowPrefab, Vector3.zero, Quaternion.identity).GetComponent<Window>();
        nextWindow.Init(level + 1);
        nextWindow.gameObject.SetActive(false);
    }
}
