using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class collectablesTotal : MonoBehaviour {
    public static collectablesTotal instance;
    public Text text;
    void Awake()
    {
        instance = this;
    }
	public void Start () {
        text = this.GetComponent<Text>();
        text.text = PlayerPrefs.GetInt("Collectables").ToString();
	}
}
