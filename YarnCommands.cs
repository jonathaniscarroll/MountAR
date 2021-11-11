using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.Events;
using System.Linq;

public class YarnCommands : MonoBehaviour
{
	[System.Serializable]
	public class Command{
		public string name;
		public UnityEvent unityEvent;
	}
	//[System.Serializable]
	//public class CommandWithString{
	//	public string name;
	//	public StringEvent stringEvent;
	//}
	public List<Command> Commands;
	public StringEvent OutputUserCommand;
	[YarnCommand("UserCommand")]
	public void UserCommand(string command){
		string[] str = command.Split(char.Parse("_"));
		string output = "";
		for(int i  = 0;i<str.Length-1;i++){
			output += str[i]+" ";
		}
		output += str[str.Length-1];
		OutputUserCommand.Invoke(output);
	}
	
	[YarnCommand("YarnCommand")]
	public void InputCommand(string commandName){
		Command command = Commands.FirstOrDefault(i => i.name == commandName);
		command.unityEvent.Invoke(); 
	}
	
	[YarnCommand("DialogueTrigger")]
	public void InputDialogueTriggerCommand(string command){
		OutputUserCommand.Invoke(command);
	}
}
