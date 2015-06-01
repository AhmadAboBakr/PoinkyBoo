using UnityEngine;
using System.Collections;
using TouchScript.Gestures;

using System.Collections.Generic;

public class SpiralTileGenerator : MonoBehaviour {

    public static SpiralTileGenerator generator;
    public GameObject tilePrefab;
    float speed;
    List<GameObject> tiles;
    int index;
    float time;
    Vector3 pos = new Vector3(0, 5, 0);

    public GameObject CurrentTile
    {
        get // get the current tile aw kma yqoolon al tile al 7alya
        {
            return tiles[index];
        }
    }

    void Awake()
    {
        if (generator)
            generator = this;
    }

    void Start()
    {
        GameManager.Move += this.move;//subscribe 
        index = 0;
        Debug.Log("start");
        tiles = new List<GameObject>();
        speed = GameManager.instance.poinkySpeed.y;
        time = -speed / (0.51f * Physics.gravity.y);
    
       Camera.main.GetComponent<PanGesture>().StateChanged += InputTest_StateChanged;
       
        tiles.Add(GameObject.Instantiate(tilePrefab, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 180, 0))) as GameObject);
        for (int i = 0; i < 100; i++)
        {
            tiles.Add(GameObject.Instantiate(tilePrefab, new Vector3(0, 0, 2 * i), Quaternion.Euler(new Vector3(0, 180, 0))) as GameObject);

            if (! tiles[i].GetComponent<Rigidbody>())
            {
                tiles[i].AddComponent<Rigidbody>().isKinematic = true;
                
            }
            //tiles[i].GetComponent<Rigidbody>().transform.Rotate(Vector3.up, Time.deltaTime);
            
            tiles[i].transform.RotateAround(this.transform.position + new Vector3(0, 10, 0), Vector3.back, Random.Range(0, 360));
        }
    }

    void InputTest_StateChanged(object sender, GestureStateChangeEventArgs e)
    {
        foreach (var tile in tiles)
        {
            if (e.State == Gesture.GestureState.Changed)
            {
                tile.transform.RotateAround(pos, Vector3.back, .12f * Screen.width * (sender as PanGesture).LocalDeltaPosition.x);
            }
        }

       

    }

    // Update is called once per frame
    void Update()
    {
        

        //speed = GameManager.manager.poinkySpeed.y;
        //time = -speed / (0.51f * Physics.gravity.y);
        //if (tiles[index].transform.position.z > 0)
        //{
        //    //if tiles shifted
        //    foreach (var tile in tiles)
        //    {
        //        var position = tile.transform.position;
        //        position = Vector3.Lerp(position, position + new Vector3(0, position.y, -speed), Time.deltaTime / time);
        //        tile.transform.position = position;
        //    }
        //}
        //else
        //{
        //    GameManager.manager.IsMoving = false;
        //}

        ////removing tiles out of screen
        //if (tiles[0].transform.position.z <= Camera.main.transform.position.z)
        //{
        //    GameObject.Destroy(tiles[0]);
        //    tiles.Remove(tiles[0]);
        //    index--;

        //}
    }
    public void move()
    {
        Debug.Log(index);
        while (tiles[index].transform.position.z > 0)
        {
            foreach (var tile in tiles)
            {
                var position = tile.transform.position;
                position = Vector3.Lerp(position, position + new Vector3(0, position.y, -speed), Time.deltaTime / time);
                tile.transform.position = position;
            }
        }
        index++;
        tiles.Add(GameObject.Instantiate(tilePrefab, new Vector3(3 * Random.Range(-1, 2)*0, 0, speed * (10)), Quaternion.Euler(new Vector3(270, 180, 0))) as GameObject);
        tiles[tiles.Count-1].transform.RotateAround(Vector3.back, this.transform.position + new Vector3(0, 10, 0), Random.Range(0, 360));

       // CollectablesGenerator.generator.generate();
    }
}
