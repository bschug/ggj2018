using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Segment_Manager : MonoBehaviour {

	public int[] layerAmount;
	public Vector2 cameraConstrainsX;
	public Vector2 cameraConstrainsY;

	void OnDrawGizmos()
	{
		Gizmos.color = Color.cyan;
		if (!Application.isPlaying) {
			for (int i = 0; i <= layerAmount.Length; i++) {

				Vector3 newPosition = startPosition () + (Vector3.up * i*Distance());
				Gizmos.DrawRay (newPosition, Vector2.right * this.GetComponentInChildren<SpriteRenderer> ().sprite.bounds.size.x*this.transform.localScale.x);
				Gizmos.DrawSphere (newPosition,0.1f);
			}
		}

	}

	Vector3 startPosition()
	{
		float valueX = this.GetComponentInChildren<SpriteRenderer> ().sprite.bounds.size.x/2;
		valueX = valueX * this.transform.localScale.x;
		float valueY = this.GetComponentInChildren<SpriteRenderer> ().sprite.bounds.size.y/2;
		valueY = valueY * this.transform.localScale.y;
		return this.transform.position -new Vector3 (valueX, valueY, 0);
	}
	public float Distance()
	{
		float sizeY = this.GetComponentInChildren<SpriteRenderer> ().sprite.bounds.size.y;
		sizeY = sizeY * this.transform.localScale.y;
		return sizeY / (layerAmount.Length);
	}
}
