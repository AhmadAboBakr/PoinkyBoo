﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomGenerator : MonoBehaviour
{
    public static RoomGenerator generator;
    public GameObject[] roomPrefabs;
    public GameObject[] spiralPrefabs;
    public GameObject[] desertPrefabs;

    public GameObject exitRoom;

    bool roomExitGenerated;

    public GameObject[] doors;
    int count = 0;
    //  public GameObject[] spiralRoomsPrefab;
    float speed;
    int random;
    List<GameObject> rooms;

    float lastRoomLocation;

    void Awake()
    {
        generator = this;
        GameManager.clear += Clear;
    }
    void Start()
    {
        roomExitGenerated = false;
        rooms = new List<GameObject>();
        speed = GameManager.instance.poinkySpeed.y;
        for (int i = 0; i < 10; i++)  
        {           
            if (rooms.Count > 0)
            {
                var lastRoom = rooms[rooms.Count - 1];
                lastRoomLocation = lastRoom.transform.position.z;
                lastRoomLocation += lastRoom.transform.GetChild(0).GetComponent<Renderer>().bounds.size.z;
            }
            else
            {
                lastRoomLocation = -5;
            }
                random = Random.Range(0, roomPrefabs.Length);
               // rooms.Add(GameObject.Instantiate(roomPrefabs[random], new Vector3(0f, 3, lastRoomLocation), Quaternion.Euler(-90, 0, 0)) as GameObject);
            if(GameManager.instance.GameMode == Mode.MainMode)
            {
                rooms.Add(GameObject.Instantiate(roomPrefabs[random], new Vector3(0f, 3, lastRoomLocation), Quaternion.Euler(-90, 0, 0)) as GameObject);                                      
            }

            //else if(GameManager.instance.GameMode == Mode.Desert)
            //    {
            //        rooms.Add(GameObject.Instantiate(desertPrefabs[random], new Vector3(0f, 3, lastRoomLocation), Quaternion.Euler(-90, 0, 0)) as GameObject);
            //    }    
     
            count++;
        }
    }

    void Update()
    {
        if (rooms[0].transform.position.z < Camera.main.transform.position.z -20)
        {
            var lastRoom = rooms[rooms.Count - 1];
            float lastRoomLocation = lastRoom.transform.position.z;
            lastRoomLocation += lastRoom.transform.GetChild(0).GetComponent<Renderer>().bounds.size.z;
            random = Random.Range(0, roomPrefabs.Length);

            if (GameManager.instance.GameMode == Mode.MainMode)
            {
                rooms.Add(GameObject.Instantiate(roomPrefabs[random], new Vector3(0f, 3, lastRoomLocation), Quaternion.Euler(-90, 0, 0)) as GameObject);
            }
            else if (GameManager.instance.GameMode == Mode.Desert)
            {
                //GameObject.Instantiate(exitRoom, new Vector3(0f, 3, lastRoomLocation), Quaternion.Euler(-90, 0, 0));
                if(roomExitGenerated == false)
                {
                    rooms.Add(GameObject.Instantiate(exitRoom, new Vector3(0f, 3, lastRoomLocation), Quaternion.Euler(-90, 0, 0)) as GameObject);
                    roomExitGenerated = true;
                   // rooms.Add(GameObject.Instantiate(desertPrefabs[random], new Vector3(0f, 3, lastRoomLocation), Quaternion.Euler(-90, 0, 0)) as GameObject);

                    //without this, it generates a desert room above the exit room
                    lastRoom = rooms[rooms.Count - 1];
                    lastRoomLocation = lastRoom.transform.position.z;
                    lastRoomLocation += lastRoom.transform.GetChild(0).GetComponent<Renderer>().bounds.size.z;
                }
                rooms.Add(GameObject.Instantiate(desertPrefabs[random], new Vector3(0f, 3, lastRoomLocation), Quaternion.Euler(-90, 0, 0)) as GameObject);
            }    
            else
            {
                random = Random.Range(0, spiralPrefabs.Length);
                rooms.Add(GameObject.Instantiate(spiralPrefabs[random], new Vector3(0f, 3, lastRoomLocation), Quaternion.Euler(-90, 0, 0)) as GameObject);
            }
            count++;
            //exitRoom.gameObject.SetActive(false);
            GameObject.Destroy(rooms[0]);
            rooms.Remove(rooms[0]);
        }
        if (GameManager.instance.IsMoving)
        {
            foreach (var room in rooms)
            {
               // exitRoom.transform.position = Vector3.Lerp(room.transform.position, room.transform.position + new Vector3(0, 0, -speed / 2), Time.deltaTime);
                room.transform.position = Vector3.Lerp(room.transform.position, room.transform.position + new Vector3(0, 0, -speed / 2), Time.deltaTime);
            }
        }          
    }
    public void Clear()
    {
        while(rooms.Count>0)
        {
            GameObject.Destroy(rooms[0]);
            rooms.Remove(rooms[0]);
        }
         GameManager.instance.GameMode = Mode.MainMode;
         Start();
    }
}
