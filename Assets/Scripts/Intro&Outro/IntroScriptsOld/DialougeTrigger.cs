using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialougeTrigger : MonoBehaviour {

	public Dialouge dialouge; 

	public void TriggerDialouge()
	{
		if (SceneManager.GetActiveScene ().name == "Intro") {
			FindObjectOfType<DialougeManager> ().StartDialouge (dialouge); 
		} else {
			return; 
		}
		}

	}

