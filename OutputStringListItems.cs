using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutputStringListItems : MonoBehaviour
{
	[System.Serializable]
	public class StringListItem {
		public int itemNumber;
		public StringEvent outputString;
	}
	public List<StringListItem> StringListItems;
	public StringListEvent OnOutputComplete;
	public void InputStringList(List<string> input){
		foreach(StringListItem item in StringListItems){
			
			string str = input[item.itemNumber];
			//Debug.Log(str,gameObject);
			item.outputString.Invoke(str);
		}
		OnOutputComplete.Invoke(input);
	}
}
