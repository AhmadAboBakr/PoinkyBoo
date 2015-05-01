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

    void Update()
    {
        if (InputManager.method == InputMethod.buttons && GameManager.instance.isStarted)
        {
            senstivity = PlayerPrefs.GetFloat("Senstivity");
            if (pressed)
            {
                TileGenerator.generator.CurrentTile.Move(buttonDirection * Time.deltaTime * 35 * senstivity);
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
