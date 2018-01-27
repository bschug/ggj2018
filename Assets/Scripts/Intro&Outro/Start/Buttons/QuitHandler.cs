﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class QuitHandler: MonoBehaviour {

	public CanvasGroup uiCanvasGroup; 
	public CanvasGroup confirmQuitCanvasGroup; 


	private void Awake()
	{
		//disable the quit confirmation panel
		DoConfirmQuitNo(); 
	}

	///<summary>
	/// called if clicked on No
	/// </summary>

	public void DoConfirmQuitNo(){

		Debug.Log ("Back to the game"); 

		//enable the normal UI
		uiCanvasGroup.alpha = 1; 
		uiCanvasGroup.interactable = true; 
		uiCanvasGroup.blocksRaycasts = true; 

		//disable the confirmation quit ui
		confirmQuitCanvasGroup.alpha = 0; 
		confirmQuitCanvasGroup.interactable = false; 
		confirmQuitCanvasGroup.blocksRaycasts = false;
	}

	// called if clicked on Yes

	public void DoConfirmQuitYes(){

		Debug.Log ("Ok bye bye");
		Application.Quit(); 
	}

	///<summary>
	/// Called if clicked on Quit
	/// </summary>
	public void DoQuit()
	{
		Debug.Log ("Check from Quit Confirmation"); 

		//reduce visability of normal UI, and disbale all interaction
		uiCanvasGroup.alpha = 0.5f;
		uiCanvasGroup.interactable = false; 
		uiCanvasGroup.blocksRaycasts = false; 

		//enable confirmation UI and make it visible
		confirmQuitCanvasGroup.alpha = 1; 
		confirmQuitCanvasGroup.interactable = true; 
		confirmQuitCanvasGroup.blocksRaycasts = true; 

	}

	}

