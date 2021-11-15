using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRenderOrder : MonoBehaviour
{
	public List<SpriteRenderer> SpriteRenderers;
	public int Modifier = 100;
	
	void Start(){
		SpriteRenderers = new List<SpriteRenderer>(GetComponentsInChildren<SpriteRenderer>());
	}
	
	public void UpdateRenderers(){
		foreach(SpriteRenderer renderer in SpriteRenderers){
			renderer.sortingOrder = (int)(renderer.transform.position.z * Modifier);
		}
	}
}
