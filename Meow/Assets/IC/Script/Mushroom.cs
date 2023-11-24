using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (GameManager.instance.hp > 0)
            {
                GameManager.instance.hp -= 1;

                if(GameManager.instance.hp <= 0)
                {
                    GameManager.instance.GameOver();
                }
                else
                {
                    GameManager.instance.Respawn();
                }
            }
        }
    }
}
