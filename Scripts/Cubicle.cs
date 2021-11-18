using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cubicle : ScriptableObject
{
	public List<CubicleObject> CubicleObjects{
		get{
			if(cubicleObjects==null){
				cubicleObjects = new List<CubicleObject>();
			}
			return cubicleObjects;
		} set{
			if(cubicleObjects==null){
				cubicleObjects = new List<CubicleObject>();
			}
			cubicleObjects = value;
		}
	}
	[SerializeField]
	private List<CubicleObject> cubicleObjects;
}
