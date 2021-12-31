using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutputStringList : MonoBehaviour
{
	public StringListReference StringList;
	public StringListEvent StringListEvent;
	public void OutputList(){
		StringListEvent.Invoke(StringList);
	}
	public StringEvent IteratedStringEvent;
	public void IterateList(int input){
		string output = StringList.Value[input];
		IteratedStringEvent.Invoke(output);
	}
	public IntEvent CountEvent;
	public void OutputCount(){
		CountEvent.Invoke(StringList.Value.Count);
	}
}
