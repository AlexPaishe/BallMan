using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioBallManScript : MonoBehaviour
{
    [SerializeField] private Slider[] slid;
    [SerializeField] private Image[] SelectSlider;
    [SerializeField] private AudioSource[] Audio;
    [SerializeField] private AudioMixerGroup[] audioMixer;

    private void Awake()
    {
        for(int i = 0; i < slid.Length; i ++)
        {
            slid[i].value = PlayerPrefs.GetFloat(slid[i].name);
            SelectSlider[i].fillAmount = slid[i].value;
        }
    }

    private void Start()
    {
        for(int i = 0; i < audioMixer.Length; i ++)
        {
            float mixer = (PlayerPrefs.GetFloat(slid[i].name) * 50) - 30;
            audioMixer[i].audioMixer.SetFloat(slid[i].name, mixer);
        }
    }

    public void MusicSlider(float var)//Реализация изменения грмкости музыки
    {
        SelectSlider[0].fillAmount = var;
        PlayerPrefs.SetFloat(slid[0].name, var);
        float mixer = (var * 50) - 30;
        audioMixer[0].audioMixer.SetFloat(slid[0].name, mixer);
    }

    public void SoundEffectSlider(float var)//Реализация изменения громкости эффектов
    {
        SelectSlider[1].fillAmount = var;
        PlayerPrefs.SetFloat(slid[1].name, var);
        float mixer = (var * 50) - 30;
        audioMixer[1].audioMixer.SetFloat(slid[1].name, mixer);
    }

    public void UIEffectSlider(float var)//Реализация изменения громкости врагов
    {
        SelectSlider[2].fillAmount = var;
        PlayerPrefs.SetFloat(slid[2].name, var);
        float mixer = (var * 50) - 30;
        audioMixer[2].audioMixer.SetFloat(slid[2].name, mixer);
    }

    public void Sound(int var)//Реализация произведения звуков 
    {
        Audio[var].Play();
    }

    public void StopSound()//Реализация остановки звука
    {
        Audio[2].Stop();
    }

    public bool PauseSound(int var)//Реализация остановке звука при паузе
    {
        bool vars = false;
        if (Audio[var].isPlaying)
        {
            Audio[var].Pause();
            vars = true;
        }
        return vars;
    }

    public void LavaSounf(bool frozen)//Реализация изменения звуков лавы
    {
        if(frozen == false)
        {
            Audio[4].Stop();
            Audio[5].Play();
        }
        else
        {
            Audio[5].Stop();
            Audio[4].Play();
        }
    }
}
