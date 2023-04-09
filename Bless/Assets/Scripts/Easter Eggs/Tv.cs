using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Tv : MonoBehaviour
{
    public GameObject ScreenOff;
    public GameObject ScreenOn;
    public GameObject SelectionScreen;

    private VideoPlayer _player;

    private void Start()
    {
        _player = ScreenOn.GetComponent<VideoPlayer>();
    }

    public void Power()
    {
        if (ScreenOff.activeInHierarchy == true)
            PowerOn();
        else
            PowerOff();
    }
    public void Play(VideoClip videoClip)
    {
        _player.clip = videoClip;
        _player.Play();
        SelectionScreen.SetActive(false);
    }

    private void PowerOn()
    {
        ScreenOff.SetActive(false);
        ScreenOn.SetActive(true);
        SelectionScreen.SetActive(true);
    }
    private void PowerOff()
    {
        ScreenOff.SetActive(true);
        ScreenOn.SetActive(false);
        _player.clip = null;
    }
}
