using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


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

    public Text counter;
    public Image filledimage;
    public Image background;

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
        GetShieldPowerUp();
        
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
            background.gameObject.SetActive(true);
            GetMaqgnetTime();
            GetShieldPowerUp();
        }
        if (GameManager.instance.Powerup == PowerUps.Magnet)
        {
            MagnetTimer += Time.deltaTime;
            int x = magnetTime-(int)MagnetTimer;
            counter.text = x+"";
            filledimage.fillAmount =1-( MagnetTimer*1.0f / magnetTime);
        }
        if (MagnetTimer > magnetTime)
        {
            GameManager.instance.Powerup = PowerUps.None;
            MagnetTimer = 0;
            background.gameObject.SetActive(false);
        }
        if (GameManager.instance.Powerup == PowerUps.SafetyNet)
        {
            SafetyNetTimer += Time.deltaTime;
            int x =safetyNetTime- (int)SafetyNetTimer;
            counter.text = x + "";
            filledimage.fillAmount =1-( SafetyNetTimer * 1.0f / safetyNetTime);
        }
        if (SafetyNetTimer > safetyNetTime)
        {
            GameManager.instance.Powerup = PowerUps.None;
            Destroy(safetyNet.gameObject);
            safetyNet = null;
            SafetyNetTimer = 0;
            background.gameObject.SetActive(false);

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

}