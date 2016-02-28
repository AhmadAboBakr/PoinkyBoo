using UnityEngine;
using System.Collections;

public class NormalTile : Tile {
    Rigidbody myRigidBody;

    void Awake()
    {
        this.myRigidBody = this.GetComponent<Rigidbody>();
    }
    public override void Move(float force)
    {
        if (force < -1800)
        {
            this.myRigidBody.velocity = Vector3.zero;
            this.transform.position = Vector3.zero;
        }
        this.myRigidBody.AddForce(new Vector3(force, 0, 0), ForceMode.Impulse);

    }

    public override void Clear()
    {
        this.transform.position = new Vector3(0, 0, this.transform.position.z);
        this.myRigidBody.velocity = Vector3.zero;

    }
}
