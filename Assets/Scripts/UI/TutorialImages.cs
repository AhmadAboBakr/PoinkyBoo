using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialImages : MonoBehaviour {

    public static TutorialImages instance;
    public Image imgLeft, imgRight;
    float fadeSpeed=0.8f, fade;
    float fading=1;

    void Awake()
    {
        TutorialImages.instance = this;
        GameManager.clear += FadeReset;
    }
    public void StartFadding()
    {
        StartCoroutine("fadeButtons");
    }

 
   public void FadeReset()
   {
       imgLeft.color = new Color(1f, 1f, 1f, 1);
       imgRight.color = new Color(1f, 1f, 1f, 1);
       fading = 1;
       
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
