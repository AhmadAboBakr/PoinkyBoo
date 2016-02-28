using UnityEngine;
using System.Collections;

public class ButtonsHandle : MonoBehaviour
{
    public bool pressed;
    public int buttonDirection = 1;
    float senstivity;
    // Use this for initialization

    void Start()
    {
    }
    void OnEnable()
    {
        senstivity = PlayerPrefs.GetFloat("Senstivity");
    }

    void Update()
    {
        if (GameManager.instance.isStarted)
        {
            if (pressed)
            {
                TileGenerator.instance.CurrentTile.Move(buttonDirection * Time.deltaTime * 35 * senstivity * senstivity );

                if (HUDManager.instance.score == 5)
                {
                   // TutorialImages.instance.FadeOut();
                }
            }
        }
    }
    public void OnPointerEnter()
    {
		if (!GameManager.instance.isStarted) {
			HUDManager.instance.myAnimator.SetTrigger("fade");
			GameManager.instance.isStarted=true;
		}
        pressed = true;
    }
    public void OnPointerExit()
    {
        pressed = false;
    }
}
