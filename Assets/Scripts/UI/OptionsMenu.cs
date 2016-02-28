using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class OptionsMenu : MonoBehaviour
{
    public static OptionsMenu menu;
    public Slider music;
    public Slider senstivity;
    public Toggle tapAndHold;
    public Toggle swipe;
    public Toggle acc;
    void Awake()
    {
        menu = this;        
        music.value = PlayerPrefs.GetFloat("MusicVol");
        senstivity.value = PlayerPrefs.GetFloat("Senstivity");
    }
    // Use this for initialization
    void Start()
    {
        int inputMode= PlayerPrefs.GetInt("InputMode");
        switch (inputMode)
        {
            case 0:
                swipe.isOn = true;
                break;
            case 1:
                acc.isOn = true;
                break;

            case 2:
                tapAndHold.isOn = true;
                break;

            default:
                tapAndHold.isOn = true;
                break;

        }
        OnInputTypeChange(inputMode);

        music.value = PlayerPrefs.GetFloat("MusicVol");
        senstivity.value = PlayerPrefs.GetFloat("Senstivity");
        foreach (var item in FindObjectsOfType<AudioSource>())
        {
            item.volume = music.value;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) ) 
        {
            OnBackPressed();
        }
    }

    public void OnBackPressed()
    {
        //totdo create a random user ID and store it in a playerprefs to know when a player change his mind
        //
        Dictionary<string, object> dict = new Dictionary<string, object>();
        dict.Add("Senstivity", senstivity.value);
        dict.Add("MusicVol", music.value);
        dict.Add("InputType", PlayerPrefs.GetInt("InputMode"));
        //UnityAnalytics.CustomEvent("Options", dict);
        //GA.API.Design.NewEvent("Options:senstivity", senstivity.value);
        //GA.API.Design.NewEvent("Options:Music", music.value);
        //GA.API.Design.NewEvent("Options:InputType", PlayerPrefs.GetInt("InputMode"));
        OptionsMenu.menu.gameObject.SetActive(false);
        MainMenu.menu.gameObject.SetActive(true);
    }
    public void OnMusicSliderChange()
    {
        PlayerPrefs.SetFloat("MusicVol", music.value);

        foreach (var item in FindObjectsOfType<AudioSource>())
        {
            item.volume = music.value;
        }
    }
    public void OnSenstivitySliderChange()
    {
        PlayerPrefs.SetFloat("Senstivity", senstivity.value);
        GameManager.instance.Senstivity = senstivity.value;
    }
    public void OnInputTypeChange(int id)
    {
        PlayerPrefs.SetInt("InputMode", id);
        InputManager.instance.changeInputType();
    }
}
