using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour {

	public AudioClip [] dialouge; 
	public Animator anim; 
	public bool destructable;


	public AudioClip [] GetDialouge (){

		return dialouge;
	}


	//Check if this trigger is destructable and if I want to destroy it after use
	public void destruct (){

		if (destructable) {

			Destroy (this.gameObject); 
		} else {
			return; 
		}
	}

}
