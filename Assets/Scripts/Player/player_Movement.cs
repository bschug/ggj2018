using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class player_Movement : MonoBehaviour {

	public bool locked_Movement;
	public Vector2 inputs;
	public Vector3 currentPlayerPosition;

	[HideInInspector]
	public Vector2 checkDirection;
	[Range(0,100)]
	public float movinSpeed;
	//Enumeration for State-Machine
	public enum lookingDirectionState
	{
		facingUp,
		facingDown,
		facingRight,
		facingLeft
	}

	public lookingDirectionState _lookingDirection;

	//Check which State the player is currently in
	public lookingDirectionState getState()
	{
		return _lookingDirection;
	}

	public bool doTransition(lookingDirectionState _desiredState)
	{
		if (_desiredState != getState ()) 
		{
			_lookingDirection = _desiredState;
			return true;
		}
		else
		{
			return false;
		}
	}
}
