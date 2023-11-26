using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearScene : MonoBehaviour
{
    public float time;
    public AudioSource clearsource;
    private float count;

    void Start()
    {
        AudioSource clearsource = GetComponent<AudioSource>();
    }

    void Update()
    {
        count += Time.deltaTime;
        
        if(count > time)
        {
            SceneManager.LoadScene("SelectScene");
            clearsource.Play();
        }
    }
}
