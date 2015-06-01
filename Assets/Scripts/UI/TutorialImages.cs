using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialImages : MonoBehaviour {

    public static TutorialImages instance;
    Image imgLeft, imgRight;
    float fadeSpeed, fade;

    void Awake()
    {
        TutorialImages.instance = this;
        GameManager.clear += FadeReset;
    }

	void Start () {
        imgRight = GameObject.FindGameObjectWithTag("TutImgRight").GetComponent<Image>();
        imgLeft = GameObject.FindGameObjectWithTag("TutImgLeft").GetComponent<Image>();
        fadeSpeed = 0.8f;
	}
	
   public void FadeOut()
    {
       // imgLeft.CrossFadeAlpha(0, 0.4f, true);     
         fade = Mathf.SmoothStep(1f, 0f, fadeSpeed);  
         imgLeft.color = new Color(1f, 1f, 1f, fade);
         imgRight.color = new Color(1f, 1f, 1f, fade);
    }

   public void FadeReset()
   {
       fade = Mathf.SmoothStep(0f, 1f, fadeSpeed);       
       imgLeft.color = new Color(1f, 1f, 1f, fade);
       imgRight.color = new Color(1f, 1f, 1f, fade);
   }

	void Update () {
	}
}
