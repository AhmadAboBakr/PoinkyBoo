﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PowerUpGenerator : MonoBehaviour {
    public static PowerUpGenerator generator;
    public GameObject Magnet;
    public Coroutine powerupsCour;
    public GameObject Sliding;
    public GameObject PoinkyMultiplier;
    public List<GameObject> PowerUps;    
    float random;
    float speed;
    float time;
    public float powerupdiff;
    int counter = 0;
    RaycastHit r;
	// Use this for initialization
    //public void generatePowerup() 
    //{
    //    counter++;
    //    if(counter==powerupdiff)
    //    {
    //        powerupdiff++;
    //        counter = 0;
    //        random = 3 * Random.Range(-1, 2);
    //        if (Random.Range(0, 2) == 0)
    //        {
    //            PowerUps.Add(Instantiate(Magnet, new Vector3(random, 5, speed * (10) + 5), Quaternion.identity) as GameObject);

    //        }
    //        else if (Random.Range(0, 2) == 1)
    //        {
    //            PowerUps.Add(Instantiate(Sliding, new Vector3(random, 5, speed * (10) + 5), Quaternion.identity) as GameObject);
    //        }
    //    }
    //}
    public IEnumerator Generatepowerup()
    {
        while (true)
        {
            int type=Random.Range(0, 3);
                //powerupdiff++;
                //if (powerupdiff == 20)
                    //powerupdiff = 10;
                //Debug.Log(powerupdiff);
                random = 3 * Random.Range(-1, 2);
                //if (type == 0)
                //{
                //    PowerUps.Add(Instantiate(Magnet, new Vector3(random, 5, speed * (10) + 5), Quaternion.identity) as GameObject);

                //}
                //else if (type == 1)
                //{
                //    PowerUps.Add(Instantiate(Sliding, new Vector3(random, 5, speed * (10) + 5), Quaternion.identity) as GameObject);
                //}
                //else if (type == 2)
                //{
                    //PowerUps.Add(Instantiate(PoinkyMultiplier, new Vector3(random, 5, speed * (10) + 5), Quaternion.identity) as GameObject);
                //}
                yield return new WaitForSeconds(powerupdiff);
        }
    }
    void Awake()
    {
        if (!PowerUpGenerator.generator)
            PowerUpGenerator.generator = this;
        GameManager.clear += Clear;
        GameManager.Move += Generate;
    }
	void Start () {
        powerupdiff = 10;
        //GameManager.Move += this.generatePowerup; 
        speed = GameManager.instance.poinkySpeed.y;
        PowerUps = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
        speed = GameManager.instance.poinkySpeed.y;
        time = -speed / (0.5f * Physics.gravity.y);
        if (PowerUps.Count>0&&PowerUps[0].transform.position.z < Camera.main.transform.position.z)
        {
            Destroy(PowerUps[0]);
            PowerUps.Remove(PowerUps[0]);
        }
        //Debug.Log(powerupdiff);
        //foreach (var powerup in PowerUps)
        //{
        //    if (GameManager.manager.IsMoving)
        //    {
        //        powerup.transform.position = Vector3.Lerp(powerup.transform.position, powerup.transform.position + new Vector3(0, 0, -speed), Time.deltaTime / time);
        //    }
        //}
	}
    public void EatPowerup(GameObject powrUp)
    {
        PowerUps.Remove(powrUp);
        Destroy(powrUp);
    }
    public void Clear()
    {
        for (int i = 0; i < PowerUps.Count; i++)
        {
            Destroy(PowerUps[i].gameObject);
        }
        powerupdiff = 2;
        PowerUps.Clear();
        Start();
        PowerUpManager.Manager.ClearTimer();

    }
    public void Generate() 
    {
       //powerupsCour = StartCoroutine(PowerUpGenerator.generator.Generatepowerup());
        if (Physics.Raycast(new Vector3(0, -3, speed * (10) + 5), -Vector3.up, out r, float.MaxValue, 1 << 12))
        {
            if (r.collider.tag != "Spiral")
            {
                if (powerupdiff <= 0)
                {
                    int type = Random.Range(0, 3);
                    //powerupdiff = Random.Range(2,3);
                    powerupdiff = Random.Range(30, 40);

                    random = 3 * Random.Range(-1, 2);
                    if (type == 0)
                    {
                        PowerUps.Add(Instantiate(Magnet, new Vector3(random, 5, speed * (10) + 5), Quaternion.identity) as GameObject);

                    }
                    else if (type == 1)
                    {
                        PowerUps.Add(Instantiate(Sliding, new Vector3(random , 5, speed * (10) + 5), Quaternion.identity) as GameObject);
                    }
                    else if (type == 2)
                    {
                        PowerUps.Add(Instantiate(PoinkyMultiplier, new Vector3(random , 5, speed * (10) + 5), Quaternion.identity) as GameObject);
                    }
                }
                else
                {
                    powerupdiff--;
                }
            }
        }
    }
    public void StopGenerate()
    {
        //StopCoroutine(powerupsCour);
        //powerupdiff = 10;

    }
}
