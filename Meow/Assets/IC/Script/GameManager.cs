using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Cat_State
{
    Solid,
    Liquid,
    Gas,
    Fly
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int hp;
    public Vector3 spawnpoint;

    public Cat_State catState;

    void Start()
    {
        instance = this;
        catState = Cat_State.Solid;
    }
}
