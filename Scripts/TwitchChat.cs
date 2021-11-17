using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.ComponentModel;
using System.Net.Sockets;
using System.IO;
using UnityEngine.UI;

public class TwitchChat : MonoBehaviour {

	private TcpClient twitchClient;
	private StreamReader reader;
	private StreamWriter writer;

	public string username, password, channelName; //Get the password from https://twitchapps.com/tmi
	public StringStringEvent OutputMessage;

	void Start () {
		Connect();
	}
	
	void Update () {
		if (!twitchClient.Connected)
		{
			Connect();
		}

		ReadChat();
	}

	private void Connect()
	{
		Debug.Log("doing a connect ");
		twitchClient = new TcpClient("irc.chat.twitch.tv", 6667);
		reader = new StreamReader(twitchClient.GetStream());
		writer = new StreamWriter(twitchClient.GetStream());
	
		writer.WriteLine("PASS " + password);
		writer.WriteLine("NICK " + username);
		writer.WriteLine("USER " + username + " 8 * :" + username);
		writer.WriteLine("JOIN #" + channelName);
		writer.Flush();
	}

	private void ReadChat()
	{
		if(twitchClient.Available > 0)
		{
			var message = reader.ReadLine(); //Read in the current message
			Debug.Log(message);
			if (message.Contains("PRIVMSG"))
			{
				//Get the users name by splitting it from the string
				var splitPoint = message.IndexOf("!", 1);
				var chatName = message.Substring(0, splitPoint);
				chatName = chatName.Substring(1);

				//Get the users message by splitting it from the string
				splitPoint = message.IndexOf(":", 1);
				message = message.Substring(splitPoint + 1);
				
				OutputMessage.Invoke(chatName,message);
				//print(String.Format("{0}: {1}", chatName, message));
				//chatBox.text = chatBox.text + "\n" + String.Format("{0}: {1}", chatName, message);

				//Run the instructions to control the game!
				//GameInputs(message);
			}
			if(message == "PING :tmi.twitch.tv")
			{
				SendIRC("PONG :tmi.twitch.tv");
			}
		}
	}

	private void SendIRC(string message){
		writer.WriteLine(message);
		writer.Flush();
	}
}
