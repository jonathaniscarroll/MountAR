using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DitherManipulation : MonoBehaviour
{
	
	public Renderer meshRenderer;
	public Material instancedMaterial;
	
	[Range(0,1)]
	public float GradientIntensity;
	public float UVHorzSpeed;
	public float UVVertSpeed;
	
	void Start()
	{
		meshRenderer = GetComponent<Renderer>();
		instancedMaterial = meshRenderer.material;
		
	}

	public void Selected(){
		GradientIntensity = 1f;
		UVHorzSpeed = 50f;
		AlterMaterial();
	}
	public void Deselected(){
		GradientIntensity = .5f;
		UVHorzSpeed = 5f;
		AlterMaterial();
	}
	private void AlterMaterial(){
		instancedMaterial.SetFloat("_GradientInfluence", GradientIntensity);
		instancedMaterial.SetVector("_ScrollSpeed", new Vector4(UVHorzSpeed, UVVertSpeed, 0,0));
	}
}
