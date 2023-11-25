using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Cat_State
{
    Solid,
    Liquid,
    Gas,
    Fly,
    Die
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isStart;

    public int hp;
    public Vector3 spawnpoint;

    public GameObject GameOverPanel;
    public PlayerController cat;

    public Cat_State catState;

    void Start()
    {
        instance = this;
        isStart = false;
        catState = Cat_State.Solid;
    }

    public void Respawn()
    {
        cat.Respawn();
    }

    public void GameOver()
    {
        cat.Die();
        catState = Cat_State.Die;
        isStart = false;

        GameOverPanel.SetActive(true);
    }
}
