using UnityEngine;
using System.Collections;

public class Poinky : MonoBehaviour {
    Rigidbody myRigidBody;
    Animator myAnimator;
    Vector3 velocity;
    public static Poinky instance;
    
    
    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }
	void Start () {
        myRigidBody = this.GetComponent<Rigidbody>();
        myAnimator = this.GetComponent<Animator>();
    }

    #region Events
    public void Play()
    {
        myRigidBody.isKinematic = false;
        myRigidBody.velocity= velocity;

    }
    public void Pause()
    {
        velocity = myRigidBody.velocity;
        myRigidBody.velocity = Vector3.zero;
        myRigidBody.isKinematic = true;
    }
    public void reset()
    {
        myRigidBody.velocity = Vector3.zero;
    }
    #endregion
    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "platform":
                float deltaX = this.transform.position.x - collision.transform.position.x;
                if (deltaX < -0.2f)
                {
                    myRigidBody.velocity = new Vector3(-2 , 10, 0) ;
                }
                else if (deltaX > 0.2f)
                {
                    myRigidBody.velocity = new Vector3(2, 10, 0) ;

                }
                else
                {
                    myRigidBody.velocity = new Vector3(0, 10, 0);
                }
                break;
            case "wall":
                Vector3 currentVelocity= myRigidBody.velocity;

                currentVelocity.x = -Mathf.Sign(collision.contacts[0].point.x)*3;
                myRigidBody.velocity = currentVelocity;
                break;
            default:
                break;
        }
    }
}
