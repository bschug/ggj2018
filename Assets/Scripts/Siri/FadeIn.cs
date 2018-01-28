using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour {
	[Range(0,0.1f)] 
	public float FadeOutSpeed;

	public GameObject FadeInScreen;
	// Use this for initialization
	void Start () {
		StartCoroutine(FadeInScene(1));
	}

	public bool interactionHasEnded(float index)
	{
		if (index <= 0) {
			return true;
		}
		return false;
	}

	IEnumerator FadeInScene(int myIndex)
	{

		float index = myIndex;
		while (!interactionHasEnded(index))
		{
			index =  (index-FadeOutSpeed) ;
			FadeInScreen.GetComponent<SpriteRenderer> ().color = new Color (0, 0, 0, index);
			yield return null;
		}

		StopCoroutine (FadeInScene (0));


	}
}
