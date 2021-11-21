using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIAudioSource : MonoBehaviour
{
	public float minPitch = 1;
	public float maxPitch = 1.15f;
	public float minVol = .5f;
	public float maxVol = .6f;
	
	private AudioSource Speaker;
    // Start is called before the first frame update
    void Start()
    {
	    Speaker = GetComponent<AudioSource>();
    }

	//Called by buttons
	public void MakeSound(AudioClip soundToMake)
	{
		SetPitchAndVol();
		
		if(Speaker != null && soundToMake != null){
			Speaker.PlayOneShot(soundToMake);	
		}
		
	}
	private void SetPitchAndVol()
	{
		float randomPitch = Random.Range(minPitch, maxPitch);
		float randomVol = Random.Range(minVol, maxVol);
	}
}
