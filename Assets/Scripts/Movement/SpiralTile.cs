using UnityEngine;
using System.Collections;

public class SpiralTile : Tile {

    void Start()
    {
    }
    public override void Move(float force)
    {
        Vector3 point = new Vector3(0,0,this.transform.position.z);
        this.transform.RotateAround(point, Vector3.back, force*5);
    }

    public override void Clear()
    {
        this.transform.position= new Vector3(0, -4, 0);
        this.transform.rotation = Quaternion.Euler(-90, 0, 0);
    }
}
