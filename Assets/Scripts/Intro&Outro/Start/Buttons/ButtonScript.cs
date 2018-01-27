using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement; 

public class ButtonScript : MonoBehaviour {

	public void StartGame()
	{
		SceneManager.LoadScene ("Intro", LoadSceneMode.Single); 

	}

	public void Menu()
	{
		SceneManager.LoadScene ("Menu", LoadSceneMode.Single); 
	}

}
