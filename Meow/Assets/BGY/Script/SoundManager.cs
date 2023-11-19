using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    static SoundManager instance;
    public AudioSource[] sources;

    public AudioSource musicsource;
    public AudioSource btnsource;
    private AudioSource PlayerWalk;
    private AudioSource PlayerMelt;
    private AudioSource PlayerOri;
    private AudioSource PlayerJump;

    public static SoundManager Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if(instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;

            sources = GetComponentsInChildren<AudioSource>();
        }
        else
        {
            DestroyObject(gameObject);
        }
    }

    public void SetMusicVolum(float volume)
    {
        musicsource.volume = volume;
    }

    public void SetButtonVolume(float volume)
    {
        btnsource.volume = volume;
    }

    public void PlaySound(string soundName)
    {
        for (int i = 0; i < sources.Length; i++)
        {
            {
                if (sources[i].gameObject.name.CompareTo(soundName) == 0) 
                {
                    sources[i].Play();
                }
            }
        }
    }


    public void OnSfx()
    {
        btnsource.Play();
    }
}
