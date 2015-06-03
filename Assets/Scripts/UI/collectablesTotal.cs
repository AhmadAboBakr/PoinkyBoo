using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class collectablesTotal : MonoBehaviour {
    public static collectablesTotal instance;
    public Text text;
    int collectables;
    void Awake()
    {
        instance = this;
    }
	public void Start () {
        text = this.GetComponent<Text>();
        text.text = PlayerPrefs.GetInt("Collectables").ToString();
	}
    public void change(int collectables)
    {
        text.text = collectables+"";
        PlayerPrefs.SetInt("Collectables", collectables);

    }
    public void add(int collectables)
    {
        collectables = PlayerPrefs.GetInt("Collectables") + collectables;
        PlayerPrefs.SetInt("Collectables", collectables);
    }
}
