using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StoreBtn : MonoBehaviour {
    public Animator anim;
    public Image img;
    public Image myImg;
    public void Start()
    {
//        img = GetComponentInParent<Image>();
        //myImg = new Image();
    }

    public void Update()
    {

    }

   public  void PlayAnim()
    {
        img.sprite=myImg.sprite;
       
        anim.SetTrigger("Play");
        anim.SetTrigger("Stop");
    }

}
