using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomVector3 : MonoBehaviour
{
	public float Scale;
	public Vector3Event OutputVector3;
	public void Output(){
		Vector3 output = Random.onUnitSphere * Scale;
		OutputVector3.Invoke(output);
	}
}
