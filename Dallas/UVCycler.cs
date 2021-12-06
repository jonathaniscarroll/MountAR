using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UVCycler : MonoBehaviour
{
	public RawImage ImageToCycle;
	public float CycleSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
	    ImageToCycle = GetComponent<RawImage>();
    }

    // Update is called once per frame
	void FixedUpdate()
    {
	    if(ImageToCycle != null){
	    	//var uvY = ImageToCycle.uvRect.y;
	    	//uvY += CycleSpeed * Time.deltaTime;
	    	var yMin = ImageToCycle.uvRect.yMin;
	    	yMin += .1f;
	    }
	    	
    }
}
