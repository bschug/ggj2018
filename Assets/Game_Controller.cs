using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Controller : MonoBehaviour {

	public GameObject startSegment;
	public GameObject currentSegment;
	public Segment_Manager manager;
	public GameObject Player;
	public Vector3 newPos;
	public float yPos;

	void Start()
	{
		currentSegment = startSegment;
		manager= startSegment.GetComponent<Segment_Manager> ();
	}

	void LateUpdate ()
	{
		yPos = Mathf.RoundToInt(Player.transform.position.y % (currentSegment.GetComponentInChildren<SpriteRenderer> ().bounds.size.y)/manager.Distance());
		yPos = Mathf.Abs(yPos);
		Player.GetComponentInChildren<SpriteRenderer> ().sortingOrder = manager.layerAmount[ Mathf.RoundToInt (yPos)];
		this.transform.position = Player.transform.position;
		newPos.x= Mathf.Clamp (this.transform.position.x, manager.cameraConstrainsX.x, manager.cameraConstrainsX.y);
		newPos.y = Mathf.Clamp (this.transform.position.y, manager.cameraConstrainsY.x, manager.cameraConstrainsY.y);
		this.transform.position = newPos + Vector3.back * 10;

 	}
	
    bool doTransition(GameObject _desiredSegment)
	{	
		if (_desiredSegment != currentSegment) 
		{
		currentSegment = null;
		currentSegment = _desiredSegment;
		manager = _desiredSegment.GetComponent<Segment_Manager> ();
		return true;
		}
		return false;
	}
}
