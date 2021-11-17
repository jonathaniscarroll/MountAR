using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnNPC : MonoBehaviour
{
	public UserController NPCPrefab;
	public Transform SpawnParent;
	public float MaxQuantity;
	//public List<NPC> NPCs;
	public StringReference CurrentUserID;
	
	
	
	[System.Serializable]
	public class NPC{
		public string userID;
		public UserController userController;
		public NPC(string id,string name,UserController prefab,Transform parent,string sprite){
			userID = id;
			userController = Instantiate(prefab,parent);
			userController.gameObject.name = name;
			userController.UserName = name;
			userController.UserID = id;
			userController.InputMessage("sprite " + sprite);
		}
	}
	

	private int quantity = 0;
	public void InputNPCData(Dictionary<string,string> data){
		NPC NPC;
		string name = "";
		string id = "";
		string sprite = "";
		
		foreach(KeyValuePair<string,string> kvp in data){
			if(kvp.Key=="userid"){
				id = kvp.Value;
			} 
			if(kvp.Key=="sprite"){
				sprite = kvp.Value;
			}
			if(kvp.Key=="username"){
				name = kvp.Value;
				
			}
		}
		if(quantity<MaxQuantity){
			//Debug.Log(quantity);
			quantity++;
			NPC = new NPC(id,name,NPCPrefab,SpawnParent,sprite);
		}
		//NPC = new NPC(id,name,NPCPrefab,SpawnParent,sprite);
	}
	
	
	
}
