using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class player_Movement : MonoBehaviour {

	public bool locked_Movement;
	public Vector2 inputs;
	public Vector3 currentPlayerPosition;
	public jump jumpStats;
	public PlayerScript Player;


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

	public void ledgeJump()
	{
		if (Player.interaction.objectCurrentlyLookedAt.GetComponent<ledge> ().closed == false) {
			Player.interaction.objectCurrentlyLookedAt.GetComponent<ledge> ().closed = true;
			this.GetComponent<BoxCollider2D> ().enabled = false;
			StartCoroutine (jump ());
		}
	}

	IEnumerator jump()
	{ 
		
		bool done = false;

		float gravity = -(2 * jumpStats.jumpHeight) / Mathf.Pow ((jumpStats.jumpDuration ), 2);
		gravity = gravity;
		print (gravity + " gravity");
		jumpStats.jumpVelocity = Mathf.Abs (gravity) * (jumpStats.jumpDuration);

		float height = 0.8f;

		float ValueX = 0;
		float ValueY = jumpStats.jumpVelocity;
		while (!done) {
			
			ValueY += gravity * Time.deltaTime;
			height += ValueY; 
			ValueX += (jumpStats.jumpDistance*Mathf.Abs(jumpStats.jumpVelocity))*Time.deltaTime*checkDirection.x;
			transform.Translate (ValueX, ValueY, 0);
			if(height <= 0) {
				done = true;
				this.GetComponent<BoxCollider2D> ().enabled = true;
				locked_Movement = false;
				StopCoroutine (jump ());

			}
			print (height + " height");
			yield return null;
			print (gravity);
		}

	}
}

[System.Serializable]
public struct jump
{


	[Range(0,1.5f)]
	public float jumpHeight;
	[Range(0,1)]
	public float jumpDuration;
	[HideInInspector]
	public float jumpVelocity;
	[Range(0,1)]
	public float jumpDistance;


}

