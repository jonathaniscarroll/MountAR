using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubicleObject : MonoBehaviour
{
	public int Rest{
		get{
			return rest;
		}
		set{
			rest = value;
		}
	}
	[SerializeField]
	private int rest;
	
	public int Work{
		get{
			return work;
		}
		set{
			work = value;
		}
	}
	[SerializeField]
	private int work;
	public Transform WorkPosition;
	public Transform RestPosition;
	
}
