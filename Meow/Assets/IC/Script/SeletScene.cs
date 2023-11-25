using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SeletScene : MonoBehaviour
{
    private int seletScene;
    GameObject soundManager;
    private void Start()
    {
        soundManager = GameObject.Find("SoundManager");
        soundManager.GetComponent<SoundManager>().SetSavepointVolume(1.0f);

        seletScene = 0;
        this.transform.position = new Vector2(0.17f, 1.05f);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            this.transform.position = new Vector3(4.78f, 1.05f);
            soundManager.GetComponent<SoundManager>().OnSfxSC();
            seletScene = 1;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            this.transform.position = new Vector2(0.17f, 1.05f);
            soundManager.GetComponent<SoundManager>().OnSfxSC();
            seletScene = 0;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            switch (seletScene)
            {
                case 0:
                    break;
                case 1:
                    SceneManager.LoadScene("Stage 1");
                    break;
            }

        }
    }
}
