using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject OptionMenu;


    void Start()
    {
        OptionMenu.SetActive(false);
        //SoundOn.SetActive(true);
        //SoundOff.SetActive(false);

}

    public void Option_btn_clicked()
    {
        OptionMenu.SetActive(true);

    }

    public void Option_BackBtn_clicked()
    {
        OptionMenu.SetActive(false);
    }

    public void Option_Sound_on_clicked()
    {

    }

    public void Option_Sound_off_clicked()
    {

    }
}
