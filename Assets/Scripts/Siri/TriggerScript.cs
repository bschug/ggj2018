using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour, Itrigger {

	[Tooltip( "In here you put all AudioClips that are supposed to be played, in the order in which they appear")]
	public AudioClip [] dialouge; 

	[Tooltip( "Is there an animation that is supposed to be triggered? like an Emoji for example? Put that Clip in here")]
	public Animator anim;

	[Tooltip( "If the Trigger is supposed to disappear, then be sure to check this bool")]
	public bool destructable;

	public triggerDetection Player;


	public AudioClip  GetDialouge (int index){

	//	anim.SetBool ("BlendIn", true);
		return dialouge[index];
	}


	//Check if this trigger is destructable and if I want to destroy it after use
	public void destruct (){



			Destroy (this.gameObject); 

	}

	public bool interactionHasEnded(float index)
	{
		if (index != dialouge.Length) {
			return false;
		}
		Player.SelectedTrigger = null;

		return true;
	
	}
	public void triggerEvent()
	{
		
		StartCoroutine (dialogueIterator ());
	}

	IEnumerator dialogueIterator()
	{
		int index = 0;
		while (!interactionHasEnded (index)) {
		Player.A_source.clip = dialouge [index];
		Player.A_source.Play();
		yield return new WaitForSeconds (dialouge[index].length);
		index++;
		}

		destruct ();
		StopCoroutine (dialogueIterator());

	}


}
