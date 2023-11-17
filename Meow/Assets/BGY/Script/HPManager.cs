using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPManager : MonoBehaviour
{
    public GameObject life1;
    public GameObject life2;
    public GameObject life3;

    void Start()
    {
    }

    void Update()
    {
        switch (GameManager.instance.hp)
        {
            case 3:
                life1.GetComponent<Image>().enabled = true;
                life2.GetComponent<Image>().enabled = true;
                life3.GetComponent<Image>().enabled = true;
                break;
            case 2:
                life3.GetComponent<Image>().enabled = false;
                life2.GetComponent<Image>().enabled = true;
                life1.GetComponent<Image>().enabled = true;
                break;
            case 1:
                life3.GetComponent<Image>().enabled = false;
                life2.GetComponent<Image>().enabled = false;
                life1.GetComponent<Image>().enabled = true;
                break;
            case 0:
                life3.GetComponent<Image>().enabled = false;
                life2.GetComponent<Image>().enabled = false;
                life1.GetComponent<Image>().enabled = false;
                break;
        }
    }
}
