using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomGenerator : MonoBehaviour
{
    public static RoomGenerator generator;
    public GameObject[] roomPrefabs;
    public GameObject[] spiralPrefabs;
    public GameObject[] doors;
    int count = 0;
    //  public GameObject[] spiralRoomsPrefab;
    float speed;
    int random;
    List<GameObject> rooms;
    void Awake()
    {
        generator = this;
        GameManager.clear += Clear;
    }
    void Start()
    {
        //StartCoroutine("ChangeType");
        rooms = new List<GameObject>();
        speed = GameManager.instance.poinkySpeed.y;
        for (int i = 0; i < 10; i++)
        {
            float lastRoomLocation;
            if (rooms.Count > 0)
            {
                var lastRoom = rooms[rooms.Count - 1];
                lastRoomLocation = lastRoom.transform.position.z;
                lastRoomLocation += lastRoom.transform.GetChild(0).GetComponent<Renderer>().bounds.size.z;
                //lastRoomLocation += 19.99f;
                // Debug.Log(lastRoom.transform.GetChild(0).GetComponent<Renderer>().bounds.size.y);
            }
            else
            {
                lastRoomLocation = -5;
            }
            if (GameManager.instance.GameMode == Mode.MainMode)
            {
                random = Random.Range(0, roomPrefabs.Length);
                rooms.Add(GameObject.Instantiate(roomPrefabs[random], new Vector3(0f, 3, lastRoomLocation), Quaternion.Euler(-90, 0, 0)) as GameObject);
            }
            else if (GameManager.instance.GameMode == Mode.Spiral)
            {
                Debug.Log("in spiral");
                random = Random.Range(0, spiralPrefabs.Length);
                rooms.Add(GameObject.Instantiate(spiralPrefabs[random], new Vector3(0f, 0, lastRoomLocation), Quaternion.Euler(-90, 0, 0)) as GameObject);
            }
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
            if (GameManager.instance.GameMode == Mode.MainMode)
            {
                random = Random.Range(0, roomPrefabs.Length);
                rooms.Add(GameObject.Instantiate(roomPrefabs[random], new Vector3(0f, 3, lastRoomLocation), Quaternion.Euler(-90, 0, 0)) as GameObject);
            }
            else
            {
                random = Random.Range(0, spiralPrefabs.Length);

                rooms.Add(GameObject.Instantiate(spiralPrefabs[random], new Vector3(0f, 3, lastRoomLocation), Quaternion.Euler(-90, 0, 0)) as GameObject);

            }
            count++;
            GameObject.Destroy(rooms[0]);
            rooms.Remove(rooms[0]);
        }
        if (GameManager.instance.IsMoving)
        {
            foreach (var room in rooms)
            {
                room.transform.position = Vector3.Lerp(room.transform.position, room.transform.position + new Vector3(0, 0, -speed / 2), Time.deltaTime);
            }
        }
        if(count>10){
            //Debug.Log("hamada: " );
                
            //if (Random.Range(0, 2) > 0){
            //    count=0;
            //    random = 0;// Random.Range(0, doors.Length);
            //    float lastRoomLocation;
            //    var lastRoom = rooms[rooms.Count - 1];
            //    lastRoomLocation = lastRoom.transform.position.z;
            //    lastRoomLocation += lastRoom.transform.GetChild(0).GetComponent<Renderer>().bounds.size.z;
            //    rooms.Add(GameObject.Instantiate(doors[random], new Vector3(0f, 4.5f, lastRoomLocation), Quaternion.Euler(-90, 0, 0)) as GameObject);
            //    Debug.Log("in random : " + random);
            //}
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
