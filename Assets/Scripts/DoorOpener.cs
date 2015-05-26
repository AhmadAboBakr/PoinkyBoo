using UnityEngine;
using System.Collections;

public class DoorOpener : MonoBehaviour {
    public Animator myAnimator;
    
    void OnTriggerEnter()
    {
        myAnimator.SetTrigger("Open");
    }
}
