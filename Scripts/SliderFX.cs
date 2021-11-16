using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// A class created for the 2D character creation scene. Adds some juice to the slider via Particle System and Audio Source
// ideally, this is attached to the slider game object we want to add juice to.
public class SliderFX : MonoBehaviour
{
	#region Assignable Objects
	[Tooltip("The particle system we want to emit from - ideally near the body part we're changing.")]
	public GameObject ParticleObject;
	[Tooltip("The sound effects you want to hear upon slider ticks.")]
	public List<AudioClip> SoundFX = new List<AudioClip>();
	[Header("Pitch of Sound FX")]
	public float MinPitch = 1f;
	public float MaxPitch = 1.1f;
	[Header("Volume of Sound FX")]
	public float MinVol = .95f;
	public float MaxVol = 1f;
	[Header("Particle System Quick Controls")]
	public int MinParticlesPerTick = 3;
	public int MaxParticlesPerTick = 7;
	#endregion
	
	#region Found Objects
	//Refs taken from ParticleObject
	private Slider OurSlider;
	private AudioSource ParticleAudioSource;
	private ParticleSystem TargetParticles;
	#endregion
	
    // Start is called before the first frame update
    void Start()
	{
		OurSlider = GetComponent<Slider>();
	    if(ParticleObject != null){
	    	ParticleAudioSource = ParticleObject.GetComponent<AudioSource>();
	    	TargetParticles = ParticleObject.GetComponent<ParticleSystem>();
	    }else{
	    	Debug.LogWarning(this.gameObject.name + "'s Slider FX component has not referenced a particle system.");
	    }
	}
    
	// Add this method to the Slider component's On Value Changed (single) events
	public void OnSliderMove(){
		if(ParticleObject != null){
			if(TargetParticles != null){
				int particlesToEmit = Random.RandomRange(MinParticlesPerTick, MaxParticlesPerTick);
				TargetParticles.Emit(particlesToEmit);
			}else{
				Debug.LogWarning(ParticleObject.name + " does not have an particle system to emit particles from.");
			}
			
			int randomSound = Random.Range(0, SoundFX.Count);
			float randomPitch = Random.Range(MinPitch, MaxPitch);
			float randomVol = Random.Range(MinVol, MaxVol);
			
			if(ParticleAudioSource != null){
				ParticleAudioSource.pitch = randomPitch;
				ParticleAudioSource.volume = randomVol;
				ParticleAudioSource.PlayOneShot(SoundFX[randomSound]);
				
			}else{
				Debug.LogWarning(ParticleObject.name + " does not have an audio source to trigger sound fx with");
			}
		}else{
			Debug.LogWarning(this.gameObject.name + "'s Slider FX component has not referenced a particle system.");
		}
	
	}

}
