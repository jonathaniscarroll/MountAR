using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostController : MonoBehaviour
{
	public Post Post{
		get{
			return _post;
		} set{
			_post = value;
			_post.ContentUpdate.AddListener(OutputPostText);
			_post.LikeEvent.AddListener(OutputLikes);
		}
	}
	[SerializeField]
	private Post _post;
	
	public int BrandMutation{
		get{
			return brandMutation;
		} set{
			brandMutation = value;
			OutputBrandMutation.Invoke(brandMutation);
		}
	}
	[SerializeField]
	private int brandMutation;
	
	public int Virality{
		get{
			return virality;
		} set{
			virality = value;
			OutputVirality.Invoke(virality);
		}
	}
	[SerializeField]
	private int virality;
	
	public PostController PostPrefab;
	public Transform PostParent;
	public StringEvent OutputPostTextEvent;
	public IntEvent OutputBrandMutation;
	public IntEvent OutputVirality;
	public IntEvent OutputLikesEvent;
	
	public void OutputPostText(string input){
		OutputPostTextEvent.Invoke(input);
	}
	public void OutputLikes(int input){
		OutputLikesEvent.Invoke(input);
	}
	
	public void SpawnPost(string input){
		PostController output = Instantiate(PostPrefab,PostParent.position,Quaternion.identity,PostParent);
		output.Post = ScriptableObject.CreateInstance<Post>();
		output.Post.Content = input;
		output.BrandMutation = BrandMutation;
		output.Virality = Virality;
	}
	
	public void SetLikes(int input){
		Post.Likes = input;
	}
	
	
}
