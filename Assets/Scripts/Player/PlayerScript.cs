using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {


	public player_Movement movement;
	public Player_Interaction interaction;
	public Controller _controller;
	float childRotation;

	//Enumeration for State-Machine
	public enum playerState
	{
		checking,
		interacting,
		inEvent,
		lookingAt
	}

	public playerState _playerState;

	//Check which State the player is currently in
	public playerState getState()
	{
		return _playerState;
	}

	void Start () {
		
	}

	void Update () {
		
		if (Input.GetKeyDown (KeyCode.F)) {
			if (getState () == playerState.interacting) {
				
				//if return true all interactions have been done
				if (interaction.interactionEnded ()) {
					clearstate ();
				}
			} else {
				// send InteractingRequest to Object
				if (interaction.InteractionRequest ()) {
					doTransition (playerState.interacting);
				}
			}
		}
			
		if(Input.GetKeyDown(KeyCode.Space))
			{
			//baustelle
			interaction.attackOpponent ();
			}

		//Only move when player is not locked
		if (!movement.locked_Movement) {
			movement.inputs = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
			if (getState () == playerState.checking) {
			}
			if (movement.inputs.magnitude != 0) {
				if (movement.inputs.x != 0) {
					
					player_Movement.lookingDirectionState direction = movement.inputs.x == 1 ? player_Movement.lookingDirectionState.facingRight : player_Movement.lookingDirectionState.facingLeft;
					//if lookingDirection != actualLookingDirection rotate Sprite
					if(movement.doTransition(direction)){
					rotateChild ();
					movement.checkDirection.x = movement.inputs.x;
					movement.checkDirection.y = 0;
					}
				} else {
					player_Movement.lookingDirectionState direction  = movement.inputs.y == 1 ? player_Movement.lookingDirectionState.facingUp : player_Movement.lookingDirectionState.facingDown;
					//if lookingDirection != actualLookingDirection rotate Sprite
					if (movement.doTransition (direction)) {
						movement.checkDirection.y = movement.inputs.y;
						movement.checkDirection.x = 0;
					}
				}
		
				_controller.move (movement.inputs * Time.deltaTime * movement.movinSpeed);
			}

			if (getState () == playerState.checking) {
				//check for responsive GameObject with BoxCast
				RaycastHit2D hit = Physics2D.BoxCast (convertToVector2 (this.transform.position) + convertToVector2 (checkBoxOffset ()), checkBoxSize (), 0, Vector2.right, 0, interaction.interactionMask);

				if (hit) {
					if (interaction.objectCurrentlyLookedAt == null) {
						Debug.LogWarning ("hitting " + hit.transform.gameObject.name);
					}
					interaction.objectCurrentlyLookedAt = hit.transform.gameObject;
				} else {
					interaction.objectCurrentlyLookedAt = null;
				}
			}
		}
	}

	void rotateChild()
	{
		this.transform.GetChild (0).transform.Rotate (Vector3.up * 180);
	}

	//Request to change PlayerState
	bool doTransition(playerState _desiredState)
	{
		if (_desiredState != getState ())
		{
			_playerState = _desiredState;
			return true;
		}
		return false;
	}

	public void clearstate()
	{
		doTransition (playerState.checking);
		interaction.objectToInteractWith = null;
		movement.locked_Movement = false;
	}

	Vector2 convertToVector2(Vector3 toConvert)
	{
		return new Vector2 (toConvert.x, toConvert.y);
	}

	//Calculating the Interaction HitboxSize
	public Vector2 checkBoxSize()
	{
		return (this.GetComponent<BoxCollider2D> ().bounds.size)/1.5f;
	}

	//Calculating the Interaction Hitbox Offset

	public Vector3 checkBoxOffset()
	{
		float offsetX = 0;
		float offsetY = 0;


		offsetX = this.GetComponent<BoxCollider2D> ().bounds.size.x * movement.checkDirection.x;
		offsetY = this.GetComponent<BoxCollider2D> ().bounds.size.y * movement.checkDirection.y;
		

		return new Vector3 (offsetX, offsetY,0);

	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;

			Gizmos.DrawWireCube (this.transform.position + checkBoxOffset (), checkBoxSize ());
		
	}
}

