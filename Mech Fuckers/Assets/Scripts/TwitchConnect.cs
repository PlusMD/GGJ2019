using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.ComponentModel;
using System.Net.Sockets;
using System.IO;
using UnityEngine.SceneManagement;

public class TwitchConnect : MonoBehaviour
{
    public string dispName;
    public string userName;
    public string authKey;

    private TcpClient twitchClient;
    private StreamReader reader;
    private StreamWriter writer;

    public Text connectionStatus;
    public Text connectionStatusDetail;

    public Text displayText;
    public Text userText;
    public Text authText;

    public GameObject connectionUI;
    public GameObject launchUI;

    void Start()
    {
        dispName = PlayerPrefs.GetString("Twitch_Display_Name");
        userName = PlayerPrefs.GetString("Twitch_Username");
        authKey = PlayerPrefs.GetString("Twitch_Auth_Key");



        displayText.text = dispName;
        userText.text = userName;
        authText.text = authKey;
    }

    public void TwitchCheckConnectionDN(string disp) {

        dispName = disp;
        
    }

    public void TwitchCheckConnectionUN(string user) {

        userName = user;
        
    }

    public void TwitchCheckConnectionAK(string auth) {

        authKey = auth;
        
    }

    public void ConnectToTwitch() {

        twitchClient = new TcpClient("irc.chat.twitch.tv", 6667);
        reader = new StreamReader(twitchClient.GetStream());
        writer = new StreamWriter(twitchClient.GetStream());

        writer.WriteLine("PASS " + authKey);
        writer.WriteLine("NICK " + "5");
        writer.WriteLine("USER " + userName + " 8 * :" + userName);

        writer.WriteLine("JOIN #" + dispName);
        writer.Flush();

        string response = reader.ReadLine();
        Debug.Log(response);

        if (response.Contains("Welcome"))
        {
            connectionStatus.text = "Connected!";
            connectionStatusDetail.text = response.ToString();
            connectionUI.SetActive(false);
            launchUI.SetActive(true);

            PlayerPrefs.SetString("Twitch_Display_Name", dispName);
            PlayerPrefs.SetString("Twitch_Username", userName);
            PlayerPrefs.SetString("Twitch_Auth_Key", authKey);
        }

        else
        {
            connectionStatus.text = "Connection failed";
            connectionStatusDetail.text = response.ToString();
        }
    }

    public void StartGame() {
        SceneManager.LoadScene(1);
    }
}

