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
    public int magnetTime;
    public int safetyNetTime;
    int degreeMagnet, degreeSafety;
    void GetMaqgnetTime()
    {
        int degree = PlayerPrefs.GetInt("CurrentMagnetLevel");
        magnetTime = (degree) * 5 + 15;

    }
    void GetShieldPowerUp()
    {
        int degree = PlayerPrefs.GetInt("CurrentShieldLevel");
        safetyNetTime = (degree) * 5 + 15;

    }

    //void setSafetyPowerup(int degree)
    //{
    //    if (!PlayerPrefs.HasKey("SafetyNetTime"))
    //    {
    //        degree = 1;
    //        PlayerPrefs.SetInt("SafetyNetTime", degree * 15);
    //    }
    //    safetyNetTime = degree * 15;

    //}

	// Use this for initialization
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
        GetShieldPowerUp(); ;

	}
    void Awake()
    {
        if (!PowerUpManager.Manager)
            PowerUpManager.Manager = this;
        safetyNet = null;
    }

    // Update is called once per frame
	
	// Update is called once per frame
	void Update () {
        if (GameManager.instance.Powerup == PowerUps.Magnet)
        {
            MagnetTimer += Time.deltaTime;

        }
        if (MagnetTimer > magnetTime)
        {
            GameManager.instance.Powerup = PowerUps.None;
            MagnetTimer = 0;
        }
        if (GameManager.instance.Powerup == PowerUps.SafetyNet)
        {
            SafetyNetTimer += Time.deltaTime;
        }
        if (SafetyNetTimer > safetyNetTime)
        {
            GameManager.instance.Powerup = PowerUps.None;
            Destroy(safetyNet.gameObject);
            safetyNet = null;
            SafetyNetTimer = 0;
        }
	}
    public void GenerateNet() 
    {
        Debug.Log(GameManager.instance.GameMode); //msh bttndh!

        if (safetyNet == null)
        {
            if (GameManager.instance.GameMode != Mode.Spiral)
            {
                safetyNet = Instantiate(slide, new Vector3(0, -0.19f, 37.31f), Quaternion.EulerAngles(0, 90 * Mathf.Deg2Rad, 0)) as GameObject;
            }
            else
            {
                safetyNet = Instantiate(slide, new Vector3(0, -10.0f, 37.31f), Quaternion.EulerAngles(0, 90 * Mathf.Deg2Rad, 0)) as GameObject;
            }
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