using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioGrandScript : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup[] audioMixer;
    [SerializeField] private Slider[] slid;
    [SerializeField] private GameObject[] Simvol;
    [SerializeField] private Material[] SlideMat;

    private AudioSource audio;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
        for (int i = 0; i < slid.Length; i++)
        {
            slid[i].value = PlayerPrefs.GetFloat(slid[i].name);
            Vector3 slide = Simvol[i].transform.localPosition;
            slide.z = -3.46f + (slid[i].value * 6.92f);
            Simvol[i].transform.localPosition =  slide;
            float offset = 0.5f - (slid[i].value * 0.5f);
            SlideMat[i].mainTextureOffset = new Vector2(offset, 0);
        }
    }

    private void Start()
    {
        for (int i = 0; i < audioMixer.Length; i++)
        {
            float mixer = (PlayerPrefs.GetFloat(slid[i].name) * 50) - 30;
            audioMixer[i].audioMixer.SetFloat(slid[i].name, mixer);
        }
    }

    public void MusicSlider(float var)//Реализация музыкального слайдера
    {
        PlayerPrefs.SetFloat(slid[0].name, var);
        float mixer = (var * 50) - 30;
        audioMixer[0].audioMixer.SetFloat(slid[0].name, mixer);
        Vector3 slide = Simvol[0].transform.localPosition;
        slide.z = -3.46f + (var * 6.92f);
        Simvol[0].transform.localPosition = slide;
        float offset = 0.5f - (var * 0.5f);
        SlideMat[0].mainTextureOffset = new Vector2(offset, 0);
    }

    public void SoundSlider( float var)//Реализация слайдера звуковых эффектов
    {
        PlayerPrefs.SetFloat(slid[1].name, var);
        float mixer = (var * 50) - 30;
        audioMixer[1].audioMixer.SetFloat(slid[1].name, mixer);
        Vector3 slide = Simvol[1].transform.localPosition;
        slide.z = -3.46f + (var * 6.92f);
        Simvol[1].transform.localPosition = slide;
        float offset = 0.5f - (var * 0.5f);
        SlideMat[1].mainTextureOffset = new Vector2(offset, 0);
    }

    public void UISlider(float var)//Реализация слайдера эффектов UI
    {
        PlayerPrefs.SetFloat(slid[2].name, var);
        float mixer = (var * 50) - 30;
        audioMixer[2].audioMixer.SetFloat(slid[2].name, mixer);
        Vector3 slide = Simvol[2].transform.localPosition;
        slide.z = -3.46f + (var * 6.92f);
        Simvol[2].transform.localPosition = slide;
        float offset = 0.5f - (var * 0.5f);
        SlideMat[2].mainTextureOffset = new Vector2(offset, 0);
    }

    public void Click()//Воспроизведение клика
    {
        audio.Play();
    }
}
