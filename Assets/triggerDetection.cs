using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerDetection : MonoBehaviour {

	TriggerScript _triggerScript;
	public LayerMask triggerMask;
	AudioSource A_source;
	BoxCollider2D BC;
	player_Movement movement;
	GameObject SelectedTrigger;

	void Start()
	{
		A_source = this.GetComponent<AudioSource> ();
		BC = this.GetComponent<BoxCollider2D> ();
		movement = this.GetComponent<player_Movement> ();
	}
	void Update()
	{
		if (SelectedTrigger == null) {

			RaycastHit2D hit = Physics2D.BoxCast (BC.transform.position, BC.size, 0, Vector2.right, 0, triggerMask);
			if (hit) {
				print ("hit Trigger");
				_triggerScript = hit.transform.gameObject.GetComponent<TriggerScript> ();
				SelectedTrigger = hit.transform.gameObject;
				StartCoroutine (dialogueIterator ());

			}
		}
	}
	IEnumerator dialogueIterator()
	{
		int index = 0;
		while (!_triggerScript.interactionHasEnded (index)) {
			A_source.clip = _triggerScript.GetDialouge (index);
			A_source.Play();
			yield return new WaitForSeconds (A_source.clip.length);
			index++;
		}

		SelectedTrigger = null;
		StopCoroutine (dialogueIterator ());

	}

}
