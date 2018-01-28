using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerDetection : MonoBehaviour {

	Itrigger _triggerScript;
	public LayerMask triggerMask;

	public AudioSource A_source;
	BoxCollider2D BC;
	player_Movement movement;

	public GameObject SelectedTrigger;

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
				SelectedTrigger = hit.transform.gameObject;
				hit.transform.gameObject.GetComponent<Itrigger> ().triggerEvent();



			}
		}
	}


}
