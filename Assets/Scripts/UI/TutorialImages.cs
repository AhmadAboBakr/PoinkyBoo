﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialImages : MonoBehaviour {

    public static TutorialImages instance;
    public Image imgLeft, imgRight;
    float fadeSpeed, fade;
    float fading;

    void Awake()
    {
        TutorialImages.instance = this;
        GameManager.clear += FadeReset;
    }
    public void StartFadding()
    {
        StartCoroutine("fadeButtons");
    }
	void Start () {

        //imgRight = GameObject.FindGameObjectWithTag("TutImgRight").GetComponent<Image>();
        //imgLeft = GameObject.FindGameObjectWithTag("TutImgLeft").GetComponent<Image>();
        if (GameManager.instance.Input != InputMethod.buttons)
            this.gameObject.SetActive(false);
        fadeSpeed = 0.8f;
        fading=1;
	}
	
 
   public void FadeReset()
   {
       imgLeft.color = new Color(1f, 1f, 1f, 1);
       imgRight.color = new Color(1f, 1f, 1f, 1);
       fading = 1;
       
   }

	void Update () {


	}
    IEnumerator fadeButtons()
    {
        while (true)
        {
            yield return new WaitForSeconds(.1f);
            fading /= 1.5f;
            if (fading <= .01f)
            {
                fading = 0;                
            }
            imgLeft.color = new Color(1f, 1f, 1f, fading);
            imgRight.color = new Color(1f, 1f, 1f, fading);
            
        }
    }
}
