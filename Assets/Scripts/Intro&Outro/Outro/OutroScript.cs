using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class OutroScript : MonoBehaviour {

	[Range(0,5)]
	public float Pause;
	public Animator _EndAnim; 
	public AnimationClip _EndAnimClip; 
	public AudioClip _SiriBling; 
	public AudioSource _audioSource; 

	// Use this for initialization
	void Start () {
		
		_audioSource = this.GetComponent<AudioSource>();
		StartCoroutine (playBling()); 

	}
	
	IEnumerator playBling(){

		yield return new WaitForSeconds (_EndAnimClip.length);
		yield return new WaitForSeconds (Pause); 
		_audioSource.clip = _SiriBling; 
		_audioSource.Play (); 
		yield return new WaitForSeconds (Pause); 
		Application.Quit();
		Debug.Log ("Application is quit"); 

	}
}
