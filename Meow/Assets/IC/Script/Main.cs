using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public GameObject Text;
    public int time;

    private int count;

    void Start()
    {
        
    }

    void Update()
    {
        count++;

        if (count % time == 0)
            Text.SetActive(!Text.active);

        if (Input.anyKey)
        {
            SceneManager.LoadScene("AnimationScene");
        }
    }
}
