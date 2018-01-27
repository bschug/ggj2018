using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroScene : MonoBehaviour {

	[Range(0,5)]
	public int Countdown; //Is used to count down till the Conversation starts
	public DialougeTrigger _dialougeTrigger; //Reference to Dialouge Trigger Script, be assigned in Inspector
	public AnimationClip _animationClip; 
	public AudioClip _startSound; 
	public AudioSource _audioSource;
	public Animator animator;
	public AudioClip _loopSound; 

	// Use this for initialization
	void Start () {
		_audioSource = this.GetComponent<AudioSource>();
		StartCoroutine (playSound ()); 
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//Corutine used for the delay of the Dialouge Trigger
	IEnumerator startDelay(){
		yield return new WaitForSeconds (Countdown);
		//_dialougeTrigger.TriggerDialouge ();
		StopCoroutine (startDelay ());
	}

	IEnumerator blendInDelay(){
		animator.SetBool ("BlendIn", true);
		yield return new WaitForSeconds (_animationClip.length);
		//Calls the Corutine
		StartCoroutine (startDelay ());
		StopCoroutine (blendInDelay ()); 
	}

	IEnumerator playSound(){
		_audioSource.Play ();
		yield return new WaitForSeconds (_startSound.length);
		_audioSource.clip = _loopSound; 
		_audioSource.Play (); 
		StartCoroutine (blendInDelay ()); 
		StopCoroutine (playSound ()); 
	}
}
