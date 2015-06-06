using UnityEngine;
using System.Collections;

public class Loading : MonoBehaviour {
    
    AsyncOperation async;
	// Use this for initialization
	void Start () {
        async = Application.LoadLevelAsync("PoinkyGame");
        async.allowSceneActivation = false;
	}
    float time = 0;
	// Update is called once per frame

    public void LoadScene()
    {
        async.allowSceneActivation = true;
    }
}
