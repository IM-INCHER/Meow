using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationScene : MonoBehaviour
{
    public GameObject[] Cuts;

    public float time;

    [SerializeField]
    private float timeCount;

    [SerializeField]
    private int count;

    void Start()
    {
        //timeCount = -100;
    }

    void Update()
    {
        timeCount += Time.deltaTime;

        if (timeCount > time && count < 5)
        {
            timeCount = 0;

            if (count > 1)
            {
                Cuts[0].SetActive(false);
                Cuts[1].SetActive(false);
            }

            Cuts[count].SetActive(true);
            count++;
        }
        else if(timeCount > time)
        {
            SceneManager.LoadScene("SelectScene");
        }

    }
}
