using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PowerUpManager : MonoBehaviour {
    public GameObject slide;
    public static PowerUpManager Manager;
    public GameObject safetyNet = null;
    float MagnetTimer;
    float SaftyNetTimer;
    public int MagnitTime;
    public int SaftyNetTime;


	// Use this for initialization
	void Start () 
    {
        if (!PlayerPrefs.HasKey("MagnitTime"))
        {
            PlayerPrefs.SetInt("MagnitTime", 15);
        }
        if (!PlayerPrefs.HasKey("SaftyNetTime"))
        {
            PlayerPrefs.SetInt("SaftyNetTime", 15);
        }
        MagnitTime = PlayerPrefs.GetInt("MagnitTime");
        SaftyNetTime = PlayerPrefs.GetInt("SaftyNetTime");

	}
    void Awake()
    {
        if (!PowerUpManager.Manager)
            PowerUpManager.Manager = this;
        safetyNet = null;
    }
	
	// Update is called once per frame
	void Update () {
        if (GameManager.instance.Powerup == PowerUps.Magnit)
        {
            MagnetTimer += Time.deltaTime;
        }
        if (MagnetTimer > MagnitTime)
        {
            Debug.Log("case1");
            GameManager.instance.Powerup = PowerUps.None;
            MagnetTimer = 0;
        }
        if (GameManager.instance.Powerup == PowerUps.SafetyNet)
        {

            SaftyNetTimer += Time.deltaTime;
        }
        if (SaftyNetTimer > SaftyNetTime)
        {
            Debug.Log("case2");
            GameManager.instance.Powerup = PowerUps.None;
            Destroy(safetyNet.gameObject);
            safetyNet = null;
            SaftyNetTimer = 0;
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
        Instantiate(poinky, new Vector3(2, poinky.gameObject.transform.position.y, 0),Quaternion.identity);
        Instantiate(poinky, new Vector3(-2, poinky.gameObject.transform.position.y, 0), Quaternion.identity);
        GameManager.instance.NumOfPoinky += 2;
    }
    
}
