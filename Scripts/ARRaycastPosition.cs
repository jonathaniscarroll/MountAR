using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARRaycastPosition : MonoBehaviour
{
	[SerializeField]
	ARRaycastManager m_RaycastManager;

	List<ARRaycastHit> m_Hits = new List<ARRaycastHit>();
	public Camera MainCamera;
	public Vector3Event OutputHitPosition;

	void Start(){
		if(MainCamera==null){
			MainCamera = Camera.main;
		}
	}
	public void OutputRaycast()
	{
		Vector3 center = MainCamera.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
		if (m_RaycastManager.Raycast(center, m_Hits))
		{
			// Only returns true if there is at least one hit
			OutputHitPosition.Invoke(m_Hits[0].pose.position);
		}
		
		
	}
}
