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
			Cubicle.CubicleObjects = value;
		}
	}
	[SerializeField]
	private List<CubicleObject> cubicleObjects;
	public CubicleEvent OutputCubicle;
	public GameObjectEvent OutputWorkObjectEvent;
	
	public void Initilize(){
		Cubicle = ScriptableObject.CreateInstance<Cubicle>();
		Cubicle.CubicleObjects = CubicleObjects;
		OutputCubicle.Invoke(Cubicle);
	}
	
	public void OutputWorkObject(){
		
		GameObject output = Cubicle.CubicleObjects.OrderByDescending(i => i.Work).FirstOrDefault().gameObject;
		Debug.Log("wokr object",output);
		OutputWorkObjectEvent.Invoke(output);
	}
}
