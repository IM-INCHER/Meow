using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    void Start()
    {
        this.gameObject.SetActive(true);
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space))
        {
            GameManager.instance.isStart = true;
            this.gameObject.SetActive(false);
        }
    }
}
