using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestCode : MonoBehaviour
{
    public InputField x;
    string y;
    void Update()
    {
        
    }

    public void thiingy(string auth) {
        y = x.text;
        Debug.Log(y);

        PlayerPrefs.SetString("Twitch_Auth_Key", auth);
    }

}

