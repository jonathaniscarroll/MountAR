using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class ObjectUI<T> : MonoBehaviour
{
	[Header("Generator")]
	public Transform Parent;
	[Header("Prefab")]
	public ObjectUI<T> Prefab;
	
	public T ThisObject{
		get{
			return thisObject;
		}set{
			thisObject = value;
			//OutputObjectName.Invoke(thisObject.ToString());
			Output.Invoke(thisObject);
		}
	}
	[SerializeField]
	private T thisObject;
	
	public UnityEvent<T> Output;
	
	public void InputObject(T input){
		ThisObject = input;
		//Output.Invoke(input);
	}
	public void Generate(T input){
		ObjectUI<T> output = Instantiate(Prefab,Parent);
		output.ThisObject = input;
	}
	public void OutputObject(){
		Output.Invoke(ThisObject);
	}
}
