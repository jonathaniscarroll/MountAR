using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetSpriteName : MonoBehaviour
{
	public StringEvent OutputSpriteNameEvent;
	public void OutputSpriteName(Sprite input){
		OutputSpriteNameEvent.Invoke(input.name);
	}
}
