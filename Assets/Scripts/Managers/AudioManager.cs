using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour {
   
  // //PlayerPrefs playerPrefs;
  // public List<AudioSource> audioSources = new List<AudioSource>();
   
    void Awake()
    {
    }

	// Use this for initialization
	void Start () {

       //Debug.Log(PlayerPrefs.GetFloat("MusicVol"));
       Debug.Log(this.GetComponent<AudioSource>().volume);
	}
	
	// Update is called once per frame
	void Update () {
        this.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("MusicVol");       
	}

}
