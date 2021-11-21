using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemJuicer : MonoBehaviour
{
	[Header("PSToJuice is set up by the ParticleSystemFinder component.")]
	public ParticleSystem PSToJuice;
	public int MinParticles = 10;
	public int MaxParticles = 20;
	
	public void EmitParticles(){
		if(PSToJuice != null){
			int RandomCount = Random.Range(MinParticles, MaxParticles);
			PSToJuice.Emit(RandomCount);
		}
	}
    
}
