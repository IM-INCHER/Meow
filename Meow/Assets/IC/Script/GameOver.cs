using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void ReButton()
    {
        SceneManager.LoadScene("Stage 1");
    }

    public void HomeButton()
    {
        SceneManager.LoadScene("MainScene");
    }
}
