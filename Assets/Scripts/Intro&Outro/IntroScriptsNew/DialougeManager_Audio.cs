using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialougeManager_Audio : MonoBehaviour {

	public AudioClip m_dialougeAudio;
	public Animator animator; 
	public GameObject continueButton; 
	public IntroScript_audio _introAudio; 
	bool dialougeHasStarted; 
	bool dialougeHasEnded = false; 

	private Queue <AudioClip> sentences; 

	// Use this for initialization
	void Start () {

		sentences = new Queue <AudioClip> (); 
	}

	public void playNextSentence(){

		if (sentences.Count == 0) {

			EndDialouge (); 
			return; 
		}

		AudioClip sentence = sentences.Dequeue (); 
		m_dialougeAudio = sentence; 
	}


	public void EndDialouge(){


	}
}
