using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SeletScene : MonoBehaviour
{
    private int seletScene;

    public AudioSource StageMove;

    private void Start()
    {
        seletScene = 0;
        this.transform.position = new Vector2(0.17f, 1.05f);
        AudioSource StageMove = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            this.transform.position = new Vector3(4.78f, 1.05f);
            seletScene = 1;
            StageMove.Play();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            this.transform.position = new Vector2(0.17f, 1.05f);
            seletScene = 0;
            StageMove.Play();
        }

        if (Input.GetKeyDown(KeyCode.Space))
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
