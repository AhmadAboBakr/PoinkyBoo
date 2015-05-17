using UnityEngine;
using System.Collections;
using Facebook.MiniJSON;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
delegate void LoadPictureCallback(Texture texture);

public class FacebookIntegration : MonoBehaviour {
    public Text nameW7agatTania;
    public Image soreetAlProfile;
    public static FacebookIntegration instance;
    public Dictionary<string, string> profile;
    List<object> friends;
    public Text ttttt;
    
	// Use this for initialization
    void Awake()
    {
        instance = this;
    }
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void Login() 
    {
        if (!FB.IsLoggedIn)
        {
            FB.Login("email,publish_actions,user_friends", LoginCallback);
            ttttt.text += "facebook logged in ";
        }
        else
        {
            OnLoggedIn();
            ServerHandle.instance.SendFbId();

        }
    }
    public void LoginCallback(FBResult result)
    {
        Debug.Log("LoginCallback");
        ttttt.text += "log in call back";

        if (FB.IsLoggedIn)
        {
            OnLoggedIn();
        }
    }

    public void OnLoggedIn()
    {
        ttttt.text += "on logged in entre ";


        FB.API("/me?fields=id,first_name,friends.limit(100).fields(first_name,id)", Facebook.HttpMethod.GET, APICallback);
        //LoadPictureAPI(Util.GetPictureURL("me", 128, 128), MyPictureCallback);
        ttttt.text += "on logged in exit ";


    }
    //void MyPictureCallback(Texture texture)
    //{

    //    if (texture == null)
    //    {
    //        // Let's just try again
    //        LoadPictureAPI(Util.GetPictureURL("me", 128, 128), MyPictureCallback);
    //        return;
    //    }
    //    var spr = new Sprite();
    //    //spr.texture= texture.;
    //    //soreetAlProfile.sprite = spr;
    //    //GameStateManager.UserTexture = texture;
    //}
    IEnumerator LoadPictureEnumerator(string url, LoadPictureCallback callback)
    {
        WWW www = new WWW(url);
        yield return www;
        callback(www.texture);
    }
    void LoadPictureAPI(string url, LoadPictureCallback callback)
    {
        FB.API(url, Facebook.HttpMethod.GET, result =>
        {
            if (result.Error != null)
            {
                Util.LogError(result.Error);
                return;
            }

            var imageUrl = Util.DeserializePictureURLString(result.Text);

            StartCoroutine(LoadPictureEnumerator(imageUrl, callback));
        });
    }
    void LoadPictureURL(string url, LoadPictureCallback callback)
    {
        StartCoroutine(LoadPictureEnumerator(url, callback));

    }
    void APICallback(FBResult result)
    {
        ttttt.text += "APICallback entre \n";

        if (result.Error != null)
        {
            Util.LogError(result.Error);
            // Let's just try again                                                                                                
            FB.API("/me?fields=id,email,first_name,friends.limit(100).fields(first_name,id)", Facebook.HttpMethod.GET, APICallback);
            return;
        }
       // ttttt.text = result.Text;
        profile=Util.DeserializeJSONProfile(result.Text);
        ServerHandle.instance.SendFbId();
        //FB.UserId;
        //ttttt.text += "profile add \n";
        
        //GameStateManager.Username = profile["first_name"];
            
            
            friends = Util.DeserializeJSONFriends(result.Text);
            //ttttt.text += "friends add \n";

        //profile["first_name"];
        foreach (var friend in friends)
        {
            nameW7agatTania.text += "\n" + friend.ToString();
        }
        nameW7agatTania.text =result.Text;
        
    }
    public void Share(string score)
    {
        //Debug.Log("http://apps.facebook.com/" + FB.AppId + "/?challenge_brag=" + (FB.IsLoggedIn ? FB.UserId : "guest"));
        FB.Feed(
                linkCaption: "Play Poinky with me",
                picture: "http://i.imgur.com/WD7nqDE.png",
                linkName: "I just got " +score+ " in Poinky! Can you beat me?",
                link: "https://www.facebook.com/PoinkyGame" 
                );
    }       
}
