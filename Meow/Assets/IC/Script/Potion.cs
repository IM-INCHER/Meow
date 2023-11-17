using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.gameObject.tag == "Player")
        {
            //Debug.Log("Ãæµ¹");

            if (GameManager.instance.hp < 3)
            {
                GameManager.instance.hp++;
                Destroy(this.gameObject);
            }
        }
    }
}
