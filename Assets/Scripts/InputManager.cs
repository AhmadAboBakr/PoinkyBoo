using UnityEngine;
using System.Collections;
using TouchScript.Gestures;
public enum InputMethod
{
    Touch,
    Accelerometer,
    buttons
}
public class InputManager : MonoBehaviour
{
    public float senstivity;
    public static InputManager manager;
    public GameObject ButtonsHandle;
    public static InputMethod method;

    void Awake()
    {
        manager = this;
    }
    void Start()
    {
        if (!PlayerPrefs.HasKey("Senstivity"))
        {
            PlayerPrefs.SetFloat("Senstivity", 1);
        }
        senstivity = PlayerPrefs.GetFloat("Senstivity");

        if (!PlayerPrefs.HasKey("InputMode"))
        {
            PlayerPrefs.SetInt("InputMode", (int)InputMethod.Touch);
            method = InputMethod.buttons;
        }
        else
        {
            method = (InputMethod)PlayerPrefs.GetInt("InputMode");

        }
        GetComponent<PanGesture>().StateChanged += InputTest_StateChanged;
        ButtonsHandle.SetActive(false);

    }

    void InputTest_StateChanged(object sender, GestureStateChangeEventArgs e)
    {



        if (GameManager.instance.isStarted)
        {
            if (method == InputMethod.Touch)
            {
                if (e.State == Gesture.GestureState.Changed)
                {
                    TileGenerator.generator.CurrentTile.Move((sender as PanGesture).LocalDeltaPosition.x * Screen.dpi * 0.8f * senstivity);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (method == InputMethod.Accelerometer && GameManager.instance.isStarted)
        {
            float x = Input.acceleration.x;
            if (Mathf.Abs(x) > .15f)
            {
                x = Mathf.Sign(x);
            }
            else
            {
                x = 0;
            }
            float senstivity = PlayerPrefs.GetFloat("Senstivity") * 10;
            float movement = Input.acceleration.x;
            //TileGenerator.generator.CurrentTile.transform.position += new Vector3(x,0,0);
            TileGenerator.generator.CurrentTile.GetComponent<Rigidbody>().velocity = Vector3.zero;
            TileGenerator.generator.CurrentTile.Move(movement * senstivity);
        }
        else if (method == InputMethod.buttons && GameManager.instance.isStarted)
        {
            ButtonsHandle.SetActive(true);
        }
    }
    public void changeInputType()
    {
        method = (InputMethod)
            PlayerPrefs.GetInt("InputMode");
    }

}
