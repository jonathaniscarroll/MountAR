using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnSphere : MonoBehaviour
{
	public float Scale;
	public List<GameObject> Buttons;
	public void Spawn(){
		foreach(GameObject Button in Buttons){
			Vector3 point = (Random.onUnitSphere+transform.position) * Scale;
			//GameObject output = Instantiate(Button,point,Quaternion.identity,transform);
			Button.transform.position = point;
			Button.transform.LookAt(transform,Vector3.up);
			
		}
	}
}
