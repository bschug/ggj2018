using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class IntroScript_audio : MonoBehaviour {

	[Range(0,5)]
	public int Pause;
	public Animator _Anfang;
	public AnimationClip _AnfangClip; 
	public AudioClip _introDialouge1; 
	public AudioClip _introDialouge2; 
	public AudioSource _audioSource; 

	void Start(){

		_audioSource = this.GetComponent<AudioSource>();
		StartCoroutine (playAnimation ()); 

	}

	IEnumerator playAnimation(){

		_Anfang.SetBool ("BlendIn", true); 
		yield return new WaitForSeconds (_AnfangClip.length);
		StartCoroutine (playSound ()); 
		StopCoroutine (playAnimation ()); 
	}
		

	IEnumerator playSound(){
		_audioSource.clip = _introDialouge1; 
		_audioSource.Play ();
		yield return new WaitForSeconds (_introDialouge1.length);
		yield return new WaitForSeconds (Pause);
		_audioSource.clip = _introDialouge2; 
		_audioSource.Play (); 
		yield return new WaitForSeconds (_introDialouge2.length);
		SceneManager.LoadScene("Mascha_Scene", LoadSceneMode.Single); 
		StopCoroutine (playSound ()); 
	}

}
