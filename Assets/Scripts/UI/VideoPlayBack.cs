using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class VideoPlayBack : MonoBehaviour {
    AudioSource source;
    public Loading loadingScript;
    float elapsedTime = 0;
	// Use this for initialization
	void Start () {
        ((MovieTexture)GetComponent<RawImage>().texture).Play();
        source =this.GetComponent<AudioSource>();
        source.Play();
        elapsedTime = 0;

	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(elapsedTime);
        elapsedTime += Time.deltaTime;
        if (elapsedTime>25)
        {
            loadingScript.LoadScene();
        }
	}
}
