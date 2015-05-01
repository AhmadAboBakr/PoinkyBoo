using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PowerUpManager : MonoBehaviour {
    public GameObject slide;
    public static PowerUpManager Manager;

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
        Instantiate(slide, new Vector3(0, 0, 37.31f), Quaternion.EulerAngles(0, 90*Mathf.Deg2Rad, 0));
        
        //(Instantiate(slide) as GameObject).transform.position = new Vector3(0, 0, 69.25f);


    }
    
}
