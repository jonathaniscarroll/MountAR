using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Scale:ScriptableObject{
	public List<int> pitches;
	public Scale(){
		pitches = new List<int>();
	}
}
