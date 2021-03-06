﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.ComponentModel;
using System.Net.Sockets;
using System.IO;
using UnityEngine.UI;

public class TwitchIntegration : MonoBehaviour
{

    public int WaveNumber = 1;
    public int TwitchCurrency = 500;
    public int TwitchCurrencyLevel = 500;
    public Transform SpawnersObj;
    public List<Transform> Spawners = new List<Transform>();

    int EnemyBombers = 3;
    public GameObject EnemyBomb;
    public GameObject Enemy1;
    public GameObject Enemy2;
    public GameObject Enemy3;
    public GameObject Enemy4;
    public int Enemy1Cost = 50;
    public int Enemy2Cost = 100;
    public int Enemy3Cost = 150;
    public int Enemy4Cost = 200;
    public Text currencyText;

    public Text waveCounter;
    public Animator uiAnimController;

    int CurSpawner = 0;

    // I'm going to document this one since it's networking so you don't get confused
    // It's not a hard to understand script, but some stuff is unclear

    private TcpClient twitchClient; // Twitch's client
	private StreamReader reader; // The reader of the stream, records chatter's chattings
	private StreamWriter writer; // Writes stuff to the chat

	// Password from https://twitchapps.com/tmi, username and channel name are your Twitch ones
	public string username, password, channelName;

	//public Text chatBox; // UI text that displays chat on-screen. You can comment this out or delete it if you don't want it

    // Start connecting to Twitch, and start putting a 'thank you' message in the chat,
	// along with any commands you want to remind chatters they can use
	// Change the '60f' to however often in seconds you want the message to be put in chat
    void Start()
    {
        password = PlayerPrefs.GetString("Twitch_Auth_Key");
        channelName = PlayerPrefs.GetString("Twitch_Display_Name");
        username = PlayerPrefs.GetString("Twitch_Username");

        Debug.Log(password); 

        //player prefs.getstring twitch autho key. 
        //outside- seprate (public) which will record
        //player pref as string 

        foreach (Transform Spawner in SpawnersObj)
        {
            Spawners.Add(Spawner);
        }
		Connect ();
		InvokeRepeating ("chatThankYou", 0f, 30f);
        SpawnBombers();
    }

    // Reconnect if we lost connection, and if we're connected start reading the chat
    void Update()
    {
		if (!twitchClient.Connected) {
			Connect ();
		} else {
			ReadChat ();
		}
    }

	// Connect to Twitch using usernames, passwords and all that stuff. Anything you
	// don't understand I probably don't either, but it needs to be there so no touchy
	private void Connect (){
		twitchClient = new TcpClient ("irc.chat.twitch.tv", 6667);
		reader = new StreamReader (twitchClient.GetStream ());
		writer = new StreamWriter (twitchClient.GetStream ());

		writer.WriteLine ("PASS " + password);
		writer.WriteLine ("NICK " + username);
		writer.WriteLine ("USER " + username + " 8 * :" + username);

		writer.WriteLine ("JOIN #" + channelName);
		writer.Flush ();

        string response = reader.ReadLine();
        Debug.Log(response);
    }

	// Begin reading the chat, checking for messages and getting the important bits
	// If you've removed the UI stuff earlier you'll need to remove chatBox.text = String.Format etc
	private void ReadChat(){

		if(twitchClient.Available > 0){

			var message = reader.ReadLine ();

			if (message.Contains ("PRIVMSG")) {
				var splitPoint = message.IndexOf ("!", 1);
				var chatName = message.Substring (0, splitPoint);
				chatName = chatName.Substring (1);

				splitPoint = message.IndexOf (":", 1);
				message = message.Substring(splitPoint + 1);
				//chatBox.text = String.Format ("{0}: {1}", chatName, message)  + "\n" + chatBox.text;

				GameInputs (message);
			}

			// This keeps the connection alive, pings twitch to stop it from cutting off the connection
			if (message.Contains("PING"))
			{
				writer.WriteLine("PONG :tmi.twitch.tv");
				writer.Flush();
			}
		}
	}

