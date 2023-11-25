using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Savepoint : MonoBehaviour
{
    private bool isSave;
    GameObject soundManager;

    private void Start()
    {
        isSave = false;
        soundManager = GameObject.Find("SoundManager");
        soundManager.GetComponent<SoundManager>().SetSavepointVolume(1.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GetComponent<Animator>().SetTrigger("save");

            if (isSave == false)
            {
                GameManager.instance.spawnpoint = this.transform.position;
                isSave = true;
                soundManager.GetComponent<SoundManager>().OnSfxSP();
            }
        }
    }
}
