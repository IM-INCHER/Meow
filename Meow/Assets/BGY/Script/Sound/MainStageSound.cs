using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainStageSound : MonoBehaviour
{
    public AudioSource bgm;

    private void Awake()
    {
        var soundManagers = FindObjectsOfType<MainStageSound>();
        if (soundManagers.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        bgm.Play();
    }


    void Update()
    {
        if (SceneManager.GetActiveScene().name == "SelectScene")
        {
            Destroy(gameObject);
        }

        //if(SceneManager.GetActiveScene().name == "SelectScene")
        //{
        //    DontDestroyOnLoad(gameObject);
        //}
    }
}
