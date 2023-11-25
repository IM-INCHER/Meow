using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource[] sources;

    public AudioSource btnsource;
    public AudioSource savepointsource;
    public AudioSource itemsource;
    //public AudioSource walksource;
    public AudioSource damagesource;
    public AudioSource gameoversource;
    public AudioSource pipesource;
    public AudioSource pipecrushsource;

    //효과음 볼륨 조절-------------------------------------------
    //버튼
    public void SetButtonVolume(float volume)
    {
        btnsource.volume = volume;
    }
    //세이브 포인트
    public void SetSavepointVolume(float volume)
    {
        savepointsource.volume = volume;
    }
    //아이템 획득
    public void SetItemVolume(float volume)
    {
        itemsource.volume = volume;
    }
    //데미지
    public void SetDamageVolume(float volume)
    {
        damagesource.volume = volume;
    }
    //게임오버
    public void SetGameOverVolume(float volume)
    {
        gameoversource.volume = volume;
    }
    //파이프 들어갈 때
    public void SetPipeVolume(float volume)
    {
        pipesource.volume = volume;
    }
    //파이프 벽에 부딪칠 때
    public void SetPipeCrush(float volume)
    {
        pipecrushsource.volume = volume;
    }

    //효과음 플레이-------------------------------------------
    public void OnSfx()
    {
        btnsource.Play();
    }

    public void OnSfxSP()
    {
        savepointsource.Play();
    }

    public void OnSfxIT()
    {
        itemsource.Play();
    }

    public void OnSfxDG()
    {
        damagesource.Play();
    }

    public void OnSfxGO() 
    { 
        gameoversource.Play();
    }

    public void OnSfxPP()
    {
        pipesource.Play();
    }

    public void OnSfxPC()
    {
        pipecrushsource.Play();
    }
}
