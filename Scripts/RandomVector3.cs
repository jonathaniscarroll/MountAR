using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomVector3 : MonoBehaviour
{
	public float Scale;
	public Vector3 Modifier = Vector3.one;
	public Vector3Event OutputVector3;
	public void Output(){
		Vector3 output = Random.onUnitSphere * Scale;
		output = new Vector3(output.x *Modifier.x,output.y *Modifier.y,output.z *Modifier.z);
		OutputVector3.Invoke(output);
	}
	
}
