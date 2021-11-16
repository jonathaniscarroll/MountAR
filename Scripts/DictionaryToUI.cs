using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DictionaryToUI : MonoBehaviour
{
	[SerializeField]
	private Dictionary<string,string> thisElementData;
	public Dictionary<string,string> ThisElementData{
		get{
			return thisElementData;
		}
		set{
			thisElementData = value;
			OutputData.Invoke(thisElementData);
		}
	}
	
	public List<DictionaryToUI> ExistingUI{
		get{
			return _existingUI;
		}
		set {
			_existingUI = value;
		}
	}
	[SerializeField]
	private List<DictionaryToUI> _existingUI;
	
	public void Spawn(Dictionary<string,string> input){
		//cehck if post exists
		if(ExistingUI==null){
			ExistingUI = new List<DictionaryToUI>();
		}
		DictionaryToUI newPost = null;
		foreach(DictionaryToUI post in ExistingUI){
			Debug.Log("existing " + post.ThisElementData[CompareKey] + " input" + input[CompareKey]);
			if(post.ThisElementData[CompareKey]==input[CompareKey]){
				newPost = post;
			}
		}
		if(newPost == null){
			newPost = Instantiate(UIPrefab,UIParent);	
			ExistingUI.Add(newPost);
		}
		
		newPost.ThisElementData = input;
		if(OrderList)
			ExistingUI = ReorderList(ExistingUI);
	}
	
	public List<DictionaryToUI> ReorderList(List<DictionaryToUI> posts){
		List <DictionaryToUI> orderedList = posts.OrderByDescending(x => float.Parse(x.ThisElementData[OrderKey])).ToList();
		int count = 0;
		foreach(DictionaryToUI mp in orderedList){
			mp.transform.SetSiblingIndex(count);
			count++;
		}
		return orderedList;
	}
	
	public DictionaryToUI UIPrefab;
	public Transform UIParent;
	public string CompareKey;
	public string OrderKey;
	public bool OrderList;
	public StringStringDictionaryEvent OutputData;
	
}
