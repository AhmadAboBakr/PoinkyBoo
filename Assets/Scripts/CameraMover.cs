using UnityEngine;
using System.Collections;

public class CameraMover : MonoBehaviour
{
    public static CameraMover instance;
    void Awake()
    {
        if (!CameraMover.instance)
        {
            CameraMover.instance = this;

        }
    }
    Animator CameraAnimator;
    RaycastHit r;
    public GameObject cube;
    // Use this for initialization
    void Start()
    {
        CameraAnimator = this.GetComponent<Animator>();
        GameManager.Move += CameraMove;
        //GameManager.clear += CameraReturn;
    }

    // Update is called once per frame
    public void CameraMove()
    {
        if (Physics.Raycast(new Vector3(0, -3, this.transform.position.z + 10), -Vector3.up, out r, float.MaxValue, 1 << 12))
        {
            Debug.Log("wsel  " + r.collider.tag);

            if (r.collider.tag == "Normal" || r.collider.tag == "DesertDoorOut")
            {
                CameraAnimator.SetBool("InDesert", false);
            }
            else if (r.collider.tag == "Desert" || r.collider.tag == "DesertDoorIn")
            {
                CameraAnimator.SetBool("InDesert", true);
            }
        }
    }
    public void CameraReturn()
    {
        CameraAnimator.SetBool("InDesert", false);
        Debug.Log("rage3");

        //if (Physics.Raycast(new Vector3(0, -3, this.transform.position.z + 10), -Vector3.up, out r, float.MaxValue, 1 << 12))
        //{
        //    if (r.collider.tag == "Normal" || r.collider.tag == "DesertDoorOut")
        //    {
        //        CameraAnimator.SetBool("InDesert", false);
        //    }
        //    else if (r.collider.tag == "Desert" || r.collider.tag == "DesertDoorIn")
        //    {
        //        CameraAnimator.SetBool("InDesert", true);
        //    }
        //}
       
    }
    public void Death()
    {
        CameraAnimator.SetTrigger("Death");
        Debug.Log("maaaat");
    }

}
