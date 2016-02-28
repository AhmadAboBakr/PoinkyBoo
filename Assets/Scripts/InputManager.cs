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
    public static InputManager instance;
    public GameObject ButtonsHandle;
    
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        GameManager.instance.Senstivity = PlayerPrefs.GetFloat("Senstivity");
        GameManager.instance.Input= (InputMethod)PlayerPrefs.GetInt("InputMode"); 
        GetComponent<PanGesture>().StateChanged += InputTest_StateChanged;
        ButtonsHandle.SetActive(false);
    }

    void InputTest_StateChanged(object sender, GestureStateChangeEventArgs e)
    {



        if (GameManager.instance.isStarted)
        {
            if (GameManager.instance.Input == InputMethod.Touch)
            {
                if (e.State == Gesture.GestureState.Changed)
                {
                    TileGenerator.instance.CurrentTile.Move((sender as PanGesture).LocalDeltaPosition.x * Screen.dpi * 0.8f * GameManager.instance.Senstivity);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (GameManager.instance.Input == InputMethod.Accelerometer && GameManager.instance.isStarted)
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
            TileGenerator.instance.CurrentTile.GetComponent<Rigidbody>().velocity = Vector3.zero;
            TileGenerator.instance.CurrentTile.Move(movement * senstivity);
        }
        else if (GameManager.instance.Input == InputMethod.buttons && GameManager.instance.isStarted)
        {
            ButtonsHandle.SetActive(true);
            float movement = Input.GetAxis("Horizontal");
            TileGenerator.instance.CurrentTile.Move(movement *2);

        }
    }
    public void changeInputType()
    {
        GameManager.instance.Input = (InputMethod)
            PlayerPrefs.GetInt("InputMode");
    }

}
