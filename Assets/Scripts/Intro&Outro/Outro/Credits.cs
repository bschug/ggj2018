using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour {

	public AnimationClip _animator; 


	// Use this for initialization
	void Start () {

		StartCoroutine (waitforEnd ()); 
	}


	IEnumerator waitforEnd(){
		yield return new WaitForSeconds (_animator.length); 
		StopCoroutine (waitforEnd());
		SceneManager.LoadScene ("StartScene", LoadSceneMode.Single); 
	}

	// Update is called once per frame
	void Update () {
		
	}
}
