using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour {
    public static AudioManager instance;
    AudioSource player;
    public AudioClip jump;
    public AudioClip collect;
    public AudioClip hitWall;

   
    void Awake()
    {
        instance = this;
    }

	// Use this for initialization
	void Start () {
        player = this.GetComponent<AudioSource>();
        player.loop = false;
//       Debug.Log(this.GetComponent<AudioSource>().volume);
	}
	
	// Update is called once per frame
	void Update () {
        //this.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("MusicVol");       
	}
    public void HitWall()
    {
        player.clip = hitWall;
        player.Play();
    }
    public void CoinCollect()
    {
        player.clip = collect;
        player.Play();
    }
    public void Jump()
    {
        player.clip = jump;
        player.Play();
    }

}
