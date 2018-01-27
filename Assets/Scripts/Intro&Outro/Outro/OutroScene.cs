using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutroScene : MonoBehaviour {

	[Range(0,5)]
	public int Countdown; //Is used to count down till the Conversation starts
	public DialougeTrigger _dialougeTrigger; //Reference to Dialouge Trigger Script, be assigned in Inspector
	public AnimationClip _animationClip; 
	public AudioClip _startSound; 
	public AudioSource _audioSource;
	public Animator animator;

	// Use this for initialization
	void Start () {
		_audioSource = this.GetComponent<AudioSource>();
		StartCoroutine (blendInDelay ());  
	}

	// Update is called once per frame
	void Update () {

	}

	//Corutine used for the delay of the Dialouge Trigger

	IEnumerator blendInDelay(){
		_audioSource.Play (); 
		yield return new WaitForSeconds (_animationClip.length);
		//Calls the Corutine
		//_dialougeTrigger.TriggerDialouge ();

		StopCoroutine (blendInDelay ()); 
	}

}
