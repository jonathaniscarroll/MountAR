using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCenter : MonoBehaviour {
	public List<Transform> targets;	
	public Camera camera;
	public float speed;
	public float minSize;
	public float maxSize;

	// Use this for initialization
	void Start () {
		targets = new List<Transform>();
		if(camera==null){
			camera = Camera.main;
		}
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 position = GetCenterPoint();
		transform.position = new Vector3(position.x,transform.position.y,position.z);
		if(position!=transform.position){
			//transform.position = position;
			//transform.position += new Vector3(position.x,0,0);
		} else {
			//transform.position = position;
		}
	}

	Vector3 GetCenterPoint(){
		if(targets.Count==0){
			return Vector3.zero;
		}

		if(targets.Count ==1){
			return targets[0].position;		
		}

		var bound = new Bounds(targets[0].position, Vector3.zero);
		foreach(Transform t in targets){
			bound.Encapsulate(t.position);
		}

		Vector3 center = bound.center;
		// Debug.Log("center: " + bound.center + " extent: " + bound.extents.x);
		if((bound.extents.x>minSize&&bound.extents.x<maxSize)||(bound.extents.y>minSize&&bound.extents.y<maxSize)){
			 center = Vector3.Lerp(transform.position,center,Time.deltaTime*speed);

				camera.orthographicSize = bound.extents.x;
			
		} 
		else if(bound.extents.x>maxSize) {

			 camera.orthographicSize = bound.extents.x;
			 center = Vector3.Lerp(transform.position,transform.position+Vector3.right,Time.deltaTime*speed);
			
		}else {
			camera.orthographicSize = minSize;
		}
		
		return center;
		
	}
	public void AddToTargets(Transform input){
		targets.Add(input);
	}
}
