using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is used to move a game object towards another.
// Written by Dallas Flett-Wapash

public class LerpToObject : MonoBehaviour
{
	[Header("Movement Options")]

	[Tooltip("Will this object move?")]
	public bool ShouldMove = true;

	public enum MovementOptions
	{
		LerpToTarget,
		SnapToTarget
	}

	[Tooltip("Will this object snap to its target, or move smoothly towards it?")]
	public MovementOptions MovementStyle = new MovementOptions();

	[Tooltip("Where will this object move to? Will default to MainCamera if unset.")]
	public Transform MoveTarget;

	[Tooltip("How fast will this object move?")]
	public float MoveSpeed = 5f;

	private void Awake()
	{
		if (MoveTarget == null)
		{
			MoveTarget = Camera.main.transform;
		}
	}

	public void ToggleMovement()
	{
		ShouldMove = !ShouldMove;
	}

	public void Update()
	{
		if (ShouldMove)
		{
			if (MovementStyle == MovementOptions.SnapToTarget && MoveTarget != null)
			{
				transform.position = MoveTarget.position;
			}

			if (MovementStyle == MovementOptions.LerpToTarget && MoveTarget != null)
			{
				transform.position = Vector3.Lerp(transform.position, MoveTarget.position, MoveSpeed * Time.deltaTime);
			}
		}
	}

}
