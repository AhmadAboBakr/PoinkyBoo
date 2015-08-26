using UnityEngine;
using System.Collections;

public class MainScreen : MonoBehaviour {
    //cached Objects 
    Animator myAnimator;
    void Start()
    {
        myAnimator = this.GetComponent<Animator>();
    }
    public void PlayButton()
    {

    }
    public void InfoButton()
    {
        myAnimator.SetTrigger("GoToOther");
    }
}
