using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class ControlHandler: MonoBehaviour {

	public CanvasGroup uiCanvasGroup; 
	public CanvasGroup controlFieldCanvasGroup; 


	private void Awake()
	{
		//disable the quit confirmation panel
		BackToMenu(); 
	}

	///<summary>
	/// called if clicked on No
	/// </summary>

	public void BackToMenu(){

		Debug.Log ("Back to the game"); 

		//enable the normal UI
		uiCanvasGroup.alpha = 1; 
		uiCanvasGroup.interactable = true; 
		uiCanvasGroup.blocksRaycasts = true; 

		//disable the confirmation quit ui
		controlFieldCanvasGroup.alpha = 0; 
		controlFieldCanvasGroup.interactable = false; 
		controlFieldCanvasGroup.blocksRaycasts = false;
	}

	///<summary>
	/// Called if clicked on Quit
	/// </summary>
	public void GetControls()
	{
		Debug.Log ("Check from Control Confirmation"); 

		//reduce visability of normal UI, and disbale all interaction
		uiCanvasGroup.alpha = 0.5f;
		uiCanvasGroup.interactable = false; 
		uiCanvasGroup.blocksRaycasts = false; 

		//enable confirmation UI and make it visible
		controlFieldCanvasGroup.alpha = 1; 
		controlFieldCanvasGroup.interactable = true; 
		controlFieldCanvasGroup.blocksRaycasts = true; 

	}

	}

