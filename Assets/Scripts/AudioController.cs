using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioClip sound;
    public float volume;
    public float pitch;
    public bool playOnAwake = false;
    AudioSource source;

    public AudioSource GetSource() { return source; }
    // Start is called before the first frame update
    void Awake() 
    {
        gameObject.AddComponent<AudioSource>().playOnAwake = playOnAwake;
        source= gameObject.GetComponent<AudioSource>();
    }

    private void Start()
    {
        source.clip = sound;
        source.volume = volume;
        source.pitch = pitch;
    }
}
