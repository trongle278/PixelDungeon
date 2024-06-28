using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuSetting : MonoBehaviour
{
    public GameObject settingPanel;
    public GameObject backButton;

    public Slider _musicSlider, _sfxSlider;

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
        settingPanel.SetActive(true);
    }

    public void BackToMainMenu()
    {
        settingPanel.SetActive(false);
        backButton.SetActive(false);
    }
}
