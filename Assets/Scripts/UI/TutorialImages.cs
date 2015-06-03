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
        StartCoroutine("fadeButtons");
    }
	void Start () {

        //imgRight = GameObject.FindGameObjectWithTag("TutImgRight").GetComponent<Image>();
        //imgLeft = GameObject.FindGameObjectWithTag("TutImgLeft").GetComponent<Image>();
        fadeSpeed = 0.8f;
        fading=1;
	}
	
 
   public void FadeReset()
   {
       fade = Mathf.SmoothStep(0f, 1f, fadeSpeed);
       imgLeft.color = new Color(1f, 1f, 1f, fade);
       imgRight.color = new Color(1f, 1f, 1f, fade);
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
                break;
                
            }
            imgLeft.color = new Color(1f, 1f, 1f, fading);
            imgRight.color = new Color(1f, 1f, 1f, fading);
            
        }
        yield return null;
    }
}
