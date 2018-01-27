using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class IntroScript_audio : MonoBehaviour {

	public AudioClip _introDialouge; 
	public AudioSource _audioSource; 

	void Start(){

		_audioSource = this.GetComponent<AudioSource>();
		StartCoroutine (playSound ()); 

	}

	IEnumerator playSound(){
		_audioSource.Play ();
		yield return new WaitForSeconds (_introDialouge.length);
		SceneManager.LoadScene("main", LoadSceneMode.Single); 
		StopCoroutine (playSound ()); 
	}


}
