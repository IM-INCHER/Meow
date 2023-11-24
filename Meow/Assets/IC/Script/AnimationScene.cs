using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationScene : MonoBehaviour
{
    public GameObject[] Cuts;

    public int time;
    private int timeCount = -100;

    [SerializeField]
    private int count;

    void Start()
    {
        
    }

    void Update()
    {
        timeCount++;

        if (timeCount % time == 0 && count < 5)
        {
            if (count > 1)
            {
                Cuts[0].SetActive(false);
                Cuts[1].SetActive(false);
            }

            Cuts[count].SetActive(true);
            count++;
        }
        else if(timeCount % time == 0)
        {
            SceneManager.LoadScene("SelectScene");
        }

    }
}
