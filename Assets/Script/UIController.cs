using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Slider musicSlider, sfxSlider;

    public void ToggleMusic()
    {
        AudioManeger.Instance.ToggleMusic();
    }
    public void ToggleSFX()
    {
        AudioManeger.Instance.ToggleSFX();
    }
    public void MusicVolume()
    {
        AudioManeger.Instance.MusicVolume(musicSlider.value);
    }
    public void SFXvolume()
    {
        AudioManeger.Instance.SFXVolume(sfxSlider.value);   
    }
}
