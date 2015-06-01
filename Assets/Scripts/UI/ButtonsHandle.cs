using UnityEngine;
using System.Collections;

public class ButtonsHandle : MonoBehaviour
{
    public static ButtonsHandle handleManager;
    public bool pressed;
    public int buttonDirection = 1;
    float senstivity;
    // Use this for initialization
    void Awake()
    {
        if (!ButtonsHandle.handleManager)
            ButtonsHandle.handleManager = this;
    }
    void Start()
    {
    }
    void OnEnable()
    {
        senstivity = PlayerPrefs.GetFloat("Senstivity");
    }

    void Update()
    {
        if (GameManager.instance.Input == InputMethod.buttons && GameManager.instance.isStarted)
        {
            if (pressed)
            {
                TileGenerator.instance.CurrentTile.Move(buttonDirection * Time.deltaTime * 35 * senstivity * senstivity );
            }
        }
    }
    public void OnPointerEnter()
    {
        pressed = true;
    }
    public void OnPointerExit()
    {
        pressed = false;
    }
}
