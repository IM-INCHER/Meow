using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearScene : MonoBehaviour
{
    public float time;

    private float count;

    void Start()
    {
        
    }

    void Update()
    {
        count += Time.deltaTime;
        
        if(count > time)
        {
            SceneManager.LoadScene("SelectScene");
        }
    }
}
