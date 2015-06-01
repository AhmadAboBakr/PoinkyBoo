using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomGenerator : MonoBehaviour
{
    public static RoomGenerator generator;
    public GameObject[] roomPrefabs;
    public GameObject[] spiralPrefabs;
    public GameObject[] desertPrefabs;
    public Mode previousRoomMode;
    public GameObject DesertEntrance;
    public GameObject DesertExit;


    public GameObject[] doors;
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
        GameManager.clear += Clear;
    }
    void Start()
    {
        rooms = new List<GameObject>();
        speed = GameManager.instance.poinkySpeed.y; 
        lastRoomLocation = -5;

        for (int i = 0; i < 10; i++)
        {
            GenerateRoom();
        }
    }

    void Update()
    {
        if (rooms[0].transform.position.z < Camera.main.transform.position.z - 20)
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

        if (GameManager.instance.GameMode == Mode.MainMode)
        {
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
            if (previousRoomMode != Mode.Desert)
            {
                rooms.Add(GameObject.Instantiate(DesertEntrance, new Vector3(0f, 3, lastRoomLocation), Quaternion.Euler(-90, 0, 0)) as GameObject);
            }
            else
            {
                random = Random.Range(0, desertPrefabs.Length);
                rooms.Add(GameObject.Instantiate(desertPrefabs[random], new Vector3(0f, 3, lastRoomLocation), Quaternion.Euler(-90, 0, 0)) as GameObject);
            }
            previousRoomMode = Mode.Desert;

        }
        else if (GameManager.instance.GameMode == Mode.Spiral)
        {
            if (previousRoomMode != Mode.Spiral)
            {
                rooms.Add(GameObject.Instantiate(doors[0], new Vector3(0f, 3, lastRoomLocation), Quaternion.Euler(-90, 0, 0)) as GameObject);
            }
            else
            {
                random = Random.Range(0, spiralPrefabs.Length);
                rooms.Add(GameObject.Instantiate(spiralPrefabs[random], new Vector3(0f, 3, lastRoomLocation), Quaternion.Euler(-0, 0, 0)) as GameObject);
            }
            previousRoomMode = Mode.Spiral;
        }
        count++;
        int modeChanger = count% 45;

        switch (modeChanger)
        {
            case 5:
                GameManager.instance.GameMode = Mode.Spiral;
                break;
            case 10:
                GameManager.instance.GameMode = Mode.MainMode;
                break;
            case 15:
                GameManager.instance.GameMode = Mode.Desert;
                break;
            default:
                break;
        }

    }
}