    // Use this bit for thanking players but also displaying a list of commands they can use during the game
    // If there's different commands for different states of the game i.e. movement, fighting
    // Probably put some if statements in there so you don't flood the chat with one giant message
    private void chatThankYou()
    {
        string message1 = "PRIVMSG #" + channelName + " :" + "Command List: " + "'!tank' Cost: " + Enemy1Cost + " - '!drone' Cost: " + Enemy2Cost + " - '!chopper' Cost: " + Enemy3Cost + " - '!heavytank' Cost: " + Enemy4Cost;
        writer.WriteLine(message1);
        writer.Flush();
    }

    // Reading chat's commands happens here. Use the template if statement and copy it to make new commands
    // MAKE SURE when making a new command the string you enter is LOWER CASE since ToLower tries to keep it consistent
    private void GameInputs(string ChatInputs)
    {
        /*ChatInputs.ToLower();
        switch (ChatInputs)
        {
            case "!tank":
                SpawnAI1();
                break;
            case "!drone":
                SpawnAI2();
                break;
            case "!chopper":
            case "!helicopter":
                SpawnAI3();
                break;
            case "!heavy tank":
            case "!heavytank":
            case "!big tank":
            case "!bigtank":
                SpawnAI4();
                break;;
            default:
                Debug.Log("Chat command incorrect!");
                break;
        }*/

        if (ChatInputs.ToLower() == "!tank")
        {
            SpawnAI1();
        }

        if (ChatInputs.ToLower() == "!drone")
        {
            SpawnAI2();
        }

        if (ChatInputs.ToLower() == "!chopper")
        {
            SpawnAI3();
        }

        if (ChatInputs.ToLower() == "!heavytank")
        {
            SpawnAI4();
        }
    }

    void SpawnAI1()
    {
        if (Enemy1Cost > TwitchCurrency)
        {
            return;
        }
        TwitchCurrency -= Enemy1Cost;
        CurSpawner += 1;
        if(CurSpawner == Spawners.Count)
        {
            CurSpawner = 0;
        }
        Instantiate(Enemy1, Spawners[CurSpawner].position, Quaternion.identity);
        CheckTwitchCurrency();
        currencyText.text = TwitchCurrency.ToString();
    }

    void SpawnAI2()
    {
        if (Enemy2Cost > TwitchCurrency)
        {
            return;
        }
        TwitchCurrency -= Enemy2Cost;
        CurSpawner += 1;
        if (CurSpawner == Spawners.Count)
        {
            CurSpawner = 0;
        }
        Instantiate(Enemy2, Spawners[CurSpawner].position, Quaternion.identity);
        CheckTwitchCurrency();
        currencyText.text = TwitchCurrency.ToString();
    }

    void SpawnAI3()
    {
        if (Enemy3Cost > TwitchCurrency)
        {
            return;
        }
        TwitchCurrency -= Enemy3Cost;
        CurSpawner += 1;
        if (CurSpawner == Spawners.Count)
        {
            CurSpawner = 0;
        }
        Instantiate(Enemy3, Spawners[CurSpawner].position, Quaternion.identity);
        CheckTwitchCurrency();
        currencyText.text = TwitchCurrency.ToString();
    }

    void SpawnAI4()
    {
        if (Enemy4Cost > TwitchCurrency)
        {
            return;
        }
        TwitchCurrency -= Enemy4Cost;
        CurSpawner += 1;
        if (CurSpawner == Spawners.Count)
        {
            CurSpawner = 0;
        }
        Instantiate(Enemy4, Spawners[CurSpawner].position, Quaternion.identity);
        CheckTwitchCurrency();
        currencyText.text = TwitchCurrency.ToString();
    }

    void SpawnBombers()
    {
        for(int i = 0; i < EnemyBombers; i++)
        {
            Instantiate(EnemyBomb, Spawners[CurSpawner].position, Quaternion.identity);
            CurSpawner += 1;
            if(CurSpawner == Spawners.Count)
            {
                CurSpawner = 0;
            }
        }
        EnemyBombers += 1;
    }

    void CheckTwitchCurrency()
    {
        if(TwitchCurrency <= 0)
        {
            Invoke("WaveEnd", 20);
        }
    }

    void WaveEnd()
    {
        waveCounter.text = "PREPARE FOR ROUND " + (WaveNumber + 1);
        uiAnimController.SetTrigger("WaveChange");
        TwitchCurrencyLevel += 100;
        TwitchCurrency = TwitchCurrencyLevel;
        currencyText.text = TwitchCurrencyLevel.ToString();
        WaveNumber += 1;
        SpawnBombers();
    }

}
