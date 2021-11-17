using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
	public List<GameObject> ObjectsToKeep;
    // Start is called before the first frame update
    void Start()
	{
		foreach(GameObject obj in ObjectsToKeep){
			DontDestroyOnLoad(obj);	
		}
	    
    }

}
