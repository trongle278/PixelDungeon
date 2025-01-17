using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISoundController : MonoBehaviour
{
    public GameObject settingPanel;
    public GameObject backButton;
    public GameObject hidePausedMenu;

    public Slider _musicSlider, _sfxSlider;


    public void ToggleMusic()
    {
        AudioManager.Instance.ToggleMusic();
    }

    public void ToggleSFX()
    {
        AudioManager.Instance.ToggleSFX();
    }

    public void MusicVolume()
    {
        AudioManager.Instance.MusicVolume(_musicSlider.value);
    }
    public void SFXVolume()
    {
        AudioManager.Instance.SFXVolume(_sfxSlider.value);
    }

    public void Setting()
    {
        hidePausedMenu.SetActive(false);
        settingPanel.SetActive(true);
    }

    public void BackToMainMenu() 
    {
        hidePausedMenu.SetActive(true);
        backButton.SetActive(false);
    }
}
