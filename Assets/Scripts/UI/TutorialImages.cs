using UnityEngine;
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
        
    }
	void Start () {

        //imgRight = GameObject.FindGameObjectWithTag("TutImgRight").GetComponent<Image>();
        //imgLeft = GameObject.FindGameObjectWithTag("TutImgLeft").GetComponent<Image>();
        fadeSpeed = 0.8f;
        fading=1;
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

        fade -= 0.1f;
       // imgLeft.color = new Color(1f, 1f, 1f, fade);
       // imgRight.color = new Color(1f, 1f, 1f, fade);
	}
    IEnumerator fadeButtons()
    {
        while (true)
        {
            return new WaitForSeconds(.2f);
            fading /= 1.1;
            if (fading < .01f)
            {
                fading = 0;
                
            }
            imgLeft.color = new Color(1f, 1f, 1f, fading);
            imgRight.color = new Color(1f, 1f, 1f, fading);

        }
    }
}
