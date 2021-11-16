using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnArTouch : MonoBehaviour
{
	public UnityEvent OnTouch;
	public LayerMask BlockLayers;

    // Update is called once per frame
    void Update()
	{
	    if (Input.touchCount == 0)
		    return;
		Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

		if(Physics.Raycast(ray,BlockLayers)){
			return;
		}
	    OnTouch.Invoke();
    }
}
