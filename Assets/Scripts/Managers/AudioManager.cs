using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour {
    public static AudioManager instanceJump, instanceCollect, instanceHitWall;
    //public AudioSource s1;
    AudioSource srcJump, srcCollect, srcHitWall;
    AudioSource[] srcs;
    public AudioClip jump;
    public AudioClip collect;
    public AudioClip hitWall;

   
    void Awake()
    {
        instanceJump = this;
        instanceCollect = this;
        instanceHitWall = this;
    }

	// Use this for initialization
	void Start () {
        srcJump = this.GetComponent<AudioSource>();
        //player2 = this.GetComponent<AudioSource>();
       // player3 = this.GetComponent<AudioSource>();

        srcJump.loop = false;
        //srcCollect.loop = false;
        //srcHitWall.loop = false;

//       Debug.Log(this.GetComponent<AudioSource>().volume);
        srcs = this.GetComponents<AudioSource>(); //1-hit wall, 2-collect, 3-jump

	}
	
	// Update is called once per frame
	void Update () {
        //this.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("MusicVol");       
	}
    public void HitWall()
    {
        //srcJump.clip = hitWall;
        //srcJump.PlayOneShot(hitWall);

        srcs[0].PlayOneShot(hitWall);
    }
    public void CoinCollect()
    {
        //srcJump.clip = collect;
        //srcJump.PlayOneShot(collect);
        srcs[1].PlayOneShot(collect);

    }
    public void Jump()
    {
        //srcJump.clip = jump;
        //srcJump.PlayOneShot(jump);
        srcs[2].PlayOneShot(jump);

    }

}
