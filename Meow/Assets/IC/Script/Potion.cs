using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    GameObject soundManager;
    private void Start()
    {
        soundManager = GameObject.Find("SoundManager");
        soundManager.GetComponent<SoundManager>().SetSavepointVolume(1.0f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.gameObject.tag == "Player")
        {
            //Debug.Log("Ãæµ¹");

            if (GameManager.instance.hp < 3)
            {
                GameManager.instance.hp++;
                soundManager.GetComponent<SoundManager>().OnSfxIT();
                Destroy(this.gameObject);
            }
        }
    }
}
