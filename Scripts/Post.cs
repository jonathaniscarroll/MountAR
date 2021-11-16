using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Post : ScriptableObject
{
	public string Content{
		get{return _content;} set{_content = value;ContentUpdate.Invoke(_content);}
	}
	[SerializeField]
	private string _content;
	public StringEvent ContentUpdate = new StringEvent();
	public int Likes{
		get{return likes;}set{likes = value;LikeEvent.Invoke(likes);}
	}
	[SerializeField]
	private int likes;
	public IntEvent LikeEvent = new IntEvent();
}
