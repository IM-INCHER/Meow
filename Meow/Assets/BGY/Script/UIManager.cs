using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject OptionMenu;

    [SerializeField]
    public AudioClip audioClick;


    void Start()
    {
        OptionMenu.SetActive(false);

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) 
        {
            OptionMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void Option_BackBtn_clicked()
    {
        OptionMenu.SetActive(false);
        Time.timeScale = 1;
        //SoundFXManager.instance.PlayUiSoundFXClip(audioClick, 1f);
    }

    public void Restart()
    {
        SceneManager.LoadScene("Stage 1");
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("SelectScene");
        Time.timeScale = 1;
    }
}
