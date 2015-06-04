using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomGenerator : MonoBehaviour
{
    public static RoomGenerator generator;
    public GameObject[] roomPrefabs;
    public GameObject[] spiralPrefabs;
    public GameObject[] desertPrefabs; 
    public GameObject[] waterPrefabs;
    
    public Mode previousRoomMode;
    public GameObject DesertEntrance;
    public GameObject DesertExit;
    

    public GameObject[] doors;
    public GameObject wallColliders;

    int count = 0;
    //  public GameObject[] spiralRoomsPrefab;
    float speed;
    int random;
    public List<GameObject> rooms;

    float lastRoomLocation;

    void Awake()
    {
        generator = this;
        previousRoomMode = Mode.MainMode;
      //  wallColliders.gameObject.SetActive(true);
        GameManager.clear += Clear;
    }
    void Start()
    {
        previousRoomMode = Mode.MainMode;
        rooms = new List<GameObject>();
        speed = GameManager.instance.poinkySpeed.y; 
        lastRoomLocation = -5;

        for (int i = 0; i < 40; i++)
        {
            GenerateRoom();
        }
    }

    void Update()
    {

        if (rooms[0].transform.position.z < Camera.main.transform.position.z - 80)
        {
            GenerateRoom();
            GameObject.Destroy(rooms[0]);
            rooms.Remove(rooms[0]);
        }
        if (GameManager.instance.IsMoving)
        {
            //foreach (var room in rooms)
            //{
            //    exitRoom.transform.position = Vector3.Lerp(room.transform.position, room.transform.position + new Vector3(0, 0, -speed / 2), Time.deltaTime);
            //    room.transform.position = Vector3.Lerp(room.transform.position, room.transform.position + new Vector3(0, 0, -speed / 2), Time.deltaTime);
            //}
        }
    }
    public void Clear()
    {
        while (rooms.Count > 0)
        {
            GameObject.Destroy(rooms[0]);
            rooms.Remove(rooms[0]);
        }
        GameManager.instance.GameMode = Mode.MainMode;
        count = 0;
        Start();
    }
    void GenerateRoom()
    {
        float lastRoomLocation ;
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

        //if (GameManager.instance.GameMode != Mode.MainMode)
        //{
        //    wallColliders.gameObject.SetActive(false);
        //}

        if (GameManager.instance.GameMode == Mode.MainMode)
        {
           // wallColliders.gameObject.SetActive(true);

            if (previousRoomMode != Mode.Desert)
            {
                random = Random.Range(0, roomPrefabs.Length);
                rooms.Add(GameObject.Instantiate(roomPrefabs[random], new Vector3(0f, 3, lastRoomLocation), Quaternion.Euler(-90, 0, 0)) as GameObject);
            }
            else
            {
                rooms.Add(GameObject.Instantiate(DesertExit, new Vector3(0f, 3, lastRoomLocation), Quaternion.Euler(-90, 0, 0)) as GameObject);
            }
            previousRoomMode = Mode.MainMode;
        }
        else if (GameManager.instance.GameMode == Mode.Desert)
        {
            //wallColliders.gameObject.SetActive(false);

            if (previousRoomMode != Mode.Desert)
            {
                rooms.Add(GameObject.Instantiate(DesertEntrance, new Vector3(0f, 3, lastRoomLocation), Quaternion.Euler(-90, 0, 0)) as GameObject);
            }
            else
            {
                random = Random.Range(0, desertPrefabs.Length);
                rooms.Add(GameObject.Instantiate(desertPrefabs[random], new Vector3(0f, 3, lastRoomLocation), Quaternion.Euler(0, 0, 0)) as GameObject);
            }
            previousRoomMode = Mode.Desert;
        }
        else if (GameManager.instance.GameMode == Mode.Spiral)
        {
           // wallColliders.gameObject.SetActive(true);

            if (previousRoomMode != Mode.Spiral)
            {
                rooms.Add(GameObject.Instantiate(doors[0], new Vector3(0f, 4.59f, lastRoomLocation), Quaternion.Euler(-90, 0, 0)) as GameObject);
            }
            else
            {
                random = Random.Range(0, spiralPrefabs.Length);
                rooms.Add(GameObject.Instantiate(spiralPrefabs[random], new Vector3(0f, 3, lastRoomLocation), Quaternion.Euler(-0, 0, 0)) as GameObject);
            }
            previousRoomMode = Mode.Spiral;
        }
        else if (GameManager.instance.GameMode == Mode.Water)
        {
            //wallColliders.gameObject.SetActive(false);

            if (previousRoomMode != Mode.Desert)
            {
                rooms.Add(GameObject.Instantiate(DesertEntrance, new Vector3(0f, 3, lastRoomLocation), Quaternion.Euler(-90, 0, 0)) as GameObject);
            }
            else
            {
                random = Random.Range(0, waterPrefabs.Length);
                rooms.Add(GameObject.Instantiate(waterPrefabs[random], new Vector3(0f, 3, lastRoomLocation), Quaternion.Euler(0, 0, 0)) as GameObject);
            }
            previousRoomMode = Mode.Desert;

        }


        count++;
        int modeChanger = count% 40;

        switch (modeChanger)
        {
            case 10:
                GameManager.instance.GameMode = Mode.Spiral;
                break;
            case 1:
            case 15:
            case 30:
                GameManager.instance.GameMode = Mode.MainMode;
                break;
            case 24:
                GameManager.instance.GameMode = Mode.Desert;
                break;
            case 34:
                GameManager.instance.GameMode = Mode.Water;
 
                break;
            default:
                break;
        }

    }
}
