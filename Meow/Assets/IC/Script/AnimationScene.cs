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
    public AudioSource CutOne;
    public AudioSource CutTwo;
    public AudioSource CutThree;
    public AudioSource CutFour;

    void Start()
    {
        //timeCount = -100;
        AudioSource CutOne = GetComponent<AudioSource>();
        AudioSource CutTwo = GetComponent<AudioSource>();
        AudioSource CutThree = GetComponent<AudioSource>();
        AudioSource CutFour = GetComponent<AudioSource>();
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
        else if (timeCount > time)
        {
            SceneManager.LoadScene("SelectScene");
        }

        if (count == 0)
        {
            CutOne.Play();
        }

        if (count == 1)
        {
            CutTwo.Play();
        }

        if (count == 2)
        {
            CutThree.Play();
        }

        if (count == 4)
        {
            CutFour.Play();
        }
    }
}
