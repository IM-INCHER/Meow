using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    GameObject soundManager;
    private void Start()
    {
        soundManager = GameObject.Find("SoundManager");
        //soundManager.GetComponent<SoundManager>().SetDamageVolume(1.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (GameManager.instance.hp > 0)
            {
                GameManager.instance.hp -= 1;
                soundManager.GetComponent<SoundManager>().OnSfxDG();
                //soundManager.GetComponent<SoundManager>().OnSfxDG();
                if (GameManager.instance.hp <= 0)
                {
                    GameManager.instance.GameOver();
                    soundManager.GetComponent<SoundManager>().OnSfxGO();
                }
                else
                {
                    GameManager.instance.Respawn();
                }
            }
        }
    }
}
