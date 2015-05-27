using UnityEngine;
using System.Collections;

public class SpiralTile : Tile {

    void Update()
    {
        Debug.Log(this.transform.rotation.eulerAngles.z);
    }
    public override void Move(float force)
    {
        //Vector3 point = new Vector3(0,0,this.transform.position.z);
        if (force < 0)
        {
            if (this.transform.rotation.eulerAngles.z <= 270 && this.transform.rotation.eulerAngles.z >180) //90
            {
                force = 0;
            }
        }
        else
        {
            if (this.transform.rotation.eulerAngles.z >= 90 && this.transform.rotation.eulerAngles.z <= 180)
            {
                force = 0;
            } 
        }
        this.transform.Rotate(0, 0, force); //force*5      
    }

    public override void Clear()
    {
        //this.transform.position= new Vector3(0, -4, 0);
        this.transform.rotation = Quaternion.Euler(-90, 0, 0);
    }
}
