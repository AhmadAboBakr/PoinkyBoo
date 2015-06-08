using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class PowerUpManager : MonoBehaviour
{
    public GameObject slide;
    public static PowerUpManager Manager;
    public GameObject safetyNet = null;
    public float MagnetTimer;
    public float SafetyNetTimer;
    public int magnetTime;
    public int safetyNetTime;
    int degreeMagnet, degreeSafety;

    public Text  Magnetcounter;
    public Image Magnetfilledimage;
    public Image Magnetbackground;
    public Text  SafetyNetcounter;
    public Image SafetyNetfilledimage;
    public Image SafetyNetbackground;
     
    void GetMaqgnetTime()
    {
        if (PlayerPrefs.GetInt("CurrentMagnetLevel") == null)
        {
            PlayerPrefs.SetInt("CurrentMagnetLevel", 0);
        }

        int degree = PlayerPrefs.GetInt("CurrentMagnetLevel");
        magnetTime = (degree * 5) + 10;
       


    }
    void GetShieldPowerUp()
    {
        if(PlayerPrefs.GetInt("CurrentShieldLevel") == null)
        {
            PlayerPrefs.SetInt("CurrentShieldLevel", 0);
        }

        int degree = PlayerPrefs.GetInt("CurrentMagnetLevel");
        safetyNetTime = (degree * 5) + 10;        
    }

  
	void Start () 
    {
        if (!PlayerPrefs.HasKey("CurrentShieldLevel"))
        {
            PlayerPrefs.SetInt("CurrentShieldLevel", 0);
        }
        if (!PlayerPrefs.HasKey("CurrentMagnetLevel"))
        {
            PlayerPrefs.SetInt("CurrentMagnetLevel", 0);
        }
        GetMaqgnetTime();
        
	}
    void Awake()
    {
        if (!PowerUpManager.Manager)
            PowerUpManager.Manager = this;
        safetyNet = null;
    }
	void Update () 
    {
        if(GameManager.instance.Powerup!=PowerUps.None)
        {
            GetShieldPowerUp();
        }
        if (GameManager.instance.Powerup == PowerUps.Magnet)
        {
            MagnetTimer += Time.deltaTime;
            int x = magnetTime - (int)MagnetTimer;
            Magnetcounter.text = x + "";
            Magnetfilledimage.fillAmount = 1 - (MagnetTimer * 1.0f / magnetTime);
        }
        if (MagnetTimer > magnetTime)
        {
            GameManager.instance.Powerup = PowerUps.None;
            MagnetTimer = 0;
            Magnetbackground.gameObject.SetActive(false);
        }
        if (GameManager.instance.Powerup == PowerUps.SafetyNet)
        {
            SafetyNetTimer += Time.deltaTime;
            int x = safetyNetTime - (int)SafetyNetTimer;
            SafetyNetcounter.text = x + "";
            SafetyNetfilledimage.fillAmount = 1 - (SafetyNetTimer * 1.0f / safetyNetTime);
        }
        if (SafetyNetTimer > safetyNetTime)
        {
            GameManager.instance.Powerup = PowerUps.None;
            Destroy(safetyNet.gameObject);
            safetyNet = null;
            SafetyNetTimer = 0;
            SafetyNetbackground.gameObject.SetActive(false);

        }
	}
    public void GenerateNet() 
    {
        Debug.Log(GameManager.instance.GameMode); //msh bttndh!
        if (safetyNet == null)
        {
            //if (GameManager.instance.GameMode != Mode.Spiral)
            //{
                safetyNet = Instantiate(slide, new Vector3(0, -0.19f, 37.31f), Quaternion.EulerAngles(0, 90 * Mathf.Deg2Rad, 0)) as GameObject;
            //}
            //else
            //{
            //    safetyNet = Instantiate(slide, new Vector3(0, -10.0f, 37.31f), Quaternion.EulerAngles(0, 90 * Mathf.Deg2Rad, 0)) as GameObject;
            //}
        }
    }
    public void MultiplyPoinky(GameObject poinky)
    {
        Instantiate(poinky, new Vector3(2, poinky.gameObject.transform.position.y, 0), Quaternion.identity);
        Instantiate(poinky, new Vector3(-2, poinky.gameObject.transform.position.y, 0), Quaternion.identity);
        GameManager.instance.NumOfPoinky += 2;
    }
    public void ClearTimer()
    {
        Magnetcounter.text = "";
        Magnetfilledimage.fillAmount = 1;
        Magnetbackground.gameObject.SetActive(false);
        SafetyNetcounter.text = "";
        SafetyNetfilledimage.fillAmount = 1;
        SafetyNetbackground.gameObject.SetActive(false);
    }
    public void eatMagnet()
    {
        GetMaqgnetTime();
        Magnetbackground.gameObject.SetActive(true);
        MagnetTimer = 0;
        int x = magnetTime - (int)MagnetTimer;
        Magnetcounter.text = x + "";

    }
    public void eatSafteyNet()
    {
        GetShieldPowerUp();
        SafetyNetbackground.gameObject.SetActive(true);
        SafetyNetTimer = 0;
        int x = safetyNetTime - (int)SafetyNetTimer;
        SafetyNetcounter.text = x + "";

    }
}