using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectFollowTransform : MonoBehaviour
{
	public Camera TargetCamera;
	//public GameObject ObjectToFollow{
	//	get;set;
	//}
	public Vector3 TargetPosition{
		get;set;
	}
	public Vector3Event OutputVector3;
	
	public void Follow(){
		//Vector3 targPos = ObjectToFollow.transform.position;
		if(TargetCamera==null){
			TargetCamera = Camera.main;
		}
		Vector3 pos = TargetCamera.WorldToScreenPoint (TargetPosition);
		OutputVector3.Invoke(pos);
	}
}
