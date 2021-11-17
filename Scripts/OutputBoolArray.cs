using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class OutputBoolArray : MonoBehaviour
{
	[System.Serializable]
	public class BoolArrayEvent:UnityEvent<bool[]>{
		
	}
	
	public bool[] BoolArray;
	public BoolArrayEvent OutputBoolArrayEvent;
	public void ChangeRandom(){
		System.Random rnd =new System.Random();
		
		int boolToChange = rnd.Next(BoolArray.Length);
		//Debug.Log(boolToChange);
		BoolArray[boolToChange] = !BoolArray[boolToChange];
		OutputBoolArrayEvent.Invoke(BoolArray);
	}
	public void SetArray(bool[] input){
		BoolArray = input;
		OutputBoolArrayEvent.Invoke(BoolArray);
	}
}
