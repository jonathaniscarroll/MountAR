using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubicleObject : MonoBehaviour
{
	public string Name{
		get{return _name;}
		set{_name = value;}
	}
	[SerializeField]
	private string _name;
	
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
	
	public int Brand{
		get{
			return brand;
		}
		set{
			brand = value;
		}
	}
	[SerializeField]
	private int brand;
	public int Virality{
		get{
			return virality;
		} set{
			virality = value;
		}
	}
	[SerializeField]
	private int virality;
	
	public int Value{
		get{
			_value = Virality + Brand + Work + Rest;
			return _value;
		}
	}
	[SerializeField]
	private int _value;
	
	public Transform InteractionPoint;
	public SpriteRenderer SpriteRenderer;
	
}
