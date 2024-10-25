using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AudioSlider : MonoBehaviour
{
    [Header("Music")]
    public AudioSource musicSource;
    public Slider musicSlider;
    public TextMeshProUGUI musicPercentageText;


    [Header("SFX")]
    public AudioSource sfxSource;
    public Slider sfxSlider;
    public TextMeshProUGUI sfxPercentageText;


    void Start()
    {
        // Kaydedilmi� de�erleri y�kle
        float savedMusicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        float savedSFXVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);

        // Slider ve AudioSource'lar� ayarla
        musicSlider.value = savedMusicVolume;
        sfxSlider.value = savedSFXVolume;
        musicSource.volume = savedMusicVolume;
        sfxSource.volume = savedSFXVolume;

        // Ba�lang�� y�zdelerini g�ncelle
        UpdateMusicPercentageText(savedMusicVolume);
        UpdateSFXPercentageText(savedSFXVolume);
    }

    public void OnMusicValueChanged()
    {
        // Slider de�erini AudioSource'a uygula
        musicSource.volume = musicSlider.value;

        // Y�zdeyi g�ncelle
        UpdateMusicPercentageText(musicSlider.value);

        // De�eri kaydet
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
    }

    public void OnSFXValueChanged()
    {
        // Slider de�erini AudioSource'a uygula
        sfxSource.volume = sfxSlider.value;

        // Y�zdeyi g�ncelle
        UpdateSFXPercentageText(sfxSlider.value);

        // De�eri kaydet
        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
    }

    private void UpdateMusicPercentageText(float value)
    {
        int percentage = Mathf.RoundToInt(value * 100);
        musicPercentageText.text = percentage.ToString() + "%";
    }

    private void UpdateSFXPercentageText(float value)
    {
        int percentage = Mathf.RoundToInt(value * 100);
        sfxPercentageText.text = percentage.ToString() + "%";
    }
}
