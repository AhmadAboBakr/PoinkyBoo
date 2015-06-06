using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class DisableSelf : MonoBehaviour {

    // Use this for initialization
    public void Disable()
    {
        this.gameObject.SetActive(false);
    }
    public void StartFading()
    {
        this.GetComponent<Animator>().SetTrigger("StartFade");
    }
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartFading();
        }
	}
}
