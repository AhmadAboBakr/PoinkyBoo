using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SettingsMenu : MonoBehaviour {
    public static SettingsMenu instance;
    public Slider volumeSlider;
    public Slider senstivitySlider;
    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }
    void Start () {
        if (!volumeSlider)
        {
            volumeSlider= GameObject.Find("VolumeSlider").GetComponent<Slider>();
        }
        if (!senstivitySlider)
        {
            senstivitySlider= GameObject.Find("SenstivitySlider").GetComponent<Slider>();
        }
        volumeSlider.value = RemasteredGameManager.instance.Volume;
        senstivitySlider.value = RemasteredGameManager.instance.Senstivity;
    }
    void OnVolumeSliderChange()
    {
        RemasteredGameManager.instance.Volume = volumeSlider.value;
    }
    void OnSenstivitySliderChange()
    {
        RemasteredGameManager.instance.Senstivity = senstivitySlider.value;

    }

}
