using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour, Itrigger {

	[Range(0,0.1f)] 
    public float FadeOutSpeed;
	public string sceneToLoad;
	public GameObject FadeInScreen;

	public void triggerEvent ()
	{
		StartCoroutine (FadeOutAndSwitchScene (0));
	}
	public bool interactionHasEnded(float index)
	{
		if (index >= 1) {
			return true;
		}
		return false;
	}

	IEnumerator FadeOutAndSwitchScene(int myIndex)
	{

		float index = myIndex;
		while (!interactionHasEnded(index))
		{
			index=  (index+FadeOutSpeed) ;
			FadeInScreen.GetComponent<SpriteRenderer> ().color = new Color (0, 0, 0, index);
			yield return null;
		}
		SceneManager.LoadScene (sceneToLoad, LoadSceneMode.Single);
		StopCoroutine (FadeOutAndSwitchScene (1));


	}

	Vector2 DisplayBoxSize()
	{
		float ValueX = this.GetComponent<BoxCollider2D>().size.x*this.transform.localScale.x;
		float ValueY = this.GetComponent<BoxCollider2D>().size.y*this.transform.localScale.y;
		return new Vector2 (ValueX, ValueY);
	}
	void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawCube(this.transform.position,DisplayBoxSize());
	}
}
