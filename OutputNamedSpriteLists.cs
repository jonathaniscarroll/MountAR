using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutputNamedSpriteLists : MonoBehaviour
{
	[System.Serializable]
	public class NamedSpriteList{
		public string Name;
		public SpriteListReference SpriteList;
		
	}
	public List<NamedSpriteList> NamedSpriteLists;
	public SpriteListEvent OutputSpriteList;
	public void InputString(string input){
		List<Sprite> output = NamedSpriteLists.Find(c => c.Name == input).SpriteList;
		if(output.Count>0){
			OutputSpriteList.Invoke(output);
		}
	}
	
}
