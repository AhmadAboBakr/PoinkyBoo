using UnityEngine;
using System.Collections;

public class MenuToggle : MonoBehaviour {
    //cached Objects 
    Animator myAnimator;
    void Start()
    {
        myAnimator = this.GetComponent<Animator>();
    }
    public void PlayButton()
    {

    }
    public void GoToOtherMenu()
    {
        myAnimator.SetTrigger("GoToOther");
    }
    public void GoToOMainMenu()
    {
        myAnimator.SetTrigger("GoToMain");
    }
}
