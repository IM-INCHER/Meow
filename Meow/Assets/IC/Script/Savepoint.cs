using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Savepoint : MonoBehaviour
{
    private bool isSave;

    private void Start()
    {
        isSave = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GetComponent<Animator>().SetTrigger("save");

            if(isSave == false)
            {
                GameManager.instance.spawnpoint = this.transform.position;
                isSave = true;
            }
        }
    }
}