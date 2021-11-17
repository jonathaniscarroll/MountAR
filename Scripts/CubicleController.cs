using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;
[System.Serializable]
public class CubicleEvent:UnityEvent<Cubicle>{
	
}

public class CubicleController : MonoBehaviour
{
	public Cubicle Cubicle{
		get{return cubicle;}set{cubicle = value;}
	}
	[SerializeField]
	private Cubicle cubicle;
	
	
	public List<CubicleObject> CubicleObjects{
		get{
			return cubicleObjects;
		}
		set{
			cubicleObjects = value;
			if(Cubicle!=null)
			Cubicle.CubicleObjects = value;
		}
	}
	[SerializeField]
	private List<CubicleObject> cubicleObjects;
	public CubicleEvent OutputCubicle;
	//public TransformEvent OutputWorkObjectEvent;
	//public TransformEvent OutputRestObjectEvent;
	public TransformEvent OutputInteractionObject;
	
	public void Initilize(){
		if(CubicleObjects==null || CubicleObjects.Count ==0){
			GetCubicleObjects();
		}
		Cubicle = ScriptableObject.CreateInstance<Cubicle>();
		Cubicle.CubicleObjects = CubicleObjects;
		OutputCubicle.Invoke(Cubicle);
	}
	
	public void OutputWorkObject(){
		if(Cubicle==null){
			return;
		}
		Transform output = Cubicle.CubicleObjects.OrderByDescending(i => i.Work).FirstOrDefault().InteractionPoint;
		//Debug.Log("wokr object",output);
		OutputInteractionObject.Invoke(output);
	}
	public void OutputRestObject(){
		Transform output = Cubicle.CubicleObjects.OrderByDescending(i => i.Rest).FirstOrDefault().InteractionPoint;
		//Debug.Log("wokr object",output);
		OutputInteractionObject.Invoke(output);
	}
	public void OutputRandomObject(){
		Transform output = Cubicle.CubicleObjects[Random.Range(0,Cubicle.CubicleObjects.Count)].InteractionPoint;
		OutputInteractionObject.Invoke(output);
		
	}
	public void GetCubicleObjects(){
		CubicleObjects = new List<CubicleObject>(GetComponentsInChildren<CubicleObject>());
	}
}
