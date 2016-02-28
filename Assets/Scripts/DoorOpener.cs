using UnityEngine;
using System.Collections;

public class DoorOpener : MonoBehaviour {
    public Animator myAnimator;
    void Start()
    {
        myAnimator = this.GetComponent<Animator>();
    }
    void OnTriggerEnter()
    {
       // Debug.Log("kotomoto ya 7lwa yabata");
        myAnimator.SetTrigger("Open");
    }
}
