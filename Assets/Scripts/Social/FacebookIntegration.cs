using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
delegate void LoadPictureCallback(Texture texture);

public class FacebookIntegration : MonoBehaviour
{
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
    void Start()
    {

    }
}

