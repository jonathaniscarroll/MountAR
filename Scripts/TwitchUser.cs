using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TwitchUser : MonoBehaviour
{
	public List<UserController> CurrentUsers;
	public List<UserController> PossibleUsers;
	public GameObjectEvent OutputNewUser;
	public string PrependName;
	
	public void ReadChatMessage(string userName, string message){
		UserController user = CurrentUsers.FirstOrDefault(i => i.UserName == userName);
		if(user == null){
			user = GenerateUser(userName);
		}
		user.Message = message;
	}
	
	public UserController GenerateUser(string userName){
		UserController newUser = Instantiate(PossibleUsers[Random.Range(0,PossibleUsers.Count)]);
		newUser.UserName = PrependName + userName;
		newUser.gameObject.name = userName;
		CurrentUsers.Add(newUser);
		OutputNewUser.Invoke(newUser.gameObject);
		return newUser;
	}
}
