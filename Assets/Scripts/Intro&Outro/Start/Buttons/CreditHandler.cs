using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class CreditHandler: MonoBehaviour {

	public CanvasGroup uiCanvasGroup; 
	public CanvasGroup CreditCanvasGroup; 


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
		CreditCanvasGroup.alpha = 0; 
		CreditCanvasGroup.interactable = false; 
		CreditCanvasGroup.blocksRaycasts = false;
	}


	///<summary>
	/// Called if clicked on Quit
	/// </summary>
	public void GetCredits()
	{
		Debug.Log ("Check from credit Close Confirmation"); 

		//reduce visability of normal UI, and disbale all interaction
		uiCanvasGroup.alpha = 0.5f;
		uiCanvasGroup.interactable = false; 
		uiCanvasGroup.blocksRaycasts = false; 

		//enable confirmation UI and make it visible
		CreditCanvasGroup.alpha = 1; 
		CreditCanvasGroup.interactable = true; 
		CreditCanvasGroup.blocksRaycasts = true; 

	}

	}

