using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour {


	public static Singleton instance;
 
	// Use this for initialization
	void Awake () {
		DontDestroyOnLoad (this.gameObject);
		if (instance == null) 
		{
			instance = this;
		}
		else
		{
			Destroy (this.gameObject);
		}
	}
	

}

