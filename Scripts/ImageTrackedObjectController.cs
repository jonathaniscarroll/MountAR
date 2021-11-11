using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Events;

public class ImageTrackedObjectController : MonoBehaviour
{
	public ARTrackedImageManager ARTrackedImageManager;
	public List<ImageTrackedObject> ImageTrackedObjects;
	public StringEvent DebugText;
	private Dictionary<string,ImageTrackedObject> StoredTrackedObjects = new Dictionary<string, ImageTrackedObject>();
	
	[System.Serializable]
	public class ImageTrackedObject{
		public string name;
		public GameObject gameObject;
		public UnityEvent OnImageTracked;
		public UnityEvent OnEndImageTracked;
	}
	
	private void OnEnable(){
		ARTrackedImageManager.trackedImagesChanged += ImageChanged;	
	}
	private void OnDisable(){
		ARTrackedImageManager.trackedImagesChanged -= ImageChanged;
	}
	
	private void ImageChanged(ARTrackedImagesChangedEventArgs eventArgs){
		foreach(ARTrackedImage trackedImage in eventArgs.added){
			AddImageObject(trackedImage);
		}
		foreach(ARTrackedImage trackedImage in eventArgs.updated){
			UpdateImageObject(trackedImage);
		}
		foreach(ARTrackedImage trackedImage in eventArgs.removed){
			RemoveImageObject(trackedImage);
		}
		
	}
	
	private void AddImageObject(ARTrackedImage trackedImage){
		Debug.Log("adding " + trackedImage.name);
		string name = trackedImage.referenceImage.name;
		ImageTrackedObject imageTrackedObject = ImageTrackedObjects.Find(x=> x.name == name);
		StoredTrackedObjects.Add(name,imageTrackedObject);
		imageTrackedObject.OnImageTracked.Invoke();
		
	}
	
	private void UpdateImageObject(ARTrackedImage trackedImage){
		string name = trackedImage.referenceImage.name;
		Vector3 position = trackedImage.transform.position;
		Quaternion rotation = trackedImage.transform.rotation;
		//DebugText.Invoke("name " + name + " position " + position);
		//ImageTrackedObject imageTrackedObject = ImageTrackedObjects.Find(x=> x.name == name);
		if(!StoredTrackedObjects.ContainsKey(name)){
			AddImageObject(trackedImage);
		}
		ImageTrackedObject imageTrackedObject = StoredTrackedObjects[name];
		
		
		GameObject imageObject = imageTrackedObject.gameObject;
		//imageObject.SetActive(true);
		imageObject.transform.position = position;
		imageObject.transform.rotation = rotation;
		//imageTrackedObject.OnImageTracked.Invoke();
	}
	
	public void RemoveImageObject(ARTrackedImage trackedImage){
		string name = trackedImage.referenceImage.name;
		if(StoredTrackedObjects.ContainsKey(name)){
			StoredTrackedObjects[name].OnEndImageTracked.Invoke();
		}
	}
}
