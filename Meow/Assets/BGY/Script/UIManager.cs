using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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
            GameManager.instance.isStart = false;
        }
    }

    public void Option_BackBtn_clicked()
    {
        OptionMenu.SetActive(false);
        GameManager.instance.isStart = true;
        //SoundFXManager.instance.PlayUiSoundFXClip(audioClick, 1f);
    }

    public void Option_Sound_on_clicked()
    {

    }

    public void Option_Sound_off_clicked()
    {

    }
}
