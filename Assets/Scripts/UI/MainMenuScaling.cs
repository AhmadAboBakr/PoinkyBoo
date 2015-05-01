using UnityEngine;
using System.Collections;

public class MainMenuScaling : MonoBehaviour {
	// Use this for initialization
    public Canvas parent;
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        //this.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -parent.GetComponent<RectTransform>().rect.height/2.0f);
        this.GetComponent<RectTransform>().sizeDelta = new Vector2(parent.GetComponent<RectTransform>().rect.height * 2, parent.GetComponent<RectTransform>().rect.height * 2);

	}
}
