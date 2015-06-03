using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PowerUpManager : MonoBehaviour
{
    public GameObject slide;
    public static PowerUpManager Manager;
    public GameObject safetyNet = null;
    float MagnetTimer;
    float SafetyNetTimer;
    public int MagnetTime;
    public int SafetyNetTime;
    int degreeMagnet, degreeSafety;


    // Use this for initialization
    void Start()
    {
        
    }

    void setMagnetPowerup(int degree)
    {
        if (!PlayerPrefs.HasKey("MagnetTime"))
        {
            degree = 1;
            PlayerPrefs.SetInt("MagnetTime", degree * 15);
            PlayerPrefs.SetInt("MagnetDegree", degree);
        }
        else
        {
            MagnetTime = degree * 15;    
            
        }
    }

    void setSafetyPowerup(int degree)
    {
        if (!PlayerPrefs.HasKey("SafetyNetTime"))
        {
            degree = 1;
            PlayerPrefs.SetInt("SafetyNetTime", degree * 15);
        }
        SafetyNetTime = degree * 15;

    }
    void Awake()
    {
        if (!PowerUpManager.Manager)
            PowerUpManager.Manager = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.Powerup == PowerUps.Magnet)
        {
            MagnetTimer += Time.deltaTime;
        }
        if (MagnetTimer > MagnetTime)
        {
            GameManager.instance.Powerup = PowerUps.None;
            MagnetTimer = 0;
        }
        if (GameManager.instance.Powerup == PowerUps.SafetyNet)
        {
            SafetyNetTimer += Time.deltaTime;
        }
        if (MagnetTimer > SafetyNetTimer)
        {
            GameManager.instance.Powerup = PowerUps.None;
            SafetyNetTimer = 0;
        }
    }
    public void GenerateNet()
    {
        if (safetyNet == null)
        {
            safetyNet = Instantiate(slide, new Vector3(0, -0.19f, 37.31f), Quaternion.EulerAngles(0, 90 * Mathf.Deg2Rad, 0)) as GameObject;
        }
        //(Instantiate(slide) as GameObject).transform.position = new Vector3(0, 0, 69.25f);
    }
    public void MultiplyPoinky(GameObject poinky)
    {
        Instantiate(poinky, new Vector3(2, poinky.gameObject.transform.position.y, 0), Quaternion.identity);
        Instantiate(poinky, new Vector3(-2, poinky.gameObject.transform.position.y, 0), Quaternion.identity);
        GameManager.instance.NumOfPoinky += 2;
    }

}
