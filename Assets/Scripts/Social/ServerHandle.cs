using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class ServerHandle : MonoBehaviour {
    public static ServerHandle instance;
    public Text ttttt;

	// Use this for initialization
    void Awake()
    {
        if (!ServerHandle.instance)
        instance = this;
    }
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public WWW GET(string url)
    {

        WWW www = new WWW(url);
        StartCoroutine(WaitForRequest(www));
        return www;
    }

    public WWW POST(string url, WWWForm form)
    {
        
        WWW www = new WWW(url, form);

        StartCoroutine(WaitForRequest(www));
        return www;
    }

    private IEnumerator WaitForRequest(WWW www)
    {
        yield return www;

        // check for errors
        if (www.error == null)
        {
            ttttt.text+=("WWW Ok!: " + www.text);
        }
        else
        {
            ttttt.text += ("WWW shit!: " + www.error);
        }
    }
    public void SendFbId() 
    {
        ttttt.text += FacebookIntegration.instance.profile["first_name"];
       
        WWWForm form = new WWWForm();
        ttttt.text += FacebookIntegration.instance.profile["first_name"];

        form.AddField("name", FacebookIntegration.instance.profile["first_name"]);

        //HUDManager.manager.gameObject.SetActive(true);
        ttttt.text += FB.UserId;

        form.AddField("fbId", FB.UserId);
        POST("http://45.55.200.82:80/api/user", form);
        ttttt.text += "end of request";


    }
}
