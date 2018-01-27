using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player_Interaction : MonoBehaviour {

	public GameObject objectToInteractWith;
	public GameObject objectCurrentlyLookedAt;
	public LayerMask interactionMask;
	public PlayerScript Player;
	public player_Movement movement;
	public GameObject Weapon;

	public int getObjectType()
	{
		return objectToInteractWith.layer;
	}

	public bool InteractionRequest()
	{
		if (objectCurrentlyLookedAt != null) {
			objectToInteractWith = objectCurrentlyLookedAt;
			objectCurrentlyLookedAt = null;
			movement.locked_Movement = true;
			interactionEnded ();
			return true;
		} 
		else
		{
			Debug.LogWarning ("No Object within reach");
			return false;
		}
	}
	public bool interactionEnded()
	{
		
		//Debug.Log ("started interaction with " + objectToInteractWith.name);
		if (objectToInteractWith.GetComponent<IObject> ().response ()) {
			return false;
		}
		else
		{
			Debug.LogWarning ("interaction completed");
		
			return true;
		}

	}

	public void stopInteraction()
	{
		objectToInteractWith = null;
	}

	public void attackOpponent()
	{
		StartCoroutine (attackDuration ());
	}

	IEnumerator attackDuration()
	{
		Weapon.gameObject.SetActive (true);
		yield return new WaitForSeconds (0.3f);
		Weapon.gameObject.SetActive (false);
		StopCoroutine (attackDuration ());
	}
}
