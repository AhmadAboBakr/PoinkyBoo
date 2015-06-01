using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PowerUpManager : MonoBehaviour {
    public GameObject slide;
    public static PowerUpManager Manager;
    public GameObject safetyNet;

	// Use this for initialization
	void Start () {
	}
    void Awake()
    {
        if (!PowerUpManager.Manager)
            PowerUpManager.Manager = this;
    }
	
	// Update is called once per frame
	void Update () {
        
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
