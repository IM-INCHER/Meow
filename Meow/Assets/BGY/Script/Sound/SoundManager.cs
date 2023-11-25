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
    public AudioSource selectsource;

    //ȿ���� ���� ����-------------------------------------------
    //��ư
    public void SetButtonVolume(float volume)
    {
        btnsource.volume = volume;
    }
    //���̺� ����Ʈ
    public void SetSavepointVolume(float volume)
    {
        savepointsource.volume = volume;
    }
    //������ ȹ��
    public void SetItemVolume(float volume)
    {
        itemsource.volume = volume;
    }
    //������
    public void SetDamageVolume(float volume)
    {
        damagesource.volume = volume;
    }
    //���ӿ���
    public void SetGameOverVolume(float volume)
    {
        gameoversource.volume = volume;
    }
    //������ �� ��
    public void SetPipeVolume(float volume)
    {
        pipesource.volume = volume;
    }
    //������ ���� �ε�ĥ ��
    public void SetPipeCrushVolume(float volume)
    {
        pipecrushsource.volume = volume;
    }


    public void SetSelectVolume(float volume)
    {
        selectsource.volume = volume;
    }

    //ȿ���� �÷���-------------------------------------------
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

    public void OnSfxSC()
    {
        selectsource.Play();
    }
}
