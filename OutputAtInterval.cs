using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutputAtInterval : MonoBehaviour
{
	public float Interval;
	private float LastValue;
	public FloatEvent OutputInterval;
	public void InputFloat(float input){
		float output = input - LastValue;
		if(output>=Interval){
			OutputInterval.Invoke(output);	
		}
		
		LastValue = input;
	}
}
