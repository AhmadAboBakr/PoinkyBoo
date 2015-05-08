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

//       Debug.Log(this.GetComponent<AudioSource>().volume);
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log("what is love");
        this.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("MusicVol");       
	}

}
