using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Needs a requirecomponenttypeofParticleSystemJuicer
public class ParticleSystemFinder : MonoBehaviour
{
	private ParticleSystemJuicer Juice;
	public string IDToLink;
	private ParticleSystem FoundParticleSystem;
	
    // Start is called before the first frame update
    void Start()
	{
		Juice = GetComponent<ParticleSystemJuicer>();
		FindParticleSystem();
	}
    
	public void FindParticleSystem()
	{
		var search = FindObjectsOfType<ParticleSystemIDGiver>();
		foreach(ParticleSystemIDGiver id in search){
			
			if(id != null && id.PSID == IDToLink){
				FoundParticleSystem = id.GetComponent<ParticleSystem>();
				Juice.PSToJuice = FoundParticleSystem;
				return;
			}
		}
		
	}
    // Update is called once per frame
    void Update()
    {
        
    }
}
