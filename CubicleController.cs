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
	public GameObjectEvent OutputWorkObjectEvent;
	public GameObjectEvent OutputRestObjectEvent;
	
	public void Initilize(){
		if(CubicleObjects==null || CubicleObjects.Count ==0){
			GetCubicleObjects();
		}
		Cubicle = ScriptableObject.CreateInstance<Cubicle>();
		Cubicle.CubicleObjects = CubicleObjects;
		OutputCubicle.Invoke(Cubicle);
	}
	
	public void OutputWorkObject(){
		GameObject output = Cubicle.CubicleObjects.OrderByDescending(i => i.Work).FirstOrDefault().gameObject;
		//Debug.Log("wokr object",output);
		OutputWorkObjectEvent.Invoke(output);
	}
	public void OutputRestObject(){
		GameObject output = Cubicle.CubicleObjects.OrderByDescending(i => i.Rest).FirstOrDefault().gameObject;
		//Debug.Log("wokr object",output);
		OutputRestObjectEvent.Invoke(output);
	}
	public void GetCubicleObjects(){
		CubicleObjects = new List<CubicleObject>(GetComponentsInChildren<CubicleObject>());
	}
}
