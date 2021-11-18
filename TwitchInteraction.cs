using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwitchInteraction : MonoBehaviour
{
	public List<string> InternNames{get{return internNames;}set{internNames = value;}}
	[SerializeField]
	private List<string> internNames = new List<string>();
	public StringStringEvent OutputInternCommand;
	public void InputStringList(List<string> input){
		string command = input.Find(f=>f.ToLower()=="live"||f.ToLower()=="work");
		string internName = "";
		for(int i = 0; i<input.Count;i++){
			if(string.Equals(input[i].ToLower(),"intern")){
				//this word is intern
			} else {
				foreach(string iName in InternNames){
					if(iName.Contains(input[i])){
						internName = iName;
						OutputInternCommand.Invoke(internName,command);
						return;
					}
				}
			}
			
		}
	}
}
