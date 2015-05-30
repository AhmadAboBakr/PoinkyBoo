using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class TileGenerator : MonoBehaviour
{
    public static TileGenerator generator;
    public GameObject normalTile;
    public GameObject immovableTile;
    public GameObject spiralTile;
    public GameObject desertTile;
    public List<Tile> tiles;
    float speed;
    int index;
    int count;
    float time;
    public Tile CurrentTile
    {
        get
        {
            return tiles[index];
        }
    }

    void Awake()
    {
        if (!TileGenerator.generator)
        {
            TileGenerator.generator = this;
            GameManager.Move += this.Move;
            GameManager.clear += Clear;
        }
    }

    void Start()
    {
        index = 0;
        count = 0;
        tiles = new List<Tile>();
        speed = GameManager.instance.poinkySpeed.y;
        time = -2 * speed / Physics.gravity.y;
        tiles.Add((GameObject.Instantiate(normalTile, new Vector3(0, 0, 0), Quaternion.identity) as GameObject).GetComponent<Tile>());
        {
            for (int i = 1; i < 10; i++)
            {
                Vector3 positionOfTile = new Vector3(0, 0, speed * i);
                Debug.Log(positionOfTile);
                Debug.Log(Physics.Raycast(positionOfTile, -Vector3.up, 10));
                if (Physics.Raycast(positionOfTile, -Vector3.up, 10))
                    Debug.Log("There is something in front of the object!");
            //    var currentRoom=Physics.OverlapSphere(positionOfTile, 1f /* Radius */)[0].gameObject.tag;
             //   Debug.Log(currentRoom);

                if (GameManager.instance.GameMode == Mode.MainMode) //currentRoom == "Normal"
                {
                    tiles.Add((GameObject.Instantiate(normalTile, new Vector3(3 * Random.Range(-1, 2) * 0, 0, speed * i), Quaternion.identity) as GameObject).GetComponent<Tile>());
                    count++;
                }

                else if (GameManager.instance.GameMode == Mode.Desert)
                {
                    tiles.Add((GameObject.Instantiate(desertTile, new Vector3(0, 0, speed * i), Quaternion.identity) as GameObject).GetComponent<Tile>());
                    count++;
                }
                else if (GameManager.instance.GameMode == Mode.Spiral) //currentRoom == "Spiral"
                {
                    var tile = (GameObject.Instantiate(spiralTile, new Vector3(0, -4, speed * i), Quaternion.Euler(-90, 0, 0)) as GameObject).GetComponent<Tile>();
                    tiles.Add(tile);
                    Vector3 point = new Vector3(0, 0, tile.transform.position.z);
                    tile.transform.RotateAround(point, Vector3.back, Random.Range(0, 45) * 8);
                }
            }
        }
    }

    void Update()
    {
        speed = GameManager.instance.poinkySpeed.y;
        time = -speed / (0.51f * Physics.gravity.y);


        //if (GameManager.instance.GameMode == Mode.MainMode)
        //{
        //    tiles.Add((GameObject.Instantiate(normalTile, new Vector3(3 * Random.Range(-1, 2), 0, speed), Quaternion.identity) as GameObject).GetComponent<Tile>());           
        //} else


        if (tiles[index].transform.position.z > 0)
        {
            //if tiles shifted
            foreach (var tile in tiles)
            {
                var position = tile.transform.position;
                position = Vector3.Lerp(position, position + new Vector3(0, 0, -speed), Time.deltaTime / time);
                tile.transform.position = position;
            }

            foreach (var collectable in CollectablesGenerator.generator.collectables)
            {
                if (GameManager.instance.IsMoving)
                {
                    collectable.transform.position = Vector3.Lerp(collectable.transform.position, collectable.transform.position + new Vector3(0, 0, -speed), Time.deltaTime / time);
                }
            }
            foreach (var powerup in PowerUpGenerator.generator.PowerUps)
            {
                powerup.transform.position = Vector3.Lerp(powerup.transform.position, powerup.transform.position + new Vector3(0, 0, -speed), Time.deltaTime / time);
            }
            //PoinkyMovementController.Hitile = false;

        }
        else
        {
            //GameManager.instance.IsMoving = false;
        }

        //removing tiles out of screen
        if (tiles[0].transform.position.z <= Camera.main.transform.position.z)
        {
            GameObject.Destroy(tiles[0].gameObject);
            tiles.Remove(tiles[0]);
            index--;

        }
    }
    public void Move()
    {
        Debug.Log("tiles move");

        while (tiles[index].transform.position.z > 0)
        {
            foreach (var tile in tiles)
            {
                var position = tile.transform.position;
                position = Vector3.Lerp(position, position + new Vector3(0, position.y, -speed), Time.deltaTime / time);
                tile.transform.position = position;
            }
            foreach (var collectable in CollectablesGenerator.generator.collectables)
            {
                    collectable.transform.position = Vector3.Lerp(collectable.transform.position, collectable.transform.position + new Vector3(0, 0, -speed), Time.deltaTime / time);

            }
            foreach (var powerup in PowerUpGenerator.generator.PowerUps)
            {
                powerup.transform.position = Vector3.Lerp(powerup.transform.position, powerup.transform.position + new Vector3(0, 0, -speed), Time.deltaTime / time);
            }
        }
        index++;

        Vector3 positionOfTile = new Vector3(0, -16, speed * 10);

      //  var currentRoom = Physics.OverlapSphere(positionOfTile, 1f /* Radius */)[0].gameObject.tag;

        RaycastHit r;
        //if(LayerMask.)
        Physics.Raycast(new Vector3(0, -3, speed * 10), -Vector3.up, out r, float.MaxValue, 1<<12);
       // Debug.Log(r.collider.tag);
        
        if (r.collider.tag == "Normal") //currentRoom=="Normal" //GameManager.instance.GameMode == Mode.MainMode
        {
            count++;
            if (count > 10 && Random.Range(0, 4) == 0)
            {
                count = 0;
                tiles.Add((GameObject.Instantiate(immovableTile, new Vector3(0, 0, speed * (10)), Quaternion.identity) as GameObject).GetComponent<Tile>());
            }
            else
            {
                tiles.Add((GameObject.Instantiate(normalTile, new Vector3( 0*Random.Range(-1, 2), 0, speed * (10)), Quaternion.identity) as GameObject).GetComponent<Tile>());

            }
        }
        else if (r.collider.tag == "Desert") //GameManager.instance.GameMode == Mode.Desert
        {
            tiles.Add((GameObject.Instantiate(desertTile, new Vector3(Random.Range(-1, 2), 0, speed * (10)), Quaternion.identity) as GameObject).GetComponent<Tile>());
        }
        else if (r.collider.tag == "Spiral" || r.collider.tag == "Door") //currentRoom == "Spiral"//GameManager.instance.GameMode == Mode.Spiral
        {
            tiles.Add((GameObject.Instantiate(spiralTile, new Vector3(0, 3, speed * (10)), Quaternion.Euler(0, 0, Random.Range(-90,90))) as GameObject).GetComponent<Tile>());
           // tiles[tiles.Count-1].transform.RotateAround(this.transform.position + new Vector3(0, 0, 0), Vector3.back, Random.Range(0, 45) * 8);

        }
        CollectablesGenerator.generator.generate();
    }
    public void Clear()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            GameObject.Destroy(tiles[i].gameObject);
        }
        tiles.Clear();
        //Debug.Log(tiles.Count);
        index = 0;
        Start();
    }
}
