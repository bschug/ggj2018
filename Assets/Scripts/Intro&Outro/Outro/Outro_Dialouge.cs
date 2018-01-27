using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement; 

public class Outro_Dialouge : MonoBehaviour {


	public Text m_nameText; 
	public Text m_dialougeText;

	public Animator animator; 
	public Animator _creditRoll;
	public GameObject continueButton; 

	public AudioClip _thunderSound; 
	public IntroScene _introScene; 
	bool dialougeHasStarted;
	bool dialougeHasEnded = false;

	//Variable which keeps track of our senteces in our current dialouge
	//FirstInFirstOut
	private Queue <string> senteces; 

	// Use this for initialization
	void Start () { 
		senteces = new Queue <string> (); 

	}

	//Starts the conversation with the name of the character shown and his first sentece
	public void StartDialouge (Dialouge dialouge)
	{
		dialougeHasStarted = true;
		continueButton.SetActive (true); 

		Debug.Log ("Starting conversation with" + dialouge.name);

		m_nameText.text = dialouge.name; 

		senteces.Clear (); 

		foreach (string sentence in dialouge.sentencs) 
		{
			senteces.Enqueue (sentence);
		}

		DisplayNextSentence ();
	}

	//Shows us the next sentece in Queue
	public void DisplayNextSentence()
	{
		//If there are no more sentences to display the conversation ends
		if (senteces.Count == 0)
		{
			EndDialouge();
			return; 
		}


		//int randomDialougeIndex = Random.Range (0, senteces.Count);
		string sentence = senteces.Dequeue() ; 
		m_dialougeText.text = sentence; 
		Debug.Log (sentence); 

	}

	void EndDialouge(){
		Debug.Log ("End of conversation");
		animator.SetBool ("BlendIn", true); 
		continueButton.SetActive (false);
		dialougeHasEnded = true;
		SceneManager.LoadScene ("CreditScene", LoadSceneMode.Single); 
	}

	IEnumerator soundDelay(){
		_introScene._audioSource.clip = _thunderSound; 

		_introScene._audioSource.Play();
		yield return new WaitForSeconds (_thunderSound.length); 
		//Debug.Log ("Load Scene"); 
		animator.SetBool("startScroll",true); 
		StopCoroutine (soundDelay ()); 

	}
	void Update()
	{

		if (Input.GetKeyDown (KeyCode.E)&& !dialougeHasEnded && dialougeHasStarted) 
		{
			DisplayNextSentence ();
		}

	}
}
